import { EAUBaseValidator, ResourceHelpers, ErrMsgCodesConstants } from "eau-core";
import { DocumentFormValidationContext } from "./DocumentFormValidationContext";
import { DocumentMustServeToVM, ItemChoiceType1 } from "../models";

export class DocumentMustServeToVMValidator extends EAUBaseValidator<DocumentMustServeToVM, DocumentFormValidationContext> {
    constructor() {
        super();

        this.ruleFor(m => m.itemAbroadDocumentMustServeTo)
            .notEmpty()
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.DefaultNotEmptyErrorMessage, new DocumentMustServeToVM(), 'itemAbroadDocumentMustServeTo')).when(m => m.itemElementName == ItemChoiceType1.AbroadDocumentMustServeTo);

        this.ruleFor(m => m.itemAbroadDocumentMustServeTo).matches("^[а-яА-Яa-zA-Z\\s+\\d+~@#$%^&*()_{}|\"':>=|!<.,/\\\\?;-]+$")
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.FieldValidationCyrillicLatinNumbersSymbols, new DocumentMustServeToVM(), 'itemAbroadDocumentMustServeTo'));

        this.ruleFor(m => m.itemInRepublicOfBulgariaDocumentMustServeTo).notEmpty()
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.DefaultNotEmptyErrorMessage, new DocumentMustServeToVM(), 'itemInRepublicOfBulgariaDocumentMustServeTo')).when(m => m.itemElementName == ItemChoiceType1.InRepublicOfBulgariaDocumentMustServeTo);

        this.ruleFor(m => m.itemInRepublicOfBulgariaDocumentMustServeTo).matches("^[а-яА-Яa-zA-Z\\s+\\d+~@#$%^&*()_{}|\"':>=|!<.,/\\\\?;-]+$")
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.FieldValidationCyrillicLatinNumbersSymbols, new DocumentMustServeToVM(), 'itemInRepublicOfBulgariaDocumentMustServeTo'));
    }
}