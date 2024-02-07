import { AsyncUIProps, BaseProps, withAsyncFrame } from 'cnsys-ui-react';
import { attributesClassFormControlRequiredLabel, EAUBaseComponent, ValidationSummary, ValidationSummaryErrors, ValidationSummaryStrategy, attributesClassFormControlReqired } from 'eau-core';
import { observable, runInAction } from 'mobx';
import { observer } from 'mobx-react';
import React from 'react';
import { Link } from 'react-router-dom';
import { Alert } from 'reactstrap';
import { Constants } from '../../Constants';
import { UserInputModel } from '../../models/ModelsManualAdded';
import { UserService } from '../../services/UserService';
import { ResetPasswordValidator } from '../../validations/UsersValidator';

interface ResetPasswordProps extends BaseProps, AsyncUIProps {
}

@observer class ResetPasswordImpl extends EAUBaseComponent<ResetPasswordProps, UserInputModel>{

    @observable notification: any;

    constructor() {
        super();

        this.model = new UserInputModel();
        this.validators = [new ResetPasswordValidator()];
        this.resetPassword = this.resetPassword.bind(this);
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
                                {this.labelFor(x => x.email, "GL_EMAIL_L", attributesClassFormControlRequiredLabel)}
                                {this.textBoxFor(x => x.email, attributesClassFormControlReqired)}
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div className="button-bar button-bar--form button-bar--responsive">
                <div className="right-side">
                    <button className="btn btn-primary" onClick={this.resetPassword}>{this.getResource("GL_CONFIRM_L")}</button>
                </div>
                <div className="left-side">
                    <Link to={Constants.PATHS.Home} className="btn btn-secondary" >{this.getResource("GL_CANCEL_L")}</Link>
                </div>
            </div>
        </div>
    }

    private resetPassword() {
        if (this.validators[0].validate(this.model)) {

            this.props.registerAsyncOperation(new UserService().resetPassword(this.model.email).then(result => {
                runInAction(() => {
                    this.notification = <Alert color="success">{this.getResource("GL_COMPLETE_FORGOTTEN_PASS_I")}</Alert>
                    this.model = new UserInputModel();
                })
            }))
        }
    }
}

export const ResetPasswordUI = withAsyncFrame(ResetPasswordImpl, false)