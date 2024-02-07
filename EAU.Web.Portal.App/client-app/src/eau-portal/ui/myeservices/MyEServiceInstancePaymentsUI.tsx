import { ObjectHelper } from 'cnsys-core';
import { AsyncUIProps, BaseProps, withAsyncFrame, withRouter } from 'cnsys-ui-react';
import { Constants, EAUBaseComponent, PaymentRequestStatuses } from 'eau-core';
import { observer } from 'mobx-react';
import React from 'react';
import { Obligation } from '../../models/ModelsManualAdded';

interface MyEServiceInstancePaymentsUIProps extends BaseProps, AsyncUIProps {
    paymentList?: Obligation[];
}
 
@observer class MyEServiceInstancePaymentsUI extends EAUBaseComponent<MyEServiceInstancePaymentsUIProps, Obligation>{
    
    constructor(props: MyEServiceInstancePaymentsUIProps) {
    
        super(props);

        this.getResourceByPaymentRequestStatus = this.getResourceByPaymentRequestStatus.bind(this);
    }

    render() {
       
        let dataResultRow: any = null;
        let dataResultTableRow: any = null;

        if (this.props.paymentList.length > 0) { 

            this.props.paymentList.map((payment, index) => (

                payment.paymentRequests.map((paymentRequestItem, itemIndex) => (

                    (PaymentRequestStatuses.Paid == paymentRequestItem.status)  ?
                    
                        dataResultTableRow = <>
                                
                            {dataResultTableRow} 

                            <tr key={index + "_" + paymentRequestItem + "_" + itemIndex}>
                                <td>
                                    <p className="th-title d-sm-none">{this.getResource('GL_PAYMENT_PORTAL_L')}</p>
                                    <p className="td-text">{this.getResource(Constants.PAYMENT_METHOD_TYPE[paymentRequestItem.registrationDataType])}</p>
                                </td>
                                <td>
                                    <p className="th-title d-sm-none">{this.getResource('GL_STATUS_L')}</p>
                                    <p className="td-text">{this.getResourceByPaymentRequestStatus(paymentRequestItem.status)}</p>
                                </td>
                                <td>
                                    <p className="th-title d-sm-none">{this.getResource('GL_APPLICATION_DATE_L')}</p>
                                    <p className="td-text">{!ObjectHelper.isNullOrUndefined(paymentRequestItem.sendDate) ? paymentRequestItem.sendDate.format(Constants.DATE_FORMATS.dateTimeHourMinute) : ''}</p>
                                </td>
                                <td>
                                    <p className="th-title d-sm-none">{this.getResource('GL_EXECUTED_DATE_L')}</p>
                                    <p className="td-text">{!ObjectHelper.isNullOrUndefined(paymentRequestItem.payDate) ? paymentRequestItem.payDate.format(Constants.DATE_FORMATS.dateTimeHourMinute) : ''}</p>
                                </td>
                                <td className="text-right">
                                    <p className="th-title d-sm-none">{this.getResource('GL_AMOUNT_L')}</p>
                                    <p className="td-text">{paymentRequestItem.amount.toFixed(2) + " " + this.getResource("GL_BGN_ABBRAVETION_L")}</p>
                                </td>
                            </tr>
                        </>
                    : null
                ))
            ))

            if (!ObjectHelper.isNullOrUndefined(dataResultTableRow)) {

                dataResultRow = <>
                    <fieldset className="fields-group fields-group--no-border">
            
                        <legend>
                            <h2 className="section-title">{this.getResource('GL_PAYMENTS_L')}</h2>
                        </legend>

                        <div className="table-responsive-block">
                            <table className="table table-hover" aria-label={this.getResource("GL_SERVICE_PAYMENTS_LIST_I")}>
                                <thead className="">
                                    <tr>                                   
                                        <th>{this.getResource('GL_PAYMENT_PORTAL_L')}</th>
                                        <th>{this.getResource('GL_STATUS_L')}</th>
                                        <th>{this.getResource('GL_APPLICATION_DATE_L')}</th>
                                        <th>{this.getResource('GL_EXECUTED_DATE_L')}</th>
                                        <th className="text-right">{this.getResource('GL_AMOUNT_L')}</th>
                                    </tr>
                                </thead>

                                <tbody>
                                    {dataResultTableRow}
                                </tbody>
                             </table>
                        </div>
                    </fieldset>
                </>
            }

        }
       
        return <>
          {dataResultRow}
        </>
    }

    private getResourceByPaymentRequestStatus(status: PaymentRequestStatuses): string {
        switch (status) {
            case PaymentRequestStatuses.Sent:
                return this.getResource("GL_PAYMENT_REQUEST_STATUS_SENT_L");
            case PaymentRequestStatuses.Paid:
                return this.getResource("GL_PAYMENT_REQUEST_STATUS_PAID_L");
            case PaymentRequestStatuses.Cancelled:
                 return this.getResource("GL_PAYMENT_REQUEST_STATUS_CANCELLED_L");
            case PaymentRequestStatuses.Expired:
                return this.getResource("GL_PAYMENT_REQUEST_STATUS_EXPIRED_L");
            default:
                return "";
        }
    }
}

export default withRouter(withAsyncFrame(MyEServiceInstancePaymentsUI, false));