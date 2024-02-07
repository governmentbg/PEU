import { BaseProps, AsyncUIProps, withAsyncFrame } from "cnsys-ui-react";
import { attributesClassFormControlRequiredLabel, Constants, EAUBaseComponent, attributesClassFormControlReqired } from "eau-core";
import React from "react";
import { DeadlineUI, DocumentURIUI, FieldFormUI, PaymentInstructionsVM } from "../..";

interface PaymentInstructionsProps extends BaseProps, AsyncUIProps {
    
}


export class PaymentInstructionsUIImpl extends EAUBaseComponent<PaymentInstructionsProps, PaymentInstructionsVM> {
    constructor(props: BaseProps) {
        super(props);

        //Bind       

        //Init
    }

    renderEdit(): JSX.Element {
        return (
            <>
                <FieldFormUI title={this.getResource("GL_PaymentData_L")}>
                    <div className="row">
                        <div className="form-group col-6 col-sm-4 col-lg-2">
                            {this.labelFor(m => m.amount, null, attributesClassFormControlRequiredLabel)}
                            <div className="form-row">
                                <div className="col">
                                    {this.amountFor(m => m.amount, attributesClassFormControlReqired)}
                                </div>
                                <div className="col-auto">
                                    <p className="col-form-label">{this.model.amountCurrency}</p>
                                </div>                                
                            </div>
                        </div>
                        <div className="form-group col-12">
                            <h4 className="form-control-label">{this.getResourceByProperty(m => m.paymentReason)}</h4>
                            {this.textDisplayFor(m => m.paymentReason)}
                        </div>
                    </div>
                    <div className="row">
                        <div className="form-group col-sm-4">
                            <h4 className="form-control-label">{this.getResourceByProperty(m => m.bankName)}</h4>
                            {this.textDisplayFor(m => m.bankName)}
                        </div>
                        <div className="form-group col-sm-4">
                            <h4 className="form-control-label">{this.getResourceByProperty(m => m.bic)}</h4>
                            {this.textDisplayFor(m => m.bic)}
                        </div>
                        <div className="form-group col-sm-4">
                            <h4 className="form-control-label">{this.getResourceByProperty(m => m.iban)}</h4>
                            {this.textDisplayFor(m => m.iban)}
                        </div>
                    </div>
                </FieldFormUI>
                <FieldFormUI title={this.getResourceByProperty(m => m.deadlineForPayment)}>
                    <DeadlineUI {...this.bind(m => m.deadlineForPayment)} />
                </FieldFormUI>
            </>
        )
    }

    renderDisplay(): JSX.Element {
        return (
            <>
                {this.model.documentURI ?
                    <div className="row">
                        <div className="form-group col-12">
                            <h2 className="page-subtitle">
                                {this.getResource("GL_URI_L")} <DocumentURIUI {...this.bind(m => m.documentURI)} />
                            </h2>
                            <p className="page-date">{this.getResource("GL_DATE_L")} {this.dateDisplayFor(this.model.documentURI.receiptOrSigningDate, Constants.DATE_FORMATS.date)}</p>
                        </div>
                    </div>
                    : null
                }
                <FieldFormUI title={this.getResource("GL_PaymentData_L")}>
                    <div className="row">
                        <div className="form-group col-6 col-sm-4 col-lg-2">
                            <h4 className="form-control-label">{this.getResourceByProperty(m => m.amount)}</h4>
                            <div className="form-row">
                                <div className="col">
                                    {this.model.amount} {this.model.amountCurrency}
                                </div>
                                <div className="col-auto">
                                </div>
                            </div>
                        </div>
                        <div className="form-group col-12">
                            <h4 className="form-control-label">{this.getResourceByProperty(m => m.paymentReason)}</h4>
                            {this.textDisplayFor(m => m.paymentReason)}
                        </div>
                    </div>                 
                </FieldFormUI>
                <FieldFormUI title={this.getResourceByProperty(m => m.deadlineForPayment)}>
                    <DeadlineUI {...this.bind(m => m.deadlineForPayment)} />
                    <div className="alert alert-warning">
                        <p>{this.model.deadlineMessage}</p></div>
                </FieldFormUI>
            </>
        )
    }

}

export const PaymentInstructionsUI = withAsyncFrame(PaymentInstructionsUIImpl)
