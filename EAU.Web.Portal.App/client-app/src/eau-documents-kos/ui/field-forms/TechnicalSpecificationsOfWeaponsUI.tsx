﻿import { SelectListItem } from "cnsys-core";
import { BaseProps } from "cnsys-ui-react";
import { attributesClassFormControlLabel, attributesClassFormControlReqired, attributesClassFormControlRequiredLabel, EAUBaseComponent } from "eau-core";
import { ApplicationFormManagerProps, withCollectionItems, withDocumentFormManager } from "eau-documents";
import { action } from "mobx";
import React from "react";
import { TechnicalSpecificationOfWeapon } from "../../models/ModelsAutoGenerated";

interface TechnicalSpecificationsOfWeaponsProps extends BaseProps, ApplicationFormManagerProps {
    skipWeaponPurpose?: boolean;
}

class TechnicalSpecificationOfWeaponImpl extends EAUBaseComponent<TechnicalSpecificationsOfWeaponsProps, TechnicalSpecificationOfWeapon>{

    private weaponTypes: SelectListItem[];
    private weaponPurposes: SelectListItem[];

    constructor(props: TechnicalSpecificationsOfWeaponsProps) {
        super(props);

        this.weaponTypes = [];

        if (this.props.documentFormManager.additionalData.weaponTypes) {
            let arr = JSON.parse(this.props.documentFormManager.additionalData.weaponTypes);

            if (arr && Array.isArray(arr))
                arr.map(weaponType => this.weaponTypes.push(new SelectListItem(weaponType)))
        }

        this.weaponPurposes = [];

        if (this.props.documentFormManager.additionalData.weaponPurposes) {
            let arr = JSON.parse(this.props.documentFormManager.additionalData.weaponPurposes);

            if (arr && Array.isArray(arr))
                arr.map(weaponPurpose => this.weaponPurposes.push(new SelectListItem(weaponPurpose)))
        }

        this.handleWeaponTypeChange = this.handleWeaponTypeChange.bind(this);
        this.handleWeaponPurposeChange = this.handleWeaponPurposeChange.bind(this);
    }

    renderEdit() {
        return (
            <>
                <div className="row">
                    <div className="form-group col-sm-6">
                        {this.labelFor(x => x.make, null, attributesClassFormControlRequiredLabel)}
                        {this.textBoxFor(x => x.make, attributesClassFormControlReqired)}
                    </div>
                    <div className="form-group col-sm-6">
                        {this.labelFor(x => x.model, null, attributesClassFormControlLabel)}
                        {this.textBoxFor(x => x.model)}
                    </div>
                </div>
                <div className="row">
                    <div className="form-group col-sm-6">
                        {this.labelFor(x => x.caliber, null, attributesClassFormControlRequiredLabel)}
                        {this.textBoxFor(x => x.caliber, attributesClassFormControlReqired)}
                    </div>
                    <div className="form-group col-sm-6">
                        {this.labelFor(x => x.weaponNumber, null, attributesClassFormControlRequiredLabel)}
                        {this.textBoxFor(x => x.weaponNumber, attributesClassFormControlReqired)}
                    </div>
                </div>
                <div className="row">
                    <div className="form-group col-12">
                        {this.labelFor(x => x.weaponTypeCode, null, attributesClassFormControlLabel)}
                        {this.dropDownListFor(x => x.weaponTypeCode, this.weaponTypes, null, this.handleWeaponTypeChange, true, this.getResource("GL_CHOICE_L"))}
                    </div>
                    {
                        this.props.skipWeaponPurpose
                            ? null
                            : <div className="form-group col-12">
                                {this.labelFor(x => x.weaponPurposeCode, null, attributesClassFormControlLabel)}
                                {this.dropDownListFor(x => x.weaponPurposeCode, this.weaponPurposes, null, this.handleWeaponPurposeChange, true, this.getResource("GL_CHOICE_L"))}
                            </div>
                    }
                </div>
            </>
        );
    }

    renderDisplay() {
        return (
            <>
                <div className="row">
                    <div className="form-group col-sm-6">
                        {this.labelFor(x => x.make, null, attributesClassFormControlLabel)}
                        {this.textDisplayFor(x => x.make)}
                    </div>
                    {this.model.model ?
                        <div className="form-group col-sm-6">
                            {this.labelFor(x => x.model, null, attributesClassFormControlLabel)}
                            {this.textDisplayFor(x => x.model)}
                        </div> : null}
                </div>
                <div className="row">
                    <div className="form-group col-sm-6">
                        {this.labelFor(x => x.caliber, null, attributesClassFormControlLabel)}
                        {this.textDisplayFor(x => x.caliber)}
                    </div>
                    <div className="form-group col-sm-6">
                        {this.labelFor(x => x.weaponNumber, null, attributesClassFormControlLabel)}
                        {this.textDisplayFor(x => x.weaponNumber)}
                    </div>
                </div>
                <div className="row">
                    {this.model.weaponTypeName ?
                        <div className="form-group col-12">
                            {this.labelFor(x => x.weaponTypeCode, null, attributesClassFormControlLabel)}
                            {this.textDisplayFor(x => x.weaponTypeName)}
                        </div> : null}
                    {
                        this.model.weaponPurposeName
                            ? <div className="form-group col-12">
                                {this.labelFor(x => x.weaponPurposeCode, null, attributesClassFormControlLabel)}
                                {this.textDisplayFor(x => x.weaponPurposeName)}
                            </div>
                            : null
                    }
                </div>
            </>
        );
    };

    //#region Handlers

    @action handleWeaponTypeChange(e: any) {
        let currentWeaponType = this.weaponTypes.find(x => x.value == e.target.value);

        if (currentWeaponType) {
            this.model.weaponTypeCode = currentWeaponType.value;
            this.model.weaponTypeName = currentWeaponType.text;
        } else {
            this.model.weaponTypeCode = null;
            this.model.weaponTypeName = null;
        }
    }

    @action handleWeaponPurposeChange(e: any) {
        let currentWeaponPurpose = this.weaponPurposes.find(x => x.value == e.target.value);

        if (currentWeaponPurpose) {
            this.model.weaponPurposeCode = currentWeaponPurpose.value;
            this.model.weaponPurposeName = currentWeaponPurpose.text;
        } else {
            this.model.weaponPurposeCode = null;
            this.model.weaponPurposeName = null;
        }
    }

    //#endregion
}

export const TechnicalSpecificationsOfWeaponsUI = withDocumentFormManager(withCollectionItems(TechnicalSpecificationOfWeaponImpl, {
    initItem: () => new TechnicalSpecificationOfWeapon(),
    addButtonLabelKey: 'GL_ADD_WEAPON_L'
}))