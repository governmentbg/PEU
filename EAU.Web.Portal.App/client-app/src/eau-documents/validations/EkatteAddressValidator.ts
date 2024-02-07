import { EAUBaseValidator, ResourceHelpers, Nomenclatures, EkatteTypes, ErrMsgCodesConstants } from "eau-core";
import { DocumentFormValidationContext } from "./DocumentFormValidationContext";
import { EkatteAddress } from "../models";
import { ObjectHelper } from "cnsys-core";

export class EkatteAddressValidator extends EAUBaseValidator<EkatteAddress, DocumentFormValidationContext> {
    constructor() {
        super();

        Nomenclatures.getEkattes().then(ekattes => {
            let settlements = ekattes.filter(e => e.ekatteTypeID == EkatteTypes.Settlement);
            let settlementsWithAreasIds = new Set(ekattes.filter(e => e.ekatteTypeID == EkatteTypes.Area).map(a => a.parentID));
            let ekatteSettlementsWithAreas = settlements.filter(s => settlementsWithAreasIds.has(s.ekatteID));

            this.ruleFor(x => x.districtCode).notEmpty()
                .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.DefaultNotEmptyErrorMessage, new EkatteAddress(), 'districtCode'));

            this.ruleFor(x => x.municipalityCode).notEmpty()
                .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.DefaultNotEmptyErrorMessage, new EkatteAddress(), 'municipalityCode'));

            this.ruleFor(x => x.settlementCode).notEmpty()
                .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.DefaultNotEmptyErrorMessage, new EkatteAddress(), 'settlementCode'));

            this.ruleFor(x => x.areaCode).notEmpty().when(x => ekatteSettlementsWithAreas.findIndex(e => e.code == x.settlementCode) > -1)
                .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.DefaultNotEmptyErrorMessage, new EkatteAddress(), 'areaCode'));

            this.ruleFor(x => x.postCode).notEmpty()
                .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.DefaultNotEmptyErrorMessage, new EkatteAddress(), 'postCode'));

            this.ruleFor(m => m.postCode).matches("^[0-9]+$")
                .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.OnlyDigitsAllowed, new EkatteAddress(), 'postCode'));

            this.ruleFor(m => m.postCode).length(4, 4)
                .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.FieldMustContainsExaclyNumSymbols, new EkatteAddress(), 'postCode', 4));

            this.ruleFor(x => x.street).notEmpty()
                .withMessage(ResourceHelpers.formatErrorMessage("DOC_GL_EkatteAddress_StreetRequired_L", new EkatteAddress(), 'street')).when(m => ObjectHelper.isStringNullOrEmpty(m.housingEstate));

            this.ruleFor(m => m.street).match(/^[а-яА-Яa-zA-Z\s+\d+~@#$%^&*()_{}|"„“':>=|!<.,/\\?;-]+$/g).withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.FieldValidationCyrillicLatinNumbersSymbols, new EkatteAddress(), 'street'));
            this.ruleFor(m => m.housingEstate).match(/^[а-яА-Яa-zA-Z\s+\d+~@#$%^&*()_{}|"':>=|!<.,/\\?;-]+$/g).withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.FieldValidationCyrillicLatinNumbersSymbols, new EkatteAddress(), 'housingEstate'));

        })
    }
}