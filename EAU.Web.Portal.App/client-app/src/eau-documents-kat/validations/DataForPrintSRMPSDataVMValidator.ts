﻿import { EAUBaseValidator } from "eau-core";
import { DocumentFormValidationContext } from "eau-documents";
import { DataForPrintSRMPSDataVM, PersonEntityChoiceType } from "../models/ModelsAutoGenerated";
import { VehicleOwnerOrHolderValidator } from "./VehicleOwnerOrHolderValidator";
import { ObjectHelper } from "../../cnsys-core";

export class DataForPrintSRMPSDataVMValidator extends EAUBaseValidator<DataForPrintSRMPSDataVM, DocumentFormValidationContext> {
    constructor() {
        super();

        this.ruleFor(m => m.holderData).setValidator(new VehicleOwnerOrHolderValidator()).when(x => x.selectedNewOwner == "new owner");
        this.ruleFor(m => m.userData).setValidator(new VehicleOwnerOrHolderValidator()).when(x => x.holderNotSameAsUser);

    }

    public validate(obj: DataForPrintSRMPSDataVM): boolean {
        let res = super.validate(obj);        

        if (obj.holderData && obj.userData) {
            if (obj.holderData.person && obj.userData.person) {
                if (obj.holderData.person.identifier.item == obj.userData.person.identifier.item && !ObjectHelper.isStringNullOrEmpty(obj.holderData.person.identifier.item)) {
                    obj.addError("userData", this.getMessage("DOC_KAT_DataForPrintSRMPSData_holderSameAsUser_E"));
                    res = false;
                }
            }
            if (obj.holderData.entity && obj.userData.entity) {
                if (obj.holderData.entity.identifier == obj.userData.entity.identifier && !ObjectHelper.isStringNullOrEmpty(obj.holderData.entity.identifier)) {
                    obj.addError("userData", this.getMessage("DOC_KAT_DataForPrintSRMPSData_holderSameAsUser_E"));
                    res = false;
                }
            }
        }        
        if (obj.userData && obj.newOwners && obj.newOwners.length > 0) {
            if (obj.userData.selectedChoiceType == PersonEntityChoiceType.Person              
                && obj.newOwners.filter((newOwner) => newOwner.selectedChoiceType == PersonEntityChoiceType.Person 
                    && newOwner.person.identifier.item && newOwner.person.identifier.item != ""
                    && newOwner.person.identifier.item == obj.userData.person.identifier.item).length > 0) {
                
                obj.addError("userData", this.getMessage("DOC_KAT_DataForPrintSRMPSData_UserMustBeDifferentFromAllOwners_E"));
                res = false;                
            }
            if (obj.userData.selectedChoiceType == PersonEntityChoiceType.Entity                 
                && obj.newOwners.filter((newOwner) => newOwner.selectedChoiceType == PersonEntityChoiceType.Entity 
                    && newOwner.entity.identifier && newOwner.entity.identifier != ""
                    && newOwner.entity.identifier == obj.userData.entity.identifier).length > 0) {

                obj.addError("userData", this.getMessage("DOC_KAT_DataForPrintSRMPSData_UserMustBeDifferentFromAllOwners_E"));
                res = false;                
            }
        }
        if (obj.userData && !obj.checkedUserData) {
            obj.userData.addError(this.getMessage("DOC_KAT_DataForPrintSRMPSData_checkedUserData_E"));
            res = false;
        }

        if (obj.holderData && !obj.checkedHolderData && obj.selectedNewOwner == "new owner") {
            obj.holderData.addError(this.getMessage("DOC_KAT_DataForPrintSRMPSData_checkedHolderData_E"));
            res = false;
        }

        return res;
    }
}




