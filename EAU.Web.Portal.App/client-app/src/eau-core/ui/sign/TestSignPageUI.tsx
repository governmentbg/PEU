import * as React from "react";
import { observer } from 'mobx-react';
import { observable, action } from 'mobx';
import { AjaxHelper, ObjectHelper } from 'cnsys-core';
import { BaseProps, withAsyncFrame, AsyncUIProps } from 'cnsys-ui-react';
import { appConfig } from '../../common/ApplicationConfig';
import { EAUBaseComponent } from "../EAUBaseComponent";
import { BTrustProcessorDataService } from '../../services';
import { Link } from "react-router-dom";

interface TestSignPageUIProps extends BaseProps, AsyncUIProps {
}

@observer class TestSignPageUIImpl extends EAUBaseComponent<TestSignPageUIProps, any> {
    private bissWorkingPorts: string[] = ['53952', '53953', '53954', '53955'];
    private workingPort: string = undefined;

    @observable private bissMustTurnOn: boolean;
    @observable private bissError: string;
    @observable private isSuccess: boolean;

    constructor(props: TestSignPageUIProps) {
        super(props);

        //Bind
        this.startLocalSign = this.startLocalSign.bind(this);
        this.processLocalSigning = this.processLocalSigning.bind(this);
        this.processBissError = this.processBissError.bind(this);

        //Init
        this.bissError = undefined;
        this.bissMustTurnOn = false;
        this.isSuccess = false;
    }

    render(): JSX.Element {
        return (
            <div className="page-wrapper" id="ARTICLE-CONTENT">                
                <p>{this.getResource('GL_TEST_SIGN_INFO_I')}
                    <a href={appConfig.webHelpUrl + "?context=90"}>{this.getResource("GL_HERE")}</a>
                </p>
                
                <div className="row">
                    {this.bissMustTurnOn == true ?
                        <div className="alert alert-warning mt-0 mb-4">
                            <p dangerouslySetInnerHTML={{ __html: this.getResource('GL_SIGN_SRV_LOCL_NOTAVL_I') }}></p>
                        </div>
                        : null}
                    {ObjectHelper.isStringNullOrEmpty(this.bissError) ? null : <div className="col-12"><div className="alert alert-danger mt-0 mb-4" role="alert">{this.bissError}</div></div>}
                    {this.isSuccess == true ? <div className="col-12"><div className="alert alert-success mt-0 mb-4" role="alert">{this.getResource('GL_TEST_SIGN_SUCCESS_L')}</div></div> : null}
                </div>
                <div className="button-bar button-bar--responsive">
                    <div className="right-side"></div>
                    <div className="left-side">
                        <button type="button" className="btn btn-light" onClick={this.startLocalSign}>
                            <i className="ui-icon ui-icon-sign mr-1" aria-hidden="true"></i>
                            {this.getResource('GL_SIGN_LOCAL_L')}
                        </button>
                    </div>
                </div>               
            </div>);
    }

    @action startLocalSign(e: any): void {
        let that = this;
        this.workingPort = undefined;
        this.bissMustTurnOn = false;
        this.bissError = undefined;
        this.isSuccess = false;

        this.props.registerAsyncOperation(Promise.all(this.bissWorkingPorts.map(function (port: string, index: number) {
            return $.ajax(`https://localhost:${port}/version`, { timeout: 5000 }).then((r) => {
                let ver: number = Number(r.version);

                if (!isNaN(ver) && ver >= 2.20) {
                    that.workingPort = port;
                    return that.processLocalSigning(port);
                }
            }).catch((err) => {
                console.log(err);
            });
        })).then(() => {
            if (!that.workingPort) {
                //Моля, стартирайте  B-Trust BISS, за да можете да подписвате. Ако приложението не е инсталирано, можете да изтеглите инсталатор от тук: B-Trust BISS (https://testportal.bpo.bg/e-signature/sign-a-document-test)
                that.bissMustTurnOn = true;
                return;
            }
        }));
    }

    processLocalSigning(bissPort: string): Promise<any> {
        let that = this;
        let baseUrl: string = 'https://localhost:' + bissPort;
        let getsignerData: any = {
            showValidCerts: true,
            selector: {
                issuers: []
            }
        };

        //Избор на сертификат.
        return AjaxHelper.ajax<any>({
            url: baseUrl + '/getsigner',
            contentType: 'application/json',
            type: 'POST',
            headers: { 'Accept-Language': appConfig.clientLanguage && appConfig.clientLanguage === 'bg' ? 'bg' : 'en' },
            crossDomain: true,
            dataType: "json",
            data: JSON.stringify(getsignerData),
            error: this.processBissError
        }).then((result: any) => {
            if (result.reasonCode == '200') {
                //chain: съдържа сертификационната верига на подписващия сертификат. Подписващия сертификат е на позиция 0
                let userCertBase64: string = result.chain[0];

                //Извикваме нашето API,за да ни върне хеша на документа.
                return (new BTrustProcessorDataService()).createBissTestSignRequest(userCertBase64).then(result => {
                    let hashTime: number = result.documentHashTime[0];

                    //команда към BISS да подпише
                    return AjaxHelper.ajax<any>({
                        url: baseUrl + '/sign',
                        contentType: 'application/json',
                        type: 'POST',
                        crossDomain: true,
                        headers: { 'Accept-Language': appConfig.clientLanguage && appConfig.clientLanguage === 'bg' ? 'bg' : 'en' },
                        dataType: "json",
                        data: JSON.stringify(result.signRequest),
                        error: this.processBissError
                    }).then((signResult: any) => {
                        if (signResult.reasonCode === 200) {
                            //Извикваме нашето API,за да сглоби подписа с документа.
                            return new BTrustProcessorDataService().completeBissTestSignProcess(userCertBase64, signResult.signatures[0], hashTime)
                                .then(() => {
                                    that.isSuccess = true;
                                }).catch(err => {
                                    that.bissError = err.message;
                                });
                        } else {
                            that.bissError = signResult.reasonText;
                        }
                    });
                }).catch(err => {
                    if (err.message)
                        that.bissError = err.message;
                });
            }
            else {
                that.bissError = result.reasonText;
            }
        });
    }

    @action processBissError(xhr: any, textStatus: JQuery.Ajax.ErrorTextStatus, errorThrown: string): void {
        if (xhr.responseJSON)
            this.bissError = xhr.responseJSON.reasonText;
        else
            this.bissError = errorThrown;
    }
}

export const TestSignPageUI = withAsyncFrame(TestSignPageUIImpl, false, false);