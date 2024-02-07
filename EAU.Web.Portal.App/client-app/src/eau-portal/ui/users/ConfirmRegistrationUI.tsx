import { ObjectHelper } from 'cnsys-core';
import { AsyncUIProps, BaseProps, BaseRouteParams, BaseRouteProps, BaseRoutePropsExt, withAsyncFrame, withRouter } from 'cnsys-ui-react';
import { EAUBaseComponent, ValidationSummaryErrors } from 'eau-core';
import { observable } from 'mobx';
import { observer } from 'mobx-react';
import React from 'react';
import { Alert } from 'reactstrap';
import { UserService } from '../../services/UserService';

interface ConfirmRegistrationRouteParams extends BaseRouteParams {
    processId: string;
}

interface ConfirmRegistrationProps extends BaseRouteProps<ConfirmRegistrationRouteParams>, BaseProps, AsyncUIProps, BaseRoutePropsExt {
}

@observer class ConfirmRegistrationImpl extends EAUBaseComponent<ConfirmRegistrationProps, any>{

    private _userService: UserService;
    private processId: string;

    constructor(props: ConfirmRegistrationProps) {
        super(props);

        this.renewActivationLinkForRegistration = this.renewActivationLinkForRegistration.bind(this);

        this._userService = new UserService();
        this.processId = this.props.match.params.processId;
    }

    @observable notification: any;

    componentDidMount() {
        this.completeRegistration();
    }

    render() {
        return <div className="page-wrapper" id="ARTICLE-CONTENT">
            {this.notification || null}
            <ValidationSummaryErrors errors={this.props.asyncErrorMessages} />
        </div>
    }

    private completeRegistration() {

        if (ObjectHelper.isStringNullOrEmpty(this.processId)) {
            this.props.routerExt.goTo('/', null);
        } else {

            this.props.registerAsyncOperation(this._userService.completeRegistration(this.processId).then((result) => {

                if (result.isProcessExpired) {
                    this.notification = <Alert color="warning">{this.getResource("GL_USR_0004_E")} <button className="btn btn-link" onClick={this.renewActivationLinkForRegistration}>{this.getResource("GL_USR_SEND_REG_L")}</button></Alert>
                } else {
                    this.notification = <Alert color="success">{this.getResource("GL_CONFIRMED_REGISTRATION_I")}</Alert>
                }
            }))
        }
    }

    private renewActivationLinkForRegistration() {

        this.props.registerAsyncOperation(this._userService.renewUserRegistrationLink(this.processId).then(() => {
            this.notification = <Alert color="success">{this.getResource("GL_00012_I")}</Alert>
        }))
    }
}

export const ConfirmRegistrationUI = withRouter(withAsyncFrame(ConfirmRegistrationImpl, false));