﻿import { EAUBaseValidator } from "eau-core";
import { DeclarationsVMValidator, DocumentFormValidationContext, ElectronicServiceApplicantVMValidator, ServiceTermTypeAndApplicantReceiptVMValidator } from "eau-documents";
import { ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensVM } from "../../models/ModelsAutoGenerated";
import { ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensDataValidator } from "../ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensDataValidator";

export class ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensValidator
    extends EAUBaseValidator<ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensVM, DocumentFormValidationContext> {

    constructor() {
        super();

        this.ruleFor(m => m.electronicServiceApplicant).setValidator(new ElectronicServiceApplicantVMValidator());
        this.ruleFor(m => m.circumstances).setValidator(new ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensDataValidator());
        this.ruleFor(m => m.declarations).setValidator(new DeclarationsVMValidator());
        this.ruleFor(m => m.serviceTermTypeAndApplicantReceipt).setValidator(new ServiceTermTypeAndApplicantReceiptVMValidator());
    }
}