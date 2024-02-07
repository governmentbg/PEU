﻿import { EAUBaseValidator, ErrMsgCodesConstants, ResourceHelpers } from "eau-core";
import { DocumentFormValidationContext } from "eau-documents";
import { ScopeOfCertification, SecurityServiceTypesVM, TerritorialScopeOfServicesDistrictsVM, TerritorialScopeOfServicesVM } from "../models/ModelsAutoGenerated";

export class SecurityServiceTypesVMValidator extends EAUBaseValidator<SecurityServiceTypesVM, DocumentFormValidationContext> {

    constructor() {
        super();

        this.ruleFor(m => m.territorialScopeOfServices).setValidator(new TerritorialScopeOfServicesVMValidator()).when(x => x.isSelected);

    }    
}

export class TerritorialScopeOfServicesVMValidator extends EAUBaseValidator<TerritorialScopeOfServicesVM, DocumentFormValidationContext>{
    constructor() {
        super();

        this.ruleFor(x => x.scopeOfCertification).notEmpty()
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.DefaultNotEmptyErrorMessage, new TerritorialScopeOfServicesVM(), 'scopeOfCertification'));

        this.ruleFor(x => x.districts).notEmpty().withMessage(this.getMessage("DOC_COD_TerritorialScopeOfServicesDistrict_IsRequired_E")).when(x => x.scopeOfCertification == ScopeOfCertification.SelectedDistricts);
    }
}

export class TerritorialScopeOfServicesDistrictsVMValidator extends EAUBaseValidator<TerritorialScopeOfServicesDistrictsVM, DocumentFormValidationContext>{
    constructor() {
        super();

        this.ruleFor(x => x.districtGRAOCode).notEmpty()
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.DefaultNotEmptyErrorMessage, new TerritorialScopeOfServicesDistrictsVM(), 'districtGRAOCode'));

    }
}