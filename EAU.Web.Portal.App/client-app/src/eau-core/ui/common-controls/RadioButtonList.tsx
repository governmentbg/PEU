import { ObjectHelper } from "cnsys-core";
import { BaseFieldComponent, BaseFieldProps, SelectListItem } from 'cnsys-ui-react';
import { observer } from "mobx-react";
import React from "react";
import { withSimpleErrorLabel } from '../WithSimpleErrorLabel';

interface RadioButtonListProps extends BaseFieldProps {
    items?: SelectListItem[],
    disabled?: boolean
}

@observer class RadioButtonListImpl extends BaseFieldComponent<RadioButtonListProps> {

    private groupName: string;

    constructor(props: RadioButtonListProps) {
        super(props);

        this.groupName = ObjectHelper.newGuid();
    }

    renderInternal() {

        return (
            <>
                {(this.props.items && this.props.items.length > 0) ? this.props.items.map((item, i) => {
                    let radioId = `${this.groupName}_${i}`;

                    return (
                        <div {...this.props.attributes} key={radioId}>
                            {
                                this.props.disabled
                                    ? <input type="radio" className="custom-control-input" id={radioId} name={this.groupName} value={item.value} checked={this.props.modelReference.getValue() == item.value ? true : false} disabled />
                                    : <input type="radio" className="custom-control-input" onChange={this.handleChange} id={radioId} name={this.groupName} value={item.value} checked={this.props.modelReference.getValue() == item.value ? true : false} />
                            }
                            <label className="custom-control-label" htmlFor={radioId}>{item.text}</label>
                        </div>);
                }) : null}
            </>
        );
    }
}

export const RadioButtonList = withSimpleErrorLabel(RadioButtonListImpl);