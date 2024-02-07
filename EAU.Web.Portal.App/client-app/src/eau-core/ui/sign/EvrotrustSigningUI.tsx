import * as React from 'react';
import { action, observable, runInAction } from 'mobx';
import { observer } from 'mobx-react';
import { ArrayHelper, ObjectHelper, ValidationHelper } from 'cnsys-core';
import { AsyncUIProps, BaseProps, withAsyncFrame } from 'cnsys-ui-react';
import { Signer, SignerSigningStatuses, SigningChannels } from '../../models/sign';
import { EvrotrustProcessorDataService, SigningProcessesDataService } from '../../services';
import { EAUBaseComponent } from "../EAUBaseComponent";
import { ISigningProcessContextProps } from './SigningProcessContext';
import { withSigningProcessContext } from './withSigningProcessContext';

interface EvrotrustSigningUIProps extends BaseProps, AsyncUIProps, ISigningProcessContextProps {
}

@observer class EvrotrustSigningUIImpl extends EAUBaseComponent<EvrotrustSigningUIProps, Signer> {
    private timerId: number;

    @observable private waitTryInitRemoteSigningBySignerIdent: boolean;
    @observable private userIdentFromInput: string;

    constructor(props?: EvrotrustSigningUIProps) {
        super(props);

        //Bind
        this.createRemoteSignRequest = this.createRemoteSignRequest.bind(this);
        this.rejectChannel = this.rejectChannel.bind(this);
        this.componentWillUnmount = this.componentWillUnmount.bind(this);
        this.checkSignerSign = this.checkSignerSign.bind(this);
        this.userIdentFromInputChange = this.userIdentFromInputChange.bind(this);

        //Init
        this.userIdentFromInput = undefined;

        if (!ObjectHelper.isStringNullOrEmpty(this.model.ident) && this.model.ident.length > 6 && this.model.status == SignerSigningStatuses.Waiting) {
            this.waitTryInitRemoteSigningBySignerIdent = true;
            this.createRemoteSignRequest();
        } else if (this.model.status == SignerSigningStatuses.StartSigning) {
            this.waitTryInitRemoteSigningBySignerIdent = false;
            this.checkSignerSign();
        } else {
            this.waitTryInitRemoteSigningBySignerIdent = false;
        }
    }

    render(): JSX.Element {
        if (this.waitTryInitRemoteSigningBySignerIdent === true) return null;

        let modelErrors = this.model.getModelErrors();

        return (
            <div id="signEvrotrust" className="interactive-container">
                {this.model.status == SignerSigningStatuses.StartSigning ? <div className="interactive-container__loading"></div> : null}
                <div className="interactive-container__content">
                    {this.model.status == SignerSigningStatuses.Waiting &&
                        <>
                            <div className="alert alert-info mt-0">
                                <p>{this.getResource('GL_SIGN_ENTER_ID_DECL_EVROTRUST_I')}</p>
                            </div>
                            {modelErrors && modelErrors.length > 0 && <div className="alert alert-danger" role="alert">{modelErrors[0].message}</div>}
                            {this.props.asyncErrors && this.props.asyncErrors.length > 0 ? <>{this.props.drawErrors()}{this.props.drawWarnings()}</> : null}
                            <div className="row">
                                <div className="col-sm-6">
                                    <div className="row">
                                        <div className="col">
                                            <label htmlFor="SIGN_ID" className="form-control-label">{this.getResource('GL_SIGN_ID_L')}</label>
                                        </div>
                                    </div>
                                    <div className="row">
                                        <div className="form-group col">
                                            <input id="SIGN_ID" type="text" className="form-control" value={this.userIdentFromInput || ''} onChange={this.userIdentFromInputChange} />
                                        </div>
                                        <div className="form-group col-auto">
                                            <button className="btn btn-primary" onClick={this.createRemoteSignRequest}>{this.getResource('GL_SEND_L')}</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </>}

                    {this.model.status == SignerSigningStatuses.StartSigning &&
                        <div className="alert alert-info mt-0">
                            {this.getResource('GL_SIGN_ONGOING_EVRTRUST_I')}
                        </div>}
                </div>

                <div className="interactive-container__controls">
                    <button onClick={this.rejectChannel} className="btn btn-light btn-sm" onFocus={this.onHover} onBlur={this.onHoverLeave} onMouseOver={this.onHover} onMouseLeave={this.onHoverLeave} title={this.getResource('GL_CANCEL_SIGNING_L')}>
                        <i className="ui-icon ui-icon-times" aria-hidden="true"></i>
                    </button>
                </div>

            </div>);
    }

    componentWillUnmount() {
        if (this.timerId)
            clearTimeout(this.timerId);
    }

    private userIdentFromInputChange(e: any): void {
        this.userIdentFromInput = e.target.value;
    }

    private rejectChannel(e: any): void {
        if (this.timerId)
            clearTimeout(this.timerId);

        this.model.clearErrors();

        var dataService = new EvrotrustProcessorDataService();

        this.props.registerAsyncOperation(dataService.signerRejectSigning(this.props.processID, this.props.signerID).bind(this).then(() => {
            if (this.props.signerRejectChannel) {
                this.props.signerRejectChannel(false);
            }
        }));
    }

    @action private createRemoteSignRequest(): void {
        let that = this;
        let identType: number = 0;
        let ident: string = this.model.ident;

        if (!ObjectHelper.isStringNullOrEmpty(this.userIdentFromInput)) {

            if (ValidationHelper.isValidEGNLNCh(this.userIdentFromInput))
                identType = 0;
            else if (ValidationHelper.isEmailAddress(this.userIdentFromInput))
                identType = 1;
            else {
                identType = 2;
            }

            ident = this.userIdentFromInput;
        } else {
            if (this.waitTryInitRemoteSigningBySignerIdent === false) {
                return;
            }
        }

        this.props.registerAsyncOperation(new EvrotrustProcessorDataService().createSignRequest(this.props.processID, this.props.signerID, identType, ident)
            .then(() => {
                runInAction(() => {
                    that.waitTryInitRemoteSigningBySignerIdent = false;

                    that.model.status = SignerSigningStatuses.StartSigning;
                    that.model.signingChannel = SigningChannels.EvrotrustRemote;
                    that.model.rejectReson = undefined;

                    that.checkSignerSign();
                });
            }).finally(() => {
                if (that.waitTryInitRemoteSigningBySignerIdent)
                    that.waitTryInitRemoteSigningBySignerIdent = false;
            })
        );
    }

    checkSignerSign(): void {
        let that = this;

        this.timerId = window.setTimeout(() => {
            (new SigningProcessesDataService()).search(this.props.processID).then(processes => {
                if (!processes || processes.length == 0) {
                    that.props.signerCompletSigning();
                } else if (processes.length == 1 && processes[0].signers && ArrayHelper.queryable.from(processes[0].signers).count(s => s.signerID == that.model.signerID) == 1) {
                    let currentSigner = ArrayHelper.queryable.from(processes[0].signers).first(s => s.signerID == that.model.signerID);

                    if (currentSigner.status == SignerSigningStatuses.Signed) {
                        clearTimeout(that.timerId);
                        that.props.signerCompletSigning();
                    } else if (currentSigner.status == SignerSigningStatuses.Waiting) {
                        clearTimeout(that.timerId);
                        that.props.signerRejectChannel(false, currentSigner.rejectReson);
                    } else {
                        that.checkSignerSign();
                    }
                } else {
                    //Това е случай който не би трябвало да се случи.
                    clearTimeout(that.timerId);
                    that.props.signerRejectChannel(true, "GL_SIGN_FAIL_E");
                }
            }).catch(err => {
                console.log(err);

                clearTimeout(that.timerId);

                runInAction(() => {
                    that.model.addError('GL_SIGN_FAIL_E');
                });
            });
        }, 500);
    }

    private onHover(e: any): void {
        $("#" + "signEvrotrust").addClass("interactive-container--focus");
    }

    private onHoverLeave(e: any): void {
        $("#" + "signEvrotrust").removeClass("interactive-container--focus");
    }
}

export const EvrotrustSigningUI = withAsyncFrame(withSigningProcessContext(EvrotrustSigningUIImpl), false, false);