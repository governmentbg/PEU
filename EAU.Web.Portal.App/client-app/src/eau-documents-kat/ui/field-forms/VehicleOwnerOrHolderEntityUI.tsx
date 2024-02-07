﻿import { BaseProps } from "cnsys-ui-react";
import { attributesClassFormControlReqired, attributesClassFormControlRequiredLabel, EAUBaseComponent } from "eau-core";
import { observer } from "mobx-react";
import React from "react";
import { EntityDataVM } from "../../models/ModelsAutoGenerated";

interface VehicleOwnerOrHolderEntityUIProps extends BaseProps {
    onGetData: () => void;
    isFarmer: boolean;
}

@observer export class VehicleOwnerOrHolderEntityUI extends EAUBaseComponent<VehicleOwnerOrHolderEntityUIProps, EntityDataVM>{

    renderEdit(): JSX.Element {

        return <div className="row">
            <div className="col-12">
                {
                    this.props.isFarmer
                        ? <label htmlFor="entity" className="form-control-label required-field">{this.getResource("GL_BULSTAT_L").toUpperCase()}</label>
                        : this.labelFor(m => m.identifier, null, attributesClassFormControlRequiredLabel)
                }
            </div>
            <div className="form-group col col-sm-6">
                {this.textBoxFor(m => m.identifier, attributesClassFormControlReqired)}
            </div>
            <div className="form-group col-auto">
                <button className="btn btn-light" onClick={this.props.onGetData}>
                    <i className="ui-icon ui-icon-import mr-1" aria-hidden="true"></i>
                    {this.getResource('GL_CHECK_L')}
                </button>
            </div>
        </div>
    }

    renderDisplay(): JSX.Element {
        return <>
            <div className="form-group col-12">
                <h4 className="form-control-label">{this.getResource("DOC_GL_EntityBasicData_name_L")}</h4>
                {this.textDisplay(this.model.name, this.model, "name")}
            </div>
            <div className="form-group col-sm-6">
                <h4 className="form-control-label">
                    {
                        this.props.isFarmer
                            ? this.getResource('GL_BULSTAT_L').toUpperCase()
                            : this.getResource('DOC_GL_EntityBasicData_identifier_L')
                    }
                </h4>
                {this.textDisplay(this.model.identifier, this.model, "identifier")}
            </div>
        </>
    }
}