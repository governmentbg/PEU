import { AsyncUIProps, BaseProps, RawHTML, withAsyncFrame } from 'cnsys-ui-react';
import { EAUBaseComponent, resourceManager } from 'eau-core';
import React from 'react';
import { UserInputModel } from '../../models/ModelsManualAdded';

interface RegistrationTypeProps extends BaseProps, AsyncUIProps {
}

class RegistrationTypeImpl extends EAUBaseComponent<RegistrationTypeProps, UserInputModel>{

    private registrationByCertFormName: string = "registrationByCert";
    private registrationByEAuthFormName: string = "registrationByEAuth";

    constructor() {
        super();

        this.registrationByCert = this.registrationByCert.bind(this);
        this.registrationByEAuth = this.registrationByEAuth.bind(this);
    }

    render() {
        return <div className="page-wrapper" id="ARTICLE-CONTENT">

            <div className="ui-form ui-form--input">
                <div className="row">
                    <div className="col-md order-2 order-md-0">

                        <section className="card card--box login-type">
                            <a className="remove-underline" href="#" onClick={(e) => this.registrationByCert(e)}>
                                <div className="card-header card-header--rounded">
                                    <div className="form-row">
                                        <div className="col align-self-center">
                                            <h2 className="card-header__title">
                                                {resourceManager.getResourceByKey("GL_REGISTRATION_CERT_L")}
                                            </h2>
                                        </div>
                                    </div>
                                </div>
                            </a>
                            <form name={this.registrationByCertFormName} action="api/Users/registrationByCertBegin" method="post"></form>
                        </section>

                        <section className="card card--box login-type">
                            <a className="remove-underline" href="#" onClick={(e) => this.registrationByEAuth(e)}>
                                <div className="card-header card-header--rounded">
                                    <div className="form-row">
                                        <div className="col align-self-center">
                                            <h2 className="card-header__title">
                                                {resourceManager.getResourceByKey("GL_REGISTRATION_EAUTHENTICATION_L")}
                                            </h2>

                                        </div>
                                    </div>
                                </div>
                            </a>
                            <form name={this.registrationByEAuthFormName} action="api/Users/registrationByEAuthBegin" method="post"></form>
                        </section>

                    </div>

                    <div className="col order-1 d-none d-md-block page-vertical-divider"></div>
                    <div className="col-md order-0 order-md-2">
                        <div className="alert alert-info mt-0" role="alert"><RawHTML rawHtmlText={this.getResource("GL_DOC_PORTAL_REGISTRATION_INFORMATION_I")} /></div>
                    </div>
                </div>
            </div>

        </div>
    }

    private registrationByCert(e: any) {
        e.preventDefault();
        document.forms[this.registrationByCertFormName].submit();
    }

    private registrationByEAuth(e: any) {
        e.preventDefault();
        document.forms[this.registrationByEAuthFormName].submit();
    }
}

export const RegistrationTypeUI = withAsyncFrame(RegistrationTypeImpl, false)