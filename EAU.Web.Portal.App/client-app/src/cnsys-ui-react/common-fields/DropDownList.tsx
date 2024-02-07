import { observer } from "mobx-react";
import * as React from "react";
import { BaseFieldComponent, BaseFieldProps, SelectListItem } from '../BaseFieldComponent';

interface DropDownListProps extends BaseFieldProps {
    items?: SelectListItem[];
    multiple?: boolean;
    /**Дали дропдаун списъка да има празен елемент, по подразбиране е true*/
    hasEmptyElement?: boolean;
    emptyElementValue?: string;
}

@observer export class DropDownList extends BaseFieldComponent<DropDownListProps> {

    constructor(props?: DropDownListProps, context?: any) {
        super(props, context);

        this.componentDidMount = this.componentDidMount.bind(this);
    }

    componentDidMount() {
        if (!this.props.modelReference.getValue() && !this.props.hasEmptyElement) {
            this.props.modelReference.setValue(this.props.items[0].value);
        }
    }

    renderInternal() {
        return (
            <select multiple={this.props.multiple} value={this.props.modelReference.getValue() != null ? this.props.modelReference.getValue() : ""} onChange={this.handleChange} name={this.getName()} id={this.getId()} {...this.fieldAttributes}>
                {this.props.hasEmptyElement ? <option key={0} value="">{this.props.emptyElementValue}</option> : null}
                {this.props.items ? this.props.items.map((item, i) => {
                    return <option value={item.value} key={this.getId() + i}>{item.text}</option>
                }) : ""}
            </select>
        );
    }

    protected getHandleChangeValue(e: any) {
        //Връщаме null вместо "", защото в определени случаи се разминават данните (Пример: използването на стойности от ENUM при търсене)
        return e.target.value || null;
    }
}