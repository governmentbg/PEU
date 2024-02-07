﻿import { EAUBaseValidator, ResourceHelpers, resourceManager, ErrMsgCodesConstants } from "eau-core";
import { DocumentFormValidationContext } from "eau-documents";
import { ProtectionPersonsProperty } from "../../models/ModelsAutoGenerated";
import { SecurityTransportValidator } from "../SecurityTransportValidator";

export class ProtectionPersonsProperty_NValidator
    extends EAUBaseValidator<ProtectionPersonsProperty, DocumentFormValidationContext> {

    constructor() {
        super();

        this.ruleFor(x => x.actualDate).notEmpty()
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.DefaultNotEmptyErrorMessage, new ProtectionPersonsProperty(), 'actualDate'));

        this.ruleFor(x => x.actualDate).isValidDate()
            .withMessage(resourceManager.getResourceByKey("DOC_GL_WRONG_DATE_AND_TIME_E"));

        this.ruleFor(x => x.securityObjectName).notEmpty()
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.DefaultNotEmptyErrorMessage, new ProtectionPersonsProperty(), 'securityObjectName'));

        this.ruleFor(m => m.securityObjectName).matches("^[а-яА-Яa-zA-Z\\s+\\d+~@#$%^&*()_{}|\"':>=|!<.,/\\\\?;-]+$")
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.FieldValidationCyrillicLatinNumbersSymbols, new ProtectionPersonsProperty(), 'securityObjectName'));

        this.ruleFor(x => x.securityObjectName).length(1, 150)
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.FieldCanNotContainsMoreThanSymbols, new ProtectionPersonsProperty(), 'securityObjectName', 150));

        this.ruleFor(x => x.address).notEmpty()
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.DefaultNotEmptyErrorMessage, new ProtectionPersonsProperty(), 'address'));

        this.ruleFor(m => m.address).matches("^[а-яА-Яa-zA-Z\\s+\\d+~№@#$%^&*()_{}|\"':>=|!<.,/\\\\?;-]+$")
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.FieldValidationCyrillicLatinNumbersSymbols2, new ProtectionPersonsProperty(), 'address'));

        this.ruleFor(x => x.address).length(1, 250)
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.FieldCanNotContainsMoreThanSymbols, new ProtectionPersonsProperty(), 'address', 250));

        this.ruleFor(x => x.securityType).notEmpty()
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.DefaultNotEmptyErrorMessage, new ProtectionPersonsProperty(), 'securityType'));

        this.ruleFor(x => x.securityTransports).setCollectionValidator(new SecurityTransportValidator());

        this.ruleFor(x => x.aischodDistrictId).notEmpty()
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.DefaultNotEmptyErrorMessage, new ProtectionPersonsProperty(), 'aischodDistrictId'));

        this.ruleFor(x => x.aischodAccessRegimeTypeId).notEmpty()
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.DefaultNotEmptyErrorMessage, new ProtectionPersonsProperty(), 'aischodAccessRegimeTypeId'));

        this.ruleFor(x => x.aischodControlTypeId).notEmpty()
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.DefaultNotEmptyErrorMessage, new ProtectionPersonsProperty(), 'aischodControlTypeId'));
    }
}

export class ProtectionPersonsProperty_TValidator
    extends EAUBaseValidator<ProtectionPersonsProperty, DocumentFormValidationContext> {

    constructor() {
        super();

        this.ruleFor(x => x.terminationDate).notEmpty()
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.DefaultNotEmptyErrorMessage, new ProtectionPersonsProperty(), 'terminationDate'));

        this.ruleFor(x => x.terminationDate).isValidDate()
            .withMessage(resourceManager.getResourceByKey("DOC_GL_WRONG_DATE_AND_TIME_E"));

        this.ruleFor(x => x.securityObjectName).notEmpty()
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.DefaultNotEmptyErrorMessage, new ProtectionPersonsProperty(), 'securityObjectName'));

        this.ruleFor(m => m.securityObjectName).matches("^[а-яА-Яa-zA-Z\\s+\\d+~@#$%^&*()_{}|\"':>=|!<.,/\\\\?;-]+$")
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.FieldValidationCyrillicLatinNumbersSymbols, new ProtectionPersonsProperty(), 'securityObjectName'));

        this.ruleFor(x => x.securityObjectName).length(1, 150)
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.FieldCanNotContainsMoreThanSymbols, new ProtectionPersonsProperty(), 'securityObjectName', 150));

        this.ruleFor(x => x.aischodDistrictId).notEmpty()
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.DefaultNotEmptyErrorMessage, new ProtectionPersonsProperty(), 'aischodDistrictId'));

        this.ruleFor(m => m.address).matches("^[а-яА-Яa-zA-Z\\s+\\d+~@№#$%^&*()_{}|\"':>=|!<.,/\\\\?;-]+$")
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.FieldValidationCyrillicLatinNumbersSymbols2, new ProtectionPersonsProperty(), 'address'));

        this.ruleFor(x => x.address).length(1, 250)
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.FieldCanNotContainsMoreThanSymbols, new ProtectionPersonsProperty(), 'address', 250));

        this.ruleFor(x => x.contractTypeNumberDate).notEmpty()
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.DefaultNotEmptyErrorMessage, new ProtectionPersonsProperty(), 'contractTypeNumberDate'));

        this.ruleFor(x => x.contractTypeNumberDate).length(1, 50)
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.FieldCanNotContainsMoreThanSymbols, new ProtectionPersonsProperty(), 'contractTypeNumberDate', 50));

        this.ruleFor(m => m.contractTerminationNote).length(0, 150)
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.FieldCanNotContainsMoreThanSymbols, new ProtectionPersonsProperty(), 'contractTerminationNote', 150));
    }
}