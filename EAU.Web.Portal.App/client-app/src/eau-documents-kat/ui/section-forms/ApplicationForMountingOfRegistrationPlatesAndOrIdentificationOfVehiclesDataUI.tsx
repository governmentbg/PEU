﻿import { BaseProps, ViewMode } from "cnsys-ui-react";
import { EAUBaseComponent } from "eau-core";
import { ApplicationFormManagerProps, withDocumentFormManager, FieldFormUI, PoliceDepartmentUI } from "eau-documents";
import { observer } from "mobx-react";
import React from "react";
import { ApplicationForMountingOfRegistrationPlatesAndOrIdentificationOfVehiclesDataVM } from "../../models/ModelsAutoGenerated";

interface ApplicationForMountingOfRegistrationPlatesAndOrIdentificationOfVehiclesDataProps extends BaseProps, ApplicationFormManagerProps {
}

@observer export class ApplicationForMountingOfRegistrationPlatesAndOrIdentificationOfVehiclesDataImpl
    extends EAUBaseComponent<ApplicationForMountingOfRegistrationPlatesAndOrIdentificationOfVehiclesDataProps, ApplicationForMountingOfRegistrationPlatesAndOrIdentificationOfVehiclesDataVM> {

    renderEdit(): JSX.Element {
        return (
            <>
                <FieldFormUI title={this.getResourceByProperty(m => m.policeDepartment)} required>
                    <PoliceDepartmentUI {...this.bind(m => m.policeDepartment, ViewMode.Display)} />
                </FieldFormUI>
                <FieldFormUI title={this.getResourceByProperty(m => m.applicationText)} required>
                    <div className="row">
                        <div className="form-group col-12">
                            {this.textAreaFor(m => m.applicationText)}
                            <div className="help-text-inline" id="DF-1">
                                {this.getResource("DOC_KAT_ApplicationForMountingOfRegistrationPlatesAndOrIdentificationOfVehiclesData_applicationText_I")}
                            </div>
                        </div>
                    </div>
                </FieldFormUI>
                <FieldFormUI title={this.getResourceByProperty(m => m.phone)} required>
                    <div className="row">
                        <div className="form-group col-sm-6">
                            {this.textBoxFor(m => m.phone)}
                        </div>
                    </div>
                </FieldFormUI>
            </>
        );
    }

    renderDisplay(): JSX.Element {
        return (
            <>
                <FieldFormUI title={this.getResourceByProperty(m => m.policeDepartment)} headerType="h4" >
                    <PoliceDepartmentUI {...this.bind(m => m.policeDepartment, ViewMode.Display)} />
                </FieldFormUI>
                <FieldFormUI title={this.getResourceByProperty(m => m.applicationText)} headerType="h4">
                    {this.textDisplayFor(m => m.applicationText)}
                </FieldFormUI>
                <FieldFormUI title={this.getResourceByProperty(m => m.phone)} headerType="h4">
                    {this.textDisplayFor(m => m.phone)}
                </FieldFormUI>
            </>
        );
    }
}

export const ApplicationForMountingOfRegistrationPlatesAndOrIdentificationOfVehiclesDataUI = withDocumentFormManager(ApplicationForMountingOfRegistrationPlatesAndOrIdentificationOfVehiclesDataImpl)