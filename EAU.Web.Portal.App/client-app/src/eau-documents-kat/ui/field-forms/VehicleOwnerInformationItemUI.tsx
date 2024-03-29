﻿import * as React from "react";
import { observer } from "mobx-react";
import { BaseProps, ViewMode } from "cnsys-ui-react";
import { EAUBaseComponent, Constants } from "eau-core";
import { VehicleOwnerInformationItemVM } from "../../models/ModelsAutoGenerated";
import { PersonAndEntityBasicDataUI } from "eau-documents";
import { VehicleOwnerAddressUI } from "./VehicleOwnerAddressUI";

@observer export class VehicleOwnerInformationItemUI extends EAUBaseComponent<BaseProps, VehicleOwnerInformationItemVM> {

    render(): JSX.Element {
        return (
            <>
                <div className="row">
                    <div className="form-group col-12">
                        <PersonAndEntityBasicDataUI {...this.bind(m => m.owner)} viewMode={ViewMode.Display} skipPersonIdentityDocumentBasicData={true} />
                    </div>
                </div>
                <div className="row">
                    <div className="form-group col-12">
                        <h4 className="form-control-label">{this.getResourceByProperty(m => m.address)}</h4>
                        <VehicleOwnerAddressUI {...this.bind(m => m.address)} />
                    </div>
                </div>               
            </>
        );
    }
}