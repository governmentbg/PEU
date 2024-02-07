import { ObjectHelper } from 'cnsys-core';
import { BaseFieldComponent, BaseFieldProps } from 'cnsys-ui-react';
import { observable } from 'mobx';
import { observer } from "mobx-react";
import moment, { isMoment } from 'moment';
import React from "react";
import Datetime from 'react-datetime';
import { withSimpleErrorLabel } from "../WithSimpleErrorLabel";
import { moduleContext } from '../../ModuleContext'

interface DateProps extends BaseFieldProps {
    dateFormat?: string;
    isValidDate?: (currentDate: any, selectedDate: any) => boolean;
    disabled?: boolean;
    fullWidth?: boolean;
    dateInputId?: string;
    showHelpInfo?: boolean;
}

@observer export class DateImpl extends BaseFieldComponent<DateProps> {
    private dateTimeImputId: string;
    private dateTimeSrHelpId: string;
    @observable date: any;

    constructor(props?: DateProps, context?: any) {
        super(props, context);

        //Bind
        this.calendarIconClick = this.calendarIconClick.bind(this);
        this.checkAndClear = this.checkAndClear.bind(this);

        //Init
        this.dateTimeImputId = ObjectHelper.isStringNullOrEmpty(this.props.dateInputId) ? this.getId() : this.props.dateInputId;
        this.dateTimeSrHelpId = `${this.dateTimeImputId}_HELP`;
    }

    renderInternal() {
        let showHelp = ObjectHelper.isNullOrUndefined(this.props.showHelpInfo) ? true : this.props.showHelpInfo;

        return (
            <>
                <div className={`input-group ${this.props.fullWidth === true ? "" : "date-control"}`}>
                    <Datetime
                        locale={moment.locale()}
                        timeFormat={false}
                        value={this.props.modelReference.getValue() || ""}
                        inputProps={{
                            disabled: this.fieldAttributes && this.fieldAttributes.disabled === true ? this.fieldAttributes.disabled : this.props.disabled,
                            onBlur: this.checkAndClear, className: "form-control" + (this.fieldAttributes ? " " + this.fieldAttributes.className : ""),
                            id: this.dateTimeImputId,
                            "aria-describedby": this.props.modelReference && this.props.modelReference.hasErrors() ? `${this.dateTimeImputId}_ERRORS ${this.dateTimeSrHelpId}` : this.dateTimeSrHelpId
                        }}
                        inputFormat={moment.defaultFormat}
                        onChange={this.handleChange}
                        closeOnSelect={true}
                        dateFormat={this.props.dateFormat ? this.props.dateFormat : true}
                        isValidDate={this.props.isValidDate ? this.props.isValidDate : null}
                        {...this.fieldAttributes}
                    />

                    <div className="input-group-append" onClick={this.calendarIconClick}>
                        <button className="btn btn-light" type="button" tabIndex={-1}>
                            <i className="ui-icon ui-icon-calendar"></i>
                        </button>
                    </div>
                </div>
                {showHelp &&
                    <>
                        <div className="help-text-inline">{moduleContext.resourceManager.getResourceByKey('GL_DATETIME_HELP_I')}</div>
                        <div className="sr-only" id={this.dateTimeSrHelpId}>{moduleContext.resourceManager.getResourceByKey('GL_DATETIME_HELP_SR_ONLY_I')}</div>
                    </>}
            </>
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

export const Date = withSimpleErrorLabel(DateImpl);