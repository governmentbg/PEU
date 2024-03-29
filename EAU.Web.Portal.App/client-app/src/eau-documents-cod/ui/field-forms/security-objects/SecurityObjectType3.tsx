﻿import { SelectListItem, ArrayHelper, ObjectHelper } from "cnsys-core";
import { BaseProps, AsyncUIProps, withAsyncFrame } from "cnsys-ui-react";
import { attributesClassFormControlRequiredLabel, Constants, EAUBaseComponent, ResourceHelpers, attributesClassFormControlLabel, attributesClassFormControlReadOnly, attributesClassFormControl, attributesClassFormControlDisabled } from "eau-core";
import { runInAction, observable } from "mobx";
import { observer } from "mobx-react";
import React from "react";
import { SecurityObjectTransportUI } from "..";
import { SecurityType, AlarmAndSecurityActivity } from "../../../models/ModelsAutoGenerated";
import { Region } from "../../../models/ModelsManualAdded";
import { withDocumentFormManager, ApplicationFormManagerProps } from "eau-documents";
import { isNotificationForTakingOrRemovingFromSecurityManager } from "../../../form-managers/NotificationForTakingOrRemovingFromSecurityManager";
import { MOIDataService } from "../../../services";

interface SecurityObjectType3Props extends BaseProps, AsyncUIProps, ApplicationFormManagerProps {
    isForTermination: boolean;
}

@observer export class SecurityObjectType3Impl extends EAUBaseComponent<SecurityObjectType3Props, AlarmAndSecurityActivity>{

    @observable private districts: Region[] = [];

    private moidDataService = new MOIDataService();

    constructor(props: SecurityObjectType3Props) {
        super(props);

        this.getDistricts = this.getDistricts.bind(this);
        this.handleDistrictsChange = this.handleDistrictsChange.bind(this);
        this.getAisChodDistrictName = this.getAisChodDistrictName.bind(this);

        if (isNotificationForTakingOrRemovingFromSecurityManager(this.props.documentFormManager)) {

            if (this.props.isForTermination && !this.model.terminationDate) {
                this.model.terminationDate = this.props.documentFormManager.getActualDate();
            }
            if (!this.props.isForTermination && !this.model.actualDate) {
                this.model.actualDate = this.props.documentFormManager.getActualDate();
            }
        }
    }

    componentDidMount() {
        this.props.registerAsyncOperation(this.loadDistricts())
    }

    private loadDistricts() {
        let that = this;

        return that.moidDataService.getRegions().then(districts => {
            runInAction(() => {
                if (districts && districts.length > 0) {
                    that.districts = ArrayHelper.queryable.from(districts).toArray();
                    that.getAisChodDistrictName();
                }
            });
        });
    }
    
    private getDistricts(): SelectListItem[] {
        return this.districts.map(d => {
            return new SelectListItem({ text: d.title, value: d.itemID });
        })
    }

    renderEdit(): JSX.Element {
        return this.props.isForTermination ? this.renderTerminationEditUI() : this.renderEditUI()
    }

    renderDisplay(): JSX.Element {
        return this.props.isForTermination ? this.renderTerminationDisplayUI() : this.renderDisplayUI()

    }

    renderEditUI() {
        return (
            <>
                <div className="row">
                    <div className="form-group col-12">
                        {this.labelFor(m => m.actualDate, null, attributesClassFormControlRequiredLabel)}
                        {this.dateFor(m => m.actualDate)}
                        <div className="sr-only" id="HELP_P-4">{this.getResource('GL_DATETIME_HELP_SR_ONLY_I')}</div>
                    </div>
                </div>
                <div className="row">
                    <div className="form-group col-md">
                    {this.labelFor(m => m.securityObjectName, null, attributesClassFormControlRequiredLabel)}
                        {this.textBoxFor(m => m.securityObjectName)}
                    </div>
                    <div className="form-group col-md-6">
                        {this.labelFor(m => m.aischodDistrictId, null, attributesClassFormControlRequiredLabel)}
                        {this.dropDownListFor(m => m.aischodDistrictId, this.getDistricts(), attributesClassFormControl, this.handleDistrictsChange, true, this.getResource("GL_CHOICE_L"))}

                    </div>
                    <div className="form-group col-12">
                        {this.labelFor(m => m.address, null, attributesClassFormControlRequiredLabel)}
                        {this.textBoxFor(m => m.address)}
                    </div>
                </div>
                <div className="row">
                    <div className="form-group col-sm-6">
                        {this.labelFor(m => m.securityType, null, attributesClassFormControlLabel)}
                        {this.dropDownListFor(m => m.securityType, ResourceHelpers.getSelectListItemsForEnum(SecurityType), null, null, true, this.getResource('GL_CHOICE_L'))}
                    </div>
                </div>
                <div className="list-fileds-group" id="TRANSPORT"><SecurityObjectTransportUI {...this.bind(m => m.securityTransports)} /></div>
            </>
        )
    }

    renderTerminationEditUI() {
        return (
            <>
                {!ObjectHelper.isNullOrUndefined(this.model.aischodObjectID) && <div className="row">
                    <div className="form-group col-12">
                        {this.labelFor(m => m.actualDate, null, attributesClassFormControlLabel)}
                        {this.dateFor(m => m.actualDate, null, null, {disabled: true})}
                        <div className="sr-only" id="HELP_P-1">{this.getResource('GL_DATETIME_HELP_SR_ONLY_I')}</div>
                    </div>
                </div>}
                <div className="row">
                    <div className="form-group col-md">
                        {this.labelFor(m => m.securityObjectName, null, !ObjectHelper.isNullOrUndefined(this.model.aischodObjectID) ? attributesClassFormControlLabel : attributesClassFormControlRequiredLabel)}
                        {this.textBoxFor(m => m.securityObjectName, !ObjectHelper.isNullOrUndefined(this.model.aischodObjectID) ? attributesClassFormControlReadOnly : attributesClassFormControl)}
                    </div>
                    <div className="form-group col-md-6">
                        {this.labelFor(m => m.aischodDistrictId, null, !ObjectHelper.isNullOrUndefined(this.model.aischodObjectID) ? attributesClassFormControlLabel : attributesClassFormControlRequiredLabel)}
                        {this.dropDownListFor(m => m.aischodDistrictId, this.getDistricts(), (!ObjectHelper.isNullOrUndefined(this.model.aischodObjectID) ? attributesClassFormControlDisabled : attributesClassFormControl), this.handleDistrictsChange, true, this.getResource("GL_CHOICE_L"))}
                    </div>
                    {!ObjectHelper.isNullOrUndefined(this.model.aischodObjectID) && <div className="form-group col-12">
                        {this.labelFor(m => m.address, null, !ObjectHelper.isNullOrUndefined(this.model.aischodObjectID) ? attributesClassFormControlLabel : attributesClassFormControlRequiredLabel)}
                        {this.textBoxFor(m => m.address, !ObjectHelper.isNullOrUndefined(this.model.aischodObjectID) ? attributesClassFormControlReadOnly : attributesClassFormControl)}
                    </div>}
                </div>
                <div className="row">
                    <div className="form-group col-12">
                        {this.labelFor(m => m.contractTypeNumberDate, null, attributesClassFormControlRequiredLabel)}
                        {this.textBoxFor(m => m.contractTypeNumberDate)}
                    </div>
                    <div className="form-group col-12">
                        {this.labelFor(m => m.terminationDate, null, attributesClassFormControlRequiredLabel)}
                        {this.dateFor(m => m.terminationDate)}
                        <div className="sr-only" id="HELP_P-2">{this.getResource('GL_DATETIME_HELP_SR_ONLY_I')}</div>
                    </div>
                    <div className="form-group col-12">
                        {this.labelFor(m => m.contractTerminationNote, null, attributesClassFormControlLabel)}
                        {this.textBoxFor(m => m.contractTerminationNote)}
                    </div>
                </div>
            </>
        )
    }

    renderDisplayUI() {
        return (
            <>
                <div className="row">
                    <div className="form-group col-12">
                        <h4 className="form-control-label">{this.getResourceByProperty(m => m.actualDate)}</h4>
                        {this.dateDisplayFor(this.model.actualDate, Constants.DATE_FORMATS.date)}
                        {this.propertyErrorsDispleyFor(m => m.actualDate)}
                    </div>
                </div>
                <div className="row">
                    <div className="form-group col-sm-6 col-xl-4">
                        <h4 className="form-control-label">{this.getResourceByProperty(m => m.securityObjectName)}</h4>
                        {this.textDisplayFor(m => m.securityObjectName)}
                    </div>
                    <div className="form-group col-sm-6 col-xl-4">
                        {
                            !ObjectHelper.isNullOrUndefined(this.model.districtName) ? 
                                <>
                                    <h4 className="form-control-label">{this.getResourceByProperty(m => m.districtCode)}</h4>
                                    {this.textDisplayFor(m => m.districtName)}
                                    {this.propertyErrorsDispleyFor(m => m.districtCode)}
                                </>
                            :
                                <>
                                    <h4 className="form-control-label">{this.getResourceByProperty(m => m.aischodDistrictId)}</h4>
                                    {this.textDisplayFor(m => m.aischodDistrictName)}
                                    {this.propertyErrorsDispleyFor(m => m.aischodDistrictId)}
                                </>
                        }
                    </div>
                    <div className="form-group col-12">
                        <h4 className="form-control-label">{this.getResourceByProperty(m => m.address)}</h4>
                        {this.textDisplayFor(m => m.address)}
                    </div>
                </div>
                {this.model.securityType ?
                    <div className="row">
                        <div className="form-group col-sm-6 col-xl-4">
                            <h4 className="form-control-label">{this.getResourceByProperty(m => m.securityType)}</h4>
                            {ResourceHelpers.getResourceByEmun(this.model.securityType, SecurityType)}
                        </div>
                    </div>
                    : null}
                {this.model.securityTransports && !this.isEmptysSecurityTransports() ?
                    <div className="list-fileds-group"><SecurityObjectTransportUI {...this.bind(m => m.securityTransports)} /></div>
                    : null}
            </>
        )
    }

    renderTerminationDisplayUI() {
        return (
            <>
                {!ObjectHelper.isNullOrUndefined(this.model.actualDate) && <div className="row">
                    <div className="form-group col-12">
                        <h4 className="form-control-label">{this.getResourceByProperty(m => m.actualDate)}</h4>
                        {this.dateDisplayFor(this.model.actualDate, Constants.DATE_FORMATS.date)}
                        {this.propertyErrorsDispleyFor(m => m.actualDate)}
                    </div>
                </div>}
                <div className="row">
                    <div className="form-group col-sm-6">
                        <h4 className="form-control-label">{this.getResourceByProperty(m => m.securityObjectName)}</h4>
                        {this.textDisplayFor(m => m.securityObjectName)}
                    </div>
                    <div className="form-group col-sm-6">
                        {
                            !ObjectHelper.isNullOrUndefined(this.model.districtName) ? 
                                <>
                                    <h4 className="form-control-label">{this.getResourceByProperty(m => m.districtCode)}</h4>
                                    {this.textDisplayFor(m => m.districtName)}
                                    {this.propertyErrorsDispleyFor(m => m.districtCode)}
                                </>
                            :
                                <>
                                    <h4 className="form-control-label">{this.getResourceByProperty(m => m.aischodDistrictId)}</h4>
                                    {this.textDisplayFor(m => m.aischodDistrictName)}
                                    {this.propertyErrorsDispleyFor(m => m.aischodDistrictId)}
                                </>
                        }
                    </div>
                    {!ObjectHelper.isNullOrUndefined(this.model.address) && <div className="form-group col-12">
                        <h4 className="form-control-label">{this.getResourceByProperty(m => m.address)}</h4>
                        {this.textDisplayFor(m => m.address)}
                    </div>}
                </div>
                <div className="row">
                    <div className="form-group col-12">
                        <h4 className="form-control-label">{this.getResourceByProperty(m => m.contractTypeNumberDate)}</h4>
                        {this.textDisplayFor(m => m.contractTypeNumberDate)}
                    </div>
                    <div className="form-group col-12">
                        <h4 className="form-control-label">{this.getResourceByProperty(m => m.terminationDate)}</h4>
                        {this.dateDisplayFor(this.model.terminationDate, Constants.DATE_FORMATS.date)}
                        {this.propertyErrorsDispleyFor(m => m.terminationDate)}
                    </div>
                    <div className="form-group col-12">
                    <h4 className="form-control-label">{this.getResourceByProperty(m => m.contractTerminationNote)}</h4>
                        {this.textDisplayFor(m => m.contractTerminationNote)}
                    </div>
                </div>
            </>
        )
    }

    private handleDistrictsChange(e: any) {
        this.model.aischodDistrictName = null;
        if (e.target.value) {
            this.model.aischodDistrictName = e.target.text;

            let district = this.districts.find(x => x.itemID == e.target.value);

            if (district) {
                this.model.aischodDistrictName = district.title;
            }
        }
    }

    private getAisChodDistrictName() {

        if (!ObjectHelper.isArrayNullOrEmpty(this.districts) && !ObjectHelper.isNullOrUndefined(this.model.aischodObjectID) ) {

            this.model.aischodDistrictName = null;
        
            if (!ObjectHelper.isNullOrUndefined(this.model.aischodDistrictId)) {

                let district = this.districts.find(x => x.itemID.toString() == this.model.aischodDistrictId);

                if (district) {
                    this.model.aischodDistrictName = district.title;
                }
            }
        }
    }

    isEmptysSecurityTransports() {
        if (ArrayHelper.queryable.from(this.model.securityTransports).
            where(el => !ObjectHelper.isNullOrUndefined(el.vehicleOwnershipType)
                || !ObjectHelper.isNullOrUndefined(el.registrationNumber)
                || !ObjectHelper.isNullOrUndefined(el.makeAndModel)).toArray().length > 0)
            return false;
        else return true;
    }
}

export const SecurityObjectType3 = withAsyncFrame(withDocumentFormManager(SecurityObjectType3Impl));