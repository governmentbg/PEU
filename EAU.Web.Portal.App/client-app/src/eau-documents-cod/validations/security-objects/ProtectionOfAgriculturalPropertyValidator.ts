﻿import { EAUBaseValidator, ResourceHelpers, resourceManager, ErrMsgCodesConstants } from "eau-core";
import { DocumentFormValidationContext } from "eau-documents";
import { ProtectionOfAgriculturalProperty } from "../../models/ModelsAutoGenerated";
import { SecurityTransportRequiredValidator } from "../SecurityTransportValidator";

export class ProtectionOfAgriculturalProperty_NValidator
    extends EAUBaseValidator<ProtectionOfAgriculturalProperty, DocumentFormValidationContext> {

    constructor() {
        super();

        this.ruleFor(x => x.actualDate).notEmpty()
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.DefaultNotEmptyErrorMessage, new ProtectionOfAgriculturalProperty(), 'actualDate'));

        this.ruleFor(x => x.actualDate).isValidDate()
            .withMessage(resourceManager.getResourceByKey("DOC_GL_WRONG_DATE_AND_TIME_E"));

        this.ruleFor(x => x.securityObjectName).notEmpty()
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.DefaultNotEmptyErrorMessage, new ProtectionOfAgriculturalProperty(), 'securityObjectName'));

        this.ruleFor(m => m.securityObjectName).matches("^[а-яА-Яa-zA-Z\\s+\\d+~@#$%^&*()_{}|\"':>=|!<.,/\\\\?;-]+$")
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.FieldValidationCyrillicLatinNumbersSymbols, new ProtectionOfAgriculturalProperty(), 'securityObjectName'));

        this.ruleFor(x => x.securityObjectName).length(1, 150)
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.FieldCanNotContainsMoreThanSymbols, new ProtectionOfAgriculturalProperty(), 'securityObjectName', 150));

        this.ruleFor(x => x.address).notEmpty()
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.DefaultNotEmptyErrorMessage, new ProtectionOfAgriculturalProperty(), 'address'));

        this.ruleFor(m => m.address).matches("^[а-яА-Яa-zA-Z\\s+\\d+~@№#$%^&*()_{}|\"':>=|!<.,/\\\\?;-]+$")
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.FieldValidationCyrillicLatinNumbersSymbols2, new ProtectionOfAgriculturalProperty(), 'address'));

        this.ruleFor(x => x.address).length(1, 250)
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.FieldCanNotContainsMoreThanSymbols, new ProtectionOfAgriculturalProperty(), 'address', 250));

        this.ruleFor(x => x.securityType).notEmpty()
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.DefaultNotEmptyErrorMessage, new ProtectionOfAgriculturalProperty(), 'securityType'));

        this.ruleFor(x => x.securityTransports).setCollectionValidator(new SecurityTransportRequiredValidator());

        this.ruleFor(x => x.aischodDistrictId).notEmpty()
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.DefaultNotEmptyErrorMessage, new ProtectionOfAgriculturalProperty(), 'aischodDistrictId'));
    }
}

export class ProtectionOfAgriculturalProperty_TValidator
    extends EAUBaseValidator<ProtectionOfAgriculturalProperty, DocumentFormValidationContext> {

    constructor() {
        super();

        this.ruleFor(x => x.terminationDate).notEmpty()
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.DefaultNotEmptyErrorMessage, new ProtectionOfAgriculturalProperty(), 'terminationDate'));

        this.ruleFor(x => x.terminationDate).isValidDate()
            .withMessage(resourceManager.getResourceByKey("DOC_GL_WRONG_DATE_AND_TIME_E"));

        this.ruleFor(x => x.securityObjectName).notEmpty()
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.DefaultNotEmptyErrorMessage, new ProtectionOfAgriculturalProperty(), 'securityObjectName'));

        this.ruleFor(m => m.securityObjectName).matches("^[а-яА-Яa-zA-Z\\s+\\d+~@#$%^&*()_{}|\"':>=|!<.,/\\\\?;-]+$")
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.FieldValidationCyrillicLatinNumbersSymbols, new ProtectionOfAgriculturalProperty(), 'securityObjectName'));

        this.ruleFor(x => x.securityObjectName).length(1, 150)
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.FieldCanNotContainsMoreThanSymbols, new ProtectionOfAgriculturalProperty(), 'securityObjectName', 150));

        this.ruleFor(x => x.aischodDistrictId).notEmpty()
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.DefaultNotEmptyErrorMessage, new ProtectionOfAgriculturalProperty(), 'aischodDistrictId'));

        this.ruleFor(m => m.address).matches("^[а-яА-Яa-zA-Z\\s+\\d+~@№#$%^&*()_{}|\"':>=|!<.,/\\\\?;-]+$")
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.FieldValidationCyrillicLatinNumbersSymbols2, new ProtectionOfAgriculturalProperty(), 'address'));

        this.ruleFor(x => x.address).length(1, 250)
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.FieldCanNotContainsMoreThanSymbols, new ProtectionOfAgriculturalProperty(), 'address', 250));

        this.ruleFor(x => x.contractTypeNumberDate).notEmpty()
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.DefaultNotEmptyErrorMessage, new ProtectionOfAgriculturalProperty(), 'contractTypeNumberDate'));

        this.ruleFor(x => x.contractTypeNumberDate).length(1, 50)
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.FieldCanNotContainsMoreThanSymbols, new ProtectionOfAgriculturalProperty(), 'contractTypeNumberDate', 50));

        this.ruleFor(m => m.contractTerminationNote).length(0, 150)
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.FieldCanNotContainsMoreThanSymbols, new ProtectionOfAgriculturalProperty(), 'contractTerminationNote', 150));
    }
}