import React from "react";
import { EAUBaseComponent, Constants, FormEditorUI, FormDisplayUI } from "eau-core";
import { BaseProps, ViewMode } from "cnsys-ui-react";
import { RefusalVM, FieldFormUI, DocumentURIUI, ElectronicServiceProviderBasicDataUI, AISCaseURIUI } from "../..";
import { BindableReference } from "cnsys-core";

export class RefusalUI extends EAUBaseComponent<BaseProps, RefusalVM> {

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
                    <div className="plain-text form-control-box" id="OTKAZ">
                        <div className="document-template" id="EDITOR-CONTENT">
                            <FormEditorUI {...this.bind(m => m.refusalContent)}/>
                        </div>
                    </div>
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
                            <h2 className="page-subtitle">{this.getResource("GL_URI_L")} <DocumentURIUI {...this.bind(m => m.documentURI)} /></h2>
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
                            <div className="plain-text">
                                <div className="document-template">
                                    <FormDisplayUI displayHTML={this.model.refusalContent} />
                                    {this.propertyErrorsDispleyFor(m => m.refusalContent)}
                                </div>
                            </div>
                        </div>
                    </div>
                </FieldFormUI>
            </>
        )
    }
}