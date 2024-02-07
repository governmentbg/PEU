import { observer } from "mobx-react";
import * as React from "react";
import { BaseFieldComponent, BaseFieldProps, SelectListItem } from '../BaseFieldComponent';

interface CheckBoxListProps extends BaseFieldProps {
    itemsStore: any[];
    listItems: SelectListItem[];
    onChangeCallback?: (value: any) => void;
}

@observer export class CheckBoxList extends BaseFieldComponent<CheckBoxListProps> {

    renderInternal() {
        return (
            <div>
                {this.props.listItems ? this.props.listItems.map((item, i) => {

                    return <div className="checkbox" key={this.getId() + i}>
                        <label>
                            <input type="checkbox"
                                onChange={this.handleChange.bind(this, item.value)}
                                id={this.getId()}
                                name={this.getName()}
                                value={item.value}
                                checked={this.isChecked.bind(this, item.value)()} />{item.text}
                        </label>
                    </div>

                }) : ""}
            </div>
        );
    }

    isChecked(value: string) {
        if (this.props.itemsStore){
            var arr = this.props.itemsStore.filter((item) => {
                return item == value;
            })

            return arr.length > 0
        }

        return false;
    }

    handleChange(value:any) {
        this.props.onChangeCallback(value)
    }
}