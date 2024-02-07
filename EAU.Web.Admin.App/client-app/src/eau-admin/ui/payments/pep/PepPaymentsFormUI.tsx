﻿import { ObjectHelper } from "cnsys-core";
import { AsyncUIProps, BaseRouteParams, BaseRouteProps, BaseRoutePropsExt, withAsyncFrame, withRouter } from 'cnsys-ui-react';
import { attributeClassRequiredLabel, EAUBaseComponent, ValidationSummaryErrors } from 'eau-core';
import { action, observable, runInAction } from 'mobx';
import { observer } from 'mobx-react';
import React from 'react';
import { Constants } from '../../../Constants';
import { RegistrationData } from "../../../models/ModelsAutoGenerated";
import { RegistrationsDataService } from '../../../services/RegistrationsDataService';
import { PepPaymentsValidator } from '../../../validations/PepPaymentsValidator';
import BtnGroupFormUI from '../../common/BtnGroupFormUI';

interface PayemtnsFormtRouteProps extends BaseRouteParams {
    pepID?: number
}

interface PaymentFormProps extends BaseRouteProps<PayemtnsFormtRouteProps>, AsyncUIProps, BaseRoutePropsExt {
}

@observer class PepPaymentsForm extends EAUBaseComponent<PaymentFormProps, RegistrationData> {

    private pepID: any;

    @observable isLoaded: boolean = true;
    @observable isFormSubmited: boolean;
    @observable notification: any;

    private registrationsDataService: RegistrationsDataService;

    constructor(props?: PaymentFormProps) {
        super(props);

        this.registrationsDataService = new RegistrationsDataService();

        this.init();
    }

    render() {

        let dataResult: any = null;

        if (this.isLoaded) {

            if (ObjectHelper.isNullOrUndefined(this.props.match.params.pepID)
                ||
                (this.props.match.params.pepID && (ObjectHelper.isNumber(this.props.match.params.pepID)))
            ) {

                dataResult = <div className="card">
                    <div className="card-body">
                        {this.notification}
                        <ValidationSummaryErrors {...this.props} />
                        <div className="row">
                            <div className="form-group col-md-12">
                                {this.labelFor(x => x.description, "GL_PROVIDER_NAME_L", attributeClassRequiredLabel)}
                                {this.textBoxFor(x => x.description)}
                            </div>
                        </div>
                        <div className="row">
                            <div className="form-group col-md-12">
                                {this.labelFor(x => x.secretWord, "GL_PAY_SCR_KEY_L", attributeClassRequiredLabel)}
                                {this.textBoxFor(x => x.secretWord)}
                            </div>
                        </div>
                        <div className="row">
                            <div className="form-group col-md-4">
                                {this.labelFor(x => x.cin, "GL_PAY_PEP_KIN_L", attributeClassRequiredLabel)}
                                {this.textBoxFor(x => x.cin)}
                            </div>
                            <div className="form-group col-md-4">
                                {this.labelFor(x => x.notificationUrl, "GL_PAY_URL_MESS_L", attributeClassRequiredLabel)}
                                {this.textBoxFor(x => x.notificationUrl)}
                            </div>
                            <div className="form-group col-md-4">
                                {this.labelFor(x => x.portalUrl, "GL_PAY_URL_PORTAL_L", attributeClassRequiredLabel)}
                                {this.textBoxFor(x => x.portalUrl)}
                            </div>
                        </div>
                        <div className="row">
                            <div className="form-group col-md-4">
                                {this.labelFor(x => x.serviceUrl, "GL_PAY_URL_SERVICES_L", attributeClassRequiredLabel)}
                                {this.textBoxFor(x => x.serviceUrl)}
                            </div>
                            <div className="form-group col-md-4">
                                {this.labelFor(x => x.iban, "GL_BANK_ACCOUNT_L", attributeClassRequiredLabel)}
                                {this.textBoxFor(x => x.iban)}
                            </div>
                            <div className="form-group col-md-4">
                                {this.labelFor(x => x.validityPeriod, "GL_PAY_POV_PAYMENT_L", attributeClassRequiredLabel)}
                                <div className="row">
                                    <div className="col-sm-7 col-md-7 col-lg-5 col-xl-4 form-group">
                                        {this.durationTimeFor(x => x.validityPeriod)}
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <BtnGroupFormUI refuseLink={Constants.PATHS.paymentsPep} onSave={this.onSave} />
                </div>
            }
            else {
                dataResult = <div className="alert alert-dismissible alert-warning fade show">
                    <p>{this.getResource("GL_NO_DATA_FOUND_L")}</p>
                </div>
            }
        }

        return <>
            {dataResult}
        </>
    }

    private onSave() {

        if (this.validators[0].validate(this.model)) {
            if (this.pepID) {

                this.props.registerAsyncOperation(this.registrationsDataService.updateRegistrationData(this.model)
                    .then(() => {
                        this.notification = <div className="alert alert-success">
                            <p>{this.getResource("GL_UPDATE_OK_I")}</p>
                        </div>
                    }).catch((e) => {
                        this.notification = null;
                        throw e;
                    })
                );
            } else {

                this.props.registerAsyncOperation(this.registrationsDataService.addRegistrationData(this.model)
                    .then(() => {
                        runInAction.bind(this)(() => {
                            this.notification = <div className="alert alert-success">
                                <p>{this.getResource("GL_SAVE_OK_I")}</p>
                            </div>
                            this.model = new RegistrationData();
                        })
                    }).catch((e) => {
                        this.notification = null;
                        throw e;
                    })
                )
            }
        }
    }

    @action private init() {

        this.pepID = this.props.match.params.pepID;

        this.model = new RegistrationData();
        this.onSave = this.onSave.bind(this);

        this.model.type = 2;

        if (this.pepID && ObjectHelper.isNumber(this.pepID)) {

            this.isLoaded = false;

            this.props.registerAsyncOperation(
                this.registrationsDataService.getPaymentById(this.pepID)
                    .then((result) => {
                        if (!ObjectHelper.isNullOrUndefined(result))
                            this.model = result
                    })
                    .finally(() => { this.isLoaded = true })
            )
        }

        this.validators = [new PepPaymentsValidator()];
    }
}

export const PepPaymentsFormUI = withRouter(withAsyncFrame(PepPaymentsForm, false)); 