import { ObjectHelper } from 'cnsys-core';
import { observable } from 'mobx';
import { observer } from "mobx-react";
import moment, { isMoment } from 'moment';
import React from "react";
import Datetime from 'react-datetime';
import { BaseFieldComponent, BaseFieldProps } from '../BaseFieldComponent';

interface TimeProps extends BaseFieldProps {
    timeFormat?: string;
    isValidDate?: (currentDate: any, selectedDate: any) => boolean;
    disabled?: boolean;
}

@observer export class Time extends BaseFieldComponent<TimeProps> {
    dateTimeImputId: string;
    @observable date: any;

    constructor(props?: TimeProps, context?: any) {
        super(props, context);

        this.dateTimeImputId = ObjectHelper.newGuid();
        this.calendarIconClick = this.calendarIconClick.bind(this);
        this.checkAndClear = this.checkAndClear.bind(this);
    }

    renderInternal() {
        return (
            <div className="input-group date-control">
                <Datetime
                    locale={moment.locale()}
                    dateFormat={false}
                    timeFormat={this.props.timeFormat ? this.props.timeFormat : "HH:mm"}
                    viewMode="time"
                    value={this.props.modelReference.getValue() || ""}
                    inputProps={{
                        disabled: this.fieldAttributes && this.fieldAttributes.disabled === true ? this.fieldAttributes.disabled : this.props.disabled,
                        onBlur: this.checkAndClear, className: "form-control" + (this.fieldAttributes ? " " + this.fieldAttributes.className : ""),
                        id: this.dateTimeImputId
                    }}
                    inputFormat={moment.defaultFormat}
                    onChange={this.handleChange}
                    closeOnSelect={true}
                    isValidDate={this.props.isValidDate ? this.props.isValidDate : null}
                    {...this.fieldAttributes}
                />

                <div className="input-group-append" onClick={this.calendarIconClick}>
                    <button className="btn btn-secondary" type="button">
                        <i className="ui-icon ui-icon-clock"></i>
                    </button>
                </div>
            </div>
        );
    }

    protected calendarIconClick(event: any): void {
        document.getElementById(this.dateTimeImputId).focus();
    }

    protected getHandleChangeValue(event: any) {
        var date: moment.Moment = event;

        return date;
    }

    checkAndClear(event: any) {
        var value = this.props.modelReference.getValue();

        if (value != undefined && !isMoment(value))
            this.props.modelReference.setValue(null);
    }
}