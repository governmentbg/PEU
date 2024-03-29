﻿import { EAUBaseValidator } from "eau-core";
import { ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesVM } from "../../models/ModelsAutoGenerated";
import { DocumentFormValidationContext, ElectronicServiceApplicantVMValidator, DeclarationsVMValidator, ServiceTermTypeAndApplicantReceiptVMValidator } from "eau-documents";
import { ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesDataVMValidator } from "../ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesDataVMValidator";


export class ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesValidator extends EAUBaseValidator<ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesVM, DocumentFormValidationContext> {
    constructor() {
        super();

        this.ruleFor(m => m.electronicServiceApplicant).setValidator(new ElectronicServiceApplicantVMValidator());
        this.ruleFor(m => m.circumstances).setValidator(new ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesDataVMValidator());
        this.ruleFor(m => m.declarations).setValidator(new DeclarationsVMValidator());
        this.ruleFor(m => m.serviceTermTypeAndApplicantReceipt).setValidator(new ServiceTermTypeAndApplicantReceiptVMValidator());
    }
}