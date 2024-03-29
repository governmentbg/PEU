﻿import { BaseProps, ViewMode } from "cnsys-ui-react";
import { EAUBaseComponent, ValidationSummary, ValidationSummaryStrategy } from "eau-core";
import { FieldFormUI } from "eau-documents";
import { observer } from "mobx-react";
import React from "react";
import { VehicleBuyerDataCollectionUI, VehicleOwnerDataCollectionUI, VehicleRegistrationDataCollectionUI } from ".";
import { VehicleRegistrationChangeVM } from "../../models/ModelsAutoGenerated";

interface VehicleRegistrationChangeUIProps extends BaseProps {
}

@observer export class VehicleRegistrationChangeUI extends EAUBaseComponent<VehicleRegistrationChangeUIProps, VehicleRegistrationChangeVM> {

    renderEdit(): JSX.Element {
        return (
            <>
                <FieldFormUI title={this.getResourceByProperty(m => m.vehicleRegistrationData)}>  
                    <ValidationSummary model={this.model} strategy={ValidationSummaryStrategy.includeOnlyModelErrors} />
                    <VehicleRegistrationDataCollectionUI {...this.bind(m => m.vehicleRegistrationData)} />
                </FieldFormUI>
                <FieldFormUI title={this.getResourceByProperty(m => m.currentOwners)}>
                    <VehicleOwnerDataCollectionUI {...this.bind(m => m.currentOwners)} newOwner={false} coutntOfVehicle={this.model.vehicleRegistrationData.length} addButtonLabelKey={"GL_ADD_CURRENT_OWNER_L"} />
                </FieldFormUI>
                <FieldFormUI title={this.getResourceByProperty(m => m.newOwners)}>
                    <VehicleBuyerDataCollectionUI {...this.bind(m => m.newOwners)} />
                </FieldFormUI>
            </>
        );
    }

    renderDisplay(): JSX.Element {
        return (
            <>
                <FieldFormUI title={this.getResourceByProperty(m => m.vehicleRegistrationData)}>
                    <ValidationSummary model={this.model} strategy={ValidationSummaryStrategy.includeOnlyModelErrors} viewMode={ViewMode.Display} />
                    <VehicleRegistrationDataCollectionUI {...this.bind(m => m.vehicleRegistrationData)} />
                </FieldFormUI>
                <FieldFormUI title={this.getResourceByProperty(m => m.currentOwners)}>
                    <VehicleOwnerDataCollectionUI {...this.bind(m => m.currentOwners)} />
                </FieldFormUI>
                <FieldFormUI title={this.getResourceByProperty(m => m.newOwners)}>
                    <VehicleBuyerDataCollectionUI {...this.bind(m => m.newOwners)} />
                </FieldFormUI>
            </>
        );
    }
}