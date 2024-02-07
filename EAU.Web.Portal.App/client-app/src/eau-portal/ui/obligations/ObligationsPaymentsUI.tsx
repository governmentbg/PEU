import { ArrayHelper, ObjectHelper } from 'cnsys-core';
import { AsyncUIProps, BaseProps, withAsyncFrame } from 'cnsys-ui-react';
import { ANDObligationSearchMode, ANDSourceIds, attributesClassFormControlReqired, attributesClassFormControlRequiredLabel, EAUBaseComponent, KATDocumentTypes, ObligationRequest, ObligationSearchCriteria, ObligationSearchModes, ObligationStatuses, ObligationTypes, ObligedPersonIdentTypes, PaymentRequestStatuses, RegistrationDataTypes, StartPaymentRequest, ValidationSummaryErrors } from 'eau-core';
import { action, observable } from 'mobx';
import { observer } from 'mobx-react';
import React from 'react';
import { Modal, ModalBody, ModalFooter, ModalHeader } from 'reactstrap';
import { Constants } from '../../Constants';
import { ANDObligationSearchCriteria, Obligation, ObligationPaymentRequest, PaymentRequest } from '../../models/ModelsManualAdded';
import { ObligationsDataService } from '../../services/ObligationsDataService';
import { ObligationPaymentRequestValidator } from '../../validations/ObligationPaymentRequestValidator';
import { createPaymentRequest, formatAmount, formatDate, genOkCancelUrl, processObligation, redirectToPaymentUrl } from './ObligationsHelpers';

interface ObligationsPaymentsUIProps extends BaseProps, AsyncUIProps {
    searchCriteria: ANDObligationSearchCriteria;
    obligation: Obligation;
    andSourceIds: ANDSourceIds
}

@observer class ObligationsPaymentsImplUI extends EAUBaseComponent<ObligationsPaymentsUIProps, ObligationPaymentRequest>{

    private obligationsDataService: ObligationsDataService;
    private sentRequestPepOfDaeu: PaymentRequest;
    private paidRequestPepOfDaeu: PaymentRequest;
    private expiredRequestPepOfDaeu: PaymentRequest;
    private sentRequestEPay: PaymentRequest;
    private paidRequestEPay: PaymentRequest;
    private expiredRequestEPay: PaymentRequest;

    @observable private isModalOpen: boolean;
    @observable private registrationDataType: RegistrationDataTypes;

    constructor(props: ObligationsPaymentsUIProps) {
        super(props);

        this.validators = [new ObligationPaymentRequestValidator()]
        this.obligationsDataService = new ObligationsDataService();

        if (this.props.obligation.paymentRequests?.length > 0) {
            this.sentRequestPepOfDaeu = ArrayHelper.queryable.from(this.props.obligation.paymentRequests).firstOrDefault(r => r.status == PaymentRequestStatuses.Sent && r.registrationDataType == RegistrationDataTypes.PepOfDaeu);
            this.paidRequestPepOfDaeu = ArrayHelper.queryable.from(this.props.obligation.paymentRequests).firstOrDefault(r => r.status == PaymentRequestStatuses.Paid && r.registrationDataType == RegistrationDataTypes.PepOfDaeu);
            this.expiredRequestPepOfDaeu = ArrayHelper.queryable.from(this.props.obligation.paymentRequests).firstOrDefault(r => (r.status == PaymentRequestStatuses.Expired && r.registrationDataType == RegistrationDataTypes.PepOfDaeu)
                || (r.status == PaymentRequestStatuses.Cancelled && r.registrationDataType == RegistrationDataTypes.PepOfDaeu));

            this.sentRequestEPay = ArrayHelper.queryable.from(this.props.obligation.paymentRequests).firstOrDefault(r => r.status == PaymentRequestStatuses.Sent && r.registrationDataType == RegistrationDataTypes.ePay);
            this.paidRequestEPay = ArrayHelper.queryable.from(this.props.obligation.paymentRequests).firstOrDefault(r => r.status == PaymentRequestStatuses.Paid && r.registrationDataType == RegistrationDataTypes.ePay);
            this.expiredRequestEPay = ArrayHelper.queryable.from(this.props.obligation.paymentRequests).firstOrDefault(r => (r.status == PaymentRequestStatuses.Expired && r.registrationDataType == RegistrationDataTypes.ePay)
                || (r.status == PaymentRequestStatuses.Cancelled && r.registrationDataType == RegistrationDataTypes.ePay));
        }

        this.onObligedIdentifierTypeChange = this.onObligedIdentifierTypeChange.bind(this);
        this.onPayerIdentifierTypeChange = this.onPayerIdentifierTypeChange.bind(this);
        this.onPayByEPAY = this.onPayByEPAY.bind(this);
        this.onPayByCard = this.onPayByCard.bind(this);
        this.onCloseModal = this.onCloseModal.bind(this);
        this.onPay = this.onPay.bind(this);
    }

    private get isObligedPersonEntity() {
        return this.props.obligation.additionalData.obligedPersonIdentType === 'company';
    }

    @action private initModel() {
        this.model = new ObligationPaymentRequest();

        if (this.props.searchCriteria.mode == ANDObligationSearchMode.Document) {
            if (this.isObligedPersonEntity) {
                this.model.obligedPersonIdentType = ObligedPersonIdentTypes.BULSTAT;
                this.model.obligedPersonIdent = this.props.obligation.additionalData.obligedPersonIdent;

            } else {
                this.model.obligedPersonIdentType = ObligedPersonIdentTypes.EGN;
            }
        }

        this.model.payerIdentType = ObligedPersonIdentTypes.EGN;
        this.model.clearErrors(true);
        this.props.clearErrors();
    }

    @action private onPayByCard() {
        this.initModel();
        this.registrationDataType = RegistrationDataTypes.PepOfDaeu;

        if (this.sentRequestPepOfDaeu) {
            this.onPay();
        } else {
            this.isModalOpen = true;
        }
    }

    @action private onPayByEPAY() {
        this.initModel();
        this.registrationDataType = RegistrationDataTypes.ePay;

        if (this.sentRequestEPay) {
            this.onPay();
        } else {
            this.isModalOpen = true;
        }
    }

    @action private onCloseModal() {
        this.isModalOpen = false;
        this.registrationDataType = null;
    }

    @action private onObligedIdentifierTypeChange(e: any) {
        this.model.obligedPersonIdentType = e.target.value;
        this.model.obligedPersonIdent = null;
    }

    @action private onPayerIdentifierTypeChange(e: any) {
        this.model.payerIdentType = e.target.value;
        this.model.payerIdent = null;
    }

    private get modalHeader() {

        if (this.registrationDataType == RegistrationDataTypes.PepOfDaeu) {
            return this.getResource('GL_PAYMENT_PEPDAEU_L');

        } else if (this.registrationDataType == RegistrationDataTypes.ePay) {
            return this.getResource('GL_PAYMENT_EPAY_L')
        }

        return '';
    }

    @action private onPay = () => {

        if (this.sentRequestEPay || this.sentRequestPepOfDaeu) {
            this.model.obligedPersonName = this.sentRequestEPay ? this.sentRequestEPay.obligedPersonName : this.sentRequestPepOfDaeu.obligedPersonName;

            if (this.props.searchCriteria.mode == ANDObligationSearchMode.Document) {
                this.model.obligedPersonIdent = this.sentRequestEPay ? this.sentRequestEPay.obligedPersonIdent : this.sentRequestPepOfDaeu.obligedPersonIdent
                this.model.obligedPersonIdentType = this.sentRequestEPay ? this.sentRequestEPay.obligedPersonIdentType : this.sentRequestPepOfDaeu.obligedPersonIdentType
            }

            this.model.payerIdent = this.sentRequestEPay ? this.sentRequestEPay.payerIdent : this.sentRequestPepOfDaeu.payerIdent;
            this.model.payerIdentType = this.sentRequestEPay ? this.sentRequestEPay.payerIdentType : this.sentRequestPepOfDaeu.payerIdentType;
        }

        if (this.validators[0].validate(this.model)) {
            var request = new ObligationRequest();
            request.type = ObligationTypes.AND;
            request.obligationIdentifier = this.props.obligation.obligationIdentifier;
            request.obligationDate = this.props.obligation.obligationDate;
            request.obligedPersonName = this.model.obligedPersonName;
            request.obligedPersonIdent = this.model.obligedPersonIdent;
            request.obligedPersonIdentType = this.model.obligedPersonIdentType;

            request.obligationSearchCriteria = new ObligationSearchCriteria();
            request.obligationSearchCriteria.mode = ObligationSearchModes.AND;
            request.obligationSearchCriteria.type = ObligationTypes.AND;
            request.obligationSearchCriteria.obligedPersonIdent = this.props.searchCriteria.obligedPersonIdent;
            request.obligationSearchCriteria.drivingLicenceNumber = this.props.searchCriteria.drivingLicenceNumber;
            request.obligationSearchCriteria.personalDocumentNumber = this.props.searchCriteria.personalDocumentNumber;
            request.obligationSearchCriteria.uic = this.props.searchCriteria.uic;
            request.obligationSearchCriteria.foreignVehicleNumber = this.props.searchCriteria.foreignVehicleNumber;
            request.obligationSearchCriteria.andSourceId = this.props.andSourceIds;
            request.obligationSearchCriteria.documentType = this.props.searchCriteria.documentType;
            request.obligationSearchCriteria.documentSeries = this.props.searchCriteria.documentSeries;
            request.obligationSearchCriteria.documentNumber = this.props.searchCriteria.documentNumber;
            request.obligationSearchCriteria.initialAmount = this.props.searchCriteria.amount;

            if (this.registrationDataType == RegistrationDataTypes.ePay || this.props.obligation.obligationID == null) {
                this.props.registerAsyncOperation(this.createObligationAndPaymentRequest(this.props.obligation, request));

            } else if (this.registrationDataType == RegistrationDataTypes.PepOfDaeu && this.props.obligation.obligationID) {

                if (this.props.obligation.paymentRequests?.length > 0) {

                    if (ObjectHelper.isNullOrUndefined(this.sentRequestPepOfDaeu) && ObjectHelper.isNullOrUndefined(this.paidRequestPepOfDaeu) && ObjectHelper.isNullOrUndefined(this.expiredRequestPepOfDaeu)) {
                        this.props.registerAsyncOperation(this.createObligationAndPaymentRequest(this.props.obligation, request));

                    } else {
                        if (this.sentRequestPepOfDaeu) {
                            if (ObjectHelper.isStringNullOrEmpty(this.sentRequestPepOfDaeu.additionalData.pepAccessCode)) {
                                this.obligationsDataService.getPEPAccessCode(this.sentRequestPepOfDaeu.paymentRequestID, this.props.obligation.iban).then(res => {
                                    this.sentRequestPepOfDaeu.additionalData.pepAccessCode = res.code;

                                    redirectToPaymentUrl(this.sentRequestPepOfDaeu);
                                });
                            } else {
                                redirectToPaymentUrl(this.sentRequestPepOfDaeu);
                            }
                        }
                    }

                } else {
                    this.props.registerAsyncOperation(this.createObligationAndPaymentRequest(this.props.obligation, request));
                }
            }
        }
    }

    private createObligationAndPaymentRequest(obligation: Obligation, obligationRequest: ObligationRequest): Promise<void> {

        return processObligation(obligation, obligationRequest).then(obligationResult => {
            let okCancelUrl = genOkCancelUrl(this.props.searchCriteria,
                this.props.searchCriteria.mode == ANDObligationSearchMode.ObligedPerson
                    ? Constants.PATHS.Obligations
                    : Constants.PATHS.KATObligations);
            let withDiscount = obligation.discountAmount != obligation.amount;
            const paymentRequest = createPaymentRequest(obligation, this.registrationDataType, okCancelUrl, withDiscount, this.model);

            return this.startPayment(paymentRequest, obligationResult);
        })
    }

    @action private startPayment(paymentRequest: StartPaymentRequest, obligation: Obligation) {

        return this.obligationsDataService.startPayment(paymentRequest, obligation.obligationID).then(result => {
            let sendRequest = result;

            if (paymentRequest.registrationDataType == RegistrationDataTypes.PepOfDaeu) {

                if (ObjectHelper.isStringNullOrEmpty(sendRequest.additionalData.pepAccessCode)) {
                    this.obligationsDataService.getPEPAccessCode(sendRequest.paymentRequestID, obligation.iban).then(res => {
                        sendRequest.additionalData.pepAccessCode = res.code;

                        redirectToPaymentUrl(result);
                        if (result.additionalData.okCancelUrl) {
                            setTimeout(() => window.location.replace(result.additionalData.okCancelUrl), 2000)
                        }
                    });
                } else {
                    redirectToPaymentUrl(result);
                    if (result.additionalData.okCancelUrl) {
                        setTimeout(() => window.location.replace(result.additionalData.okCancelUrl), 2000)
                    }
                }

            } else if (paymentRequest.registrationDataType == RegistrationDataTypes.ePay) {
                redirectToPaymentUrl(result);
            }
        });
    }

    private get showModalForExistingPayment() {

        return <div className="alert alert-warning">
            <p>{this.getResource('GL_EPAY_STATUS_SENT_CONFIRM_I')
                .replace('{TYPE}', this.getResource(`GL_${this.props.obligation.additionalData.documentType}_L`).toLocaleLowerCase())
                .replace('{NUMBER}', this.props.obligation.additionalData.documentNumber)
                .replace('{SENDDATE}', formatDate(this.sentRequestEPay ? this.sentRequestEPay.sendDate : this.sentRequestPepOfDaeu.sendDate))
                .replace('{PAYMENT_PORTAL}', this.getResource(this.sentRequestEPay ? "GL_PAYMENT_METHOD_EPAY_L" : 'GL_PAYMENT_METHOD_PEPDAEU_L'))
                .replace('{AMOUNT}', formatAmount(this.sentRequestEPay ? this.sentRequestEPay.amount : this.sentRequestPepOfDaeu.amount))}</p>
        </div>
    }

    render() {

        if (this.props.obligation.status != ObligationStatuses.Paid && this.props.obligation.status != ObligationStatuses.Processed) {

            return <>
                {this.paidRequestPepOfDaeu ? null : <p><button className="btn btn-link text-nowrap" onClick={this.onPayByCard}>{this.getResource('GL_PAY_BY_CARD_L')}</button></p>}
                {this.paidRequestEPay ? null : <p><button className="btn btn-link text-nowrap" onClick={this.onPayByEPAY}>{this.getResource('GL_PAY_BY_EPAY_L')}</button></p>}
                {
                    this.model
                        ? <Modal isOpen={this.isModalOpen} toggle={() => this.isModalOpen = !this.isModalOpen} size="md">
                            <ModalHeader>{this.modalHeader}</ModalHeader>
                            <ModalBody>
                                <ValidationSummaryErrors asyncErrors={this.props.asyncErrors} />
                                {
                                    this.sentRequestPepOfDaeu || this.sentRequestEPay
                                        ? this.showModalForExistingPayment
                                        : <div className="ui-form ui-form--input">
                                            <fieldset className="fields-group">
                                                <legend>
                                                    <h4 className="field-title">{this.getResource('GL_Debtor_L')}</h4>
                                                </legend>
                                                <div className="row">
                                                    <div className="form-group col-md-6">
                                                        {this.labelFor(x => x.obligedPersonName, 'GL_NAME_L', attributesClassFormControlRequiredLabel)}
                                                        {this.textBoxFor(x => x.obligedPersonName, attributesClassFormControlReqired)}
                                                    </div>
                                                </div>
                                                {
                                                    this.props.searchCriteria.mode == ANDObligationSearchMode.Document
                                                        ? <>
                                                            <div className="row">
                                                                <div className="col">
                                                                    {this.labelFor(x => x.obligedPersonIdent, 'GL_IDENTIFIER_L', attributesClassFormControlRequiredLabel)}
                                                                </div>
                                                            </div>
                                                            <div className="row">
                                                                <div className="form-group col-sm-6">
                                                                    {
                                                                        this.isObligedPersonEntity
                                                                            ? <input type="text" name="obligedPersonIdent" id="obligedPersonIdent" className="form-control" disabled aria-describedby="obligedPersonIdent_HELP" value={this.model.obligedPersonIdent} />
                                                                            : this.textBoxFor(x => x.obligedPersonIdent, attributesClassFormControlReqired)
                                                                    }
                                                                </div>
                                                                <div className="form-group col-auto">
                                                                    <div className="form-inline">
                                                                        <div className="custom-control-inline custom-control custom-radio">
                                                                            <input className="custom-control-input" name="obligedIdentType" id="obligedIdentType_1" type="radio"
                                                                                onChange={this.onObligedIdentifierTypeChange}
                                                                                value={ObligedPersonIdentTypes.EGN}
                                                                                checked={this.model.obligedPersonIdentType == ObligedPersonIdentTypes.EGN || this.model.obligedPersonIdentType == ObligedPersonIdentTypes.LNC}
                                                                                disabled={this.isObligedPersonEntity} />
                                                                            <label className="custom-control-label" htmlFor="obligedIdentType_1">{this.getResource('GL_PERSON_ID_L')}</label>
                                                                        </div>
                                                                        <div className="custom-control-inline custom-control custom-radio">
                                                                            <input className="custom-control-input" name="obligedIdentType" id="obligedIdentType_2" type="radio"
                                                                                onChange={this.onObligedIdentifierTypeChange}
                                                                                value={ObligedPersonIdentTypes.BULSTAT}
                                                                                checked={this.model.obligedPersonIdentType == ObligedPersonIdentTypes.BULSTAT}
                                                                                disabled={this.isObligedPersonEntity} />
                                                                            <label className="custom-control-label" htmlFor="obligedIdentType_2">{this.getResource('GL_UIC_BULSTAT_L')}</label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </>
                                                        : null
                                                }
                                            </fieldset>
                                            <fieldset className="fields-group">
                                                <legend>
                                                    <h4 className="field-title">{this.getResource('GL_PAYING_OBLIGATION_L')}</h4>
                                                </legend>
                                                <div className="row">
                                                    <div className="col">
                                                        {this.labelFor(x => x.payerIdent, 'GL_IDENTIFIER_L', attributesClassFormControlRequiredLabel)}
                                                    </div>
                                                </div>
                                                <div className="row">
                                                    <div className="form-group col-sm-6">
                                                        {this.textBoxFor(x => x.payerIdent, attributesClassFormControlReqired)}
                                                    </div>
                                                    <div className="form-group col-auto">
                                                        <div className="form-inline">
                                                            <div className="custom-control-inline custom-control custom-radio">
                                                                <input className="custom-control-input" name="payerIdentType" id="payerIdentType_1" type="radio"
                                                                    onChange={this.onPayerIdentifierTypeChange}
                                                                    value={ObligedPersonIdentTypes.EGN}
                                                                    checked={this.model.payerIdentType == ObligedPersonIdentTypes.EGN || this.model.payerIdentType == ObligedPersonIdentTypes.LNC} />
                                                                <label className="custom-control-label" htmlFor="payerIdentType_1">{this.getResource('GL_PERSON_ID_L')}</label>
                                                            </div>
                                                            <div className="custom-control-inline custom-control custom-radio">
                                                                <input className="custom-control-input" name="payerIdentType" id="payerIdentType_2" type="radio"
                                                                    onChange={this.onPayerIdentifierTypeChange}
                                                                    value={ObligedPersonIdentTypes.BULSTAT}
                                                                    checked={this.model.payerIdentType == ObligedPersonIdentTypes.BULSTAT} />
                                                                <label className="custom-control-label" htmlFor="payerIdentType_2">{this.getResource('GL_UIC_BULSTAT_L')}</label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </fieldset>
                                        </div>
                                }
                            </ModalBody>
                            <ModalFooter>
                                <div className="button-bar button-bar--responsive">
                                    <div className="right-side">
                                        <button type="button" className="btn btn-primary" data-dismiss="modal" onClick={this.onPay}>{this.getResource(this.sentRequestPepOfDaeu || this.sentRequestEPay ? 'GL_EPAY_NEW_REQUEST_L' : 'GL_PAY_L')}</button>
                                    </div>
                                    <div className="left-side">
                                        <button type="button" className="btn btn-secondary" data-dismiss="modal" onClick={this.onCloseModal}>{this.getResource('GL_REFUSE_L')}</button>
                                    </div>
                                </div>
                            </ModalFooter>
                        </Modal>
                        : null
                }
            </>
        }

        return null;
    }
}

export const ObligationsPaymentsUI = withAsyncFrame(ObligationsPaymentsImplUI, false);