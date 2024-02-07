import { EAUBaseDataService, ObligationRequest, ObligationSearchCriteria, PaymentRequestSearchCriteria, StartPaymentRequest } from 'eau-core';
import { AccessCodeUIResponse, ANDObligationSearchCriteria, ANDObligationSearchResponse, Obligation, PaymentRequest } from '../models/ModelsManualAdded';

export class ObligationsDataService extends EAUBaseDataService {

    protected baseUrl(): string {
        return super.baseUrl() + "Obligations";
    }

    public searchObligations(criteria: ObligationSearchCriteria): Promise<Obligation[]> {

        return this.get<Obligation[]>("", Obligation, criteria, null).then(function (result: Obligation[]) {
            criteria.count = this.jqXHR.getResponseHeader('count') ? this.jqXHR.getResponseHeader('count') : 0;
            return result;
        });
    }

    public searchObligationsAND(criteria: ANDObligationSearchCriteria): Promise<ANDObligationSearchResponse> {
        return this.get<ANDObligationSearchResponse>("/AND", ANDObligationSearchResponse, criteria);
    }

    /**
     * Добавя задължение
     * @param obligationRequest
     */
    public createObligation(obligationRequest: ObligationRequest): Promise<Obligation> {
        return this.post<Obligation>("", Obligation, obligationRequest)
            .then((result) => {
                return result;
            });
    }

    /**
     * Стартира заявка за плащане
     * @param serviceGroup
     */
    public startPayment(startPaymentData: StartPaymentRequest, obligationId: number): Promise<PaymentRequest> {

        return this.post<PaymentRequest>(`/${obligationId}/StartPayment`, PaymentRequest, startPaymentData)
            .then((paymentRequest) => { return paymentRequest });
    }


    public ANDUndeliveredPersonObligation() {

        return false;
    }

    /**
     * Търсене на задължения
     * @param criteria 
     */
    public searchObligationsServiceInstances(criteria: ObligationSearchCriteria): Promise<Obligation[]> {
        return this.get<Obligation[]>(`/ServiceInstances`, Obligation, criteria, null).then(function (result: Obligation[]) {
            return result;
        });
    }

    /**
     * Търсене на задължения
     * @param criteria 
     */
    public searchPaymentRequests(criteria: PaymentRequestSearchCriteria): Promise<PaymentRequest[]> {
        return this.get<PaymentRequest[]>(`/requests`, PaymentRequest, criteria, null).then(function (result: PaymentRequest[]) {
            return result;
        });
    }

    /**
     * Изчита код за достъп от ДАЕУ
     * @param serviceGroup
     */
    public getPEPAccessCode(paymentRequestId: number, iban: string): Promise<AccessCodeUIResponse> {

        return this.post<AccessCodeUIResponse>(`/PreparePepAccessCodePaymentRequest/${paymentRequestId}/${iban}`, AccessCodeUIResponse, null)
            .then((accessCode) => { return accessCode });
    }
}