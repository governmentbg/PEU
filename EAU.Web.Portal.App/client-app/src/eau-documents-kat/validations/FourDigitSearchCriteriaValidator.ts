import { ObjectHelper } from "cnsys-core";
import { EAUBaseValidator, ErrMsgCodesConstants, ResourceHelpers } from "eau-core";
import { DocumentFormValidationContext } from "eau-documents";
import { FourDigitSearchCriteria, FourDigitSearchTypes } from "../models/ModelsManualAdded";
import { moduleContext } from "../ModuleContext";

export class FourDigitSearchCriteriaValidator extends EAUBaseValidator<FourDigitSearchCriteria, DocumentFormValidationContext> {
    constructor() {
        super();

        this.ruleFor(m => m.policeDepartment)
            .notEmpty()
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.DefaultNotEmptyErrorMessage, new FourDigitSearchCriteria(), 'policeDepartment'));

        this.ruleFor(m => m.plateStatus)
            .notEmpty()
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.DefaultNotEmptyErrorMessage, new FourDigitSearchCriteria(), 'plateStatus'));

        this.ruleFor(m => m.fromRegNumber)
            .notEmpty()
            .when(m => m.fourDigitSearchType == FourDigitSearchTypes.ByInterval && !ObjectHelper.isStringNullOrEmpty(m.toRegNumber))
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.DefaultNotEmptyErrorMessage, new FourDigitSearchCriteria(), 'fromRegNumber'));

        this.ruleFor(m => m.toRegNumber)
            .notEmpty()
            .when(m => m.fourDigitSearchType == FourDigitSearchTypes.ByInterval && !ObjectHelper.isStringNullOrEmpty(m.fromRegNumber))
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.DefaultNotEmptyErrorMessage, new FourDigitSearchCriteria(), 'toRegNumber'));

        this.ruleFor(m => m.fromRegNumber)
            .matches("\\d{4}")
            .when(m => m.fourDigitSearchType == FourDigitSearchTypes.ByInterval)
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.FieldMustContainsExaclyNumSymbols, new FourDigitSearchCriteria(), 'fromRegNumber', moduleContext.resourceManager.getResourceByKey('GL_EXACTLI_4_DIGITS_I')));

        this.ruleFor(m => m.toRegNumber)
            .matches("\\d{4}")
            .when(m => m.fourDigitSearchType == FourDigitSearchTypes.ByInterval)
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.FieldMustContainsExaclyNumSymbols, new FourDigitSearchCriteria(), 'toRegNumber', moduleContext.resourceManager.getResourceByKey('GL_EXACTLI_4_DIGITS_I')));

        this.ruleFor(m => m.fromRegNumber)
            .must(obj => {
                if (ObjectHelper.isStringNullOrEmpty(obj.toRegNumber)
                    || ObjectHelper.isStringNullOrEmpty(obj.fromRegNumber)
                    || !/\d{4}/g.test(obj.fromRegNumber) 
                    || !/\d{4}/g.test(obj.toRegNumber))
                    return true;
                
                return Number(obj.fromRegNumber) <= Number(obj.toRegNumber);
            })
            .when(m => m.fourDigitSearchType == FourDigitSearchTypes.ByInterval)
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.FiledLessThenOrEqual, new FourDigitSearchCriteria(), 'fromRegNumber', ResourceHelpers.getResourceByProperty('toRegNumber', new FourDigitSearchCriteria())));;

        this.ruleFor(m => m.specificRegNumber)
            .notEmpty()
            .when(m => m.fourDigitSearchType == FourDigitSearchTypes.ByRegNumber)
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.DefaultNotEmptyErrorMessage, new FourDigitSearchCriteria(), 'specificRegNumber'));

        this.ruleFor(m => m.specificRegNumber)
            .matches("\\d{4}")
            .when(m => m.fourDigitSearchType == FourDigitSearchTypes.ByRegNumber)
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.FieldMustContainsExaclyNumSymbols, new FourDigitSearchCriteria(), 'specificRegNumber', '4'));
    }
}