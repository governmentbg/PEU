﻿import { EAUBaseValidator, resourceManager, ResourceHelpers, ErrMsgCodesConstants } from "eau-core";
import { DocumentFormValidationContext } from "eau-documents";
import { CertificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDVM } from "../../models/ModelsAutoGenerated";

export class CertificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDValidator extends EAUBaseValidator<CertificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDVM, DocumentFormValidationContext> {
    constructor() {
        super();

        this.ruleFor(m => m.reportDate).isValidDate().withMessage(resourceManager.getResourceByKey("DOC_GL_WRONG_DATE_AND_TIME_E"));
        this.ruleFor(m => m.reportDate).notEmpty()
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.DefaultNotEmptyErrorMessage, new CertificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDVM(), 'reportDate'));
    }
}