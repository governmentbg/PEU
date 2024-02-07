import * as React from 'react';
import { observer } from 'mobx-react';
import { observable, action } from 'mobx';
import { Signer, SignerSigningStatuses, SigningChannels } from '../../models/sign';
import { ObjectHelper, AjaxHelper, appConfig } from 'cnsys-core';
import { BaseProps, withAsyncFrame, AsyncUIProps } from 'cnsys-ui-react';
import { ISigningProcessContextProps } from './SigningProcessContext';
import { EAUBaseComponent } from "../EAUBaseComponent";
import { withSigningProcessContext } from './withSigningProcessContext';
import { BTrustProcessorDataService } from '../../services';

interface SignerUIProps extends BaseProps, AsyncUIProps, ISigningProcessContextProps {
}

@observer class SignerUIImpl extends EAUBaseComponent<SignerUIProps, Signer> {
    private bissWorkingPorts: string[] = ['53952', '53953', '53954', '53955'];
    private workingPort: string = undefined;

    @observable private bissMustTurnOn: boolean;
    @observable private bissError: string;

    constructor(props?: SignerUIProps) {
        super(props);

        //Bind
        this.startLocalSign = this.startLocalSign.bind(this);
        this.processBissError = this.processBissError.bind(this);
        this.processLocalSigning = this.processLocalSigning.bind(this);

        //Init
        this.bissMustTurnOn = false;
        this.bissError = undefined;
        this.workingPort = undefined;
    }

    render(): JSX.Element {
        if (!this.model) return null;

        //let hasErrors: boolean = this.props.asyncErrors && this.props.asyncErrors.length > 0;

        return (
            <div className="interactive-container">
                <div className="interactive-container__content">
                    {this.bissMustTurnOn === false ? null : <div className="alert alert-warning mt-0" dangerouslySetInnerHTML={{ __html: this.getResource('GL_SIGN_SRV_LOCL_NOTAVL_I') }}></div>}
                    {ObjectHelper.isStringNullOrEmpty(this.bissError) ? null : <div className="alert alert-danger mt-0">{this.bissError}</div>}
                    {this.model.status == SignerSigningStatuses.Waiting ?
                        <>
                            <div className="row align-items-center">
                                {this.props.signerID && this.model.signerID != this.props.signerID ?
                                    <>
                                        <div className="col-md-4 form-group">
                                            <button type="button" className="btn btn-light btn-block" disabled={true}>
                                                <i className='ui-icon ui-icon-sign mr-1' area-hidden='true'></i>
                                                {this.getResource('GL_SIGN_LOCAL_L')}
                                            </button>
                                        </div>
                                        <div className="col-md-4 form-group">
                                            <button type="button" className="btn btn-light btn-block" disabled={true}>
                                                <i className='ui-icon ui-icon-remote-sign mr-1' area-hidden='true'></i>
                                                {this.getResource('GL_SIGN_BTRUST_L')}
                                            </button>
                                        </div>
                                        <div className="col-md-4 form-group">
                                            <button type="button" className="btn btn-light btn-block" disabled={true}>
                                                <i className='ui-icon ui-icon-remote-sign mr-1' area-hidden='true'></i>
                                                {this.getResource('GL_SIGN_EVROTRUST_L')}
                                            </button>
                                        </div>
                                    </>
                                    :
                                    <>
                                        <div className="col-md-4 form-group">
                                            <button type="button" className="btn btn-light btn-block" onClick={this.startLocalSign}>
                                                <i className='ui-icon ui-icon-sign mr-1' area-hidden='true'></i>
                                                {this.getResource('GL_SIGN_LOCAL_L')}
                                            </button>
                                        </div>
                                        <div className="col-md-4 form-group">
                                            <button className="btn btn-light btn-block" onClick={this.props.signerChooseChannel.bind(this, this.model.signerID, SigningChannels.BtrustRemote)}>
                                                <i className="ui-icon ui-icon-remote-sign mr-1" aria-hidden="true"></i>
                                                {this.getResource('GL_SIGN_BTRUST_L')}
                                            </button>
                                        </div>
                                        <div className="col-md-4 form-group">
                                            <button className="btn btn-light btn-block" onClick={this.props.signerChooseChannel.bind(this, this.model.signerID, SigningChannels.EvrotrustRemote)}>
                                                <i className="ui-icon ui-icon-remote-sign mr-1" aria-hidden="true"></i>
                                                {this.getResource('GL_SIGN_EVROTRUST_L')}
                                            </button>
                                        </div>
                                    </>}
                            </div>
                        </>
                        :
                        null}
                </div>
            </div>);
    }

    @action startLocalSign(): void {
        let that = this;
        //let workingPort: string = undefined;

        this.props.signerChooseChannel(this.model.signerID, SigningChannels.BtrustLocal);
        this.bissMustTurnOn = false;
        this.bissError = undefined;

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
                return (new BTrustProcessorDataService()).createBissSignRequest(that.props.processID, userCertBase64).then(result => {
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
                            return new BTrustProcessorDataService().completeBissSignProcess(that.props.processID, that.model.signerID, userCertBase64, signResult.signatures[0], hashTime).then(() => {
                                that.props.signerCompletSigning();
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

export const SignerUI = withAsyncFrame(withSigningProcessContext(SignerUIImpl), false, false);