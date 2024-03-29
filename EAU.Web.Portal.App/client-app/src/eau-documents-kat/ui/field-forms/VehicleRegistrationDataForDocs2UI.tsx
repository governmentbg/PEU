﻿import { BaseProps, ViewMode } from "cnsys-ui-react";
import { Constants, EAUBaseComponent } from "eau-core";
import { PoliceDepartmentUI } from "eau-documents";
import React from "react";
import { VehicleRegistrationData } from "../../models/ModelsAutoGenerated";
import { DisplayStatusesUI } from "./DisplayStatusesUI";

interface VehicleRegistrationDataForDocs2Props extends BaseProps {
}

export class VehicleRegistrationDataForDocs2UI extends EAUBaseComponent<VehicleRegistrationDataForDocs2Props, VehicleRegistrationData> {

    render(): JSX.Element {
        return (
            <>
                <div className="row">
                    <div className="form-group col-12">
                        <h4 className="form-control-label">{this.getResourceByProperty(m => m.registrationNumber)}</h4>
                        {this.textDisplayFor(m => m.registrationNumber)}
                    </div>
                </div>
                <div className="row">
                    <div className="col-sm-4 form-group">
                        <h4 className="form-control-label">{this.getResourceByProperty(m => m.makeAndModel)}</h4>
                        {this.textDisplayFor(m => m.makeAndModel)}
                    </div>
                    <div className="col-sm-4 form-group">
                        <h4 className="form-control-label">{this.getResourceByProperty(m => m.identificationNumber)}</h4>
                        {this.textDisplayFor(m => m.identificationNumber)}
                    </div>
                </div>
                <div className="row">
                    <div className="col-sm-4 form-group">
                        <h4 className="form-control-label">{this.getResourceByProperty(m => m.vehicleCategory)}</h4>
                        {this.textDisplay(this.model.vehicleCategory.code, this.model.vehicleCategory, "code")}
                    </div>
                    <div className="col-sm-4 form-group">
                        <h4 className="form-control-label">{this.getResourceByProperty(m => m.nextVehicleInspection)}</h4>
                        {this.dateDisplayFor(this.model.nextVehicleInspection, Constants.DATE_FORMATS.date)}
                    </div>
                </div>
                <div className="row">
                    <div className="col-12 form-group">
                        <h4 className="form-control-label">{this.getResourceByProperty(m => m.policeDepartment)}</h4>
                        <PoliceDepartmentUI {...this.bind(m => m.policeDepartment)} viewMode={ViewMode.Display} />
                    </div>
                </div>
                <DisplayStatusesUI statuses={this.model.statuses} />
            </>
        );
    }
}