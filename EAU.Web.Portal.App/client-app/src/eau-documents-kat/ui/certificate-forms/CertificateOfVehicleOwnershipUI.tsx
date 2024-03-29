﻿import React from "react";
import { EAUBaseComponent, ResourceHelpers, Constants } from "eau-core";
import { BaseProps, ViewMode } from "cnsys-ui-react";
import { CertificateOfVehicleOwnershipVM, VehicleOwnerInformationItemVM, OwnershipCertificateReason, DocumentFor } from "../../models/ModelsAutoGenerated";
import { FieldFormUI, ElectronicServiceProviderBasicDataUI, DocumentURIUI, AISCaseURIUI, ElectronicServiceApplicantUI, GraoAddressUI } from "eau-documents";
import { VehicleDataUI, VehicleOwnerInformationItemUI } from "../field-forms";

export class CertificateOfVehicleOwnershipUI extends EAUBaseComponent<BaseProps, CertificateOfVehicleOwnershipVM> {

    render(): JSX.Element {
        return (
            <>
                <div className="row">
                    <div className="form-group col-12">                       
                        <p className="page-subtitle">
                            {this.getResource("GL_URI_L")} <DocumentURIUI {...this.bind(m => m.documentURI)} />
                        </p>
                        <p className="page-date">{this.getResource("GL_DATE_L")} {this.dateDisplayFor(this.model.documentReceiptOrSigningDate, Constants.DATE_FORMATS.date)}</p>
                    </div>
                </div>
                <FieldFormUI title={this.getResource("GL_ElectronicServiceProviderBasicData_L")}>
                    <ElectronicServiceProviderBasicDataUI {...this.bind(m => m.electronicServiceProviderBasicData)} />
                </FieldFormUI>
                <ElectronicServiceApplicantUI {...this.bind(m => m.electronicServiceApplicant)} viewMode={ViewMode.Display} />                
                <FieldFormUI title={this.getResource("DOC_GL_CasefileData_L")}>
                    <div className="row">
                        <div className="form-group col-12">
                            <h4 className="form-control-label">{this.getResource("GL_URI_CASE_L")}</h4>
                            <p className="field-text"><AISCaseURIUI {...this.bind(m => m.aisCaseURI)} /></p>
                        </div>
                    </div>
                </FieldFormUI>
                <VehicleDataUI {...this.bind(m => m.vehicleData)} extendedMode={this.model.certificateKind == DocumentFor.OwnershipOfVehicleWithRegistrationNumberAndMake} />
                {this.model.certificateKind != DocumentFor.NoDataForPreviousOwnershipOfSpecificVehicle && 
                    this.model.certificateKind != DocumentFor.NoDataForOwnershipOfVehicles &&
                    this.model.certificateKind != DocumentFor.NoDataForOwnershipOfSpecificVehicle
                    ?
                    this.model.vehicleOwnerInformationCollection && this.model.vehicleOwnerInformationCollection.length > 0 ?
                        <FieldFormUI title={this.getTitleOfOwners()}>
                            <ul className="list-filed">
                                {
                                    this.model.vehicleOwnerInformationCollection.map((item: VehicleOwnerInformationItemVM, index: number) => {
                                        return (
                                            <li className="list-filed__item" role="group">
                                                <VehicleOwnerInformationItemUI key={index} {...this.bindArrayElement(m => m.vehicleOwnerInformationCollection[index], [index])} />
                                            </li>
                                        )
                                    })
                                }
                            </ul>
                        </FieldFormUI>
                        : null
                    :
                    <fieldset className="fields-group">
                        <legend><h3 className="field-title">В уверение на това, че</h3></legend>
                        <div className="row">
                            <div className="col-12 form-group">
                                <p className="field-text">{this.getNoDataString()}</p>
                            </div>
                        </div>
                    </fieldset>}
                <FieldFormUI title={this.getResourceByProperty(m => m.katVerificationDateTime)}>
                    <div className="row">
                        <div className="col-sm-4 form-group">
                            <h4 className="form-control-label">{this.getResource("GL_DATE_L")}</h4>
                            {this.dateDisplayFor(this.model.katVerificationDateTime, Constants.DATE_FORMATS.date)}
                        </div>
                        <div className="col-sm-4 form-group">
                            <h4 className="form-control-label">{this.getResource("GL_HOUR_L")}</h4>
                            {this.dateDisplayFor(this.model.katVerificationDateTime, Constants.DATE_FORMATS.TimeHm)}
                        </div>
                    </div>
                </FieldFormUI>
                <FieldFormUI title={this.getResourceByProperty(m => m.ownershipCertificateReason)}>
                    <p className="field-text">{ResourceHelpers.getResourceByEmun(this.model.ownershipCertificateReason as number, OwnershipCertificateReason)}</p>
                </FieldFormUI>                
            </>
        )
    }

    getTitleOfOwners(): string {

        switch (this.model.certificateKind) {
            case DocumentFor.OwnershipOfAllVehicles:
                return this.getResource("GL_Owner_L");
            case DocumentFor.OwnershipOfVehicleWithRegistrationNumberAndMake:
            case DocumentFor.OwnershipOfPreviousVehicle:
                return this.getResource("DOC_KAT_CertificateOfVehicleOwnership_vehicleOwnerInformationCollection_L");
            default:
                return "";
        }

    }

    getNoDataString(): string {

        switch (this.model.certificateKind) {

            case DocumentFor.NoDataForPreviousOwnershipOfSpecificVehicle:
                return this.getResource("GL_KAT_NoDataForPreviousOwnershipOfSpecificVehicle_L")
            case DocumentFor.NoDataForOwnershipOfVehicles:
                return this.getResource("GL_KAT_NoDataForOwnershipOfVehicles_L");
            case DocumentFor.NoDataForOwnershipOfSpecificVehicle:
                return this.getResource("GL_KAT_NoDataForOwnershipOfSpecificVehicle_L"); 
            default:
                return "";
        }        
    }
}