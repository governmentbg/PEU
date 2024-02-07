import { AsyncUIProps, BaseProps, withAsyncFrame, withRouter } from 'cnsys-ui-react';
import { EAUBaseComponent , Constants as ConstantsEAU, Service, PaymentRequestStatuses} from 'eau-core';
import { observer } from 'mobx-react';
import { observable} from 'mobx';
import React from 'react';
import { ObjectHelper } from 'cnsys-core';
import { Obligation } from '../../models/ModelsManualAdded';

interface MyEPaymentsProps extends BaseProps, AsyncUIProps {
    services: Service[];
}

@observer class MyEPaymentsResultRowUI extends EAUBaseComponent<MyEPaymentsProps, Obligation>{

    @observable private paymentRequests: Obligation[];

    render() {

        let paymentRequestsLength = this.model.paymentRequests.length;
        
        let dataResultRow: any = null;

        if (!ObjectHelper.isNullOrUndefined(paymentRequestsLength) && paymentRequestsLength == 1) {

            let firstElement = this.model.paymentRequests[0];
            
            dataResultRow = <>
                <tr key={this.model.paymentRequests[0].obligationID + "_0"}>
                    <td>
                        <p className="th-title d-sm-none">{this.getResource('GL_APPLICATION_L')}</p>
                        <p className="td-text">
                            <b>
                            {this.props.services.length > 0 ? this.props.services.filter(s => s.serviceID == this.model.serviceID)[0].name : ''}
                            </b>
                        </p>
                    </td>
                    <td>
                        <p className="th-title d-sm-none">{this.getResource('GL_DESCRIPTION_L')}</p>
                        <p className="td-text">{this.model.paymentReason}</p>
                    </td>
                    <td>
                        <p className="th-title d-sm-none">{this.getResource('GL_PAYMENT_PORTAL_L')}</p>
                        <p className="td-text">{this.getResource(ConstantsEAU.PAYMENT_METHOD_TYPE[firstElement.registrationDataType])}</p>
                    </td>
                    <td>
                        <p className="th-title d-sm-none">{this.getResource('GL_PAYMENT_DATE_L')}</p>
                        <p className="td-text">{!ObjectHelper.isNullOrUndefined(firstElement.payDate) ? firstElement.payDate.format(ConstantsEAU.DATE_FORMATS.dateTimeHourMinute) : ''}</p>
                    </td>
                    <td className="text-right">
                        <p className="th-title d-sm-none">{this.getResource('GL_AMOUNT_L')}</p>
                        <p className="td-text">{firstElement.amount.toFixed(2) + " " + this.getResource("GL_BGN_ABBRAVETION_L")}</p>
                    </td>
                </tr>
            </>
        }

        else if (!ObjectHelper.isNullOrUndefined(paymentRequestsLength) && paymentRequestsLength >= 2) {

            let countPaidPaymentRequests = 0;
            let firstPaidPaymentRequestId = 0;
            let ii = 0;
            this.model.paymentRequests.forEach(element => 
                {
                    if (PaymentRequestStatuses.Paid == element.status) {
                        countPaidPaymentRequests ++;
                        
                        if (ii == 0) {
                            firstPaidPaymentRequestId = element.paymentRequestID;
                        }

                        ii++;
                    }
                });
                
            dataResultRow = <>
            {
                this.model.paymentRequests.map((paymentRequestItem, itemIndex) => 

                (PaymentRequestStatuses.Paid == paymentRequestItem.status)  ? 

                    (firstPaidPaymentRequestId == paymentRequestItem.paymentRequestID)  ?
                    
                    <tr className={countPaidPaymentRequests == 1 ? "" : "master-row table-warning"} key={paymentRequestItem.obligationID + "_" + itemIndex}>
                        <td rowSpan={countPaidPaymentRequests}>
                            <p className="th-title d-sm-none">{this.getResource('GL_APPLICATION_L')}</p>
                            <p className="td-text">
                                <b>
                                {this.props.services.length > 0 ? this.props.services.filter(s => s.serviceID == this.model.serviceID)[0].name : ''}
                                </b>
                                {countPaidPaymentRequests > 1 ? <p className="text-warning"><em>{this.getResource('GL_SERVICE_PAID_MORE_THAN_ONCE_I')}</em></p> : ''}
                            </p>
                        </td>
                        <td rowSpan={countPaidPaymentRequests}>
                            <p className="th-title d-sm-none">{this.getResource('GL_DESCRIPTION_L')}</p>
                            <p className="td-text">{this.model.paymentReason}</p>
                        </td>
                        <td>
                            <p className="th-title d-sm-none">{this.getResource('GL_PAYMENT_PORTAL_L')}</p>
                            <p className="td-text">{this.getResource(ConstantsEAU.PAYMENT_METHOD_TYPE[paymentRequestItem.registrationDataType])}</p>
                        </td>
                        <td>
                            <p className="th-title d-sm-none">{this.getResource('GL_PAYMENT_DATE_L')}</p>
                            <p className="td-text">{!ObjectHelper.isNullOrUndefined(paymentRequestItem.payDate) ? paymentRequestItem.payDate.format(ConstantsEAU.DATE_FORMATS.dateTimeHourMinute) : ''}</p>
                        </td>
                        <td className="text-right">
                            <p className="th-title d-sm-none">{this.getResource('GL_AMOUNT_L')}</p>
                            <p className="td-text">{paymentRequestItem.amount.toFixed(2) + " " + this.getResource("GL_BGN_ABBRAVETION_L")}</p>
                        </td>
                    </tr>
                   :
                    <tr className="sub-row table-warning" key={paymentRequestItem.obligationID + "_subrow_" + itemIndex}>
                        <td>
                            <p className="th-title d-sm-none">{this.getResource('GL_PAYMENT_PORTAL_L')}</p>
                            <p className="td-text">{this.getResource(ConstantsEAU.PAYMENT_METHOD_TYPE[paymentRequestItem.registrationDataType])}</p>
                        </td>
                        <td>
                            <p className="th-title d-sm-none">{this.getResource('GL_PAYMENT_DATE_L')}</p>
                            <p className="td-text">{!ObjectHelper.isNullOrUndefined(paymentRequestItem.payDate) ? paymentRequestItem.payDate.format(ConstantsEAU.DATE_FORMATS.dateTimeHourMinute) : ''}</p>
                        </td>
                        <td className="text-right">
                            <p className="th-title d-sm-none">{this.getResource('GL_AMOUNT_L')}</p>
                            <p className="td-text">{paymentRequestItem.amount.toFixed(2) + " " + this.getResource("GL_BGN_ABBRAVETION_L")}</p>
                        </td>
                    </tr>  

                : null
                )
            }
           </>   
        }

        return <>
          {dataResultRow}
        </>
    }
}

export default withRouter(withAsyncFrame(MyEPaymentsResultRowUI, false));