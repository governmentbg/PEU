import { BindableReference, moduleContext, ObjectHelper } from 'cnsys-core';
import { runInAction } from 'mobx';
import { observer } from 'mobx-react';
import moment from 'moment';
import React from "react";
import { DateImpl as Date } from './Date';

interface PeriodProps {
    fromDateLabelText?: string;
    toDateLabelText?: string;
    modelReferenceOfFirstDate: BindableReference;
    modelReferenceOfSecondDate: BindableReference;
    dateFormat?: string;
    isValidDate?: (currentDate: any, selectedDate: any) => boolean;
}

@observer export class Period extends React.Component<PeriodProps, any> {
    private periodId: string;
    private fromId: string;
    private toId: string;

    constructor(props?: PeriodProps, context?: any) {
        super(props, context);

        this.periodId = ObjectHelper.newGuid();
        this.fromId = `${this.periodId}_from`;
        this.toId = `${this.periodId}_to`;

        if (this.props.modelReferenceOfFirstDate || this.props.modelReferenceOfSecondDate) {
            var that = this;
            runInAction("Period:EnsureModelAccess - ctor", () => {

                if (that.props.modelReferenceOfFirstDate)
                    that.props.modelReferenceOfFirstDate.ensureModelAccess();

                if (that.props.modelReferenceOfSecondDate)
                    that.props.modelReferenceOfSecondDate.ensureModelAccess();
            });
        }
    }

    render(): JSX.Element {
        const hasErrorsFirstDate = this.props.modelReferenceOfFirstDate && this.props.modelReferenceOfFirstDate.hasErrors();
        const hasErrorsSecondDate = this.props.modelReferenceOfSecondDate && this.props.modelReferenceOfSecondDate.hasErrors();

        return (
            <div className="row period-component">
                <div className="form-group col-auto">
                    <div className="d-flex">
                        <label htmlFor={this.fromId} className="mr-2 col-form-label">{this.props.fromDateLabelText ? this.props.fromDateLabelText : moduleContext.resourceManager.getResourceByKey('GL_FROM_L')}</label>
                        <Date fullHtmlName="modelReferenceOfFirstDate"
                            dateInputId={this.fromId}
                            modelReference={this.props.modelReferenceOfFirstDate}
                            onChange={this.onFromDateChange}
                            dateFormat={this.props.dateFormat ? this.props.dateFormat : null}
                            isValidDate={this.props.isValidDate ? this.props.isValidDate : null}
                            showHelpInfo={false} />
                    </div>
                </div>
                <div className="form-group col-auto">
                    <div className="d-flex">
                        <label htmlFor={this.toId} className="mr-2 col-form-label">{this.props.toDateLabelText ? this.props.toDateLabelText : moduleContext.resourceManager.getResourceByKey('GL_TO_L')}</label>
                        <Date fullHtmlName="modelReferenceOfSecondDate"
                            dateInputId={this.toId}
                            modelReference={this.props.modelReferenceOfSecondDate}
                            onChange={this.onToDateChange}
                            dateFormat={this.props.dateFormat ? this.props.dateFormat : null}
                            isValidDate={this.props.isValidDate ? this.props.isValidDate : null}
                            showHelpInfo={false} />
                    </div>
                </div>
                <div className='form-group col-12 feedback-up'>
                    <div className="help-text-inline">{moduleContext.resourceManager.getResourceByKey('GL_DATETIME_HELP_I')}</div>
                    <div className="sr-only" id={`${this.fromId}_HELP`}>{moduleContext.resourceManager.getResourceByKey('GL_DATETIME_HELP_SR_ONLY_I')}</div>
                    <div className="sr-only" id={`${this.toId}_HELP`}>{moduleContext.resourceManager.getResourceByKey('GL_DATETIME_HELP_SR_ONLY_I')}</div>
                    {hasErrorsFirstDate
                        &&
                        <ul className="invalid-feedback" id={`${this.fromId}_ERRORS`}>
                            {this.props.modelReferenceOfFirstDate.getErrors().map((err, idx) => { return (<li key={idx}><i className="ui-icon ui-icon-error mr-1"></i>{err}</li>); })}
                        </ul>}
                    {hasErrorsSecondDate
                        &&
                        <ul className="invalid-feedback" id={`${this.toId}_ERRORS`}>
                            {this.props.modelReferenceOfSecondDate.getErrors().map((err, idx) => { return (<li key={idx}><i className="ui-icon ui-icon-error mr-1"></i>{err}</li>); })}
                        </ul>}
                </div>
            </div>
        );
    }

    private onFromDateChange(event: any, value: any, modelReference: BindableReference) {
        if (value && moment.isMoment(value))
            modelReference.setValue(value.startOf('day'));
        else
            modelReference.setValue(value);
    }

    private onToDateChange(event: any, value: any, modelReference: BindableReference) {
        if (value && moment.isMoment(value))
            modelReference.setValue(value.endOf('day'));
        else
            modelReference.setValue(value);
    }
}