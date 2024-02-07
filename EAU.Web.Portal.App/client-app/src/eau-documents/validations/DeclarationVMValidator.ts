﻿import { EAUBaseValidator, ResourceHelpers, resourceManager, ErrMsgCodesConstants } from "eau-core";
import { DocumentFormValidationContext } from "./DocumentFormValidationContext";
import { DeclarationVM, DeclarationsVM } from "../models/ModelsAutoGenerated";
import { ObjectHelper } from "cnsys-core";

export class DeclarationsVMValidator extends EAUBaseValidator<DeclarationsVM, DocumentFormValidationContext> {
    constructor() {
        super();

        this.ruleFor(m => m.declarations).setCollectionValidator(new DeclarationVMValidator());
    }

    public validate(obj: DeclarationsVM): boolean {
        let res = super.validate(obj);
        let ctx = super.getValidationContext();

        if (obj.declarations && obj.declarations.length > 0) {
            obj.declarations.forEach(function (item) {
                let decl = ctx.documentFormManager.service.declarations.filter(d => d.code == item.code)[0];

                if (decl && !item.isDeclarationFilled && decl.isRquired) {
                    obj.addError(resourceManager.getResourceByKey(item.code == "Policy_GDPR" ? ErrMsgCodesConstants.MissingRequiredPolicy : ErrMsgCodesConstants.MissingRequiredDeclaration));
                    res = false;
                    return res;                       
                }
            });
        }                

        return res;
    }
}

export class DeclarationVMValidator extends EAUBaseValidator<DeclarationVM, DocumentFormValidationContext> {

    public validate(obj: DeclarationVM): boolean {
        let res = super.validate(obj);
        let ctx = super.getValidationContext();
        let decl = ctx.documentFormManager.service.declarations.filter(d => d.code == obj.code)[0];

        if (res) {
            if (obj.isDeclarationFilled
                && decl.isAdditionalDescriptionRequired
                && ObjectHelper.isStringNullOrEmpty(obj.furtherDescriptionFromDeclarer)) {
                obj.addError('furtherDescriptionFromDeclarer', ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.DefaultNotEmptyErrorMessage, DeclarationVM, 'furtherDescriptionFromDeclarer'));
                res = false;
            }

            if (decl && !obj.isDeclarationFilled && decl.isRquired) {
                obj.addError('isDeclarationFilled',                    
                    ResourceHelpers.formatErrorMessage(obj.code == "Policy_GDPR" ? ErrMsgCodesConstants.MissingRequiredPolicy : ErrMsgCodesConstants.MissingRequiredDeclaration, DeclarationVM, 'isDeclarationFilled'));
                res = false;
            }
        }       

        return res;
    }
}