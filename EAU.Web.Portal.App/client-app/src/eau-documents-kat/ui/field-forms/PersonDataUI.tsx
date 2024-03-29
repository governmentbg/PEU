﻿import { BaseProps, ViewMode } from "cnsys-ui-react";
import { Constants, EAUBaseComponent, ResourceHelpers } from "eau-core";
import { BIDMaritalStatus, GraoAddressUI, PersonBasicDataUI } from "eau-documents";
import React from "react";
import { PersonDataVM } from "../../models/ModelsAutoGenerated";
import { StatusUI } from "./StatusUI";

export class PersonDataUI extends EAUBaseComponent<BaseProps, PersonDataVM>{
    render(): JSX.Element {
        return (
            <>
                <PersonBasicDataUI {...this.bind(m => m.personBasicData)} viewMode={ViewMode.Display} />
                {this.model.maritalStatus ?
                    <div className="row">
                        <div className="form-group col-12">
                            <h4 className="form-control-label">{this.getResourceByProperty(m => m.maritalStatus)}</h4>
                            {ResourceHelpers.getResourceByEmun(this.model.maritalStatus, BIDMaritalStatus)}
                            <div className="alert alert-info">
                                {this.getResource("DOC_KAT_PersonData_maritalStatus_I")}
                            </div>
                        </div>
                    </div>
                    : null}
                {this.model.deathDate ?
                    <div className="row">
                        <div className="form-group col-12">
                            <h4 className="form-control-label">{this.getResourceByProperty(m => m.deathDate)}</h4>
                            {this.dateDisplayFor(this.model.deathDate, Constants.DATE_FORMATS.date)}
                            <div className="alert alert-info">
                                {this.getResource("DOC_KAT_CHECK_ESGRAON_I")}
                            </div>
                        </div>
                    </div>
                    : null
                }
                <div className="row">
                    <div className="form-group col-12">
                        <h4 className="form-control-label">{this.getResourceByProperty(m => m.permanentAddress)}</h4>
                        <GraoAddressUI {...this.bind(m => m.permanentAddress)} viewMode={ViewMode.Display} />
                    </div>
                </div>
                {this.model.status ?
                    <div className="row">
                        <div className="form-group col-12">
                            <h4 className="form-control-label">{this.getResourceByProperty(m => m.status)}</h4>
                            <div className={this.model.status.blocking === true ? "alert alert-danger" : "alert alert-warning"}>
                                <StatusUI {...this.bind(m => m.status)} />
                            </div>
                        </div>
                    </div>
                    : null
                }
            </>
        )
    }
}