import { ObjectHelper } from 'cnsys-core';
import { observer } from "mobx-react";
import moment from 'moment';
import React from "react";
import Datetime from 'react-datetime';
import { BaseFieldComponent, BaseFieldProps } from '../BaseFieldComponent';

interface DateProps extends BaseFieldProps {
    dateFormat?: string;
    isValidDate?: (currentDate: any, selectedDate: any) => boolean;
}

@observer export class Date extends BaseFieldComponent<DateProps> {
    private dateTimeImputId: string;

    constructor(props?: DateProps, context?: any) {
        super(props, context);

        this.dateTimeImputId = ObjectHelper.newGuid();
        this.calendarIconClick = this.calendarIconClick.bind(this);
    }

    renderInternal() {
        return (
            <div className="form-group">
                <div className="input-group">
                    <Datetime
                        locale={moment.locale()}
                        timeFormat={false}
                        value={this.props.modelReference.getValue() || ""}
                        inputProps={{ className: "form-control input-datetime" + (this.fieldAttributes ? " " + this.fieldAttributes.className : ""), id: this.dateTimeImputId }}
                        inputFormat={moment.defaultFormat}
                        onChange={this.handleChange} {...this.fieldAttributes}
                        closeOnSelect={true}
                        dateFormat={this.props.dateFormat ? this.props.dateFormat : moment.defaultFormat.substring(0, moment.defaultFormat.indexOf('T'))}
                        isValidDate={this.props.isValidDate ? this.props.isValidDate : null} />
                    <span className="input-group-addon" style={{ cursor: "pointer" }} onClick={this.calendarIconClick}>
                        <i className="fa fa-calendar"></i>
                    </span>
                </div>
            </div>
        );
    }

    protected calendarIconClick(event: any): void {
        document.getElementById(this.dateTimeImputId).focus();
    }

    protected getHandleChangeValue(event: any) {
        return event;
    }
}