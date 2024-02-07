import * as React from 'react';
import { observer } from 'mobx-react';
import { observable, runInAction, action } from 'mobx';
import { ObjectHelper, ValidationHelper } from 'cnsys-core'
import { BaseProps, AsyncUIProps, withAsyncFrame } from 'cnsys-ui-react';
import { EAUBaseComponent } from "../EAUBaseComponent";
import { BTrustProcessorDataService } from '../../services';
import { withSigningProcessContext } from './withSigningProcessContext';
import { ISigningProcessContextProps } from './SigningProcessContext';
import { SignerSigningStatuses, SigningChannels, Signer, BtrustDocStatus, BtrustUserInputRequest, BtrustUserInputTypes } from '../../models/sign';

const BY_EGN_LNCH_EMAIL_PHONE: string = "byEgnLnchEmailPhone";
const BY_PROFILEID: string = "byProfile";

interface BtrustSigningUIProps extends BaseProps, AsyncUIProps, ISigningProcessContextProps {
}

@observer class BtrustSigningUIImpl extends EAUBaseComponent<BtrustSigningUIProps, Signer> {
    private timerId: number;
    private radioButtonGroupId: string;
    private radioButtonByEgnLnchEmailPhone: string;
    private radioButtonByProfile: string;

    @observable private waitTryInitRemoteSigningBySignerIdent: boolean;
    @observable private profileID: string;
    @observable private profileError: string;
    @observable private otp: string;
    @observable private otpError: string;
    @observable private waySendRemoteRequest: string;
    @observable private egnLnchEmailPhone: string;
    @observable private egnLnchEmailPhoneError: string;

    constructor(props?: BtrustSigningUIProps) {
        super(props);

        //Bind
        this.createRemoteSignRequest = this.createRemoteSignRequest.bind(this);
        this.rejectChannel = this.rejectChannel.bind(this);
        this.componentWillUnmount = this.componentWillUnmount.bind(this);
        this.checkSignerSign = this.checkSignerSign.bind(this);
        this.profileIDChange = this.profileIDChange.bind(this);
        this.otpChange = this.otpChange.bind(this);
        this.createRemoteSignRequestInternal = this.createRemoteSignRequestInternal.bind(this);
        this.onWaySendRequestChange = this.onWaySendRequestChange.bind(this);
        this.egnLnchEmailPhoneChange = this.egnLnchEmailPhoneChange.bind(this);

        //Init
        this.radioButtonGroupId = ObjectHelper.newGuid();
        this.radioButtonByEgnLnchEmailPhone = `ByEGN${this.radioButtonGroupId}`;
        this.radioButtonByProfile = `ByProfile${this.radioButtonGroupId}`;
        this.profileID = undefined;
        this.profileError = undefined;
        this.otp = undefined;
        this.otpError = undefined;
        this.waySendRemoteRequest = BY_EGN_LNCH_EMAIL_PHONE;
        this.waitTryInitRemoteSigningBySignerIdent = false;
        this.egnLnchEmailPhone = undefined;
        this.egnLnchEmailPhoneError = undefined;

        if (this.model.status == SignerSigningStatuses.StartSigning) {
            this.checkSignerSign();
        } else {
            if (!ObjectHelper.isStringNullOrEmpty(this.model.ident) && this.model.ident.length > 6) {
                //Автоматично правим заявка по ЕГН/ЛНЧ на заявителя.
                this.waitTryInitRemoteSigningBySignerIdent = true;
                this.egnLnchEmailPhone = this.model.ident;
                this.createRemoteSignRequest(null);
            }
        }
    }

    render(): JSX.Element {
        if (this.waitTryInitRemoteSigningBySignerIdent === true) return null;

        let modelErrors = this.model.getModelErrors();

        return (
            <div id="signingBTrust" className="interactive-container interactive-container--form">
                {this.model.status == SignerSigningStatuses.StartSigning ? <div className="interactive-container__loading"></div> : null}
                <div className="interactive-container__content">
                    {
                        (!ObjectHelper.isStringNullOrEmpty(this.model.name) || !ObjectHelper.isStringNullOrEmpty(this.model.ident)) &&
                        <p className="field-text">
                            <b>{this.model.name}</b>{!ObjectHelper.isStringNullOrEmpty(this.model.name) && ', '}{this.getResource('EP_SIGN_ID_SHOW_L')}: {this.model.ident}
                        </p>
                    }

                    {this.model.status == SignerSigningStatuses.Waiting ?
                        <>
                            <div className="alert alert-info mt-0">
                                {this.getResource('GL_SIGN_ENTER_ID_BTRUST_I')}
                            </div>
                            {modelErrors && modelErrors.length > 0 && <div className="alert alert-danger" role="alert">{modelErrors[0].message}</div>}
                            {this.props.asyncErrors && this.props.asyncErrors.length > 0 ? <>{this.props.drawErrors()}{this.props.drawWarnings()}</> : null}
                            <div className="row">
                                <div className="form-group col-12">
                                    <fieldset>
                                        <legend className="form-control-label">{this.getResource('GL_IdentificationType_L')}</legend>
                                        <div className="custom-control custom-radio">
                                            <input id={this.radioButtonByEgnLnchEmailPhone} name={this.radioButtonGroupId} type="radio" value={BY_EGN_LNCH_EMAIL_PHONE} className="custom-control-input" checked={this.waySendRemoteRequest == BY_EGN_LNCH_EMAIL_PHONE} onChange={this.onWaySendRequestChange} />
                                            <label htmlFor={this.radioButtonByEgnLnchEmailPhone} className="custom-control-label">{this.getResource('GL_SIGN_ID_L')}</label>
                                        </div>
                                        <div className="custom-control custom-radio">
                                            <input id={this.radioButtonByProfile} name={this.radioButtonGroupId} type="radio" value={BY_PROFILEID} className="custom-control-input" checked={this.waySendRemoteRequest == BY_PROFILEID} onChange={this.onWaySendRequestChange} />
                                            <label htmlFor={this.radioButtonByProfile} className="custom-control-label">{this.getResource('GL_SIGN_PROFILE_ID_AND_AUTHORIZATION_CODE_L')}</label>
                                        </div>
                                    </fieldset>
                                </div>
                            </div>
                            {this.waySendRemoteRequest == BY_EGN_LNCH_EMAIL_PHONE ?
                                <div className="row">
                                    <div className="col-md-6 col-lg-4">
                                        <div className="row">
                                            <div className="col">
                                                <label htmlFor="PERSON_ID" className="form-control-label">{this.getResource('GL_SIGN_ID_L')}</label>
                                            </div>
                                        </div>
                                        <div className="row">
                                            <div className="form-group col">
                                                <input id="PERSON_ID" type="text" className="form-control" value={this.egnLnchEmailPhone ? this.egnLnchEmailPhone : ''} onChange={this.egnLnchEmailPhoneChange} />
                                                {ObjectHelper.isStringNullOrEmpty(this.egnLnchEmailPhoneError) ? null : <ul id="egnLnchEmailPhoneError" className="invalid-feedback"><li key={0}><i className="ui-icon ui-icon-error mr-1"></i>{this.egnLnchEmailPhoneError}</li></ul>}
                                            </div>
                                            <div className="form-group col-auto">
                                                <button className="btn btn-primary" onClick={this.createRemoteSignRequest}>{this.getResource('GL_SEND_L')}</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                :
                                <div className="row">
                                    <div className="col-sm-5 col-lg-3">
                                        <div className="row">
                                            <div className="col">
                                                <label htmlFor="BTRUST_PROFIL_ID" className="form-control-label">{this.getResource('GL_SIGN_BTRUST_ID_L')}</label>
                                            </div>
                                        </div>
                                        <div className="row">
                                            <div className="form-group col">
                                                <input id="BTRUST_PROFIL_ID" type="text" className="form-control" value={this.profileID ? this.profileID : ''} onChange={this.profileIDChange} />
                                                {ObjectHelper.isStringNullOrEmpty(this.profileError) ? null : <ul className="invalid-feedback" id="err-profileError"><li key={0}><i className="ui-icon ui-icon-error mr-1"></i>{this.profileError}</li></ul>}
                                            </div>
                                        </div>
                                    </div>
                                    <div className="col-sm-7 col-lg-4">
                                        <div className="row">
                                            <div className="col">
                                                <label htmlFor="OTP" className="form-control-label">{this.getResource('GL_SIGN_CODE_L')}</label>
                                            </div>
                                        </div>
                                        <div className="row">
                                            <div className="form-group col">
                                                <input id="OTP" type="text" className="form-control" value={this.otp ? this.otp : ''} onChange={this.otpChange} />
                                                {ObjectHelper.isStringNullOrEmpty(this.profileError) ? null : <ul id="otpError" className="invalid-feedback"><li key={0}><i className="ui-icon ui-icon-error mr-1"></i>{this.otpError}</li></ul>}
                                            </div>
                                            <div className="form-group col-sm-auto">
                                                <button className="btn btn-primary" onClick={this.createRemoteSignRequest}>{this.getResource('GL_SEND_L')}</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>}
                        </>
                        :
                        <div className="alert alert-info mt-0">
                            {this.getResource('GL_SIGN_ONGOING_BTRUST_I')}
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

    @action private onWaySendRequestChange(event: any): void {
        this.waySendRemoteRequest = event.target.value;
    }

    @action private egnLnchEmailPhoneChange(e: any): void {
        this.egnLnchEmailPhone = e.target.value;

        if (ValidationHelper.isValidEGNLNCh(this.egnLnchEmailPhone)
            || ValidationHelper.isValidPhone(this.egnLnchEmailPhone)
            || ValidationHelper.isEmailAddress(this.egnLnchEmailPhone)) {
            this.egnLnchEmailPhoneError = undefined;
        } else {
            this.egnLnchEmailPhoneError = this.getResource('GL_INVALID_IDENT_E');
        }
    }

    @action private profileIDChange(e: any): void {
        this.profileID = e.target.value;

        if (ObjectHelper.isStringNullOrEmpty(this.profileID)) {
            this.profileError = this.getResource('GL_INPUT_FIELD_MUST_E');
        } else {
            this.profileError = undefined;
        }
    }

    @action private otpChange(e: any): void {
        this.otp = e.target.value;

        if (ObjectHelper.isStringNullOrEmpty(this.otp)) {
            this.otpError = this.getResource('GL_INPUT_FIELD_MUST_E');
        } else {
            this.otpError = undefined;
        }
    }

    private rejectChannel(e: any): void {
        if (this.timerId)
            clearTimeout(this.timerId);

        this.model.clearErrors();

        if (this.props.signerRejectChannel) {
            this.props.signerRejectChannel(true);
        }
    }

    @action private createRemoteSignRequest(e: any): void {
        let inputType: BtrustUserInputTypes;
        let userData: BtrustUserInputRequest;

        if (this.waySendRemoteRequest == BY_EGN_LNCH_EMAIL_PHONE) {
            if (ValidationHelper.isValidEGN(this.egnLnchEmailPhone)) {
                inputType = BtrustUserInputTypes.EGN;
            } else if (ValidationHelper.isValidLNCh(this.egnLnchEmailPhone)) {
                inputType = BtrustUserInputTypes.LNCH;
            } else if (ValidationHelper.isEmailAddress(this.egnLnchEmailPhone)) {
                inputType = BtrustUserInputTypes.EMAIL;
            } else if (ValidationHelper.isValidPhone(this.egnLnchEmailPhone)) {
                inputType = BtrustUserInputTypes.PHONE;
            } else {
                this.egnLnchEmailPhoneError = this.getResource('GL_INVALID_IDENT_E');
                this.waitTryInitRemoteSigningBySignerIdent = false;
                return;
            }

            userData = { input: this.egnLnchEmailPhone, inputType: inputType };
        } else {
            let hasError: boolean = false;
            inputType = BtrustUserInputTypes.PROFILE;

            if (ObjectHelper.isStringNullOrEmpty(this.profileError) && ObjectHelper.isStringNullOrEmpty(this.otpError)) {
                if (ObjectHelper.isStringNullOrEmpty(this.profileID)) {
                    this.profileError = this.getResource('GL_INPUT_FIELD_MUST_E');
                    hasError = true;
                }

                if (ObjectHelper.isStringNullOrEmpty(this.otp)) {
                    this.otpError = this.getResource('GL_INPUT_FIELD_MUST_E');
                    hasError = true;
                }
            }

            if (hasError == true)
                return;

            userData = { input: this.profileID, inputType: inputType, otp: this.otp };
        }

        this.createRemoteSignRequestInternal(userData);
    }

    private createRemoteSignRequestInternal(userData: BtrustUserInputRequest): void {
        let that = this;

        this.props.registerAsyncOperation(new BTrustProcessorDataService().createRemoteSignRequest(this.props.processID, this.props.signerID, userData)
            .then(() => {
                runInAction(() => {
                    that.waitTryInitRemoteSigningBySignerIdent = false;
                    that.model.status = SignerSigningStatuses.StartSigning;
                    that.model.signingChannel = SigningChannels.BtrustRemote;
                    that.model.rejectReson = undefined;
                    that.checkSignerSign();
                });
            })
            .finally(() => {
                if (that.waitTryInitRemoteSigningBySignerIdent == true)
                    that.waitTryInitRemoteSigningBySignerIdent = false;
            })
        );
    }

    checkSignerSign(): void {
        let that = this;

        this.timerId = window.setTimeout(() => {
            (new BTrustProcessorDataService()).completeRemoteSigning(this.props.processID, this.props.signerID).then(res => {
                if (res.code.toUpperCase() === 'OK'
                    && !ObjectHelper.isNullOrUndefined(res.status)) {

                    if (res.status == BtrustDocStatus.SIGNED) {
                        clearTimeout(that.timerId);
                        that.props.signerCompletSigning();
                    } else if (res.status == BtrustDocStatus.REJECTED) {
                        clearTimeout(that.timerId);
                        that.props.signerRejectChannel(false, res.rejectReson);
                    }
                    else {
                        that.checkSignerSign();
                    }
                } else {
                    that.checkSignerSign();
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

    private onHover(e: any) {
        $("#" + "signingBTrust").addClass("interactive-container--focus");
    }

    private onHoverLeave(e: any) {
        $("#" + "signingBTrust").removeClass("interactive-container--focus");
    }
}

export const BtrustSigningUI = withAsyncFrame(withSigningProcessContext(BtrustSigningUIImpl), false);