import { BaseProps } from 'cnsys-ui-react';
import {ANDSourceIds, EAUBaseComponent, KATDocumentTypes} from 'eau-core';
import React, { Fragment } from 'react';
import { ANDObligationSearchCriteria, Obligation, ObligationSearchResult } from '../../models/ModelsManualAdded';
import {formatAmount, formatDate, formatPercent, getPaymentDescription, isMainDocument} from './ObligationsHelpers';
import { ObligationsPaymentsUI } from './ObligationsPaymentsUI';

interface BDSObligationsResultsUIProps extends BaseProps {
    result: ObligationSearchResult;
    searchCriteria: ANDObligationSearchCriteria;
}

export class BDSObligationsResultsUI extends EAUBaseComponent<BDSObligationsResultsUIProps, Obligation>{

    private servedObligations: Obligation[];
    private notServedObligations: Obligation[];

    constructor(props: BDSObligationsResultsUIProps) {
        super(props);

        if (this.props.result?.obligations?.length > 0) {
            this.servedObligations = this.props.result.obligations.filter(x => x?.additionalData?.isServed?.toLowerCase() == 'true')
            this.notServedObligations = this.props.result.obligations.filter(x => x?.additionalData?.isServed?.toLowerCase() != 'true')
        }
    }

    private renderObligations(obligations: Obligation[], isServed: boolean) {
        return obligations?.length > 0
            ? obligations.map((obligation, index) => {
                return <Fragment key={`${obligation.obligationIdentifier}_${index}_bds`}>
                    {
                        index == 0
                            ? <tr className={isServed ? "table-warning" : ""} aria-hidden="true">
                                <td colSpan={11}>
                                    <span className="form-control-label">{this.getResource(isServed ? 'GL_OBLIGATION_SERVED_L' : 'GL_OBLIGATION_NOT_SERVED_L')}</span>
                                </td>
                            </tr>
                            : null
                    }
                    <tr className={isServed ? "table-warning" : ""}>
                        <td>
                            <p className="th-title d-sm-none">{this.getResource('GL_OBLIGATION_DOCUMENT_TYPE_L')}</p>
                            <p className="td-text text-nowrap">
                                {
                                    obligation.additionalData.documentType == 'PENAL DECREE'
                                        ? this.getResource(`GL_${obligation.additionalData.documentType}_SHORT_L`)
                                        : this.getResource(`GL_${obligation.additionalData.documentType}_L`)
                                }
                            </p>
                            <span className="sr-only">{this.getResource(isServed ? 'GL_OBLIGATION_SERVED_L' : 'GL_OBLIGATION_NOT_SERVED_L')}</span>
                        </td>
                        <td>
                            <p className="th-title d-sm-none">{this.getResource('GL_OBLIGATION_SERIES_L')}</p>
                            <p className="td-text">{obligation.additionalData.documentSeries}</p>
                        </td>
                        <td>
                            <p className="th-title d-sm-none">{this.getResource('GL_DOCUMENT_NUMBER_L')}</p>
                            <p className="td-text">{obligation.additionalData.documentNumber}</p>
                        </td>
                        <td>
                            <p className="th-title d-sm-none">{this.getResource('GL_DATE_OF_ISSUE_L')}</p>
                            <p className="td-text">{formatDate(obligation.additionalData.issueDate)}</p>
                        </td>
                        <td>
                            <p className="th-title d-sm-none">{this.getResource('GL_BREACH_OF_ORDER_L')}</p>
                            <p className="td-text">{obligation.additionalData.breachOfOrder}</p>
                        </td>
                        <td className="text-right">
                            <p className="th-title d-sm-none">{this.getResource('GL_AMOUNT_DUE_L')}</p>
                            <p className="td-text">{formatAmount(obligation.amount)}</p>
                        </td>
                        <td className="text-right">
                            <p className="th-title d-sm-none">{this.getResource('GL_DISCOUNT_L')}</p>
                            <p className="td-text">{formatPercent(obligation.additionalData.discount)}</p>
                        </td>
                        <td className="text-right">
                            <p className="th-title d-sm-none">{this.getResource('GL_PAYMENT_AMOUNT_L')}</p>
                            <p className="td-text">{formatAmount(obligation.discountAmount)}</p>
                        </td>
                        <td className="actions-td">
                            <p className="th-title d-sm-none">{this.getResource('GL_PAYMENT_PORTAL_L')}</p>
                            <ObligationsPaymentsUI obligation={obligation} searchCriteria={this.props.searchCriteria} andSourceIds={ANDSourceIds.BDS} />
                        </td>
                    </tr>
                    {getPaymentDescription(obligation.status, obligation.paymentRequests, obligation.additionalData.documentNumber, obligation.additionalData.documentType, isServed, 9)}
                </Fragment>
            })
            : null
    }

    render() {

        if (!this.props.result) {
            return null;
        }

        var htmlContent = null;

        if (this.props.result.errorReadingData) {
            htmlContent = <div className="alert alert-danger mt-4" role="alert">
                <p>{this.getResource('GL_UNPAID_DEBTS_E')}</p>
            </div>

        } else if (this.props.result.errorNoDataFound || !this.props.result.obligations || this.props.result.obligations.length == 0) {
            htmlContent = <div className="alert alert-info mt-4" role="alert">
                <p>{this.getResource('GL_UNPAID_DEBTS_NOTFOUND_E')}</p>
            </div>

        } else {
            htmlContent = <div className="table-responsive-block mt-0">
                <table className="table table-hover">
                    <thead>
                        <tr>
                            <th className="w-5">{this.getResource('GL_OBLIGATION_DOCUMENT_TYPE_L')}</th>
                            <th className="w-5">{this.getResource('GL_OBLIGATION_SERIES_L')}</th>
                            <th className="w-10">{this.getResource('GL_DOCUMENT_NUMBER_L')}</th>
                            <th className="w-10">{this.getResource('GL_DATE_OF_ISSUE_L')}</th>
                            <th>{this.getResource('GL_BREACH_OF_ORDER_L')}</th>
                            <th className="w-10 text-right">{this.getResource('GL_AMOUNT_DUE_L')}</th>
                            <th className="w-10 text-right">{this.getResource('GL_DISCOUNT_L')}</th>
                            <th className="w-10 text-right">{this.getResource('GL_PAYMENT_AMOUNT_L')}</th>
                            <th className="w-10 text-right">{this.getResource('GL_PAYMENT_PORTAL_L')}</th>
                        </tr>
                    </thead>
                    <tbody>{this.renderObligations(this.servedObligations, true)}</tbody>
                    <tbody>{this.renderObligations(this.notServedObligations, false)}</tbody>
                </table>
            </div>
        }

        return <fieldset className="fields-group">
            <legend>
                <h3 className="field-title">{this.getResource('GL_FIELD_OF_ACTIVITY_BDS_L')}</h3>
            </legend>
            {htmlContent}
        </fieldset>
    }
}