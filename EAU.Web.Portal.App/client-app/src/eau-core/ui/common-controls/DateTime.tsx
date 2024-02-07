import { ObjectHelper } from 'cnsys-core';
import { BaseFieldComponent, BaseFieldProps } from 'cnsys-ui-react';
import { action, observable } from "mobx";
import { observer } from "mobx-react";
import moment, { isMoment } from 'moment';
import React from "react";
import Datetime from 'react-datetime';
import { threadId } from 'worker_threads';
import { withSimpleErrorLabel } from '../WithSimpleErrorLabel';

interface DateTimeProps extends BaseFieldProps {
    dateFormat?: string | boolean;
    showSeparateDateAndTime?: boolean;
    modeDateTo?: boolean
}

@observer class DateTimeImpl extends BaseFieldComponent<DateTimeProps> {
    @observable private hours: string;
    @observable private minutes: string;

    @observable private isSetMinutesAndSeconds;


    private dateInputId: string;

    constructor(props?: DateTimeProps, context?: any) {
        super(props, context);

        this.dateInputId = ObjectHelper.newGuid();
        var value = this.props.modelReference.getValue();
        this.hours = this.formatToTwoDigitsTime(moment.isMoment(value) ? value.hours().toString() : "")
        this.minutes = this.formatToTwoDigitsTime(moment.isMoment(value) ? value.minutes().toString() : "")

        this.dateIconClick = this.dateIconClick.bind(this);
        this.handleDateChange = this.handleDateChange.bind(this);
        this.handleHoursChange = this.handleHoursChange.bind(this);
        this.handleMinutesChange = this.handleMinutesChange.bind(this);
        this.timeIconClick = this.timeIconClick.bind(this);
        this.checkAndClear = this.checkAndClear.bind(this);
    }


    changeSeparateModeDate(value: any) {

        if (this.props.modeDateTo) {
            if (moment(value).format("HH:mm") == "00:00") {
                value = moment(value).hours(23);
                value = moment(value).minutes(59);
                this.isSetMinutesAndSeconds = true;
            }
        }

        this.props.modelReference.setValue(value);
    }

    changeSeparateModeMinutes(value) {
        this.isSetMinutesAndSeconds = true;
        this.props.modelReference.setValue(value);
    }


    @action renderInternal() {
        var value = this.props.modelReference.getValue();

        if (this.props.showSeparateDateAndTime) {
            var hours = "";
            var minutes = "";

            if (moment.isMoment(value)) {
                var h = value.hours()
                var m = value.minutes();

                hours = parseInt(this.hours) != h && this.hours != "" ? this.formatToTwoDigitsTime(h.toString()) : this.hours;
                minutes = parseInt(this.minutes) != m && this.minutes != "" ? this.formatToTwoDigitsTime(m.toString()) : this.minutes;
            } else {
                this.hours = "00";
                this.minutes = "00";
            }

            return (

                <div className="row">
                    <div className="col-sm-6 col-md-7 col-lg-8 col-xl-8">
                        <div className="input-group date">
                            <Datetime
                                locale={moment.locale()}
                                timeFormat={false}
                                value={value || ""}
                                inputProps={{ className: "form-control input-datetime" + (this.fieldAttributes ? " " + this.fieldAttributes.className : ""), id: this.dateInputId }}
                                onChange={(value) => { this.changeSeparateModeDate(value) }}
                                closeOnSelect={true}
                                dateFormat={this.props.dateFormat ? this.props.dateFormat : true}
                                {...this.fieldAttributes} />
                            <span className="input-group-btn" onClick={this.dateIconClick}>
                                <button className="btn btn-default" type="button">
                                    <i className="ui-icon ui-icon-calendar" aria-hidden="true"></i>
                                </button>
                            </span>
                        </div>
                    </div>

                    <div className="col-sm-6 col-md-5 col-lg-4 col-xl-4">
                        <div className="input-group date">
                            <Datetime
                                locale={moment.locale()}
                                dateFormat={false}
                                value={value || ""}
                                inputProps={{ className: "form-control input-datetime" + (this.fieldAttributes ? " " + this.fieldAttributes.className : ""), id: this.dateInputId + 'time' }}
                                onChange={(value) => { this.changeSeparateModeMinutes(value) }}
                                closeOnSelect={true}
                                //dateFormat={this.props.dateFormat ? this.props.dateFormat : moment.defaultFormat.substring(0, moment.defaultFormat.indexOf('T'))}
                                {...this.fieldAttributes} />
                            <span className="input-group-btn" onClick={this.timeIconClick}>
                                <button className="btn btn-default" type="button">
                                    <i className="ui-icon ui-icon-clock" aria-hidden="true"></i>
                                </button>
                            </span>
                        </div>
                    </div>
                </div>
            )
        } else {
            return (

                <div className="input-group date">
                    <Datetime
                        locale={moment.locale()}
                        timeFormat={true}
                        value={value || ""}
                        inputProps={{ className: "form-control input-datetime" + (this.fieldAttributes ? " " + this.fieldAttributes.className : ""), id: this.dateInputId, onBlur: this.checkAndClear }}
                        onChange={this.handleChange}
                        dateFormat={this.props.dateFormat ? this.props.dateFormat : true}
                        {...this.fieldAttributes}
                    />
                    <div className="input-group-append" onClick={this.dateIconClick}>
                        <button className="btn btn-light" type="button">
                            <i className="ui-icon ui-icon-calendar"></i>
                        </button>
                    </div>
                </div>

            );
        }
    }

    protected dateIconClick(): void {
        document.getElementById(this.dateInputId).focus();
    }

    protected timeIconClick(): void {
        document.getElementById(this.dateInputId + 'time').focus();
    }

    protected getHandleChangeValue(event: any) {
        var date: moment.Moment = event;

        return date;
    }

    @action private handleDateChange(value: any) {
        this.handleChange(value)
    }

    @action private handleHoursChange(event: any) {
        var value = event.target.value;

        if (!isNaN(value) || value == "") {
            if ((parseInt(value) >= 0 && parseInt(value) <= 23) || value == "") {
                this.hours = value;

                var mrValue = this.props.modelReference.getValue();
                if (moment.isMoment(mrValue))
                    this.props.modelReference.setValue(moment(mrValue).hours(!isNaN(parseInt(value)) ? parseInt(this.hours) : 0));
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
                    this.props.modelReference.setValue(moment(mrValue).minutes(!isNaN(parseInt(value)) ? parseInt(this.minutes) : 0));
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

    checkAndClear(event: any) {
        var value = this.props.modelReference.getValue();

        if (value != undefined && !isMoment(value))
            this.props.modelReference.setValue(null);
    }
}

export const DateTime = withSimpleErrorLabel(DateTimeImpl);