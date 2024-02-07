import { observer } from "mobx-react";
import * as React from "react";
import { BaseFieldComponent, BaseFieldProps } from '../BaseFieldComponent';

interface PasswordProps extends BaseFieldProps {
}

@observer export class Password extends BaseFieldComponent<PasswordProps> {

    renderInternal() {
        return (
            <input type="password" onChange={this.handleChange} value={this.props.modelReference.getValue() || ""} name={this.getName()} id={this.getId()} {...this.fieldAttributes} />
        );
    }
}