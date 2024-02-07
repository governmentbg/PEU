import { ObjectHelper } from 'cnsys-core';
import { action, observable } from "mobx";
import { observer } from "mobx-react";
import moment from 'moment';
import React from "react";
import Datetime from 'react-datetime';
import { BaseFieldComponent, BaseFieldProps } from '../BaseFieldComponent';

interface DateTimeProps extends BaseFieldProps {
    dateFormat?: string | boolean;
    showSeparateDateAndTime?: boolean;
}

@observer export class DateTime extends BaseFieldComponent<DateTimeProps> {
    @observable private hours: string;
    @observable private minutes: string;
    private dateInputId: string;

    constructor(props?: DateTimeProps, context?: any) {
        super(props, context);

        this.dateInputId = ObjectHelper.newGuid();
        var mrValue = this.props.modelReference.getValue();
        this.hours = this.formatToTwoDigitsTime(moment.isMoment(mrValue) ? mrValue.hours().toString() : "")
        this.minutes = this.formatToTwoDigitsTime(moment.isMoment(mrValue) ? mrValue.minutes().toString() : "")

        this.dateIconClick = this.dateIconClick.bind(this);
        this.handleDateChange = this.handleDateChange.bind(this);
        this.handleHoursChange = this.handleHoursChange.bind(this);
        this.handleMinutesChange = this.handleMinutesChange.bind(this);
    }

    @action renderInternal() {
        var mrValue = this.props.modelReference.getValue();

        if (this.props.showSeparateDateAndTime) {
            var hours = "";
            var minutes = "";

            if (moment.isMoment(mrValue)) {
                var h = mrValue.hours()
                var m = mrValue.minutes();

                hours = parseInt(this.hours) != h && this.hours != "" ? this.formatToTwoDigitsTime(h.toString()) : this.hours;
                minutes = parseInt(this.minutes) != m && this.minutes != "" ? this.formatToTwoDigitsTime(m.toString()) : this.minutes;
            } else {
                this.hours = "00";
                this.minutes = "00";
            }

            return (
                <div className="inline-control">
                    <div className="inline-group">

                        <div className="inline-control date-control">
                            <div className="input-group">
                                <Datetime
                                    locale={moment.locale()}
                                    timeFormat={false}
                                    value={mrValue || ""}
                                    inputProps={{ className: "form-control input-datetime" + (this.fieldAttributes ? " " + this.fieldAttributes.className : ""), id: this.dateInputId }}
                                    onChange={this.handleDateChange}
                                    closeOnSelect={true}
                                    dateFormat={this.props.dateFormat ? this.props.dateFormat : moment.defaultFormat.substring(0, moment.defaultFormat.indexOf('T'))}
                                    {...this.fieldAttributes} />
                                <span className="input-group-btn" onClick={this.dateIconClick}>
                                    <button className="btn btn-default">
                                        <i className="fa fa-calendar" aria-hidden="true"></i>
                                    </button>
                                </span>
                            </div>
                        </div>
                        <div className="inline-control">
                            <div className="inline-group time-control">
                                <div className="inline-control">
                                    <input id={hours} maxLength={2} onBlur={this.formatHoursValue.bind(this)} value={hours} onChange={this.handleHoursChange} className={"form-control" + (this.fieldAttributes ? " " + this.fieldAttributes.className : "")} />
                                </div>
                                <div className="inline-control time-delimiter">
                                    :
                                    </div>
                                <div className="inline-control">
                                    <input id={minutes} maxLength={2} onBlur={this.formatMinutesValue.bind(this)} value={minutes} onChange={this.handleMinutesChange} className={"form-control" + (this.fieldAttributes ? " " + this.fieldAttributes.className : "")} />
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            )
        } else {
            return (
                <div className="form-group">
                    <div className="input-group">
                        <Datetime
                            locale={moment.locale()}
                            timeFormat={true}
                            value={mrValue || ""}
                            inputProps={{ className: "form-control input-datetime" + (this.fieldAttributes ? " " + this.fieldAttributes.className : ""), id: this.dateInputId }}
                            onChange={this.handleChange}
                            dateFormat={this.props.dateFormat ? this.props.dateFormat : moment.defaultFormat.substring(0, moment.defaultFormat.indexOf('T'))}
                            {...this.fieldAttributes}
                        />
                        <span className="input-group-addon" style={{ cursor: "pointer" }} onClick={this.dateIconClick}>
                            <i className="fa fa-calendar"></i>
                        </span>
                    </div>
                </div>
            );
        }
    }

    protected dateIconClick(): void {
        document.getElementById(this.dateInputId).focus();
    }

    protected getHandleChangeValue(value: any) {
        return value;
    }

    @action private handleDateChange(value: any) {
        this.handleChange(value)
    }

    @action private handleHoursChange(event: any) {
        var value = event.target.value;

        if (!isNaN(value) || value == "") {
            if ((parseInt(value) >= 0 && parseInt(value) <= 23) || value == "") {
                this.hours = value;
                var mrValue = this.props.modelReference.getValue()
                if (moment.isMoment(mrValue))
                    this.props.modelReference.setValue(mrValue.hours(!isNaN(parseInt(value)) ? parseInt(this.hours) : 0));
            }
        }
    }

    @action private handleMinutesChange(event: any) {
        var value = event.target.value;

        if (!isNaN(value) || value == "") {
            if ((parseInt(value) >= 0 && parseInt(value) <= 59) || value == "") {
                this.minutes = value;
                var mrValue = this.props.modelReference.getValue();
                if (moment.isMoment(mrValue))
                    this.props.modelReference.setValue(mrValue.minutes(!isNaN(parseInt(value)) ? parseInt(this.minutes) : 0));
            }
        }
    }

    formatHoursValue() {
        if (this.hours.length == 1)
            this.hours = "0" + this.hours;
        else if (this.hours == "")
            this.hours = "00"

        this.forceUpdate();
    }

    formatMinutesValue() {
        if (this.minutes.length == 1)
            this.minutes = "0" + this.minutes;
        else if (this.minutes == "")
            this.minutes = "00"

        this.forceUpdate();
    }

    formatToTwoDigitsTime(value: string) {
        if (value.length == 1)
            value = "0" + value;

        return value;
    }
}