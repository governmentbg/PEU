﻿import { BaseProps } from "cnsys-ui-react";
import { Constants, EAUBaseComponent } from "eau-core";
import React from "react";
import { ReportForChangingOwnershipGuaranteeFund, ReportForChangingOwnershipV2VehicleDataGuaranteeFund, ReportForVehicleGuaranteeFund } from "../../models/ModelsAutoGenerated";
import { DisplayStatusesUI } from "./DisplayStatusesUI";

export class GuaranteeFundUI extends EAUBaseComponent<BaseProps, ReportForChangingOwnershipGuaranteeFund | ReportForVehicleGuaranteeFund | ReportForChangingOwnershipV2VehicleDataGuaranteeFund> {

    render(): JSX.Element {
        return (
            <>
                {
                    this.model.policyValidTo
                        ? <div className="row">
                            <div className="col-sm-6 form-group">
                                <h4 className="form-control-label">{this.getResourceByProperty(m => m.policyValidTo)}</h4>
                                {this.dateDisplayFor(this.model.policyValidTo, Constants.DATE_FORMATS.date)}
                            </div>
                        </div>
                        : null
                }
                <DisplayStatusesUI statuses={this.model.status} />
            </>
        );
    }
}