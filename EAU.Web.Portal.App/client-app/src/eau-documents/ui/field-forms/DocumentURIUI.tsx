import React from "react";
import { EAUBaseComponent, Constants } from "eau-core";
import { BaseProps } from "cnsys-ui-react";
import { DocumentURI } from "../..";

export class DocumentURIUI extends EAUBaseComponent<BaseProps, DocumentURI> {

    render(): JSX.Element {
        return (
            <>
                {this.model.registerIndex+"-"+this.model.sequenceNumber+"-"+this.model.receiptOrSigningDate.format(Constants.DATE_FORMATS.date).substring(0, 10)}
            </>
        )
    }
}