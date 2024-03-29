﻿import { EAUBaseValidator } from "eau-core";
import { DeclarationsVMValidator, DocumentFormValidationContext, ElectronicServiceApplicantVMValidator, ServiceTermTypeAndApplicantReceiptVMValidator } from "eau-documents";
import { ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensVM } from "../../models/ModelsAutoGenerated";
import { ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensDataValidator } from "../ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensDataValidator";

export class ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensValidator
    extends EAUBaseValidator<ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensVM, DocumentFormValidationContext> {

    constructor() {
        super();

        this.ruleFor(m => m.electronicServiceApplicant).setValidator(new ElectronicServiceApplicantVMValidator());
        this.ruleFor(m => m.circumstances).setValidator(new ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensDataValidator());
        this.ruleFor(m => m.declarations).setValidator(new DeclarationsVMValidator());
        this.ruleFor(m => m.serviceTermTypeAndApplicantReceipt).setValidator(new ServiceTermTypeAndApplicantReceiptVMValidator());
    }
}