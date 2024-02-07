import React from "react";
import { EAUBaseComponent, Constants } from "eau-core";
import { BaseProps } from "cnsys-ui-react";
import { FieldFormUI, DocumentURIUI, AISCaseURIUI, DeptorUI } from "..";
import { ReceiptAcknowledgedPaymentForMOIVM } from "../..";

export class ReceiptAcknowledgedPaymentForMOIUI extends EAUBaseComponent<BaseProps, ReceiptAcknowledgedPaymentForMOIVM> {

    render(): JSX.Element {
        return (
            <>
                <div className="row">
                    <div className="form-group col-12">
                        <h2 className="page-subtitle">{this.getResource("GL_URI_L")} <DocumentURIUI {...this.bind(m => m.documentURI)} /></h2>
                        <p className="page-date">{this.getResource("GL_DATE_L")} {this.dateDisplayFor(this.model.documentReceiptOrSigningDate, Constants.DATE_FORMATS.date)}</p>
                    </div>
                </div>
                <div className="alert alert-success"><p>{this.model.receiptAcknowledgedPaymentMessage}</p></div>
                <FieldFormUI title={this.getResourceByProperty(m => m.receiptAcknowledgedPaymentMessage)}>
                    <div className="row">
                        <div className="form-group col-12">
                            <h4 className="form-control-label">{this.getResourceByProperty(m => m.aisCaseURI)}</h4>
                            <AISCaseURIUI {...this.bind(m => m.aisCaseURI)} />
                        </div>
                        <div className="form-group col-12">
                            <h4 className="form-control-label">{this.getResource("GL_AMOUNT_L")} </h4>
                            <p className="field-text"> {this.model.amount} {this.model.amountCurrency}</p>
                        </div>
                        <div className="form-group col-12">
                            <h4 className="form-control-label">{this.getResourceByProperty(m => m.paymentReason)}</h4>
                            {this.textDisplayFor(m => m.paymentReason)}
                        </div>
                        <div className="form-group col-12">
                            <h4 className="form-control-label">{this.getResourceByProperty(m => m.administrativeBodyName)}</h4>
                            {this.textDisplayFor(m => m.administrativeBodyName)}
                        </div>
                    </div>
                    <div className="row">
                        <div className="form-group col-sm-4">
                            <h4 className="form-control-label">{this.getResourceByProperty(m => m.bankName)}</h4>
                            {this.textDisplayFor(m => m.bankName)}
                        </div>
                        <div className="form-group col-sm-4">
                            <h4 className="form-control-label">{this.getResourceByProperty(m => m.bic)}</h4>
                            <p className="field-text">{this.textDisplayFor(m => m.bic)}</p>
                        </div>
                        <div className="form-group col-sm-4">
                            <h4 className="form-control-label">{this.getResourceByProperty(m => m.iban)}</h4>
                            {this.textDisplayFor(m => m.iban)}
                        </div>
                    </div>                   
                </FieldFormUI>
                <FieldFormUI title={this.getResource("GL_Debtor_L")}>
                    <DeptorUI {...this.bind(m => m.electronicServiceApplicant)} />
                </FieldFormUI>
            </>
        )
    }
}