import React from "react";
import { EAUBaseComponent, Constants } from "eau-core";
import { BaseProps } from "cnsys-ui-react";
import { AISCaseURIVM, DocumentURIUI } from "../..";

export class AISCaseURIUI extends EAUBaseComponent<BaseProps, AISCaseURIVM> {

    render(): JSX.Element {
        return (
            <>
                <DocumentURIUI {...this.bind(m => m.documentUri)} />
            </>
        )
    }
}