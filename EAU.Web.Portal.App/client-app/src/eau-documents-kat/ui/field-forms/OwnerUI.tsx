﻿import * as React from "react";
import { BaseProps } from "cnsys-ui-react";
import { EAUBaseComponent, attributesClassFormControlRequiredLabel, ResourceHelpers, attributesClassFormControlLabel } from "eau-core";
import { VehicleOwnerDataVM, PersonEntityFarmerChoiceType, VehicleOwnerAdditionalCircumstances } from "../../models/ModelsAutoGenerated";
import { PersonEntityDataUI } from "./PersonEntityDataUI";
import { SelectListItem, ObjectHelper } from "cnsys-core";
import { action } from "mobx";

interface OwnerUIProps extends BaseProps {
    newOwner?: boolean;
    canSoldBySyndic?: boolean;
}

export class OwnerUI extends EAUBaseComponent<OwnerUIProps, VehicleOwnerDataVM>{
    private listItems: SelectListItem[];

    constructor(props: OwnerUIProps) {
        super(props);

        //Bind   
        this.resetUI = this.resetUI.bind(this);

        //Init 
        if (this.props.newOwner === true) {
            this.listItems = ResourceHelpers.getSelectListItemsForEnum(VehicleOwnerAdditionalCircumstances).filter(m => m.value != "6" && m.value != "5" && m.value != "0");
        }
        else
            this.listItems = ResourceHelpers.getSelectListItemsForEnum(VehicleOwnerAdditionalCircumstances).filter(m => m.value != "6")

        if (this.props.canSoldBySyndic === false) {
            this.model.isSoldBySyndic = false;
        }
    }
    renderEdit(): JSX.Element {
        return (
            <>
                <PersonEntityDataUI {...this.bind(m => m.personEntityData)} callbackOnChange={this.resetUI} />
                {this.model.personEntityData.selectedPersonEntityFarmerChoiceType == PersonEntityFarmerChoiceType.Person ?
                    <div className="row">
                        <div className="form-group col-12">
                            {this.labelFor(m => m.vehicleOwnerAdditionalCircumstances, null, attributesClassFormControlLabel)}
                            {this.dropDownListFor(m => m.vehicleOwnerAdditionalCircumstances, this.listItems, null, null, true, this.getResource('GL_DDL_CHOICE_L'))}
                        </div>
                        {this.model.vehicleOwnerAdditionalCircumstances == VehicleOwnerAdditionalCircumstances.DeadPerson ?
                            <div className="form-group col-12" id="SUCCESSOR-1-3">
                                {this.labelFor(m => m.successorData, null, attributesClassFormControlRequiredLabel)}
                                {this.textAreaFor(m => m.successorData)}
                                <div className="help-text-inline" id="HELP-SUCCESSORS-1-3">
                                    {this.getResource("DOC_KAT_VehicleOwnerData_successorData_I")}
                                </div>
                            </div>
                            : null}
                    </div>
                    : this.model.personEntityData.selectedPersonEntityFarmerChoiceType == PersonEntityFarmerChoiceType.Entity && this.props.canSoldBySyndic === true ?
                        <div className="row">
                            <div className="form-group col-sm-6">
                                <div className="custom-control custom-checkbox">
                                    {this.checkBoxFor(m => m.isSoldBySyndic)}
                                </div>
                            </div>
                        </div>
                        : null}
            </>
        )
    };

    renderDisplay(): JSX.Element {
        return (
            <>
                <PersonEntityDataUI {...this.bind(m => m.personEntityData)} />
                {this.model.personEntityData.selectedPersonEntityFarmerChoiceType == PersonEntityFarmerChoiceType.Person ?
                    !ObjectHelper.isNullOrUndefined(this.model.vehicleOwnerAdditionalCircumstances) ?
                        <div className="row">
                            <div className="form-group col-12">
                                <h4 className="form-control-label">{this.getResourceByProperty(m => m.vehicleOwnerAdditionalCircumstances)}</h4>
                                {ResourceHelpers.getResourceByEmun(this.model.vehicleOwnerAdditionalCircumstances, VehicleOwnerAdditionalCircumstances)}
                            </div>
                            {this.model.vehicleOwnerAdditionalCircumstances.toString() == VehicleOwnerAdditionalCircumstances.DeadPerson.toString() ?
                                <div className="form-group col-12">
                                    <h4 className="form-control-label">{this.getResourceByProperty(m => m.successorData)}</h4>
                                    {this.textDisplayFor(m => m.successorData)}
                                </div> : null}

                        </div> : null
                    : this.model.personEntityData.selectedPersonEntityFarmerChoiceType == PersonEntityFarmerChoiceType.Entity
                        && this.model.isSoldBySyndic === true ?
                        <div className="row">
                            <div className="form-group col-12">
                                <p className="field-text check-item check-success">{this.getResourceByProperty(m => m.isSoldBySyndic)}</p>
                            </div>
                        </div> : null
                }
            </>
        )
    }

    @action private resetUI(): void {

        this.model.isSoldBySyndic = false;
        this.model.vehicleOwnerAdditionalCircumstances = null;
        this.model.successorData = null;

    }
}