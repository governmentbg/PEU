import React from "react";
import { EAUBaseComponent, Constants } from "eau-core";
import { BaseProps } from "cnsys-ui-react";
import { FieldFormUI, DocumentURIUI } from "..";
import { DocumentsWillBeIssuedMessageVM } from "../..";

export class DocumentsWillBeIssuedMessageUI extends EAUBaseComponent<BaseProps, DocumentsWillBeIssuedMessageVM> {

    render(): JSX.Element {
        return (
            <>
                {this.model.documentURI ?
                    <div className="row">
                        <div className="form-group col-12">
                            <h2 className="page-subtitle">
                                {this.getResource("GL_URI_L")} <DocumentURIUI {...this.bind(m => m.documentURI)} />
                            </h2>
                            <p className="page-date">{this.getResource("GL_DATE_L")} {this.dateDisplayFor(this.model.documentReceiptOrSigningDate, Constants.DATE_FORMATS.date)}</p>
                        </div>
                    </div>
                    : null
                }               
                <div className="alert alert-success"><p> {this.model.identityDocumentsWillBeIssuedMessage}</p></div>
            </>
        )
    }
}