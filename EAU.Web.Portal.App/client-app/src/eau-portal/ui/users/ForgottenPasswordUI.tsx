import { ObjectHelper } from 'cnsys-core';
import { AsyncUIProps, BaseProps, BaseRouteParams, BaseRouteProps, BaseRoutePropsExt, withAsyncFrame, withRouter } from 'cnsys-ui-react';
import { attributesClassFormControlRequiredLabel, attributesTypePasswordFormControlRequired, EAUBaseComponent, ValidationSummary, ValidationSummaryErrors, ValidationSummaryStrategy } from 'eau-core';
import { observable, runInAction } from 'mobx';
import { observer } from 'mobx-react';
import React from 'react';
import { Link } from 'react-router-dom';
import { Alert } from 'reactstrap';
import { Constants } from '../../Constants';
import { CompleteForgottenPasswordModel, UserInputModel } from '../../models/ModelsManualAdded';
import { UserService } from '../../services/UserService';
import { ForgottenPasswordValidator } from '../../validations/UsersValidator';

interface ForgottenPasswordRouteParams extends BaseRouteParams {
    processId: string;
}

interface ForgottenPasswordProps extends BaseRouteProps<ForgottenPasswordRouteParams>, BaseProps, AsyncUIProps, BaseRoutePropsExt {
}

@observer class ForgottenPasswordImpl extends EAUBaseComponent<ForgottenPasswordProps, UserInputModel>{

    private _userService: UserService;

    @observable notification: any;
    @observable isUserProcessLoaded: boolean;
    @observable isValidLink: boolean;
    @observable resendActivationLink: boolean;

    constructor(props: ForgottenPasswordProps) {
        super(props);

        this.completeForgottenPassword = this.completeForgottenPassword.bind(this);
        this.renewLinkForForgottenPassword = this.renewLinkForForgottenPassword.bind(this);

        this.model = new UserInputModel();
        this.validators = [new ForgottenPasswordValidator()];
        this._userService = new UserService();
    }

    componentWillMount() {
        let that = this;

        this.props.registerAsyncOperation(this._userService.getUserProcess(this.props.match.params.processId).then((isActiveLink) => {
            runInAction(() => {
                that.isUserProcessLoaded = true;
                that.isValidLink = isActiveLink;
            })
        }))
    }

    render() {

        if (!this.isUserProcessLoaded)
            return null

        return <div className="page-wrapper" id="ARTICLE-CONTENT">
            {this.notification || null}
            <ValidationSummaryErrors asyncErrors={this.props.asyncErrors} />
            <ValidationSummary model={this.model} {...this.props} strategy={ValidationSummaryStrategy.includeOnlyModelErrors} />
            {
                this.isValidLink
                    ? <>
                        <div className="ui-form ui-form--input">
                            <div className="row">
                                <div className="col-12">
                                    <div className="row">
                                        <div className="col-sm-6 form-group">
                                            {this.labelFor(x => x.password, "GL_NEW_PASSWORD_L", attributesClassFormControlRequiredLabel)}
                                            {this.textBoxFor(x => x.password, attributesTypePasswordFormControlRequired)}
                                            {this.passwordStrengthMeterFor(this.model.password)}
                                        </div>
                                        <div className="col-sm-6 form-group">
                                            {this.labelFor(x => x.confirmPassword, "GL_CONFIRM_NEW_PASSWORD_L", attributesClassFormControlRequiredLabel)}
                                            {this.textBoxFor(x => x.confirmPassword, attributesTypePasswordFormControlRequired)}
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div className="button-bar button-bar--form button-bar--responsive">
                            <div className="right-side">
                                <button className="btn btn-primary" onClick={this.completeForgottenPassword}>{this.getResource("GL_CONFIRM_L")}</button>
                            </div>
                            <div className="left-side">
                                <Link to={Constants.PATHS.Home} className="btn btn-secondary" >{this.getResource("GL_CANCEL_L")}</Link>
                            </div>
                        </div>
                    </>
                    : this.resendActivationLink
                        ? null
                        : <Alert color="warning">{this.getResource("GL_USR_0017_E")} {<button className="btn btn-link" onClick={this.renewLinkForForgottenPassword}>{this.getResource("GL_USR_SEND_PASS_L")}</button>}</Alert>
            }
        </div>
    }

    private renewLinkForForgottenPassword() {
        let processId = this.props.match.params.processId;

        if (!ObjectHelper.isNullOrUndefined(processId)) {

            this.props.registerAsyncOperation(this._userService.renewResetPassword(processId).then((result) => {
                runInAction(() => {
                    this.resendActivationLink = true;
                    this.notification = <Alert color="success">{this.getResource("GL_COMPLETE_FORGOTTEN_PASS_I")}</Alert>
                })
            }))
        }
    }

    private completeForgottenPassword() {
        var that = this;

        if (this.validators[0].validate(this.model)) {

            let processId = this.props.match.params.processId;

            if (!ObjectHelper.isNullOrUndefined(processId)) {

                var model = new CompleteForgottenPasswordModel();
                model.newPassword = this.model.password;
                model.processId = processId;

                this.props.registerAsyncOperation(this._userService.completeForgottenPassword(model).then(result => {
                    runInAction(() => {
                        that.notification = <Alert color="success">{that.getResource("GL_UPDATE_OK_I")}</Alert>
                        that.model = new UserInputModel();
                    })
                }))
            }
        }
    }
}

export const ForgottenPasswordUI = withRouter(withAsyncFrame(ForgottenPasswordImpl, false));