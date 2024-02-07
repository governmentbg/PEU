import { UrlHelper } from 'cnsys-core';
import { ObligationRequest, ObligationStatuses, ObligationTypes, RegistrationDataTypes, StartPaymentRequest } from 'eau-core';
import { Obligation, PaymentRequest } from '../models/ModelsManualAdded';
import { ObligationsDataService } from './ObligationsDataService';

export namespace PaymentHelper {
    
    /**
     * Прави заявка към платежна система 
     * 
     * @param paymentRequest 
     */
    export function processPayment(paymentRequest: PaymentRequest) {

        switch (Number(paymentRequest.registrationDataType)) {

            case RegistrationDataTypes.ePay:

                let ePayUrl = paymentRequest.additionalData.portalUrl;

                UrlHelper.postDataToUrl(ePayUrl, {
                    "PAGE": "paylogin", 
                    "ENCODED":paymentRequest.additionalData.data, 
                    "CHECKSUM": paymentRequest.additionalData.hmac,
                    "URL_OK": paymentRequest.additionalData.okCancelUrl,
                    "URL_CANCEL": paymentRequest.additionalData.okCancelUrl
                });

            break;

            case RegistrationDataTypes.PepOfDaeu:

                let pepAccessCodeUrl = UrlHelper.urlSanitizeSlashes(`${paymentRequest.additionalData.portalUrl}/Home/AccessByPaymentRequestCode`);
                UrlHelper.postDataToUrl(pepAccessCodeUrl, { "code": paymentRequest.additionalData.pepAccessCode }, true);
            break; 
        }
    }

    /**
     * Създава задължение, ако такова не съществува в нашата система, или връща вече създадено такова.
     * 
     * @param obligation 
     * @param obligationType 
     */
    export function processObligation(obligation: Obligation, obligationType): Promise<Obligation> {

        switch (Number(obligation.status)) {

            // Създаваме задължение при нас, ако такова не съществува
            case ObligationStatuses.Pending:
                let createObligatiobRequest = new ObligationRequest();
                
                createObligatiobRequest.type = obligationType;
                createObligatiobRequest.obligationDate = obligation.obligationDate;
                createObligatiobRequest.obligationIdentifier = obligation.obligationIdentifier;
                createObligatiobRequest.obligedPersonName = obligation.obligedPersonName;
                createObligatiobRequest.obligedPersonIdent = obligation.obligedPersonIdent;
                createObligatiobRequest.obligedPersonIdentType = obligation.obligedPersonIdentType;
                
                let obligationDM: ObligationsDataService;
                obligationDM = new ObligationsDataService();

                return obligationDM.createObligation(createObligatiobRequest)
                    .then(obligation => {
                        return obligation;
                });

            // Инициира плащане
            default:
                return Promise.resolve(obligation);
        }
    }

    /**
     * Създава заявка за плащане
     * 
     * @param obligation 
     * @param registrationDataType 
     * @param okCancelUrl 
     * @param withDiscount 
     */
    export function createPaymentRequest(obligation: Obligation, registrationDataType: number, okCancelUrl: string, withDiscount?: boolean): Promise<StartPaymentRequest>  {

        let startPaymentRequest = new StartPaymentRequest();

        startPaymentRequest.payerIdent = obligation.obligedPersonIdent;
        startPaymentRequest.payerIdentType = obligation.obligedPersonIdentType;
        startPaymentRequest.obligedPersonName = obligation.obligedPersonName;
        startPaymentRequest.registrationDataType = registrationDataType;
        startPaymentRequest.okCancelUrl = okCancelUrl;
        
        switch (Number(obligation.type)) {

            case ObligationTypes.AND:

                if (withDiscount)
                    startPaymentRequest.amount = obligation.discountAmount;
                else
                    startPaymentRequest.amount = obligation.amount;
            break;

            default:
                startPaymentRequest.amount = obligation.discountAmount;
            break;
        }
        
        return Promise.resolve(startPaymentRequest);
    }
}