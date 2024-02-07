import React from 'react';
import { ObjectHelper } from "cnsys-core";
import { action, observable, runInAction } from 'mobx';
import { observer } from 'mobx-react';
import numeral from 'numeral';
import { BaseFieldComponent, BaseFieldProps } from '../BaseFieldComponent';

interface AmountProps extends BaseFieldProps { }

@observer export class Amount extends BaseFieldComponent<AmountProps> {
    @observable private currencyVal: any = '';

    constructor(props?: any, context?: any) {
        super(props, context);

        this.handleOnBlur = this.handleOnBlur.bind(this);
        var mrValue = this.props.modelReference.getValue();
        this.currencyVal = mrValue && mrValue != "" ? mrValue : '';
    }

    renderInternal() {
        return (
            <input type="text"
                {...this.fieldAttributes}
                maxLength={16}
                onBlur={this.handleOnBlur}
                style={{ textAlign: "right" }}
                onChange={this.handleChange}
                value={this.currencyVal}
                name={this.getName()}
                id={this.getId()}
            />
        );
    }

    handleOnBlur() {
        runInAction(() => {
            this.props.modelReference.setValue(numeral(this.currencyVal).value());
            this.currencyVal = !ObjectHelper.isStringNullOrEmpty(this.currencyVal) ? numeral(this.currencyVal).format("0,0.00") : this.currencyVal;
        });
    }

    @action protected getHandleChangeValue(event: any) {
        var value = event.target.value.replace(/\s/g, "");

        value = (numeral as any).localeData().delimiters.decimal == "," ? value.replace(/\./g, ',') : value.replace(/,/g, '.');
        this.currencyVal = /^\d{0,9}([,|.]\d{0,2}0*)?$/.test(value) || value == "" ? value : this.currencyVal;

        this.currencyVal = ObjectHelper.isNullOrUndefined(this.currencyVal) ? '' : this.currencyVal.replace(/[^\d.[,|.]-]/g, '');

        return numeral(this.currencyVal).value();
    }
}