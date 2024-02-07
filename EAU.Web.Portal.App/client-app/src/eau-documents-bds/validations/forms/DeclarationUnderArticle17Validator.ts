﻿import { EAUBaseValidator } from "eau-core";
import { DeclarationsVMValidator, DocumentFormValidationContext, ElectronicServiceApplicantVMValidator, ServiceTermTypeAndApplicantReceiptVMValidator } from "eau-documents";
import { DeclarationUndurArticle17VM } from "../../models/ModelsAutoGenerated";
import { DeclarationUnderArticle17DataValidator } from "../DeclarationUnderArticle17DataValidator";

export class DeclarationUnderArticle17Validator extends EAUBaseValidator<DeclarationUndurArticle17VM, DocumentFormValidationContext> {

    constructor() {
        super();

        this.ruleFor(m => m.electronicServiceApplicant).setValidator(new ElectronicServiceApplicantVMValidator());
        this.ruleFor(m => m.circumstances).setValidator(new DeclarationUnderArticle17DataValidator());
        this.ruleFor(m => m.declarations).setValidator(new DeclarationsVMValidator());
        this.ruleFor(m => m.serviceTermTypeAndApplicantReceipt).setValidator(new ServiceTermTypeAndApplicantReceiptVMValidator());
    }
}