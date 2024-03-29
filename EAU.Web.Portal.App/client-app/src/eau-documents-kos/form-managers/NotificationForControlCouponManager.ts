﻿import { ResourceHelpers } from 'eau-core';
import { ApplicationFormManagerBase, DocumentFormValidationContext, Section, PoliceDepartment } from 'eau-documents';
import { action } from 'mobx';
import { NotificationForControlCouponVM } from '../models/ModelsAutoGenerated';
import { NotificationForControlCouponDataUI } from '../ui/section-forms/NotificationForControlCouponDataUI';
import { NotificationForControlCouponDataValidator } from '../validations/NotificationForControlCouponDataValidator';

export class NotificationForControlCouponManager extends ApplicationFormManagerBase<NotificationForControlCouponVM>{

    //#region ApplicationFormManagerBase

    protected createDocument(obj: any): NotificationForControlCouponVM {
        return new NotificationForControlCouponVM(obj);
    }

    //#endregion

    @action protected initDocumentForm() {
        super.initDocumentForm();

        if (!this.documentForm.circumstances.licenseInfo.issuingPoliceDepartment)
            this.documentForm.circumstances.licenseInfo.issuingPoliceDepartment = new PoliceDepartment();
    }

    protected createSections(validationContext: DocumentFormValidationContext): Section[] {
        var sections = super.createSections(validationContext);

        var circumstances = new Section();
        circumstances.code = "circumstances";
        circumstances.title = ResourceHelpers.getResourceByProperty(m => m.circumstances, this.documentForm);
        circumstances.form = this.documentForm.circumstances;
        circumstances.formUICmp = NotificationForControlCouponDataUI;
        circumstances.validator = new NotificationForControlCouponDataValidator();
        circumstances.validator.setValidationContext(validationContext);
        circumstances.validate = () => this.validateSection(circumstances);

        sections.splice(1, 0, circumstances);

        return sections;
    }
}