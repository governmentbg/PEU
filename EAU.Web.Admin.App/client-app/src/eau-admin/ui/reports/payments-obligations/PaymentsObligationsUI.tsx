﻿import { AsyncUIProps, BaseProps } from 'cnsys-ui-react';
import { EAUBaseComponent } from 'eau-core';
import { action, observable } from 'mobx';
import { observer } from 'mobx-react';
import React from 'react';
import { ObjectHelper } from 'cnsys-core';
import { PaymentsObligationsSearchCriteria } from '../../../models/ModelsAutoGenerated';
import { PaymentsObligationsResultsUI } from './PaymentsObligationsResultsUI';
import { PaymentsObligationsSearchUI } from './PaymentsObligationsSearchUI';

interface PaymentsObligationsUIProps extends BaseProps, AsyncUIProps { }

@observer export class PaymentsObligationsUI extends EAUBaseComponent<PaymentsObligationsUIProps, PaymentsObligationsSearchCriteria>{

    @observable private resultsKey = ObjectHelper.newGuid();

    constructor(props:PaymentsObligationsUIProps) {
        super(props);

        this.onSearchCallback = this.onSearchCallback.bind(this);
    }

    @action private onSearchCallback(criteria: PaymentsObligationsSearchCriteria) {
        this.model = criteria;
        this.model.page = 1;
        this.model.pageSize = 10;
        this.resultsKey = ObjectHelper.newGuid();
    }

    render() {

        return <>
            <PaymentsObligationsSearchUI onSearch={this.onSearchCallback} />
            <PaymentsObligationsResultsUI key={this.resultsKey} {...this.bind(x => x)} />
        </>
    }
}