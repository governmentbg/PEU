﻿import { EAUBaseValidator } from "eau-core";
import { RequestForApplicationForIssuingDuplicateDrivingLicenseVM } from "../../models/ModelsAutoGenerated";
import { DocumentFormValidationContext, ElectronicServiceApplicantVMValidator } from "eau-documents";


export class RequestForApplicationForIssuingDuplicateDrivingLicenseValidator extends EAUBaseValidator<RequestForApplicationForIssuingDuplicateDrivingLicenseVM, DocumentFormValidationContext> {
    constructor() {
        super();

        this.ruleFor(m => m.electronicServiceApplicant).setValidator(new ElectronicServiceApplicantVMValidator());
    }
}