import { ObjectHelper, UrlHelper } from "cnsys-core";
import { ANDObligationSearchMode, Constants, KATDocumentTypes, ObligationRequest, ObligationStatuses, PaymentRequestStatuses, RegistrationDataTypes, resourceManager, StartPaymentRequest } from "eau-core";
import moment from "moment";
import React from "react";
import { ANDObligationSearchCriteria, Obligation, ObligationPaymentRequest, PaymentRequest } from '../../models/ModelsManualAdded';
import { ObligationsDataService } from "../../services/ObligationsDataService";

export const getPaymentDescription = (obligationStatus: ObligationStatuses, paymentRequests: PaymentRequest[], documentNumber: string, documentType: string, isServed: boolean, colspan: number) => {

    if (paymentRequests?.length > 0) {

        return paymentRequests.map(req => {

            var htmlContent: string;

            if (req.status == PaymentRequestStatuses.New || req.status == PaymentRequestStatuses.Sent) {

                htmlContent = resourceManager.getResourceByKey("GL_PEPDEAU_STATUS_WAIT_I")
                    .replace('{TYPE}', resourceManager.getResourceByKey(`GL_${documentType}_L`).toLocaleLowerCase())
                    .replace('{NUMBER}', documentNumber)
                    .replace('{SENDDATE}', formatDate(req.sendDate))
                    .replace('{PAYMENT_PORTAL}', req.registrationDataType == RegistrationDataTypes.ePay ? resourceManager.getResourceByKey("GL_PAYMENT_METHOD_EPAY_L") : resourceManager.getResourceByKey("GL_PAYMENT_METHOD_PEPDAEU_L"))
                    .replace('{AMOUNT}', formatAmount(req.amount))

            } else if (req.status == PaymentRequestStatuses.Paid) {
                htmlContent = resourceManager.getResourceByKey("GL_ANDOBLIGATION_PAYMENT_STATUS_PAID_I")
                    .replace('{TYPE}', resourceManager.getResourceByKey(`GL_${documentType}_L`).toLocaleLowerCase())
                    .replace('{NUMBER}', documentNumber)
                    .replace('{AMOUNT}', formatAmount(req.amount))
                    .replace('{SENDDATE}', formatDate(req.sendDate))
                    .replace('{PAYMENT_PORTAL}', req.registrationDataType == RegistrationDataTypes.ePay ? resourceManager.getResourceByKey("GL_PAYMENT_METHOD_EPAY_L") : resourceManager.getResourceByKey("GL_PAYMENT_METHOD_PEPDAEU_L"))

            } else if (req.status == PaymentRequestStatuses.Duplicate) {
                htmlContent = resourceManager.getResourceByKey("GL_PAY_STATUS_INVALID_PAYMENT_I")
                    .replace('{AMOUNT}', formatAmount(req.amount))
                    .replace('{SENDDATE}', formatDate(req.sendDate))
                    .replace('{PAYMENT_PORTAL}', req.registrationDataType == RegistrationDataTypes.ePay ? resourceManager.getResourceByKey("GL_PAYMENT_METHOD_EPAY_L") : resourceManager.getResourceByKey("GL_PAYMENT_METHOD_PEPDAEU_L"))
            }

            if (htmlContent) {
                return <tr key={`${req.obligationID}`} className={isServed ? "row-description table-warning" : "row-description"}>
                    <td colSpan={colspan}>
                        <span className="text-info">
                            <em
                                dangerouslySetInnerHTML={{ __html: htmlContent }}>
                            </em>
                        </span>
                    </td>
                </tr>
            }
        })
    }

    return null;
}

export const formatDate = (date: any, format?: string) => {
    var dateAsMoment = moment(date);

    if (dateAsMoment.isValid()) {
        return format ? dateAsMoment.format() : dateAsMoment.format(Constants.DATE_FORMATS.date)
    }

    return '';
}

export const formatAmount = (amount: any, currency?: string) => {
    var amountAsNumber = Number(amount);

    if (!isNaN(amountAsNumber)) {
        return currency ? `${amountAsNumber.toFixed(2)} ${currency}` : `${amountAsNumber.toFixed(2)} ${resourceManager.getResourceByKey('GL_BGN_ABBRAVETION_L')}`
    }

    return '';
}

export const formatPercent = (numberAsString: any) => {
    var number = Number(numberAsString);

    if (!isNaN(number)) {
        return `${number.toFixed(2)} %`;
    }

    return '';
}

export const genOkCancelUrl = (searchCriteria: ANDObligationSearchCriteria, basePath: string) => {
    //При сетнат флаг isCallbackUrl, системата разбира, че url-a се използва за връщане на екрана с резултати.
    //В противен случай, ще върне на екрана с търсачката.
    searchCriteria.isCallbackUrl = true;
    let query = searchCriteria.toJSON();

    var queryStringArr = Object.keys(query).map((key) => {

        if (!ObjectHelper.isNullOrUndefined(query[key])) {
            return key + '=' + query[key];
        }

        return null;
    });

    queryStringArr = queryStringArr.filter(e => !ObjectHelper.isStringNullOrEmpty(e));

    return `${basePath}?${queryStringArr.join("&")}`;
}

export const processObligation = (obligation: Obligation, obligationRequest: ObligationRequest): Promise<Obligation> => {

    if (obligation.status == ObligationStatuses.Pending) {
        return new ObligationsDataService().createObligation(obligationRequest);
    } else {
        return Promise.resolve(obligation);
    }
}

export const createPaymentRequest = (obligation: Obligation, registrationDataType: RegistrationDataTypes, okCancelUrl: string, withDiscount: boolean, obligationRequest: ObligationPaymentRequest): StartPaymentRequest => {

    let startPaymentRequest = new StartPaymentRequest();

    startPaymentRequest.payerIdent = obligationRequest.payerIdent;
    startPaymentRequest.payerIdentType = obligationRequest.payerIdentType;
    startPaymentRequest.obligedPersonName = obligationRequest.obligedPersonName;

    startPaymentRequest.registrationDataType = registrationDataType;
    startPaymentRequest.okCancelUrl = okCancelUrl;

    if (withDiscount)
        startPaymentRequest.amount = obligation.discountAmount;
    else
        startPaymentRequest.amount = obligation.amount;

    return startPaymentRequest
}

export const redirectToPaymentUrl = (paymentRequest: PaymentRequest) => {

    if (paymentRequest.registrationDataType == RegistrationDataTypes.ePay) {
        let ePayUrl = paymentRequest.additionalData.portalUrl;

        UrlHelper.postDataToUrl(ePayUrl, {
            "PAGE": "paylogin",
            "ENCODED": paymentRequest.additionalData.data,
            "CHECKSUM": paymentRequest.additionalData.hmac,
            "URL_OK": paymentRequest.additionalData.okCancelUrl,
            "URL_CANCEL": paymentRequest.additionalData.okCancelUrl
        });

    } else if (paymentRequest.registrationDataType == RegistrationDataTypes.PepOfDaeu) {

        let pepAccessCodeUrl = UrlHelper.urlSanitizeSlashes(`${paymentRequest.additionalData.portalUrl}/Home/AccessByPaymentRequestCode`);
        UrlHelper.postDataToUrl(pepAccessCodeUrl, { "code": paymentRequest.additionalData.pepAccessCode }, true);
    }
}

export const showUsedSearchCriteria = (criteria: ANDObligationSearchCriteria) => {
    const criteriaResults = [];

    if (!ObjectHelper.isStringNullOrEmpty(criteria.uic)) {
        //ЕИК/БУЛСТАТ
        criteriaResults.push(`${resourceManager.getResourceByKey('GL_UIC_BULSTAT_L')} ${criteria.uic}`)
    }

    if (!ObjectHelper.isStringNullOrEmpty(criteria.obligedPersonIdent)) {
        //ЕГН/ЛНЧ/ЛН
        const replacement = 'XXXX';
        const obligedPersonIdentWithHiddenDigits = criteria.obligedPersonIdent.substring(0, criteria.obligedPersonIdent.length - 4) + replacement;
        criteriaResults.push(`${resourceManager.getResourceByKey('GL_PERSON_ID_L')} ${obligedPersonIdentWithHiddenDigits}`)
    }

    if (!ObjectHelper.isStringNullOrEmpty(criteria.drivingLicenceNumber)) {
        //Номер на СУМПС
        criteriaResults.push(`${resourceManager.getResourceByKey('GL_DRIVING_LICENCE_NUMBER_L')} ${criteria.drivingLicenceNumber}`)
    }

    if (!ObjectHelper.isStringNullOrEmpty(criteria.personalDocumentNumber)) {
        //Номер на български личен картов документ
        criteriaResults.push(`${resourceManager.getResourceByKey('GL_NUMBER_OF_BULGARIAN_ID_CARD_L')} ${criteria.personalDocumentNumber}`)
    }

    if (!ObjectHelper.isStringNullOrEmpty(criteria.foreignVehicleNumber)) {
        //Чуждестранен регистрационен номер на МПС
        criteriaResults.push(`${resourceManager.getResourceByKey('GL_ForeignVehicleNumber_L')} ${criteria.foreignVehicleNumber}`)
    }

    if (!ObjectHelper.isStringNullOrEmpty(criteria.documentType)) {
        //Тип документ
        if (criteria.documentType == KATDocumentTypes.TICKET) {
            criteriaResults.push(resourceManager.getResourceByKey('GL_TICKET_L').toLocaleLowerCase())
        } else if (criteria.documentType == KATDocumentTypes.PENAL_DECREE) {
            criteriaResults.push(resourceManager.getResourceByKey('GL_PENAL_DECREE_L').toLocaleLowerCase())
        } else if (criteria.documentType == KATDocumentTypes.AGREEMENT) {
            criteriaResults.push(resourceManager.getResourceByKey('GL_AGREEMENT_L').toLocaleLowerCase())
        }
    }

    if (!ObjectHelper.isStringNullOrEmpty(criteria.documentSeries)) {
        //Серия
        criteriaResults.push(`${resourceManager.getResourceByKey('GL_DOCUMENT_SERIES_L').toLocaleLowerCase()}: ${criteria.documentSeries}`)
    }

    if (!ObjectHelper.isStringNullOrEmpty(criteria.documentNumber)) {
        //Документ №
        criteriaResults.push(`${resourceManager.getResourceByKey('GL_DOCUMENT_NUMBER_L').toLocaleLowerCase()}: ${criteria.documentNumber}`)
    }

    if (!ObjectHelper.isStringNullOrEmpty(criteria.amount)) {
        //Дължима сума (BGN)
        criteriaResults.push(`${resourceManager.getResourceByKey('GL_AMOUNT_BGN_L').toLocaleLowerCase().replace('bgn', 'BGN')}: ${criteria.amount}`)
    }

    if (criteriaResults.length > 0) {
        return `${resourceManager.getResourceByKey('GL_FOR_L').toLocaleLowerCase()} ${criteriaResults.join(', ')}`
    }
}

export const isMainDocument = (criteria: ANDObligationSearchCriteria, obligationAdditionalData: any) => {

    if (criteria.mode == ANDObligationSearchMode.Document) {

        if (criteria.documentNumber != obligationAdditionalData.documentNumber
            || criteria.amount != obligationAdditionalData.amount
            || (criteria.documentType == KATDocumentTypes.TICKET && criteria.documentSeries != obligationAdditionalData.documentSeries)) {

            return false;
        }

        return true;
    }

    return false;
}