﻿import { EAUBaseValidator, ErrMsgCodesConstants, ResourceHelpers } from "eau-core";
import { DocumentFormValidationContext, PersonalInformationValidator, PersonBasicDataVMValidator, PoliceDepartmentValidator } from "eau-documents";
import { ApplicationByFormAnnex10DataVM, ApplicationByFormAnnex9DataVM } from "../models/ModelsAutoGenerated";

export class ApplicationByFormAnnex9DataValidator extends EAUBaseValidator<ApplicationByFormAnnex9DataVM, DocumentFormValidationContext> {

    constructor() {
        super();

        this.ruleFor(m => m.personalInformation).setValidator(new PersonalInformationValidator());
        this.ruleFor(m => m.issuingPoliceDepartment).setValidator(new PoliceDepartmentValidator());
        this.ruleFor(m => m.personGrantedFromIssuingDocument).setValidator(new PersonBasicDataVMValidator(true)).when(x => x.servicesWithOuterDocumentForThirdPerson);
        this.ruleFor(m => m.specificDataForIssuingDocumentsForKOS).notEmpty()
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.DefaultNotEmptyErrorMessage, new ApplicationByFormAnnex10DataVM(), 'specificDataForIssuingDocumentsForKOS'))
            .when(m => m.isSpecificDataForIssuingDocumentsForKOSRequired === true);
    }
}