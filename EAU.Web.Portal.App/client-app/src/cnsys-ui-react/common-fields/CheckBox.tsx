import { observer } from "mobx-react";
import * as React from "react";
import { BaseFieldComponent, BaseFieldProps } from '../BaseFieldComponent';

interface CheckBoxProps extends BaseFieldProps {
}

@observer export class CheckBox extends BaseFieldComponent<CheckBoxProps> {
    renderInternal() {
        return (
            <input type="checkbox" onChange={this.handleChange} checked={this.props.modelReference.getValue() || false} name={this.getName()} id={this.getId()} {...this.fieldAttributes} />
        );
    }

    getHandleChangeValue(event: any) {
        return event.target.checked;
    }
}