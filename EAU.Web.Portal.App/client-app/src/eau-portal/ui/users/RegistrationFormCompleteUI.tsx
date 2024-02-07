import { AsyncUIProps, BaseProps, withAsyncFrame } from 'cnsys-ui-react';
import { EAUBaseComponent, resourceManager } from 'eau-core';
import React from 'react';
import { observer } from 'mobx-react';
import { observable } from 'mobx';
import { UserService } from '../../services/UserService';
import { Alert } from 'reactstrap';
import { ObjectHelper, UrlHelper } from 'cnsys-core';

interface RegistrationFormCompleteProps extends BaseProps, AsyncUIProps {
}

@observer class RegistrationFormCompleteImpl extends EAUBaseComponent<RegistrationFormCompleteProps, any>{

    private _userService: UserService;
    private email: string;

    @observable notification: any = null;

    constructor(props: RegistrationFormCompleteProps) {
        super(props);

        this._userService = new UserService();
        this.email = !ObjectHelper.isStringNullOrEmpty(UrlHelper.getUrlParameter("email")) ? UrlHelper.getUrlParameter("email") : '';
        this.resendConfirmationEmail = this.resendConfirmationEmail.bind(this);
    }

    render() {
        return <div className="page-wrapper">
                {!ObjectHelper.isNullOrUndefined(this.notification) ?
                    this.notification
                :
                    <Alert color="success">{resourceManager.getResourceByKey("GL_00012_I")} <button className="btn btn-link" onClick={this.resendConfirmationEmail}>{this.getResource("GL_USR_SEND_REG_L")}</button></Alert>
                }
        </div>
    }

    private resendConfirmationEmail() {
        if (!ObjectHelper.isStringNullOrEmpty(this.email)) {
            this.props.registerAsyncOperation(this._userService.resendConfirmationEmail(this.email).then(() => {
                this.notification = <Alert color="info">{resourceManager.getResourceByKey("GL_00013_I")}</Alert>
            }))
        }
    }
}

export const RegistrationFormCompleteUI = withAsyncFrame(RegistrationFormCompleteImpl, false)