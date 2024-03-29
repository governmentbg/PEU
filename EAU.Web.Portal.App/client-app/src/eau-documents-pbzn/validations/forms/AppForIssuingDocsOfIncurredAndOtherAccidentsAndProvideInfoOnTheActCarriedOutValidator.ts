﻿import { EAUBaseValidator } from "eau-core";
import { ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutVM } from "../../models/ModelsAutoGenerated";
import { DocumentFormValidationContext, ElectronicServiceApplicantVMValidator, DeclarationsVMValidator, ServiceTermTypeAndApplicantReceiptVMValidator } from "eau-documents";
import { AppForIssuingDocsOfIncurredAndOtherAccidentsAndProvideInfoOnTheActCarriedOutDataVMValidator } from "../AppForIssuingDocsOfIncurredAndOtherAccidentsAndProvideInfoOnTheActCarriedOutDataVMValidator";


export class AppForIssuingDocsOfIncurredAndOtherAccidentsAndProvideInfoOnTheActCarriedOutValidator extends EAUBaseValidator<ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutVM, DocumentFormValidationContext> {
    constructor() {
        super();

        this.ruleFor(m => m.electronicServiceApplicant).setValidator(new ElectronicServiceApplicantVMValidator());
        this.ruleFor(m => m.circumstances).setValidator(new AppForIssuingDocsOfIncurredAndOtherAccidentsAndProvideInfoOnTheActCarriedOutDataVMValidator());
        this.ruleFor(m => m.declarations).setValidator(new DeclarationsVMValidator());
        this.ruleFor(m => m.serviceTermTypeAndApplicantReceipt).setValidator(new ServiceTermTypeAndApplicantReceiptVMValidator());
    }
}