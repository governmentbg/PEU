import { BaseProps } from "cnsys-ui-react";
import { attributesClassFormControlLabel, Constants, EAUBaseComponent, ResourceHelpers } from "eau-core";
import { IdentityDocumentForeignCitizenBasicData, IdentityDocumentType } from "eau-documents";
import React from "react";

export class IdentityDocumentForeignCitizenBasicDataUI extends EAUBaseComponent<BaseProps, IdentityDocumentForeignCitizenBasicData> {

    renderEdit() {
        return <>
            <div className="row">
                <div className="form-group col-sm-6 col-lg-3">
                    {this.labelFor(m => m.identityNumber, null, attributesClassFormControlLabel)}
                    {this.textBoxFor(m => m.identityNumber)}
                </div>
                <div className="form-group col-sm-6 col-lg-3">
                    {this.labelFor(m => m.identitityIssueDate, null, attributesClassFormControlLabel)}
                    {this.dateFor(m => m.identitityIssueDate)}
                </div>
                <div className="form-group col-sm-6 col-lg-3">
                    {this.labelFor(m => m.identitityExpireDate, null, attributesClassFormControlLabel)}
                    {this.dateFor(m => m.identitityExpireDate)}
                </div>
                <div className="form-group col-sm-6 col-lg-3">
                    {this.labelFor(m => m.identityIssuer, null, attributesClassFormControlLabel)}
                    {this.textBoxFor(m => m.identityIssuer)}
                </div>
            </div>
        </>
    }

    renderDisplay() {
        return <div className="row">
            <div className="form-group col-sm-12">
                {this.labelFor(m => m.identityDocumentType, null, attributesClassFormControlLabel)}
                <p className="field-text">{ResourceHelpers.getResourceByEmun(this.model.identityDocumentType, IdentityDocumentType)}</p>
            </div>
            <div className="form-group col-sm-6 col-lg-3">
                {this.labelFor(m => m.identityNumber, null, attributesClassFormControlLabel)}
                {this.textDisplayFor(m => m.identityNumber)}
            </div>
            <div className="form-group col-sm-6 col-lg-3">
                {this.labelFor(m => m.identitityIssueDate, null, attributesClassFormControlLabel)}
                <p className="field-text">{this.dateDisplayFor(this.model.identitityIssueDate, Constants.DATE_FORMATS.date)}</p>
            </div>
            <div className="form-group col-sm-6 col-lg-3">
                {this.labelFor(m => m.identitityExpireDate, null, attributesClassFormControlLabel)}
                <p className="field-text">{this.dateDisplayFor(this.model.identitityExpireDate, Constants.DATE_FORMATS.date)}</p>
            </div>
            <div className="form-group col-sm-6 col-lg-3">
                {this.labelFor(m => m.identityIssuer, null, attributesClassFormControlLabel)}
                {this.textDisplayFor(m => m.identityIssuer)}
            </div>
        </div>
    }
}