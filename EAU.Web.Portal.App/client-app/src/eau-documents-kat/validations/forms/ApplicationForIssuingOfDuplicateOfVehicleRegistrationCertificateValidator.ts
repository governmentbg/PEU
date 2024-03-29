﻿import { EAUBaseValidator } from "eau-core";
import { ApplicationForIssuingOfDuplicateOfVehicleRegistrationCertificateVM } from "../../models/ModelsAutoGenerated";
import { DocumentFormValidationContext, ElectronicServiceApplicantVMValidator, DeclarationsVMValidator, ServiceTermTypeAndApplicantReceiptVMValidator } from "eau-documents";
import { VehicleDataRequestValidator } from '../VehicleDataRequestValidator'

export class ApplicationForIssuingOfDuplicateOfVehicleRegistrationCertificateValidator extends EAUBaseValidator<ApplicationForIssuingOfDuplicateOfVehicleRegistrationCertificateVM, DocumentFormValidationContext> {
    constructor() {
        super();

        this.ruleFor(m => m.electronicServiceApplicant).setValidator(new ElectronicServiceApplicantVMValidator());
        this.ruleFor(m => m.circumstances).setValidator(new VehicleDataRequestValidator());
        this.ruleFor(m => m.declarations).setValidator(new DeclarationsVMValidator());
        this.ruleFor(m => m.serviceTermTypeAndApplicantReceipt).setValidator(new ServiceTermTypeAndApplicantReceiptVMValidator());
    }
}