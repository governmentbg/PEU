﻿import { ObjectHelper } from "cnsys-core";
import { BaseProps, ViewMode } from "cnsys-ui-react";
import { attributesClassFormControlLabel, EAUBaseComponent, ResourceHelpers } from "eau-core";
import { FieldFormUI, GraoAddressUI, IdentityDocumentType, TravelDocumentUI } from "eau-documents";
import React from "react";
import { ApplicationForIssuanceOfIdentityDocumentsAndRPOFInTheRepublicOfBulgariaDataVM } from "../../models/ModelsAutoGenerated";
import { ForeignIdentityBasicDataUI } from "../field-forms/ForeignIdentityBasicDataUI";
import { IdentityDocumentForeignCitizenBasicDataUI } from "../field-forms/IdentityDocumentForeignCitizenBasicDataUI";

export class ApplicationForIssuanceOfIdentityDocumentsAndRPOFInTheRepublicOfBulgariaDataUI
    extends EAUBaseComponent<BaseProps, ApplicationForIssuanceOfIdentityDocumentsAndRPOFInTheRepublicOfBulgariaDataVM> {

    renderEdit(): JSX.Element {
        return <>
            {
                this.model.identityDocumentsType && this.model.identityDocumentsType.length > 0
                && <FieldFormUI title={this.getResourceByProperty(m => m.identityDocumentsType)}>
                    <div className="form-group">
                        <p className="field-text"> {ResourceHelpers.getResourceByEmun(this.model.identityDocumentsType[0], IdentityDocumentType).toUpperCase()}</p>
                    </div>
                </FieldFormUI>
            }
            {
                this.model.previousIdentityDocument && <FieldFormUI title={this.getResourceByProperty(m => m.previousIdentityDocument)}>
                    <IdentityDocumentForeignCitizenBasicDataUI {...this.bind(m => m.previousIdentityDocument)} viewMode={ViewMode.Display} />
                </FieldFormUI>
            }
            <FieldFormUI title={this.getResourceByProperty(m => m.foreignIdentityBasicData)}>
                <ForeignIdentityBasicDataUI {...this.bind(m => m.foreignIdentityBasicData)} />
                {this.renderAddress()}
                {this.labelFor(m => m.abroadAddress, null, attributesClassFormControlLabel)}
                {this.textAreaFor(m => m.abroadAddress, null, 3)}
            </FieldFormUI>
            {
                this.model.travelDocument && <FieldFormUI title={this.getResourceByProperty(m => m.travelDocument)}>
                    <TravelDocumentUI {...this.bind(m => m.travelDocument)} viewMode={ViewMode.Display} />
                </FieldFormUI>
            }
        </>
    }

    renderDisplay(): JSX.Element {
        return <>
            {
                this.model.identityDocumentsType && this.model.identityDocumentsType.length > 0
                && <FieldFormUI title={this.getResourceByProperty(m => m.identityDocumentsType)}>
                    <div className="form-group">
                        <p className="field-text"> {ResourceHelpers.getResourceByEmun(this.model.identityDocumentsType[0], IdentityDocumentType).toUpperCase()}</p>
                    </div>
                </FieldFormUI>
            }
            {
                this.model.previousIdentityDocument && <FieldFormUI title={this.getResourceByProperty(m => m.previousIdentityDocument)}>
                    <IdentityDocumentForeignCitizenBasicDataUI {...this.bind(m => m.previousIdentityDocument)} viewMode={ViewMode.Display} />
                </FieldFormUI>
            }
            <FieldFormUI title={this.getResourceByProperty(m => m.foreignIdentityBasicData)}>
                <ForeignIdentityBasicDataUI {...this.bind(m => m.foreignIdentityBasicData)} />
                {this.renderAddress()}
                {
                    !ObjectHelper.isStringNullOrEmpty(this.model.abroadAddress) && <div className="row">
                        <div className="form-group col-12">
                            {this.labelFor(m => m.abroadAddress, null, attributesClassFormControlLabel)}
                            {this.textDisplayFor(m => m.abroadAddress)}
                        </div>
                    </div>
                }
            </FieldFormUI>
            {
                this.model.travelDocument && <FieldFormUI title={this.getResourceByProperty(m => m.travelDocument)}>
                    <TravelDocumentUI {...this.bind(m => m.travelDocument)} />
                </FieldFormUI>
            }
        </>
    }

    renderAddress() {

        if (this.model.permanentAddress) {
            return <div className="row">
                <div className="form-group col-12">
                    {this.labelFor(m => m.permanentAddress, null, attributesClassFormControlLabel)}
                    <GraoAddressUI {...this.bind(m => m.permanentAddress)} viewMode={ViewMode.Display} />
                </div>
            </div>
        } else if (this.model.presentAddress) {
            return <div className="row">
                <div className="form-group col-12">
                    {this.labelFor(m => m.presentAddress, null, attributesClassFormControlLabel)}
                    <GraoAddressUI {...this.bind(m => m.presentAddress)} viewMode={ViewMode.Display} />
                </div>
            </div>
        }

        return null;
    }
}