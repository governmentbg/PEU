import { ObjectHelper } from 'cnsys-core';
import { action, observable } from 'mobx';
import { observer } from "mobx-react";
import moment from 'moment';
import React from "react";
import Datetime from 'react-datetime';
import { BaseFieldComponent, BaseFieldProps } from '../BaseFieldComponent';

interface DurationTimeProps extends BaseFieldProps {
    timeFormat?: string;
    isValidDate?: (currentDate: any, selectedDate: any) => boolean;
    disabled?: boolean;
}

@observer export class DurationTime extends BaseFieldComponent<DurationTimeProps> {
    dateTimeImputId: string;
    @observable date: moment.Moment;

    constructor(props?: DurationTimeProps, context?: any) {
        super(props, context);

        this.dateTimeImputId = ObjectHelper.newGuid();
        this.calendarIconClick = this.calendarIconClick.bind(this);
        this.checkAndClear = this.checkAndClear.bind(this);

        this.date = moment();
        let modelValue = moment.duration(this.props.modelReference.getValue());

        if (modelValue) {
            this.date.hours(modelValue.hours());
            this.date.minutes(modelValue.minutes());
            this.date.seconds(modelValue.seconds());
        }
    }

    renderInternal() {
        return (
            <div className="input-group date-control">
                <Datetime
                    locale={moment.locale()}
                    dateFormat={false}
                    timeFormat={this.props.timeFormat ? this.props.timeFormat : "HH:mm:ss"}
                    viewMode="time"
                    value={this.date}
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

    @action protected getHandleChangeValue(date: any) {

        this.date = date;

        if (moment.isMoment(date)) {
            let duration = moment.duration();

            duration.add(date.hours(), "hours");
            duration.add(date.minutes(), "minutes");
            duration.add(date.seconds(), "seconds");

            return duration
        }

        return null;
    }

    checkAndClear(event: any) {
        var value = moment.duration(this.props.modelReference.getValue());

        if (value != undefined && !moment.isDuration(value))
            this.props.modelReference.setValue(null);
    }
}