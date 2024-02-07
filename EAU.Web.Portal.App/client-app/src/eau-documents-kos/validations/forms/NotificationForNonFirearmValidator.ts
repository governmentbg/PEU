﻿import { EAUBaseValidator } from "eau-core";
import { DeclarationsVMValidator, DocumentFormValidationContext, ElectronicServiceApplicantVMValidator, ServiceTermTypeAndApplicantReceiptVMValidator } from "eau-documents";
import { NotificationForNonFirearmVM } from "../../models/ModelsAutoGenerated";
import { NotificationForNonFirearmDataValidator } from "../NotificationForNonFirearmDataValidator";

export class NotificationForNonFirearmValidator
    extends EAUBaseValidator<NotificationForNonFirearmVM, DocumentFormValidationContext> {

    constructor() {
        super();

        this.ruleFor(m => m.electronicServiceApplicant).setValidator(new ElectronicServiceApplicantVMValidator());
        this.ruleFor(m => m.circumstances).setValidator(new NotificationForNonFirearmDataValidator());
        this.ruleFor(m => m.declarations).setValidator(new DeclarationsVMValidator());
        this.ruleFor(m => m.serviceTermTypeAndApplicantReceipt).setValidator(new ServiceTermTypeAndApplicantReceiptVMValidator());
    }
}