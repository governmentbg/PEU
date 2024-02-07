﻿import { BaseProps, ViewMode } from "cnsys-ui-react";
import { attributesClassFormControlLabel, EAUBaseComponent, ResourceHelpers } from "eau-core";
import { FieldFormUI, GraoAddressUI, IdentityDocumentType, TravelDocumentUI } from "eau-documents";
import React from "react";
import { ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensDataVM } from "../../models/ModelsAutoGenerated";
import { ForeignIdentityBasicDataUI } from "../field-forms/ForeignIdentityBasicDataUI";

export class ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensDataUI
    extends EAUBaseComponent<BaseProps, ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensDataVM> {

    renderEdit(): JSX.Element {
        return <>
            {
                this.model.identityDocumentsType && this.model.identityDocumentsType.length > 0
                && <FieldFormUI title={this.getResourceByProperty(m => m.identityDocumentsType)}>
                    <div className="form-group">
                        <p className="field-text"> {ResourceHelpers.getResourceByEmun(this.model.identityDocumentsType[0], IdentityDocumentType).toUpperCase()}</p>
                    </div>
                </FieldFormUI>
            }
            <FieldFormUI title={this.getResourceByProperty(m => m.foreignIdentityBasicData)}>
                <ForeignIdentityBasicDataUI {...this.bind(m => m.foreignIdentityBasicData)} hideBIDEyesColorSection hideHeight />
                <div className="row">
                    <div className="form-group col-12">
                        {this.labelFor(m => m.address, null, attributesClassFormControlLabel)}
                        <GraoAddressUI {...this.bind(m => m.address)} viewMode={ViewMode.Display} />
                    </div>
                </div>
            </FieldFormUI>
            {
                this.model.travelDocument && <FieldFormUI title={this.getResourceByProperty(m => m.travelDocument)}>
                    <TravelDocumentUI {...this.bind(m => m.travelDocument)} viewMode={ViewMode.Display} />
                </FieldFormUI>
            }
        </>
    }

    renderDisplay(): JSX.Element {
        return <>
            {
                this.model.identityDocumentsType && this.model.identityDocumentsType.length > 0
                && <FieldFormUI title={this.getResourceByProperty(m => m.identityDocumentsType)}>
                    <div className="form-group">
                        <p className="field-text"> {ResourceHelpers.getResourceByEmun(this.model.identityDocumentsType[0], IdentityDocumentType).toUpperCase()}</p>
                    </div>
                </FieldFormUI>
            }
            <FieldFormUI title={this.getResourceByProperty(m => m.foreignIdentityBasicData)} required>
                <ForeignIdentityBasicDataUI {...this.bind(m => m.foreignIdentityBasicData)} hideBIDEyesColorSection hideHeight />
                <div className="row">
                    <div className="form-group col-12">
                        {this.labelFor(m => m.address, null, attributesClassFormControlLabel)}
                        <GraoAddressUI {...this.bind(m => m.address)} viewMode={ViewMode.Display} />
                    </div>
                </div>
            </FieldFormUI>
            {
                this.model.travelDocument && <FieldFormUI title={this.getResourceByProperty(m => m.travelDocument)}>
                    <TravelDocumentUI {...this.bind(m => m.travelDocument)} />
                </FieldFormUI>
            }
        </>
    }
}