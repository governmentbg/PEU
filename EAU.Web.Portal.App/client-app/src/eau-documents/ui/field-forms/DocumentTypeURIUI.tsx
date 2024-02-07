import React from "react";
import { EAUBaseComponent, Constants } from "eau-core";
import { BaseProps } from "cnsys-ui-react";
import { DocumentTypeURI } from "../..";

export class DocumentTypeURIUI extends EAUBaseComponent<BaseProps, DocumentTypeURI> {

    render(): JSX.Element {
        return (
            <>
                {this.model.registerIndex + "-" + this.model.batchNumber}
            </>
        )
    }
}