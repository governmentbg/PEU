import { observer } from "mobx-react";
import * as React from "react";
import { BaseFieldComponent, BaseFieldProps } from '../BaseFieldComponent';

export interface TextAreaProps extends BaseFieldProps {
    rows?: number,
    cols?: number
}

@observer export class TextArea extends BaseFieldComponent<TextAreaProps> {

    renderInternal() {
        return (
            <textarea value={this.props.modelReference.getValue() || ""} rows={this.props.rows ? this.props.rows : 5} cols={this.props.cols ? this.props.cols : 30} onChange={this.handleChange} name={this.getName()} id={this.getId()} {...this.fieldAttributes} />
        );
    }
}