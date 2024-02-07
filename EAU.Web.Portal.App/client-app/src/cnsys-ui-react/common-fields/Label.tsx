import { observer } from "mobx-react";
import * as React from "react";
import { BaseFieldComponent, BaseFieldProps } from '../BaseFieldComponent';

interface LabelProps extends BaseFieldProps {
    labelText?: string;
}

@observer export class Label extends BaseFieldComponent<LabelProps> {

    render() {
        //задължително поле !!
        
        return (
            <label htmlFor={this.getId()} {...this.fieldAttributes}>{this.props.labelText}</label>
        )
    }

    renderInternal(): JSX.Element {
        throw "Not Implemented";
    }
}