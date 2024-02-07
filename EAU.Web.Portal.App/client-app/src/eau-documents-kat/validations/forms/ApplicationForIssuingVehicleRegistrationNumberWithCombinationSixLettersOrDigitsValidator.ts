﻿import { EAUBaseValidator } from "eau-core";
import { ApplicationForIssuingVehicleRegistrationNumberWithCombinationSixLettersOrDigitsVM } from "../../models/ModelsAutoGenerated";
import { DocumentFormValidationContext, ElectronicServiceApplicantVMValidator, DeclarationsVMValidator, ServiceTermTypeAndApplicantReceiptVMValidator } from "eau-documents";
import { ApplicationForIssuingVehicleRegistrationNumberWithCombinationSixLettersOrDigitsDataValidator } from '../ApplicationForIssuingVehicleRegistrationNumberWithCombinationSixLettersOrDigitsDataValidator'

export class ApplicationForIssuingVehicleRegistrationNumberWithCombinationSixLettersOrDigitsValidator
    extends EAUBaseValidator<ApplicationForIssuingVehicleRegistrationNumberWithCombinationSixLettersOrDigitsVM, DocumentFormValidationContext> {
    constructor() {
        super();

        this.ruleFor(m => m.electronicServiceApplicant).setValidator(new ElectronicServiceApplicantVMValidator());
        this.ruleFor(m => m.circumstances).setValidator(new ApplicationForIssuingVehicleRegistrationNumberWithCombinationSixLettersOrDigitsDataValidator());
        this.ruleFor(m => m.declarations).setValidator(new DeclarationsVMValidator());
        this.ruleFor(m => m.serviceTermTypeAndApplicantReceipt).setValidator(new ServiceTermTypeAndApplicantReceiptVMValidator());
    }
}