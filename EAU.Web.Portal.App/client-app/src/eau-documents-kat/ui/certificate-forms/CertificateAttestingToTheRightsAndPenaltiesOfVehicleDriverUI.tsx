﻿import { BaseProps } from "cnsys-ui-react";
import { Constants, EAUBaseComponent, ResourceHelpers, TextEditorUI } from "eau-core";
import { AISCaseURIUI, DocumentURIUI, ElectronicServiceProviderBasicDataUI, FieldFormUI } from "eau-documents";
import React from "react";
import { ANDCertificateReason, CertificateAttestingToTheRightsAndPenaltiesOfVehicleDriverVM } from "../../models/ModelsAutoGenerated";

export class CertificateAttestingToTheRightsAndPenaltiesOfVehicleDriverUI extends EAUBaseComponent<BaseProps, CertificateAttestingToTheRightsAndPenaltiesOfVehicleDriverVM> {

    renderEdit(): JSX.Element {
        return (
            <>
                <FieldFormUI title={this.getResource("GL_ElectronicServiceProviderBasicData_L")}>
                    <ElectronicServiceProviderBasicDataUI {...this.bind(m => m.electronicServiceProviderBasicData)} />
                </FieldFormUI>
                <FieldFormUI title={this.getResource("DOC_GL_CasefileData_L")}>
                    <div className="row">
                        <div className="form-group col-12">
                            <h4 className="form-control-label">{this.getResource("GL_URI_CASE_L")}</h4>
                            <p className="field-text"><AISCaseURIUI {...this.bind(m => m.aisCaseURI)} /></p>
                        </div>
                    </div>
                </FieldFormUI>
                <FieldFormUI title={this.getResource("GL_CONTENT_L")}>
                    <div className="row">
                        <div className="form-group col-12">
                            <TextEditorUI {...this.bind(m => m.certificateData)} />
                        </div>
                    </div>
                </FieldFormUI>
                <FieldFormUI title={this.getResourceByProperty(m => m.reportDate)}>
                    <div className="row">
                        <div className="form-group col-sm-auto">
                            <label htmlFor="DATE" className="form-control-label">{this.getResource("GL_DATE_L")}</label>
                            {this.dateTimeFor(m => m.reportDate)}                        
                        </div>
                    </div>
                </FieldFormUI>
                <FieldFormUI title={this.getResourceByProperty(m => m.andCertificateReason)}>
                    {ResourceHelpers.getResourceByEmun(this.model.andCertificateReason, ANDCertificateReason)}
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
                            <p className="page-subtitle">
                                {this.getResource("GL_URI_L")} <DocumentURIUI {...this.bind(m => m.documentURI)} />
                            </p>
                            <p className="page-date">{this.getResource("GL_DATE_L")} {this.dateDisplayFor(this.model.documentReceiptOrSigningDate, Constants.DATE_FORMATS.date)}</p>
                        </div>
                    </div>
                    : null}
                <FieldFormUI title={this.getResource("GL_ElectronicServiceProviderBasicData_L")}>
                    <ElectronicServiceProviderBasicDataUI {...this.bind(m => m.electronicServiceProviderBasicData)} />
                </FieldFormUI>
                <FieldFormUI title={this.getResource("DOC_GL_CasefileData_L")}>
                    <div className="row">
                        <div className="form-group col-12">
                            <h4 className="form-control-label">{this.getResource("GL_URI_CASE_L")}</h4>
                            <p className="field-text"><AISCaseURIUI {...this.bind(m => m.aisCaseURI)} /></p>
                        </div>
                    </div>
                </FieldFormUI>
                <FieldFormUI title={this.getResource("GL_CONTENT_L")}>
                    <div className="row">
                        <div className="form-group col-12">
                            {this.rawHtml(this.model.certificateData)}
                            {this.propertyErrorsDispleyFor(m => m.certificateData)}
                        </div>
                    </div>
                </FieldFormUI>
                <FieldFormUI title={this.getResourceByProperty(m => m.reportDate)}>
                    <div className="row">
                        <div className="col-sm-4 form-group">
                            <h4 className="form-control-label">{this.getResource("GL_DATE_L")}</h4>
                            {this.dateDisplayFor(this.model.reportDate, Constants.DATE_FORMATS.date)}
                            {this.propertyErrorsDispleyFor(m => m.reportDate)}
                        </div>
                        <div className="col-sm-4 form-group">
                            <h4 className="form-control-label">{this.getResource("GL_HOUR_L")}</h4>
                            {this.dateDisplayFor(this.model.reportDate, Constants.DATE_FORMATS.TimeHm)}
                        </div>
                    </div>
                </FieldFormUI>
                <FieldFormUI title={this.getResourceByProperty(m => m.andCertificateReason)}>
                    {ResourceHelpers.getResourceByEmun(this.model.andCertificateReason, ANDCertificateReason)}
                </FieldFormUI>
            </>
        )
    }
}