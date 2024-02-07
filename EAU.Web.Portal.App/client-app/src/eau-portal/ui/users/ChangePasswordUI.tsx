import { AsyncUIProps, BaseProps, withAsyncFrame } from 'cnsys-ui-react';
import { attributesClassFormControlRequiredLabel, eauAuthenticationService, EAUBaseComponent, ValidationSummary, ValidationSummaryErrors, ValidationSummaryStrategy, attributesTypePasswordFormControlRequired } from 'eau-core';
import { action, observable, runInAction } from 'mobx';
import { observer } from 'mobx-react';
import React from 'react';
import { Link } from 'react-router-dom';
import { Alert } from 'reactstrap';
import { Constants } from '../../Constants';
import { UserInputModel } from '../../models/ModelsManualAdded';
import { UserService } from '../../services/UserService';
import { ChangePasswordValidator } from '../../validations/UsersValidator';

interface ChangePasswordProps extends BaseProps, AsyncUIProps {
}

@observer class ChangePasswordImpl extends EAUBaseComponent<ChangePasswordProps, UserInputModel>{

    @observable notification: any;

    constructor(props: ChangePasswordProps) {
        super(props);

        this.model = new UserInputModel();
        this.validators = [new ChangePasswordValidator()];
        this.changePassword = this.changePassword.bind(this);

        this.props.registerAsyncOperation(eauAuthenticationService.getCurrentUser().then(u => {
            this.model.email = u.email;
        }));
    }

    render() {
        return <div className="page-wrapper" id="ARTICLE-CONTENT">
            {this.notification || null}
            <ValidationSummaryErrors asyncErrors={this.props.asyncErrors} />
            <ValidationSummary model={this.model} {...this.props} strategy={ValidationSummaryStrategy.includeOnlyModelErrors} />
            <div className="ui-form ui-form--input">
                <div className="row">
                    <div className="col-12">
                        <div className="row">
                            <div className="col-sm-6 form-group">
                                {this.labelFor(x => x.currentPassword, "GL_OLD_PASSWORD_L", attributesClassFormControlRequiredLabel)}
                                {this.textBoxFor(x => x.currentPassword, attributesTypePasswordFormControlRequired)}
                            </div>
                        </div>
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
                    <button className="btn btn-primary" onClick={this.changePassword}>{this.getResource("GL_CONFIRM_L")}</button>
                </div>
                <div className="left-side">
                    <Link to={Constants.PATHS.Home} className="btn btn-secondary" >{this.getResource("GL_CANCEL_L")}</Link>
                </div>
            </div>
        </div>
    }

    @action private changePassword() {
        if (this.validators[0].validate(this.model)) {

            this.props.registerAsyncOperation(new UserService().changePassword(this.model).then(result => {
                runInAction(() => {
                    this.notification = <Alert color="success">{this.getResource("GL_UPDATE_OK_I")}</Alert>
                    this.model = new UserInputModel();
                })
            }))
        }
    }
}

export const ChangePasswordUI = withAsyncFrame(ChangePasswordImpl, false)