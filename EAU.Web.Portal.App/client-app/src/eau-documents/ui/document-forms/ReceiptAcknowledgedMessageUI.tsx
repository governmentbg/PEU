import React from "react";
import { EAUBaseComponent, Constants, ResourceHelpers } from "eau-core";
import { BaseProps, RawHTML, ViewMode } from "cnsys-ui-react";
import { DocumentElectronicTransportType } from "../../models";
import { ReceiptAcknowledgedMessageVM, DocumentURIUI, FieldFormUI, ElectronicServiceApplicantUI } from "../..";

export class ReceiptAcknowledgedMessageUI extends EAUBaseComponent<BaseProps, ReceiptAcknowledgedMessageVM> {

    render(): JSX.Element {
        return (
            <>
                <FieldFormUI title={this.getResourceByProperty(m => m.caseAccessIdentifier)}>
                    <div className="row">
                        <div className="form-group col-12">
                            <RawHTML rawHtmlText={this.model.caseAccessIdentifier} />
                        </div> </div>
                </FieldFormUI>
                <ElectronicServiceApplicantUI {...this.bind(m => m.applicant)} viewMode={ViewMode.Display} />
                <FieldFormUI title={this.getResourceByProperty(m => m.applicationDocumentTypeName)}>
                    <div className="row">
                        <div className="form-group col-12">
                            <h4 className="form-control-label">{this.getResourceByProperty(m => m.documentTypeName)}</h4>
                            {this.textDisplayFor(m => m.documentTypeName)}
                        </div>
                        <div className="form-group col-12">
                            <h4 className="form-control-label">{this.getResourceByProperty(m => m.documentURI)}</h4>
                            <DocumentURIUI {...this.bind(m => m.documentURI)} />
                        </div>
                        <div className="form-group col-12">
                            <h4 className="form-control-label">{this.getResourceByProperty(m => m.receiptTime)}</h4>
                            {this.dateDisplayFor(this.model.receiptTime, Constants.DATE_FORMATS.dateTime)}
                        </div>
                        <div className="form-group col-12">
                            <h4 className="form-control-label">{this.getResourceByProperty(m => m.transportType)}</h4>
                            <p className="field-text">{ResourceHelpers.getResourceByEmun(this.model.transportType, DocumentElectronicTransportType)}</p>
                        </div>


                    </div>
                </FieldFormUI>

            </>
        )
    }
}