﻿import * as React from "react";
import { observer } from "mobx-react";
import { BaseProps } from "cnsys-ui-react";
import { EAUBaseComponent } from "eau-core";
import { VehicleOwnerAddress } from "../../models/ModelsAutoGenerated";
import { ObjectHelper } from "cnsys-core";

@observer export class VehicleOwnerAddressUI extends EAUBaseComponent<BaseProps, VehicleOwnerAddress> {

    render(): JSX.Element {
        let hasStreet: boolean = !ObjectHelper.isStringNullOrEmpty(this.model.addressSupplement);
        let textToShow: string = hasStreet ? `${this.model.addressSupplement}` : '';
        return (
            <>
                <p className="field-text">
                    {`${this.getResource('GL_MUNICIPALITY_L')} ${this.model.municipalityName}, ${this.getResource('GL_REGION_L')} ${this.model.districtName}, ${this.model.residenceName}`}
                </p>
                <p className="field-text">{textToShow}</p>
            </>
        );
    }
}