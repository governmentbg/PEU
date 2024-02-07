import React from "react";
import { EAUBaseComponent, Constants, ResourceHelpers } from "eau-core";
import { BaseProps, ViewMode } from "cnsys-ui-react";
import { ElectronicServiceProviderBasicDataVM, EntityBasicDataUI } from "../..";
import { ElectronicServiceProviderType } from "../../models";

export class ElectronicServiceProviderBasicDataUI extends EAUBaseComponent<BaseProps, ElectronicServiceProviderBasicDataVM> {

    render(): JSX.Element {
        return (
            <>
                <div className="row">
                    <div className="form-group col-12">
                        <h4 className="form-control-label">{this.getResourceByProperty(m => m.electronicServiceProviderType)}</h4>
                        {ResourceHelpers.getResourceByEmun(this.model.electronicServiceProviderType, ElectronicServiceProviderType)}
                    </div>
                </div>
                <EntityBasicDataUI {...this.bind(m => m.entityBasicData)} viewMode={ViewMode.Display} />
            </>
        )
    }
}