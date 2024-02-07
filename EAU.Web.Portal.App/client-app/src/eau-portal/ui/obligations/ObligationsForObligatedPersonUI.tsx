import { BaseProps } from 'cnsys-ui-react';
import { attributesClassFormControlReqired, attributesClassFormControlRequiredLabel, EAUBaseComponent } from 'eau-core';
import { observer } from 'mobx-react';
import React from 'react';
import { AdditinalDataForObligatedPersonType, ANDObligationSearchCriteria } from '../../models/ModelsManualAdded';

interface ObligationsForObligatedPersonUIProps extends BaseProps { }

@observer export class ObligationsForObligatedPersonUI extends EAUBaseComponent<ObligationsForObligatedPersonUIProps, ANDObligationSearchCriteria>{

    constructor(props: ObligationsForObligatedPersonUIProps) {
        super(props);

        this.model.additinalDataForObligatedPersonType = AdditinalDataForObligatedPersonType.DrivingLicenceNumber;

        this.onAdditinalDataForObligatedPersonTypeChange = this.onAdditinalDataForObligatedPersonTypeChange.bind(this);
    }

    private onAdditinalDataForObligatedPersonTypeChange = (e: any) => {
        this.model.additinalDataForObligatedPersonType = e.target.value;

        this.model.drivingLicenceNumber = null;
        this.model.personalDocumentNumber = null;
        this.model.foreignVehicleNumber = null;
    }

    render() {

        return <div className="row">
            <div className="form-group col-md-6 col-xl-4">
                {this.labelFor(m => m.obligedPersonIdent, "GL_PERSON_ID_L", attributesClassFormControlRequiredLabel)}
                {this.textBoxFor(m => m.obligedPersonIdent, attributesClassFormControlReqired)}
            </div>
            <div className="form-group col-md-6 col-xl-4">
                <label className="form-control-label required-field" htmlFor="additionalData">{this.getResource('GL_AdditionalData_L')}</label>
                <select className="form-control" id="additionalData" onChange={this.onAdditinalDataForObligatedPersonTypeChange}>
                    <option value={AdditinalDataForObligatedPersonType.DrivingLicenceNumber}>{this.getResource('GL_DRIVING_LICENCE_NUMBER_L')}</option>
                    <option value={AdditinalDataForObligatedPersonType.PersonalDocumentNumber}>{this.getResource('GL_NUMBER_OF_BULGARIAN_ID_CARD_L')}</option>
                    <option value={AdditinalDataForObligatedPersonType.ForeignVehicleNumber}>{this.getResource('GL_ForeignVehicleNumber_L')}</option>
                </select>
            </div>
            {
                this.model.additinalDataForObligatedPersonType == AdditinalDataForObligatedPersonType.DrivingLicenceNumber
                    ? <div className="form-group col-md-6 col-xl-4" >
                        {this.labelFor(x => x.drivingLicenceNumber, 'GL_DRIVING_LICENCE_NUMBER_L', attributesClassFormControlRequiredLabel)}
                        {this.textBoxFor(x => x.drivingLicenceNumber, attributesClassFormControlReqired)}
                    </div>
                    : null
            }
            {
                this.model.additinalDataForObligatedPersonType == AdditinalDataForObligatedPersonType.PersonalDocumentNumber
                    ? <div className="form-group col-md-6 col-xl-4">
                        {this.labelFor(x => x.personalDocumentNumber, 'GL_NUMBER_OF_BULGARIAN_ID_CARD_L', attributesClassFormControlRequiredLabel)}
                        {this.textBoxFor(x => x.personalDocumentNumber, attributesClassFormControlReqired)}
                    </div>
                    : null
            }

            {
                this.model.additinalDataForObligatedPersonType == AdditinalDataForObligatedPersonType.ForeignVehicleNumber
                    ? <div className="form-group col-md-6 col-xl-4">
                        {this.labelFor(x => x.foreignVehicleNumber, 'GL_ForeignVehicleNumber_L', attributesClassFormControlRequiredLabel)}
                        {this.textBoxFor(x => x.foreignVehicleNumber, attributesClassFormControlReqired)}
                    </div>
                    : null
            }
        </div>
    }
}