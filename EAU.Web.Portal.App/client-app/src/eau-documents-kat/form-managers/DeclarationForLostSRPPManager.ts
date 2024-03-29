﻿import { ArrayHelper } from 'cnsys-core';
import { ResourceHelpers } from 'eau-core';
import { ApplicationFormManagerBase, DocumentFormValidationContext, Section } from 'eau-documents';
import { DeclarationForLostSRPPSVM } from '../models/ModelsAutoGenerated';
import { DeclarationForLostSRPPSDataUI } from '../ui/section-forms/DeclarationForLostSRPPSDataUI';
import { DeclarationForLostSRPPSDataValidator } from '../validations/DeclarationForLostSRPPSDataValidator';

export class DeclarationForLostSRPPManager extends ApplicationFormManagerBase<DeclarationForLostSRPPSVM>{

    //#region ApplicationFormManagerBase

    createDocument(obj: any): DeclarationForLostSRPPSVM {
        return new DeclarationForLostSRPPSVM(obj);
    }

    //#endregion

    protected initDocumentForm() {
        super.initDocumentForm();
    }

    protected createSections(validationContext: DocumentFormValidationContext): Section[] {
        var sections = super.createSections(validationContext);

        var circumstances = new Section();
        circumstances.code = "circumstances";
        circumstances.title = ResourceHelpers.getResourceByProperty(m => m.circumstances, this.documentForm);
        circumstances.form = this.documentForm.circumstances;
        circumstances.formUICmp = DeclarationForLostSRPPSDataUI;
        circumstances.validator = new DeclarationForLostSRPPSDataValidator();
        circumstances.validator.setValidationContext(validationContext);
        circumstances.validate = () => this.validateSection(circumstances);

        sections.splice(1, 0, circumstances);

        //Премахваме секциите "Начин на получаване", "Декларации" и "Приложени документи".
        let sectionCodesToRemove = ['wayOfDelivery', 'declarations', 'attachedDocuments'];
        let sectionForRemoves = ArrayHelper.queryable.from(sections).where(el => sectionCodesToRemove.indexOf(el.code) >= 0).toArray();

        if (sectionForRemoves && sectionForRemoves.length > 0) {
            for (let i: number = 0; i < sectionForRemoves.length; i++) {
                ArrayHelper.removeElement(sections, sectionForRemoves[i]);
            }
        }

        return sections;
    }
}