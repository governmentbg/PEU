import { ObjectHelper } from "cnsys-core";
import { observer } from "mobx-react";
import * as React from "react";
import { BaseFieldComponent, BaseFieldProps } from '../BaseFieldComponent';

interface TextBoxProps extends BaseFieldProps {
}

@observer export class TextBox extends BaseFieldComponent<TextBoxProps> {

    renderInternal() {
        return (
            <input type="text"
                onChange={this.handleChange}
                value={!ObjectHelper.isNullOrUndefined(this.props.modelReference.getValue()) ? this.props.modelReference.getValue() : ""}
                name={this.getName()}
                id={this.getId()}
                {...this.fieldAttributes} />
        );
    }
}