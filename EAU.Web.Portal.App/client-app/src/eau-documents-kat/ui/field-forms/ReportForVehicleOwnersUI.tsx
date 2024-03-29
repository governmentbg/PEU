﻿import { BaseProps, ViewMode } from "cnsys-ui-react";
import { Constants, EAUBaseComponent, ResourceHelpers } from "eau-core";
import { FieldFormUI, GraoAddressUI, IdentityDocumentBasicData, IdentityDocumentType, PersonBasicData, PersonIdentifierUI, PersonNamesUI } from "eau-documents";
import { isMoment } from "moment";
import React from "react";
import { EntityData, PersonData, ReportForVehicleOwnersVM } from "../../models/ModelsAutoGenerated";
import { DisplayStatusUI } from "./DisplayStatusesUI";

export class ReportForVehicleOwnersUI extends EAUBaseComponent<BaseProps, ReportForVehicleOwnersVM>{

    render(): JSX.Element {

        if (this.model != null) {
            return (
                <>
                    {
                        this.model.personDataItems && this.model.personDataItems.length > 0 && <FieldFormUI title={this.getResourceByProperty(x => x.personDataItems)}>
                            <ul className="list-filed">
                                {
                                    this.model.personDataItems.map((personDataItem, index) => {
                                        return <li className="list-filed__item" role="group" key={index}>
                                            <ReportForVehiclePersonOwner key={index} {...this.bindArrayElement(x => x.personDataItems[index], [index])} />
                                        </li>
                                    })
                                }
                            </ul>
                        </FieldFormUI>
                    }
                    {
                        this.model.entityDataItems && this.model.entityDataItems.length > 0 && <FieldFormUI title={this.getResourceByProperty(x => x.entityDataItems)}>
                            <ul className="list-filed">
                                {
                                    this.model.entityDataItems.map((entityDataItem, index) => {
                                        return <li className="list-filed__item" role="group" key={index}>
                                            <ReportForVehicleEntityOwner key={index} {...this.bindArrayElement(x => x.entityDataItems[index], [index])} />
                                        </li>
                                    })
                                }
                            </ul>
                        </FieldFormUI>
                    }
                </>
            )
        }

        return null;
    }
}

//#region PersonData components

class ReportForVehiclePersonOwner extends EAUBaseComponent<BaseProps, PersonData>{

    render() {

        return <>
            <ReportForVehiclePersonBasicData {...this.bind(x => x.personBasicData)} />
            {
                isMoment(this.model.deathDate) && <div className="row">
                    <div className="form-group col-12">
                        <h4 className="form-control-label">{this.getResourceByProperty(x => x.deathDate)}</h4>
                        <p className="field-text">{this.dateDisplayFor(this.model.deathDate, Constants.DATE_FORMATS.date)}</p>
                    </div>
                </div>
            }
            <div className="row">
                <div className="form-group col-12">
                    <h4 className="form-control-label">{this.getResource("DOC_GL_PERMANENT_CURRENT_ADDRESS_L")}</h4>
                    <GraoAddressUI {...this.bind(x => x.permanentAddress)} viewMode={ViewMode.Display} />
                </div>
            </div>
            <DisplayStatusUI status={this.model.status} />
        </>
    }
}

class ReportForVehiclePersonBasicData extends EAUBaseComponent<BaseProps, PersonBasicData>{

    render() {
        return <>
            <div className="row">
                <PersonNamesUI {...this.bind(x => x.names)} viewMode={ViewMode.Display} />
                <PersonIdentifierUI {...this.bind(x => x.identifier)} viewMode={ViewMode.Display} />
            </div>
            <ReportForVehicleIdentityDocumentBasicData {...this.bind(x => x.identityDocument)} />
        </>
    }
}

class ReportForVehicleIdentityDocumentBasicData extends EAUBaseComponent<BaseProps, IdentityDocumentBasicData>{
    render() {
        return <>
            <div className="row">
                <div className="form-group col-sm-4">
                    <h4 className="form-control-label">{this.getResourceByProperty(x => x.identityDocumentType)}</h4>
                    <p className="field-text">{ResourceHelpers.getResourceByEmun(this.model.identityDocumentType, IdentityDocumentType)}</p>
                </div>
            </div>
            <div className="row">
                <div className="form-group col-sm-4">
                    <h4 className="form-control-label">{this.getResourceByProperty(x => x.identityNumber)}</h4>
                    <p className="field-text">{this.model.identityNumber}</p>
                </div>
                <div className="form-group col-sm-4">
                    <h4 className="form-control-label">{this.getResourceByProperty(x => x.identitityIssueDate)}</h4>
                    <p className="field-text">{this.dateDisplayFor(this.model.identitityIssueDate, Constants.DATE_FORMATS.date)}</p>
                </div>
                <div className="form-group col-sm-4">
                    <h4 className="form-control-label">{this.getResourceByProperty(x => x.identityIssuer)}</h4>
                    <p className="field-text">{this.model.identityIssuer}</p>
                </div>
            </div>
        </>
    }
}

//#endregion

//#region EntityData components

class ReportForVehicleEntityOwner extends EAUBaseComponent<BaseProps, EntityData>{

    render() {

        return <>
            <div className="row">
                <div className="form-group col-sm-6">
                    <h4 className="form-control-label">{this.getResourceByProperty(x => x.identifier)}</h4>
                    <p className="field-text">{this.model.identifier}</p>
                </div>
                <div className="form-group col-12">
                    <h4 className="form-control-label">{this.getResourceByProperty(x => x.name)}</h4>
                    <p className="field-text">{this.model.name}</p>
                </div>
                <div className="form-group col-sm-6">
                    <h4 className="form-control-label">{this.getResourceByProperty(x => x.fullName)}</h4>
                    <p className="field-text">{this.model.fullName}</p>
                </div>
                <div className="form-group col-sm-6">
                    <h4 className="form-control-label">{this.getResourceByProperty(x => x.nameTrans)}</h4>
                    <p className="field-text">{this.model.nameTrans}</p>
                </div>
            </div>
            <div className="row">
                <div className="form-group col-12">
                    <h4 className="form-control-label">{this.getResource("DOC_KAT_ENTITY_MANAGEMENT_ADDRESS_L")}</h4>
                    <GraoAddressUI {...this.bind(x => x.entityManagmentAddress)} viewMode={ViewMode.Display} />
                </div>
            </div>
            <DisplayStatusUI status={this.model.status} />
        </>
    }
}

//#endregion