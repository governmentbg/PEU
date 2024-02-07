﻿import * as React from "react";
import { BaseProps, ViewMode } from "cnsys-ui-react";
import { EAUBaseComponent } from "eau-core";
import { GraoAddressUI } from "eau-documents";
import { EntityDataVM } from "../../models/ModelsAutoGenerated";
import { StatusUI } from ".";

export class EntityDataUI extends EAUBaseComponent<BaseProps, EntityDataVM>{
    constructor(props: BaseProps) {
        super(props);

    }

    render(): JSX.Element {
        return (
            <>
                <div className="row">
                    <div className="form-group col-sm-6">
                        <h4 className="form-control-label">{this.getResourceByProperty(m => m.identifier)}</h4>
                        {this.textDisplayFor(m => m.identifier)}
                    </div>
                    <div className="form-group col-12">
                        <h4 className="form-control-label">{this.getResourceByProperty(m => m.name)}</h4>
                        {this.textDisplayFor(m => m.name)}
                    </div>
                    <div className="form-group col-sm-6">
                        <h4 className="form-control-label">{this.getResourceByProperty(m => m.fullName)}</h4>
                        {this.textDisplayFor(m => m.fullName)}
                    </div>
                    <div className="form-group col-sm-6">
                        <h4 className="form-control-label">{this.getResourceByProperty(m => m.nameTrans)}</h4>
                        {this.textDisplayFor(m => m.nameTrans)}
                    </div>
                </div>
                {this.model.recStatus ?
                    <div className="row">
                        <div className="form-group col-12">
                            <h4 className="form-control-label">{this.getResourceByProperty(m => m.recStatus)}</h4>
                            {this.textDisplayFor(m => m.recStatus)}
                        </div>
                    </div>
                    : null}
                <div className="row">
                    <div className="form-group col-12">
                        <h4 className="form-control-label">{this.getResourceByProperty(m => m.entityManagmentAddress)}</h4>
                        <GraoAddressUI {...this.bind(m => m.entityManagmentAddress)} viewMode={ViewMode.Display} />
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