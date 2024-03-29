﻿import { EAUBaseValidator } from "eau-core";
import { DocumentFormValidationContext, TravelDocumentValidator } from "eau-documents";
import { ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensDataVM } from "../models/ModelsAutoGenerated";
import { ForeignIdentityBasicDataValidator } from "./ForeignIdentityBasicDataValidator";

export class ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensDataValidator
    extends EAUBaseValidator<ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensDataVM, DocumentFormValidationContext> {

    constructor() {
        super();

        this.ruleFor(m => m.foreignIdentityBasicData).setValidator(new ForeignIdentityBasicDataValidator());
        this.ruleFor(m => m.travelDocument).setValidator(new TravelDocumentValidator());
    }
}