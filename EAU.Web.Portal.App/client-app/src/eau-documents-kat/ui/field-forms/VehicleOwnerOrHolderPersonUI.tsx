﻿import { BaseProps } from "cnsys-ui-react";
import { attributesClassFormControlReqired, attributesClassFormControlRequiredLabel, EAUBaseComponent, ResourceHelpers } from "eau-core";
import { PersonIdentifierChoiceType, PersonNamesUI } from "eau-documents";
import { observer } from "mobx-react";
import React from "react";
import { PersonDataVM, PersonIdentifierVM } from "../../models/ModelsAutoGenerated";

interface VehicleOwnerOrHolderPersonIdentifierUIProps extends BaseProps {
    onGetData: () => void;
}

@observer class VehicleOwnerOrHolderPersonIdentifierUI extends EAUBaseComponent<VehicleOwnerOrHolderPersonIdentifierUIProps, PersonIdentifierVM>{

    constructor(props?: any) {
        super(props);

        this.onIdentTypeChange = this.onIdentTypeChange.bind(this);
    }

    renderEdit(): JSX.Element {
        return <div className="row">
            <div className="col-12">
                {this.labelFor(m => m.item, null, attributesClassFormControlRequiredLabel)}
            </div>
            <div className="form-group col-6 col-xl-4">
                {this.textBoxFor(m => m.item, attributesClassFormControlReqired)}
            </div>
            <div className="form-group col-auto">
                <fieldset className="form-inline">
                    <legend className="sr-only"></legend>
                    {this.radioButtonListFor(m => m.itemElementName, ResourceHelpers.getSelectListItemsForEnum(PersonIdentifierChoiceType), { className: "custom-control-inline custom-control custom-radio" }, this.onIdentTypeChange)}
                </fieldset>
            </div>
            <div className="form-group col-sm-12 col-md-auto">
                <button className="btn btn-light" onClick={this.props.onGetData} type="button">
                    <i className="ui-icon ui-icon-import mr-1" aria-hidden="true"></i>
                    {this.getResource('GL_CHECK_L')}
                </button>
            </div>
        </div>
    }

    renderDisplay(): JSX.Element {
        return <div className="form-group col-sm-4">
            <h4 className="form-control-label">{ResourceHelpers.getResourceByEmun(this.model.itemElementName as number, PersonIdentifierChoiceType)}</h4>
            {this.textDisplay(this.model.item, this.model, "item")}
        </div>
    }

    private onIdentTypeChange() {
        this.model.clearErrors();
    }
}

interface VehicleOwnerOrHolderPersonUIProps extends BaseProps {
    onGetData: () => void;
}

@observer export class VehicleOwnerOrHolderPersonUI extends EAUBaseComponent<VehicleOwnerOrHolderPersonUIProps, PersonDataVM>{

    renderEdit(): JSX.Element {
        return <VehicleOwnerOrHolderPersonIdentifierUI {...this.bind(x => x.identifier)} onGetData={this.props.onGetData} />
    }

    renderDisplay(): JSX.Element {
        return <>
            <PersonNamesUI {...this.bind(m => m.names)} />
            <VehicleOwnerOrHolderPersonIdentifierUI {...this.bind(x => x.identifier)} onGetData={this.props.onGetData} />
        </>
    }
}