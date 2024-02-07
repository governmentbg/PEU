﻿import { DocumentFormValidationContext, Section, DocumentFormManagerBase } from 'eau-documents'
import { ResourceHelpers } from 'eau-core';
import { CertificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDVM } from '../models/ModelsAutoGenerated';
import { CertificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDValidator } from '../validations/forms/CertificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDValidator';
import { CertificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDUI } from '../ui/document-forms/CertificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDUI'

export class CertificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDManager extends DocumentFormManagerBase<CertificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDVM>{

    //#region ApplicationFormManagerBase

    createDocument(obj: any): CertificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDVM {
        return new CertificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDVM(obj);
    }

    //#endregion   

    protected createSections(validationContext: DocumentFormValidationContext): Section[] {
        var sections = super.createSections(validationContext);

        var main = new Section();
        main.code = "main";
        main.title = ResourceHelpers.getResourceByProperty(m => m, CertificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDVM);
        main.form = this.documentForm;
        main.formUICmp = CertificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDUI;
        main.validator = new CertificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDValidator();
        main.validator.setValidationContext(validationContext);
        main.validate = () => this.validateSection(main);

        sections.push(main);

        return sections;
    }
}