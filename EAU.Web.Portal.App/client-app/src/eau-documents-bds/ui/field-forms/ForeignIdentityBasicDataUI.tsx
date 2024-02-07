﻿import { ObjectHelper } from "cnsys-core";
import { BaseProps, ViewMode } from "cnsys-ui-react";
import { attributesClassFormControlLabel, attributesClassFormControlReqired, attributesClassFormControlRequiredLabel, EAUBaseComponent, ResourceHelpers } from "eau-core";
import { BIDEyesColor, BIDMaritalStatus } from "eau-documents";
import React from "react";
import { ForeignIdentityBasicDataVM } from "../../models/ModelsAutoGenerated";
import { ForeignCitizenDataUI } from "./ForeignCitizenDataUI";

interface ForeignIdentityBasicDataProps extends BaseProps {
    hideBIDEyesColorSection?: boolean;
    hideHeight?: boolean;
}

export class ForeignIdentityBasicDataUI extends EAUBaseComponent<ForeignIdentityBasicDataProps, ForeignIdentityBasicDataVM> {

    renderEdit(): JSX.Element {
        return <>
            {this.model.foreignCitizenData && <ForeignCitizenDataUI {...this.bind(x => x.foreignCitizenData)} egn={this.model.egn} lnch={this.model.lnCh} viewMode={ViewMode.Display} />}
            <div className="row">
                {
                    this.props.hideHeight === true
                        ? null
                        : <div className="form-group col-sm-4">
                            {this.labelFor(m => m.height, null, attributesClassFormControlLabel)}
                            {!ObjectHelper.isStringNullOrEmpty(this.model.height) ? <p className="field-text">{this.model.height}</p> : null}
                        </div>
                }
                {
                    this.props.hideBIDEyesColorSection === true
                        ? null
                        : <div className="form-group col-sm-4">
                            {this.labelFor(m => m.eyesColor, null, attributesClassFormControlLabel)}
                            <p className="field-text">{!ObjectHelper.isStringNullOrEmpty(this.model.eyesColor)
                                ? ResourceHelpers.getResourceByEmun(this.model.eyesColor, BIDEyesColor)
                                : null}
                            </p>
                        </div>
                }
                <div className="form-group col-sm-4">
                    {this.labelFor(m => m.maritalStatus, null, attributesClassFormControlLabel)}
                    <p className="field-text">{!ObjectHelper.isStringNullOrEmpty(this.model.maritalStatus)
                        ? ResourceHelpers.getResourceByEmun(this.model.maritalStatus, BIDMaritalStatus)
                        : null}
                    </p>
                </div>
            </div>
            <div className="row">
                <div className="form-group col-sm-4">
                    {this.labelFor(m => m.phone, null, attributesClassFormControlRequiredLabel)}
                    {this.textBoxFor(m => m.phone, attributesClassFormControlReqired)}
                </div>
            </div>
        </>
    }

    renderDisplay(): JSX.Element {
        return <>
            {this.model.foreignCitizenData && <ForeignCitizenDataUI {...this.bind(x => x.foreignCitizenData)} egn={this.model.egn} lnch={this.model.lnCh} />}
            <div className="row">
                {
                    this.props.hideHeight === true
                        ? null
                        : <div className="form-group col-sm-4">
                            {this.labelFor(m => m.height, null, attributesClassFormControlLabel)}
                            {this.textDisplayFor(m => m.height)}
                        </div>
                }
                {
                    this.props.hideBIDEyesColorSection === true
                        ? null
                        : <div className="form-group col-sm-4">
                            {this.labelFor(m => m.eyesColor, null, attributesClassFormControlLabel)}
                            <p className="field-text">
                                {
                                    !ObjectHelper.isStringNullOrEmpty(this.model.eyesColor)
                                        ? ResourceHelpers.getResourceByEmun(this.model.eyesColor, BIDEyesColor)
                                        : null
                                }
                                {this.propertyErrorsDispleyFor(m => m.eyesColor)}
                            </p>
                        </div>
                }
                <div className="form-group col-sm-4">
                    {this.labelFor(m => m.maritalStatus, null, attributesClassFormControlLabel)}
                    <p className="field-text">
                        {
                            !ObjectHelper.isStringNullOrEmpty(this.model.maritalStatus)
                                ? ResourceHelpers.getResourceByEmun(this.model.maritalStatus, BIDMaritalStatus)
                                : null
                        }
                        {this.propertyErrorsDispleyFor(m => m.maritalStatus)}
                    </p>
                </div>
            </div>
            <div className="row">
                <div className="form-group col-sm-4">
                    {this.labelFor(m => m.phone, null, attributesClassFormControlLabel)}
                    {this.textDisplayFor(m => m.phone)}
                </div>
            </div>
        </>
    }
}