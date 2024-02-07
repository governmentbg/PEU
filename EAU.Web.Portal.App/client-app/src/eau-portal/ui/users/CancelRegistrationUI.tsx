import { ObjectHelper } from 'cnsys-core';
import { AsyncUIProps, BaseProps, BaseRouteProps, BaseRoutePropsExt, withAsyncFrame, withRouter } from 'cnsys-ui-react';
import { EAUBaseComponent, ValidationSummaryErrors } from 'eau-core';
import { observable } from 'mobx';
import { observer } from 'mobx-react';
import React from 'react';
import { Alert } from 'reactstrap';
import { UserService } from '../../services/UserService';

interface CancelRegistrationProps extends BaseRouteProps<any>, BaseProps, AsyncUIProps, BaseRoutePropsExt {
}

@observer class CancelRegistrationImpl extends EAUBaseComponent<CancelRegistrationProps, any>{

    @observable notification: any;

    componentDidMount() {
        this.cancelRegistration();
    }

    render() {
        return <div className="page-wrapper" id="ARTICLE-CONTENT">
            {this.notification || null}
            <ValidationSummaryErrors errors={this.props.asyncErrorMessages} />
        </div>
    }

    private cancelRegistration() {
        let processId = (this.props.routerExt as any).props.match.params.processId;

        if (ObjectHelper.isStringNullOrEmpty(processId)) {
            this.props.routerExt.goTo('/', null);
        } else {

            this.props.registerAsyncOperation(new UserService().cancelRegistration(processId).then(() => {

                this.notification = <Alert color="success">{this.getResource("GL_CANCELLED_REGISTRATION_I")}</Alert>
            }))
        }
    }
}

export const CancelRegistrationUI = withRouter(withAsyncFrame(CancelRegistrationImpl, false));