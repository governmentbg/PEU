﻿import { ObjectHelper } from "cnsys-core";
import { BaseProps, AsyncUIProps, withAsyncFrame } from "cnsys-ui-react";
import { EAUBaseComponent, attributesClassFormControlRequiredLabel, attributesClassFormControlReqired, Constants } from "eau-core";
import { action, runInAction } from "mobx";
import React from "react";
import { EARKOSDataService } from "../../services/EARKOSDataService";
import { LicenseInfo } from "../../models/ModelsAutoGenerated";
import { LicenseValidator } from '../../validations/LicenseValidator';


interface LicenseProps extends BaseProps, AsyncUIProps {
    onChangeCallback?: () => void;
}

class LicenseUIImpl extends EAUBaseComponent<LicenseProps, LicenseInfo> {

    private EARKOSDataService: EARKOSDataService;

    constructor(props: LicenseProps) {
        super(props);

        this.EARKOSDataService = new EARKOSDataService();

        this.validators = [new LicenseValidator()];

        this.loadLicenseData = this.loadLicenseData.bind(this);
        this.onPermitNumberChange = this.onPermitNumberChange.bind(this);
    }

    private loadLicenseData() {

        if (!this.validators[0].validate(this.model))
            return;

        this.props.registerAsyncOperation(this.EARKOSDataService.getLicenseData(this.model.permitNumber).then(res => {
             runInAction(() => {
                if (res && !ObjectHelper.isStringNullOrEmpty(res.permitNumber)) {

                    if (res.permitNumber == this.model.permitNumber) {

                        this.model.permitNumber = res.permitNumber;
                        this.model.permitType = res.permitType;
                        this.model.permitTypeName = res.permitTypeName;
                        this.model.reason = res.reason;
                        this.model.reasonName = res.reasonName;
                        this.model.validityPeriodStart = res.validityPeriodStart;
                        this.model.validityPeriodStartSpecified = res.validityPeriodStartSpecified;
                        this.model.validityPeriodEnd = res.validityPeriodEnd;
                        this.model.validityPeriodEndSpecified = res.validityPeriodEndSpecified;
                        this.model.holderName = res.holderName;
                        this.model.holderIdentifier = res.holderIdentifier;
                        this.model.content = res.content;
                        this.model.issuingPoliceDepartment.policeDepartmentType = res.issuingPoliceDepartment.policeDepartmentType;
                        this.model.issuingPoliceDepartment.policeDepartmentTypeSpecified = res.issuingPoliceDepartment.policeDepartmentTypeSpecified;
                        this.model.issuingPoliceDepartment.policeDepartmentCode = res.issuingPoliceDepartment.policeDepartmentCode;
                        this.model.issuingPoliceDepartment.policeDepartmentName = res.issuingPoliceDepartment.policeDepartmentName;

                        if (this.props.onChangeCallback)
                            this.props.onChangeCallback();
                    }
                    else {
                        this.model.addError('permitNumber', this.getResource("DOC_KOS_ValidWeaponAcquisitionPermit_DiffNumber_E"));
                        this.clearLicenseData(this.model.permitNumber);
                    }
                }
                else
                    this.clearLicenseData(this.model.permitNumber);
            });
        }));
    }

    @action private clearLicenseData(permitNumber: string) {

        this.model.permitNumber = permitNumber;
        this.model.permitType = undefined;
        this.model.permitTypeName = undefined;
        this.model.reason = undefined;
        this.model.reasonName = undefined;
        this.model.validityPeriodStart = undefined;
        this.model.validityPeriodStartSpecified = undefined;
        this.model.validityPeriodEnd = undefined;
        this.model.validityPeriodEndSpecified = undefined;
        this.model.holderName = undefined;
        this.model.holderIdentifier = undefined;
        this.model.content = undefined;
        this.model.issuingPoliceDepartment.policeDepartmentType = undefined;
        this.model.issuingPoliceDepartment.policeDepartmentTypeSpecified = undefined;
        this.model.issuingPoliceDepartment.policeDepartmentCode = undefined;
        this.model.issuingPoliceDepartment.policeDepartmentName = undefined;

        if (this.props.onChangeCallback)
            this.props.onChangeCallback();
    }

    private onPermitNumberChange(e:any) {
        if (!ObjectHelper.isStringNullOrEmpty(this.model.permitType))
            this.clearLicenseData(e.target.value);
    }

    renderEdit(): JSX.Element {
        return <>
            <div className="row">
                <div className="col-12">
                    {this.labelFor(x => x.permitNumber, null, attributesClassFormControlRequiredLabel)}
                </div>
                <div className="form-group col col-sm-6 col-xl-5">
                    {this.textBoxFor(x => x.permitNumber, attributesClassFormControlReqired, this.onPermitNumberChange)}
                </div>
                <div className="form-group col-auto">
                    <button type="button" className="btn btn-light" onClick={this.loadLicenseData}>
                        <i className="ui-icon ui-icon-import mr-1" aria-hidden="true" />
                        {this.getResource("GL_GET_DATA_L")}
                    </button>
                </div>
            </div>
            {this.renderDisplayLicenseData()}
        </>;
    }

    renderDisplay(): JSX.Element {
        return <>
            <div className="row">
                <div className="form-group col-12">
                    <h4 className="form-control-label">{this.getResourceByProperty(x => x.permitNumber)}</h4>
                    {this.textDisplayFor(x => x.permitNumber)}
                </div>
            </div>
            {this.renderDisplayLicenseData()}
        </>;
    }

    private renderDisplayLicenseData(): JSX.Element {
        return !ObjectHelper.isStringNullOrEmpty(this.model.permitType) && <>
            <div className="row">
                <div className="form-group col-12">
                    <h4 className="form-control-label">{this.getResource("DOC_KOS_ApplicationForm_CtrlTalon_PermissInfo_L")}</h4>
                    {this.textDisplayFor(x => x.permitTypeName)}
                    <p className="field-text">
                        {this.getResource("DOC_KOS_ApplicationForm_CtrlTalon_PermissValidityPeriod_L")}: {this.dateDisplayFor(this.model.validityPeriodStart, Constants.DATE_FORMATS.date)} - {!ObjectHelper.isStringNullOrEmpty(this.model.validityPeriodEnd) && this.dateDisplayFor(this.model.validityPeriodEnd, Constants.DATE_FORMATS.date)}
                    </p>
                    <p className="field-text">{this.model.holderName}, {this.model.holderIdentifier}</p>
                    {this.textDisplayFor(x => x.content, true)}
                </div>
            </div>
            <div className="row">
                 <div className="form-group col-12">
                    <h4 className="form-control-label">{this.getResource("GL_MVR_STRUCTURE_L")}</h4>
                    {this.textDisplay(this.model.issuingPoliceDepartment.policeDepartmentName, this.model.issuingPoliceDepartment, "policeDepartmentName")}
                </div>
             </div>
        </>;
    }
}

export const LicenseUI = withAsyncFrame(LicenseUIImpl);