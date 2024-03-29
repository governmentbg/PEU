﻿import { BaseProps } from "cnsys-ui-react";
import { attributesClassFormControlLabel, attributesClassFormControlReqired, attributesClassFormControlRequiredLabel, EAUBaseComponent } from "eau-core";
import React from "react";
import { Pyrotechnics } from "../../models/ModelsAutoGenerated";


export class PyrotechnicsUI extends EAUBaseComponent<BaseProps, Pyrotechnics> {

    renderEdit() {
        return (
            <>
                <div className="row">
                    <div className="form-group col-sm-6">
                        {this.labelFor(x => x.tradeName, null, attributesClassFormControlLabel)}
                        {this.textBoxFor(x => x.tradeName)}
                    </div>
                    <div className="form-group col-sm-6">
                        {this.labelFor(x => x.kind, null, attributesClassFormControlRequiredLabel)}
                        {this.textBoxFor(x => x.kind, attributesClassFormControlReqired)}
                    </div>
                </div>
                <div className="row">
                    <div className="form-group col-sm-6">
                        {this.labelFor(x => x.quantity, null, attributesClassFormControlRequiredLabel)}
                        {this.textBoxFor(x => x.quantity, attributesClassFormControlReqired)}
                    </div>
                    <div className="form-group col-sm-6">
                        {this.labelFor(x => x.measure, null, attributesClassFormControlRequiredLabel)}
                        {this.textBoxFor(x => x.measure, attributesClassFormControlReqired)}
                    </div>
                </div>
            </>
        );
    }

    renderDisplay() {
        return (
            <>
                <div className="row">
                    <div className="form-group col-sm-6">
                        <h4 className="form-control-label">{this.getResourceByProperty(x => x.tradeName)}</h4>
                        {this.textDisplayFor(x => x.tradeName)}
                    </div>
                    <div className="form-group col-sm-6">
                        <h4 className="form-control-label">{this.getResourceByProperty(x => x.kind)}</h4>
                        {this.textDisplayFor(x => x.kind)}
                    </div>
                </div>
                <div className="row">
                    <div className="form-group col-sm-6">
                        <h4 className="form-control-label">{this.getResourceByProperty(x => x.quantity)}</h4>
                        {this.textDisplayFor(x => x.quantity)}
                    </div>
                    <div className="form-group col-sm-6">
                        <h4 className="form-control-label">{this.getResourceByProperty(x => x.measure)}</h4>
                        {this.textDisplayFor(x => x.measure)}
                    </div>
                </div>
            </>
        );
    };
}