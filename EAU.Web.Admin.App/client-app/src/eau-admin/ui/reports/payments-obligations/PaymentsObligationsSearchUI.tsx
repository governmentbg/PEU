﻿import { BaseProps } from 'cnsys-ui-react';
import { classNameMr2ColFormLabel, EAUBaseComponent, ValidationSummary, ValidationSummaryStrategy, withCollapsableContent } from 'eau-core';
import { action } from 'mobx';
import { observer } from 'mobx-react';
import React from 'react';
import { ObligedPersonIdentTypes, PaymentsObligationsSearchCriteria } from '../../../models/ModelsAutoGenerated';
import { PaymentsObligationsValidator } from '../../../validations/PaymentsObligationsValidator';
import CardFooterUI from '../../common/CardFooterUI';

interface PaymentsObligationsSearchUIProps extends BaseProps {
    onSearch: (criteria: PaymentsObligationsSearchCriteria) => void;
}

@observer class PaymentsObligationsSearchUIImpl extends EAUBaseComponent<PaymentsObligationsSearchUIProps, PaymentsObligationsSearchCriteria>{

    constructor(props) {
        super(props);

        this.model = new PaymentsObligationsSearchCriteria();
        this.model.debtorIdentifierType = ObligedPersonIdentTypes.EGN;
        this.validators = [new PaymentsObligationsValidator()]

        this.onSearch = this.onSearch.bind(this);
        this.onClear = this.onClear.bind(this);
        this.handleDebtorIdentifierTypeChange = this.handleDebtorIdentifierTypeChange.bind(this);
    }

    private onSearch() {

        if (this.validators[0].validate(this.model)) {
            this.props.onSearch(this.model)
        }
    }

    @action private onClear() {
        this.model.dateFrom = null;
        this.model.dateTo = null;
        this.model.debtorIdentifier = null;
        this.model.debtorIdentifierType = ObligedPersonIdentTypes.EGN;
        this.model.clearErrors(true);
    }

    private handleDebtorIdentifierTypeChange(e: any) {
        this.model.debtorIdentifierType = e.target.value;
    }

    render() {

        return <>
            <div className="card-body">
                <ValidationSummary model={this.model} {...this.props} strategy={ValidationSummaryStrategy.includeOnlyModelErrors} />
                <div className="row">
                    <div className="form-group col-xl-5 col-xxl-4">
                        <label>{this.getResource('GL_PERIOD_PAYMENT_OBLIGATION_DOC_L')}</label>
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
                    <div className="col-xl-7 col-xxl-6">
                        <label htmlFor="debtorIdentifier">{this.getResource('GL_DEBTOR_PAYER_IDENTIFIER_L')}</label>
                        <div className="row ">
                            <div className="form-group col-sm">
                                {this.textBoxFor(x => x.debtorIdentifier)}
                            </div>
                            <div className="form-group col-sm-auto">
                                <fieldset className="border-0 p-0 m-0">
                                    <legend className="sr-only">{this.getResource('GL_IDENTIFIER_TYPE_L')}</legend>
                                    <div className="form-inline">
                                        <div className="custom-control-inline custom-control custom-radio">
                                            <input className="custom-control-input" type="radio" onChange={this.handleDebtorIdentifierTypeChange} name={'debtor-identifier'}
                                                id={ObligedPersonIdentTypes.EGN.toString()} value={ObligedPersonIdentTypes.EGN} checked={this.model.debtorIdentifierType == ObligedPersonIdentTypes.EGN} />
                                            <label className="custom-control-label" htmlFor={ObligedPersonIdentTypes.EGN.toString()}>{this.getResource("GL_PERSON_ID_L")}</label>
                                        </div>
                                        <div className="custom-control-inline custom-control custom-radio">
                                            <input className="custom-control-input" type="radio" onChange={this.handleDebtorIdentifierTypeChange} name={'debtor-identifier'}
                                                id={ObligedPersonIdentTypes.BULSTAT.toString()} value={ObligedPersonIdentTypes.BULSTAT} checked={this.model.debtorIdentifierType == ObligedPersonIdentTypes.BULSTAT} />
                                            <label className="custom-control-label" htmlFor={ObligedPersonIdentTypes.BULSTAT.toString()}>{this.getResource("GL_UIC_BULSTAT_L")}</label>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <CardFooterUI onSearch={this.onSearch} onClear={this.onClear} />
        </>
    }
}

export const PaymentsObligationsSearchUI = withCollapsableContent(PaymentsObligationsSearchUIImpl, { titleKey: 'GL_SEARCH_TITLE_L' })