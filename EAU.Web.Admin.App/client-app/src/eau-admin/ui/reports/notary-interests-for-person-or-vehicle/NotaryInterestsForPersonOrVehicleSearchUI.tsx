﻿import { BaseProps } from 'cnsys-ui-react';
import { classNameMr2ColFormLabel, EAUBaseComponent, ValidationSummary, ValidationSummaryStrategy, withCollapsableContent } from 'eau-core';
import { action } from 'mobx';
import { observer } from 'mobx-react';
import React from 'react';
import { NotaryInterestsForPersonOrVehicleSearchCriteria } from '../../../models/ModelsAutoGenerated';
import { NotaryInterestsForPersonOrVehicleValidator } from '../../../validations/NotaryInterestsForPersonOrVehicleValidator';
import CardFooterUI from '../../common/CardFooterUI';

interface NotaryInterestsForPersonOrVehicleSearchUIProps extends BaseProps {
    onSearch: (criteria: NotaryInterestsForPersonOrVehicleSearchCriteria) => void;
}

@observer class NotaryInterestsForPersonOrVehicleSearchUIImpl extends EAUBaseComponent<NotaryInterestsForPersonOrVehicleSearchUIProps, NotaryInterestsForPersonOrVehicleSearchCriteria>{

    constructor(props) {
        super(props);

        this.model = new NotaryInterestsForPersonOrVehicleSearchCriteria();
        this.validators = [new NotaryInterestsForPersonOrVehicleValidator()]

        this.onSearch = this.onSearch.bind(this);
        this.onClear = this.onClear.bind(this);
    }

    private onSearch() {

        if (this.validators[0].validate(this.model)) {
            this.props.onSearch(this.model)
        }
    }

    @action private onClear() {
        this.model.dateFrom = null;
        this.model.dateTo = null;
        this.model.vehicleRegNumber = null;
        this.model.personIdentifier = null;
        this.model.clearErrors(true);
    }

    render() {

        return <>
            <div className="card-body">
                <ValidationSummary model={this.model} {...this.props} strategy={ValidationSummaryStrategy.includeOnlyModelErrors} />
                <div className="row">
                    <div className="form-group col-xxl-4">
                        <label className="required-field">Период на проявен интерес</label>
                        <div className="row">
                            <div className="form-group col-sm-6">
                                <div className="d-flex">
                                    {this.labelFor(x => x.dateFrom, "GL_FROM_L", classNameMr2ColFormLabel)}
                                    {this.dateFor(x => x.dateFrom, null, null, null, null, null, true)}
                                </div>
                            </div>
                            <div className="form-group col-sm-6">
                                <div className="d-flex">
                                    {this.labelFor(x => x.dateTo, "GL_TO_L", classNameMr2ColFormLabel)}
                                    {this.dateFor(x => x.dateTo, null, null, null, null, null, true)}
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className="form-groupform-group col-md-6 col-xxl-4">
                        {this.labelFor(x => x.vehicleRegNumber, 'GL_VEHICLE_REG_NUMBER_L')}
                        {this.textBoxFor(x => x.vehicleRegNumber)}
                    </div>
                    <div className="form-groupform-group col-md-6 col-xxl-4">
                        {this.labelFor(x => x.personIdentifier, 'GL_CURRENT_NEW_OWNER_IDENT_L')}
                        {this.textBoxFor(x => x.personIdentifier)}
                    </div>
                </div>
            </div>
            <CardFooterUI onSearch={this.onSearch} onClear={this.onClear} />
        </>
    }
}

export const NotaryInterestsForPersonOrVehicleSearchUI = withCollapsableContent(NotaryInterestsForPersonOrVehicleSearchUIImpl, { titleKey: 'GL_SEARCH_TITLE_L' })