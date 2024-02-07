import { BindableReference } from 'cnsys-core';
import { BaseProps } from 'cnsys-ui-react';
import { EAUBaseComponent } from 'eau-core';
import { action } from 'mobx';
import { observer } from 'mobx-react';
import React from 'react';
import { ANDObligationSearchCriteria, ObligatedPersonType } from '../../models/ModelsManualAdded';
import { ObligationsForObligatedEntityUI } from './ObligationsForObligatedEntityUI';
import { ObligationsForObligatedPersonUI } from './ObligationsForObligatedPersonUI';

interface ObligationsFormUIProps extends BaseProps {
    modelReference: BindableReference;
}

@observer export class ObligationsFormUI extends EAUBaseComponent<ObligationsFormUIProps, ANDObligationSearchCriteria>{

    constructor(props: ObligationsFormUIProps) {
        super(props);

        //Bind
        this.onObligatedPersonTypeChange = this.onObligatedPersonTypeChange.bind(this);
    }

    @action private onObligatedPersonTypeChange(e: any) {
        this.model.obligatedPersonType = e.target.value

        if (e.target.value == ObligatedPersonType.Personal) {
            this.model.uic = null;
            this.model.obligedPersonIdent = null;
            this.model.personalDocumentNumber = null;

        } else if (e.target.value == ObligatedPersonType.Entity) {
            this.model.obligedPersonIdent = null;
            this.model.drivingLicenceNumber = null;
            this.model.personalDocumentNumber = null;
            this.model.foreignVehicleNumber = null;
        }

        this.model.clearErrors(true);
    }

    render() {
        return <fieldset className="fields-group">
            <legend>
                <h3 className="field-title">{this.getResource("GL_DATA_FOR_CHECK_L")}</h3>
            </legend>
            <div className="alert alert-info" role="alert" dangerouslySetInnerHTML={{ __html: this.getResource("GL_ALL_OBLIGATION_RESULT_I") }}></div>
            <div className="row">
                <div className="form-group col-12">
                    <fieldset>
                        <legend className="form-control-label">{this.getResource('GL_PersonType_L')}</legend>
                        <div className="custom-control custom-radio">
                            <input className="custom-control-input" onChange={this.onObligatedPersonTypeChange} name="RADIO_A" id="ObligatedPersonType_Personal"
                                value={ObligatedPersonType.Personal}
                                checked={this.model.obligatedPersonType == ObligatedPersonType.Personal} type="radio" />
                            <label className="custom-control-label" htmlFor="ObligatedPersonType_Personal">{this.getResource('GL_ObligatedPersonType_Person_L')}</label>
                        </div>
                        <div className="custom-control custom-radio">
                            <input className="custom-control-input" onChange={this.onObligatedPersonTypeChange} name="RADIO_A" id="ObligatedPersonType_Entity"
                                value={ObligatedPersonType.Entity}
                                checked={this.model.obligatedPersonType == ObligatedPersonType.Entity} type="radio" />
                            <label className="custom-control-label" htmlFor="ObligatedPersonType_Entity">{this.getResource("GL_ObligatedPersonType_Entity_L")}</label>
                        </div>
                    </fieldset>
                </div>
            </div>
            {
                this.model.obligatedPersonType == ObligatedPersonType.Personal
                    ? <ObligationsForObligatedPersonUI modelReference={this.props.modelReference} />
                    : <ObligationsForObligatedEntityUI modelReference={this.props.modelReference} />
            }
        </fieldset>
    }
}