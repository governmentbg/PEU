import { ObjectHelper } from "cnsys-core";
import { observer } from "mobx-react";
import * as React from "react";
import { BaseFieldComponent, BaseFieldProps, SelectListItem } from '../BaseFieldComponent';

interface RadioButtonListProps extends BaseFieldProps {
    items?: SelectListItem[]
}

@observer export class RadioButtonList extends BaseFieldComponent<RadioButtonListProps> {

    renderInternal() {
        // Ако имаме няколко групи от radio buttons и искаме да можем да изберем по един от всяка група, всеки бутон трябва да има name = {името на групата}. 
        // Иначе от всички radio buttons (от всички групи) може да бъде избран само един.
        let groupName = ObjectHelper.newGuid();

        return (
            <div {...this.props.attributes } >
                {this.props.items ? this.props.items.map((item, i) => {
                    return <label className="radio-inline" key={i}><input type="radio" onChange={this.handleChange} id={ObjectHelper.newGuid()} name={groupName} value={item.value} checked={this.props.modelReference.getValue() == item.value ? true : false} />{item.text}</label>
                }) : ""}
            </div>
        );
    }
}