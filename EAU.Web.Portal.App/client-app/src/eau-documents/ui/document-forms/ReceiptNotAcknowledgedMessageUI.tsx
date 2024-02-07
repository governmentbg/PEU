import { ArrayHelper } from "cnsys-core";
import { BaseProps, ViewMode } from "cnsys-ui-react";
import { Constants, EAUBaseComponent, ResourceHelpers } from "eau-core";
import React from "react";
import { DocumentElectronicTransportType, ElectronicDocumentDiscrepancyType, ElectronicServiceApplicantUI, FieldFormUI, ReceiptNotAcknowledgedMessageVM } from "../..";

export class ReceiptNotAcknowledgedMessageUI extends EAUBaseComponent<BaseProps, ReceiptNotAcknowledgedMessageVM> {

    render(): JSX.Element {
        return (
            <>               
                <div className="row">
                    <div className="col-12 form-group">
                        <p className="field-text"><b>
                            {this.model.electronicServiceProvider.entityBasicData.name}, {ResourceHelpers.getResourceByProperty('identifier', this.model.electronicServiceProvider.entityBasicData)}: {this.model.electronicServiceProvider.entityBasicData.identifier}</b></p>
                    </div>
                </div>
                <ElectronicServiceApplicantUI {...this.bind(m => m.applicant)} viewMode={ViewMode.Display} />
                <FieldFormUI title={this.getResourceByProperty(m => m.discrepancies)}>
                    <div className="form-group">
                        <ul className="field-text pl-3 text-dangerA">
                            {this.renderDiscrepancies()}
                        </ul>
                    </div>
                </FieldFormUI>                
                <FieldFormUI title={this.getResourceByProperty(m => m.applicationDocumentTypeName)}>
                    <div className="row">
                        <div className="form-group col-12">
                            <h4 className="form-control-label">{this.getResourceByProperty(m => m.documentTypeName)}</h4>
                            {this.textDisplayFor(m => m.documentTypeName)}
                        </div>                       
                        <div className="form-group col-12">
                            <h4 className="form-control-label">{this.getResourceByProperty(m => m.messageCreationTime)}</h4>
                            {this.dateDisplayFor(this.model.messageCreationTime, Constants.DATE_FORMATS.dateTime)}
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
    renderDiscrepancies(): JSX.Element {
        if (this.model.discrepancies && ArrayHelper.queryable.from(this.model.discrepancies).toArray().length > 0) {
            return (
                <>
                    {ArrayHelper.queryable.from(this.model.discrepancies).toArray().map((item: ElectronicDocumentDiscrepancyType, idx: number) => {
                        return (
                            <li key={item.toString()}>
                                {this.getElectronicDocumentDiscrepancyTypeResource(item.toString())}
                            </li>
                        )
                    })}
                </>);
        }
        return null;
    }

    getElectronicDocumentDiscrepancyTypeResource(discrepancyType: string): string {

        switch (discrepancyType) {
            case "0": return this.getResource("DOC_GL_ElectronicDocumentDiscrepancyType_Item0006000005_L");
            case "1": return this.getResource("DOC_GL_ElectronicDocumentDiscrepancyType_Item0006000006_L");
            case "2": return this.getResource("DOC_GL_ElectronicDocumentDiscrepancyType_Item0006000007_L");
            case "3": return this.getResource("DOC_GL_ElectronicDocumentDiscrepancyType_Item0006000008_L");
            case "4": return this.getResource("DOC_GL_ElectronicDocumentDiscrepancyType_Item0006000009_L");
            case "5": return this.getResource("DOC_GL_ElectronicDocumentDiscrepancyType_Item0006000010_L");
            case "6": return this.getResource("DOC_GL_ElectronicDocumentDiscrepancyType_Item0006000011_L");
            default: return "";
        }
    }
}