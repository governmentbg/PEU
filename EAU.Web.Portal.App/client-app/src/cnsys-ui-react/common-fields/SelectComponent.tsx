import { observer } from "mobx-react";
import React from "react";
import Select from 'react-select';
import { BaseFieldComponent, BaseFieldProps } from '../BaseFieldComponent';
import {ObjectHelper} from "cnsys-core";

export interface IMultipleSelectListItem{
    value: string;

    label: string;
}

interface SelectProps extends BaseFieldProps {
    options: IMultipleSelectListItem[]
    defaultValues: IMultipleSelectListItem[]
    isMulti?: boolean
    placeholder: string
}

@observer export class SelectComponent extends BaseFieldComponent<SelectProps> {

    renderInternal() {
        return (
            <Select inputId={this.getId()}
                options={this.props.options}
                isClearable={true}
                isMulti={ObjectHelper.isNullOrUndefined(this.props.isMulti) || this.props.isMulti == true}
                {...this.fieldAttributes}
                className={"custom-multi-select " + (this.fieldAttributes && this.fieldAttributes.className)}
                value={this.props.defaultValues ? this.props.defaultValues : null}
                placeholder={this.props.placeholder ? this.props.placeholder : ""}
                onChange={this.handleChange}
                classNamePrefix='select' />
        );
    }

    getHandleChangeValue(options: IMultipleSelectListItem) {
        return options;
    }
}