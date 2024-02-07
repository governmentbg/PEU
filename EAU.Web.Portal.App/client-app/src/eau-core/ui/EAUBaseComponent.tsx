import { BindableReference, ErrorLevels, Helper, IModelErrors, ObjectHelper, SelectListItem, UIHelper } from 'cnsys-core';
import { BaseComponent, BaseProps } from 'cnsys-ui-react';
import moment from "moment";
import React from "react";
import { ResourceHelpers } from '../common/ResourceHelpers';
import { moduleContext } from '../ModuleContext';
import { Date } from './common-controls/Date';
import { DateTime } from './common-controls/DateTime';
import { DropDownList } from './common-controls/DropdownList';
import { DurationTime } from './common-controls/DurationTime';
import { RadioButtonList } from './common-controls/RadioButtonList';
import { SelectComponent } from "./common-controls/SelectComponent";
import { TextArea } from './common-controls/TextArea';
import { TextBox } from './common-controls/TextBox';
import { Time } from './common-controls/Time';
import { Duration } from './common-controls/Duration';
import { Amount } from './common-controls/Amount';

var txtBoxLabelForAttributes = { className: "ml-2 col-form-label" };

export const attributesClassFormControl = { className: 'form-control' };
export const attributesClassFormControlReadOnly = { className: 'form-control', readOnly: 'readonly' };
export const attributesClassFormControlDisabled = { className: 'form-control', disabled: 'disabled' };
export const attributesClassFormControlReqired = { className: 'form-control' };
export const attributesClassFormControlLabel = { className: 'form-control-label' };
export const attributesClassFormControlRequiredLabel = { className: 'form-control-label required-field' };
const attributesClassCustomControlInput = { className: "custom-control-input" };
const attributesClassCustomControlLabel = { className: "custom-control-label" };
const attributeClassRadiobuttonListElementContainer = { className: "custom-control custom-radio" };
export const attributeClassRequiredLabel = { className: 'field-title field-title--form required-field' };
export const attributesClassFormControlMinWidthZero = { className: 'form-control min-width-0' };
export const classNameMr2ColFormLabel = { className: "mr-2 col-form-label" }
export const attributesClassInlineRadioButtons = { className: "custom-control-inline custom-control custom-radio" }
export const attributesClassFormControlMaxL450 = { className: 'form-control', maxLength: 450 };
export const attributesTypePasswordFormControlRequired = { type: "password", className: "form-control" }

export abstract class EAUBaseComponent<TProps extends BaseProps, TModel extends IModelErrors> extends BaseComponent<TProps, TModel> {

    protected getResource(resourceKey: string) {
        return moduleContext.resourceManager.getResourceByKey(resourceKey);
    }

    protected getResourceByProperty(selector: (model: TModel) => any) {
        return ResourceHelpers.getResourceByProperty(selector, this.model);
    }

    protected labelFor(selector: (model: TModel) => any, labelKey?: string, attributes?: any): any {
        var labelResource = labelKey
            ? moduleContext.resourceManager.getResourceByKey(labelKey)
            : ResourceHelpers.getResourceByProperty(selector, this.model);

        return super.labelFor(selector, labelResource, attributes)
    }

    protected textBoxFor(selector: (model: TModel) => any, attributes?: any, onChange?: (event: any, value: any, modelReference: BindableReference) => void, suffixTextKey?: string): any {

        attributes = attributes ? attributes : attributesClassFormControl;

        if (suffixTextKey) {
            return <span className="input-group">
                <TextBox {...this.bind(selector)} attributes={attributes} onChange={onChange} />
                {this.labelFor(selector, suffixTextKey, txtBoxLabelForAttributes)}
            </span>
        }

        return <TextBox {...this.bind(selector)} attributes={attributes} onChange={onChange} />;
    }

    protected textAreaFor(selector: (model: TModel) => string, cols?: number, rows?: number, attributes?: any, onChange?: (event: any, value: any, modelReference: BindableReference) => void): any {
        attributes = attributes ? attributes : attributesClassFormControl;

        return <TextArea {...this.bind(selector)} cols={cols} rows={rows} attributes={attributes} onChange={onChange} />;
    }

    protected dropDownListFor(selector: (model: TModel) => any, items: SelectListItem[], attributes?: any, onChange?: (event: any, value: any, modelReference: BindableReference) => void, hasEmptyElement?: boolean, emptyElementValue?: string): any {
        attributes = attributes ? attributes : attributesClassFormControl;

        return <DropDownList {...this.bind(selector)} items={items} attributes={attributes} onChange={onChange} hasEmptyElement={hasEmptyElement} emptyElementValue={emptyElementValue} />;
    }

    protected radioButtonListFor(selector: (model: TModel) => any, items: SelectListItem[], attributes?: any, onChange?: (event: any, value: any, modelReference: BindableReference) => void, disabled?: boolean): any {
        attributes = attributes ? attributes : attributeClassRadiobuttonListElementContainer;

        return <RadioButtonList {...this.bind(selector)} items={items} attributes={attributes} onChange={onChange} disabled={disabled} />
    }

    protected checkBoxFor(selector: (model: TModel) => boolean, labelTextKey?: string, attributes?: any, onChange?: (event: any, value: any, modelReference: BindableReference) => void): any {
        var labelText = labelTextKey ? labelTextKey : null;
        return <>
            {super.checkBoxFor(selector, !attributes ? attributesClassCustomControlInput : attributes, onChange)}
            {this.labelFor(selector, labelText, attributesClassCustomControlLabel)}
        </>
    }

    protected textDisplayFor(selector: (model: TModel) => string | number, preserveLineBreaks?: boolean): JSX.Element {
        let propName = Helper.getPropertyNameBySelector(selector);

        return (
            <>
                <p className={`field-text${preserveLineBreaks ? ' preserve-line-breaks' : ""}`}>{(this.model as any)[propName]}</p>
                {this.propertyErrorsDispleyFor(selector)}
            </>);
    }

    protected selectFor(selector: (model: TModel) => any, options: any, defaultValues?: any, attributes?: any, placeholder?: string, onChange?: (event: any, value: any, modelReference: BindableReference) => void, isMulti?: boolean): any {
        return <SelectComponent {...this.bind(selector)} attributes={attributes} options={options} defaultValues={defaultValues} isMulti={isMulti} placeholder={placeholder} onChange={onChange} />
    }

    protected propertyErrorsDispleyFor(selector: (model: TModel) => any): JSX.Element {
        let propErrors: { message: string, level: any }[] = [];
        let propName = Helper.getPropertyNameBySelector(selector);

        propErrors = this.model.getPropertyErrors(propName);

        if (propErrors && propErrors.length > 0) {
            return (
                <ul className="invalid-feedback">
                    {propErrors.map((err, idx) => { return (<li key={idx}><i className="ui-icon ui-icon-error mr-1"></i>{err.message}</li>); })}
                </ul>);
        } else
            return null;

    }

    protected propertyErrorNestedModelsDispleyFor(model: IModelErrors, propName: string): JSX.Element {
        if (ObjectHelper.isNullOrUndefined(model)) return null;

        let propErrors: { message: string, level: ErrorLevels }[] = model.getPropertyErrors(propName);

        if (propErrors && propErrors.length > 0)
            return (
                <ul className="invalid-feedback">
                    {propErrors.map((err, idx) => { return (<li key={idx}><i className="ui-icon ui-icon-error mr-1"></i>{err.message}</li>); })}
                </ul>);
        else
            return null;
    }

    protected propertiesErrorsDispleyFor(propNames: string[]): JSX.Element {
        let propErrors: { message: string, level: any }[] = [];

        for (let i: number = 0; i < propNames.length; i++) {
            let curPropError: { message: string, level: any }[] = this.model.getPropertyErrors(propNames[i]);


            if (curPropError && curPropError.length > 0) {
                for (let j: number = 0; j < curPropError.length; j++) {
                    let err = curPropError[j];
                    propErrors.push({ message: err.message, level: err.level });
                }
            }

        }

        if (propErrors && propErrors.length > 0) {
            return (
                <ul className="invalid-feedback">
                    {propErrors.map((err, idx) => { return (<li key={idx}><i className="ui-icon ui-icon-error mr-1"></i>{err.message}</li>); })}
                </ul>);
        } else
            return null;
    }

    protected inlineHelpFor(selector: (model: TModel) => any, helpTextCode: string, srOnlyHelpTextCode?: string): JSX.Element {
        let propName = Helper.getPropertyNameBySelector(selector);
        let id = this.getFullHtmlName(propName).replace(".", "_");

        if (ObjectHelper.isStringNullOrEmpty(srOnlyHelpTextCode)) {
            return (
                <div className="help-text-inline" id={`${id}_HELP`}>
                    {this.getResource(helpTextCode)}
                </div>
            );
        } else {
            return (
                <>
                    <div className="help-text-inline">{this.getResource(helpTextCode)}</div>
                    <div id={`${id}_HELP`} className="sr-only">{this.getResource(srOnlyHelpTextCode)}</div>
                </>
            );
        }
    }

    //#region
    //Date

    protected dateFor(selector: (model: TModel) => any, dateFormat?: string, isValidDate?: (currentDate: any, selectedDate: any) => boolean, attributes?: any, onChange?: (event: any, value: any, modelReference: BindableReference) => void, fullWidth?: boolean, skipHelpInfo?: boolean): any;
    protected dateFor(propertyName: string, dateFormat?: string, isValidDate?: (currentDate: any, selectedDate: any) => boolean, attributes?: any, onChange?: (event: any, value: any, modelReference: BindableReference) => void, fullWidth?: boolean, skipHelpInfo?: boolean): any;
    protected dateFor(selector: any, dateFormat?: string, isValidDate?: (currentDate: any, selectedDate: any) => boolean, attributes?: any, onChange?: (event: any, value: any, modelReference: BindableReference) => void, fullWidth?: boolean, skipHelpInfo?: boolean): any {
        return <Date {...this.bind(selector)} attributes={attributes} onChange={onChange} dateFormat={dateFormat ? dateFormat : null} isValidDate={isValidDate ? isValidDate : null} fullWidth={fullWidth} showHelpInfo={!skipHelpInfo} />
    }

    protected dateDisplayFor(date: moment.Moment, format?: string, abbr?: string) {
        if (date && moment.isMoment(date)) {
            if (format)
                return abbr ? `${date.format(format)} ${abbr}` : date.format(format);
            else
                return abbr ? `${date.format("l")} ${abbr}` : date.format("l")
        }

        return "";
    }

    protected dateTimeFor(selector: (model: TModel) => any, attributes?: any, dateFormat?: string | boolean, showSeparateDateAndTime?: boolean, modeDateTo?: boolean): any {
        return <DateTime {...this.bind(selector)} attributes={attributes} dateFormat={dateFormat} showSeparateDateAndTime={showSeparateDateAndTime} modeDateTo={modeDateTo} />
    }

    protected timeFor(selector: (model: TModel) => any, attributes?: any, onChange?: (event: any, value: any, modelReference: BindableReference) => void, timeFormat?: string): any {
        return <Time {...this.bind(selector)} attributes={attributes} onChange={onChange} timeFormat={timeFormat} />;
    }

    protected durationTimeFor(selector: (model: TModel) => any, attributes?: any, onChange?: (event: any, value: any, modelReference: BindableReference) => void, timeFormat?: string): any {
        return <DurationTime {...this.bind(selector)} attributes={attributes} onChange={onChange} timeFormat={timeFormat} />;
    }

    protected durationFor(selector: (model: TModel) => any, attributes?: any, showSecondsWithDecimalPoint?: boolean, onChange?: (event: any, value: any, modelReference: BindableReference) => void): any {
        return <Duration {...this.bind(selector)} attributes={attributes} showSecondsWithDecimalPoint={showSecondsWithDecimalPoint} onChange={onChange} />;
    }

    protected amountFor(selector: (model: TModel) => number, attributes?: any, onChange?: (event: any, value: any, modelReference: BindableReference) => void): any {
        return <Amount {...this.bind(selector)} attributes={attributes} onChange={onChange} />
    }

    protected durationDisplayFor(duration: moment.Duration, displayMilliseconds = true) {
        if (duration) {
            let days = UIHelper.formatToTwoDigitsTime(Math.floor(duration.asDays()));

            let hours = UIHelper.formatToTwoDigitsTime(duration.hours());
            let minutes = UIHelper.formatToTwoDigitsTime(duration.minutes());
            let seconds = UIHelper.formatToTwoDigitsTime(duration.seconds());
            let milliseconds = UIHelper.formatToTwoDigitsTime(duration.milliseconds());
            let daysLabel = Number(days) > 1 ? this.getResource("GL_DAYS_L") : this.getResource("GL_DAY_L")

            return `${Number(days) > 0 ? days + " " + daysLabel + " " : ""}${hours}:${minutes}:${seconds}${displayMilliseconds ? '.' + milliseconds : ''}`
        }

        return null;
    }

    //#endregion

    protected currencyDisplayFor(amount: any, currencyAbbr?: string, abbrPosition?: 'before' | 'after') {

        amount = ObjectHelper.isNumber(amount) ? amount : 0;

        if (currencyAbbr) {

            if (abbrPosition && abbrPosition == "before")
                return `${currencyAbbr} ${amount.toFixed(2)}`

            return `${amount.toFixed(2)} ${currencyAbbr}`
        }

        return amount.toFixed(2);
    }

    protected getModelErrorSummary(): JSX.Element {
        if (this.model.hasErrors()) {
            let errs: { message: string, level: any }[] = this.model.getModelErrors();

            return (
                <div className="alert alert-danger" role="alert">
                    {errs.map((err, idx) => {
                        return (<p key={idx}>{err.message}</p>);
                    })}
                </div>);
        }

        return null;
    }

    protected textDisplay(propValue: string, model: IModelErrors, propName: string, preserveLineBreaks?: boolean): JSX.Element {
        if (ObjectHelper.isNullOrUndefined(model)) return null;

        let propErrors: { message: string, level: ErrorLevels }[] = model.getPropertyErrors(propName);

        return (
            <>
                <p className={`field-text${preserveLineBreaks ? ' preserve-line-breaks' : ""}`}>{propValue}</p>
                {propErrors && propErrors.length > 0 ?
                    <ul className="invalid-feedback">
                        {propErrors.map((err, idx) => { return (<li key={idx}><i className="ui-icon ui-icon-error mr-1"></i>{err.message}</li>); })}
                    </ul>
                    : null}
            </>);
    }
}