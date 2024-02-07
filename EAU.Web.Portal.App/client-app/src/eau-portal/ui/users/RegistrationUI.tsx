import { ObjectHelper } from 'cnsys-core';
import { AsyncUIProps, BaseProps, RawHTML, withAsyncFrame } from 'cnsys-ui-react';
import { attributesClassFormControlReqired, attributesClassFormControlRequiredLabel, attributesTypePasswordFormControlRequired, EAUBaseComponent, resourceManager, ValidationSummary, ValidationSummaryErrors, ValidationSummaryStrategy } from 'eau-core';
import { action, observable, runInAction } from 'mobx';
import { observer } from 'mobx-react';
import React from 'react';
import { Link } from 'react-router-dom';
import { Alert } from 'reactstrap';
import { Constants } from '../../Constants';
import { UserInputModel } from '../../models/ModelsManualAdded';
import { UserService } from '../../services/UserService';
import { RegistrationValidator } from '../../validations/UsersValidator';
import { RegistrationTypeUI } from './RegistrationTypeUI';

interface RegistrationProps extends BaseProps, AsyncUIProps {
}

@observer class RegistrationImpl extends EAUBaseComponent<RegistrationProps, UserInputModel>{

    private _userService: UserService;
    private userEmailForResendingActiovationLink: string;
    @observable notification: any;
    @observable registrationData: any = null;

    constructor() {
        super();

        this.init();

        this.register = this.register.bind(this);
        this.resendConfirmationEmail = this.resendConfirmationEmail.bind(this);
        this.handleAcceptedTermsChange = this.handleAcceptedTermsChange.bind(this);
        this.getRegistrationData = this.getRegistrationData.bind(this);
    }

    componentDidMount() {
        this.getRegistrationData();
    }

    render() {
        return <div className="page-wrapper" id="ARTICLE-CONTENT">
            {this.notification || null}
            <ValidationSummaryErrors asyncErrors={this.props.asyncErrors} />
            <ValidationSummary model={this.model} {...this.props} strategy={ValidationSummaryStrategy.includeOnlyModelErrors} />

            {!ObjectHelper.isNullOrUndefined(this.registrationData) ?
                <div className="ui-form ui-form--input">
                    <div className="row">
                        <div className="col-md order-2 order-md-0">

                            <fieldset className="fields-group">
                                <div className="row">
                                    <div className="form-group col">
                                        {this.labelFor(x => x.email, "GL_EMAIL_L", attributesClassFormControlRequiredLabel)}
                                        {this.textBoxFor(x => x.email, attributesClassFormControlReqired)}
                                    </div>
                                </div>
                                <div className="row">
                                    <div className="form-group col-12">
                                        {this.labelFor(x => x.password, "GL_PASSWORD_L", attributesClassFormControlRequiredLabel)}
                                        {this.textBoxFor(x => x.password, attributesTypePasswordFormControlRequired)}
                                        {this.passwordStrengthMeterFor(this.model.password)}
                                    </div>
                                    <div className="form-group col-12">
                                        {this.labelFor(x => x.confirmPassword, "GL_CONFIRM_PASSWORD_L", attributesClassFormControlRequiredLabel)}
                                        {this.textBoxFor(x => x.confirmPassword, attributesTypePasswordFormControlRequired)}
                                    </div>
                                </div>
                                <div className="row">
                                    <div className="form-group col-12">
                                        <div className="custom-control custom-checkbox">
                                            <input className="custom-control-input" id="accept-terms" type="checkbox" title={this.getResource("GL_CONTAINS_LINK_ACCEPTED_TERMS_I")} required checked={this.model.acceptedTerms} onChange={this.handleAcceptedTermsChange} />
                                            <label className="custom-control-label" htmlFor="accept-terms">
                                                {<RawHTML rawHtmlText={this.getResource("GL_DOC_ACCEPTED_TERMS_L").replace("_TITLE_", this.getResource("GL_PAGE_OPEN_IN_NEW_TAB_L")).replace("_ADDRESS_", "/accept-terms")} />}
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div className="row mt-2">
                                    <div className="form-group col-sm order-0 order-sm-2">
                                        <button className="btn btn-primary btn-block" aria-label="Вход в системата" type="button" onClick={(e) => this.register(e)}>{this.getResource("GL_REGISTER_L")}</button>
                                    </div>
                                    <div className="col-1 d-none d-sm-block order-1"></div>
                                    <div className="form-group col-sm order-2 order-sm-0">
                                        <Link to={Constants.PATHS.Home} className="btn btn-secondary btn-block" >{this.getResource("GL_CANCEL_L")}</Link>
                                    </div>
                                </div>
                            </fieldset>

                        </div>

                        <div className="col order-1 d-none d-md-block page-vertical-divider"></div>
                        <div className="col-md order-0 order-md-2">
                            <div className="alert alert-info mt-0" role="alert"><RawHTML rawHtmlText={this.getResource("GL_DOC_PORTAL_REGISTRATION_INFORMATION_I")} /></div>
                        </div>
                    </div>
                </div>
                :
                <RegistrationTypeUI />
            }

            <form name="registrationComplete" method="post"></form>

        </div>
    }

    @action private register(e: any) {

        this.notification = null;

        if (this.validators[0].validate(this.model)) {

            this.props.registerAsyncOperation(new UserService().register(this.model).then(result => {
                runInAction(() => {

                    this.userEmailForResendingActiovationLink = this.model.email;

                    if (result && result.emailAlreadyExists && result.emailUserStillNotActivated) {
                        this.notification = <Alert color="warning">{resourceManager.getResourceByKey("GL_USR_00007_I")} <button className="btn btn-link" onClick={this.resendConfirmationEmail}>{this.getResource("GL_USR_SEND_REG_L")}</button></Alert>
                    } else {

                        e.preventDefault();
                        document.forms["registrationComplete"].action="api/Users/registrationFormEnd?email=" + this.model.email;
                        document.forms["registrationComplete"].submit();
                    }

                    this.initModel();
                })
            }))
        }
    }

    @action private init() {
        this.initModel();
        this.validators = [new RegistrationValidator()];
        this._userService = new UserService();
    }

    @action private initModel() {
        this.model = new UserInputModel();
        this.model.acceptedTerms = false;
    }

    private resendConfirmationEmail() {
        this.props.registerAsyncOperation(this._userService.resendConfirmationEmail(this.userEmailForResendingActiovationLink).then(() => {
            this.notification = <Alert color="info">{resourceManager.getResourceByKey("GL_00013_I")}</Alert>
        }))
    }

    private handleAcceptedTermsChange(e: any) {
        this.model.acceptedTerms = !this.model.acceptedTerms;
    }

    private getRegistrationData() {
        this._userService.getRegistrationData().then((registrationDataResult) => {
            if (!ObjectHelper.isNullOrUndefined(registrationDataResult)) {
                this.registrationData = registrationDataResult;

                this.model.email = registrationDataResult.email;
            }
        })
    }
}

export const RegistrationUI = withAsyncFrame(RegistrationImpl, false)