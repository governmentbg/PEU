import React from "react";
import { EAUBaseComponent, Constants } from "eau-core";
import { BaseProps } from "cnsys-ui-react";
import { TerminationOfServiceMessageVM, DocumentURIUI, TerminationOfServiceMessageGrounds } from "../..";

export class TerminationOfServiceMessageUI extends EAUBaseComponent<BaseProps, TerminationOfServiceMessageVM> {

    render(): JSX.Element {
        return (
            <>
                <div className="row">
                    <div className="form-group col-12">
                        <h2 className="page-subtitle">{this.getResource("GL_URI_L")} <DocumentURIUI {...this.bind(m => m.documentURI)} /></h2>
                        <p className="page-date">{this.getResource("GL_DATE_L")} {this.dateDisplayFor(this.model.documentReceiptOrSigningDate, Constants.DATE_FORMATS.date)}</p>
                    </div>
                </div>
                <div className="alert alert-danger" role="alert">
                    {this.model.grounds.map((item: TerminationOfServiceMessageGrounds, index: number) => {
                        return (<p key={index}>{item.terminationOfServiceMessageGround}</p>);
                    })}
                </div>

            </>)
    }
}