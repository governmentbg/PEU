import { ArrayHelper, BindableReference, Helper, IBaseValidator, IModelErrors, ObjectHelper, ResourceHelpers, TypeSystem } from 'cnsys-core';
import { action, IReactionDisposer, observable, reaction, runInAction } from "mobx";
import { observer } from "mobx-react";
import React from "react";
import { SelectListItem } from "./BaseFieldComponent";
import { Amount, CheckBox, CheckBoxList, Date, DateTime, DropDownList, Duration, DurationTime, Hint, Label, Password, PasswordStrengthMeter, RadioButtonList, RawHTML, TextArea, TextBox, Time } from './common-fields';

export enum ViewMode {
    Edit = 1,
    Display = 2
}

//#region Props interfaces

export interface CoreProps {
    modelReference?: BindableReference,
    fullHtmlName?: string,
}

export interface BaseProps extends CoreProps {
    viewMode?: ViewMode
}

//#endregion
@observer
export class BaseComponent<TProps extends BaseProps, TModel extends IModelErrors> extends React.Component<TProps, any> {

    private currentDisposer: IReactionDisposer;
    private evaluateModelReferense: boolean;
    @observable protected model: TModel;

    protected validators: IBaseValidator<IModelErrors, any>[];
    private bindableReferencesCache: any;

    constructor(props?: TProps, context?: any) {
        super(props, context);

        this.bindableReferencesCache = {};

        this.bind = this.bind.bind(this);

        this.componentWillUpdate = this.componentWillUpdate.bind(this);
        this.componentWillUnmount = this.componentWillUnmount.bind(this);

        if (this.props && this.props.modelReference) {

            var that = this;
            runInAction("BaseComponent:EnsureModelAccess - ctor", () => {
                that.props.modelReference.ensureModelAccess();
            });

            this.model = this.props.modelReference.getValue();

            this.attachToModelReference(this.props.modelReference);
        }

        this.evaluateModelReferense = false;
    }

    render() {

        if (this.props.viewMode && this.props.viewMode == ViewMode.Edit) {
            return this.renderEdit();
        }

        if (this.props.viewMode && this.props.viewMode == ViewMode.Display) {
            return this.renderDisplay();
        }
    }

    renderEdit(): JSX.Element | JSX.Element[] {
        var that: any = this;
        throw `renderEdit is not implemented in ${that.constructor.name}`;
    }

    renderDisplay(): JSX.Element | JSX.Element[] {
        var that: any = this;
        throw `renderDisplay is not implemented in ${that.constructor.name}`;
    }

    componentWillUpdate(nextProps: TProps, nextState: any, nextContext: any): void {

        if (this.evaluateModelReferense ||
            nextProps && nextProps.modelReference && this.props.modelReference != nextProps.modelReference) {

            if (nextProps && nextProps.modelReference) {

                runInAction("BaseComponent:EnsureModelAccess - cwu", () => {
                    nextProps.modelReference.ensureModelAccess();
                });

                this.model = nextProps.modelReference.getValue();

                this.attachToModelReference(nextProps.modelReference);
            }

            this.evaluateModelReferense = false;
        }
    }

    componentWillUnmount(): void {
        if (this.currentDisposer)
            this.currentDisposer();
    }

    private attachToModelReference(modelReference: BindableReference): void {

        if (this.currentDisposer) {
            this.currentDisposer();
            this.currentDisposer = null;
        }

        var that = this;
        this.currentDisposer = reaction(reaction => {
            return modelReference.getValue();
        }, (model, reaction) => {
            /*викаме, за да се преизчертае компонента*/
            that.setState({});

            /*Инвалидираме данните в modelReference за достъп до модела.*/
            modelReference.invalidateModelAccess();

            that.evaluateModelReferense = true;

            that.currentDisposer();
            that.currentDisposer = null;

        }, { name: "BaseComponent: BindableReferenceReaction" });
    }

    //#region Common UI Components Helpers

    protected textBoxFor(selector: (model: TModel) => any, attributes?: any, onChange?: (event: any, value: any, modelReference: BindableReference) => void): any {
        return <TextBox {...this.bind(selector)} attributes={attributes} onChange={onChange} />
    }

    protected textAreaFor(selector: (model: TModel) => string, cols?: number, rows?: number, attributes?: any, onChange?: (event: any, value: any, modelReference: BindableReference) => void): any {
        return <TextArea cols={cols} rows={rows} {...this.bind(selector)} attributes={attributes} onChange={onChange} />
    }

    protected checkBoxFor(selector: (model: TModel) => boolean, attributes?: any, onChange?: (event: any, value: any, modelReference: BindableReference) => void): any {
        return <CheckBox {...this.bind(selector)} attributes={attributes} onChange={onChange} />
    }

    protected checkBoxListFor(selector: (model: TModel) => any, listItems: SelectListItem[], itemsStore: any[], onChangeCallback?: (value: any) => void, attributes?: any): any {
        return <CheckBoxList {...this.bind(selector)} listItems={listItems} itemsStore={itemsStore} onChangeCallback={onChangeCallback} attributes={attributes} />
    }

    protected dropDownListFor(selector: (model: TModel) => any, items: SelectListItem[], attributes?: any, onChange?: (event: any, value: any, modelReference: BindableReference) => void, hasEmptyElement?: boolean, emptyElementValue?: string): any {
        return <DropDownList items={items} {...this.bind(selector)} attributes={attributes} onChange={onChange} hasEmptyElement={hasEmptyElement} emptyElementValue={emptyElementValue} />
    }

    protected dropDownListForEnum(selector: (model: TModel) => any, object: any, attributes?: any, onChange?: any, hasEmptyElement?: boolean, emptyElementValue?: string): any {
        return this.dropDownListFor(selector, BaseComponent.listForEnum(object), attributes, onChange, hasEmptyElement, emptyElementValue);
    }

    protected labelFor(selector: (model: TModel) => any, labelText: string, attributes?: any): any {
        return <Label {...this.bind(selector)} labelText={labelText} attributes={attributes} />
    }

    protected passwordStrengthMeterFor(password: string, helpText?: string, attributes?: any): any {
        return <PasswordStrengthMeter password={password} helpText={helpText} attributes={attributes} />
    }

    protected labelForEnum(enumValue: number, enumObject: any): string {
        return ResourceHelpers.getResourceByProperty(enumObject[!enumValue ? 0 : enumValue], enumObject);
    }

    protected radioButtonListFor(selector: (model: TModel) => any, items: SelectListItem[], attributes?: any, onChange?: (event: any, value: any, modelReference: BindableReference) => void): any {
        return <RadioButtonList {...this.bind(selector)} items={items} attributes={attributes} onChange={onChange} />
    }

    protected radioButtonListForEnum(selector: (model: TModel) => any, object: any, attributes?: any, onChange?: any): any {
        return this.radioButtonListFor(selector, BaseComponent.listForEnum(object), attributes, onChange);
    }

    protected dateFor(selector: (model: TModel) => any, dateFormat?: string, isValidDate?: (currentDate: any, selectedDate: any) => boolean, attributes?: any, onChange?: (event: any, value: any, modelReference: BindableReference) => void): any {
        return <Date {...this.bind(selector)} attributes={attributes} onChange={onChange} dateFormat={dateFormat ? dateFormat : null} isValidDate={isValidDate ? isValidDate : null} />;
    }

    protected dateTimeFor(selector: (model: TModel) => any, attributes?: any, dateFormat?: string | boolean, showSeparateDateAndTime?: boolean): any {
        return <DateTime {...this.bind(selector)} attributes={attributes} dateFormat={dateFormat} showSeparateDateAndTime={showSeparateDateAndTime} />
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

    protected hintFor(selector: (model: TModel) => any, contentFunc: (propName: string) => Promise<string>, attributes?: any): any {
        return <Hint {...this.bind(selector)} contentFunc={contentFunc} attributes={attributes} />
    }

    protected rawHtml(text?: string, attributes?: any): any {
        return <RawHTML rawHtmlText={text} attributes={attributes} />
    }

    protected passwordFor(selector: (model: TModel) => any, attributes?: any, onChange?: (event: any, value: any, modelReference: BindableReference) => void): any {
        return <Password {...this.bind(selector)} attributes={attributes} onChange={onChange} />
    }

    protected static listForEnum(object: any): any {
        var enumValues = TypeSystem.getEnumValues(object);
        let selectListItems = new Array<SelectListItem>();
        for (let enumValue of enumValues) {
            selectListItems.push({ text: ResourceHelpers.getResourceByProperty(object[enumValue], object), selected: false, value: enumValue })
        }
        return selectListItems
    }

    //#endregionz

    //#region Bind

    bind(selector: (model: TModel) => any): BaseProps;
    bind(selector: (model: TModel) => any, htlmName: string, modelValidators?: IBaseValidator<IModelErrors, any>[]): BaseProps;
    bind(selector: (model: TModel) => any, viewMode: ViewMode, htlmName?: string, modelValidators?: IBaseValidator<IModelErrors, any>[]): BaseProps;

    bind(propertyName: string): BaseProps;
    bind(propertyName: string, htlmName: string, modelValidators?: IBaseValidator<IModelErrors, any>[]): BaseProps;
    bind(propertyName: string, viewMode: ViewMode, htlmName?: string, modelValidators?: IBaseValidator<IModelErrors, any>[]): BaseProps;

    bind<TBind extends IModelErrors>(model: TBind, htlmName: string, modelValidators?: IBaseValidator<IModelErrors, any>[]): BaseProps;
    bind<TBind extends IModelErrors>(model: TBind, viewMode: ViewMode, htlmName?: string, modelValidators?: IBaseValidator<IModelErrors, any>[]): BaseProps;

    bind(prop1: any, prop2?: any, prop3?: any, prop4?: any): BaseProps {
        var propertyName = this.getPropertyName(prop1);
        var model = this.getModel(prop1);
        var htmlName = this.getHtmlName(propertyName, prop2, prop3);
        var viewMode = this.getViewMode(prop2);
        var fullHtmlName = this.getFullHtmlName(htmlName);
        var isModelNew = model != this.model;
        var modelValidators = this.getModelValidators(propertyName, isModelNew, prop3, prop4);

        var ret: any = null;
        var that = this;

        var brCacheItem = that.bindableReferencesCache[propertyName];

        if (brCacheItem &&
            brCacheItem.model === model &&
            ArrayHelper.arraysEqual(brCacheItem.modelValidators, modelValidators)
        )
            ret = {
                fullHtmlName: fullHtmlName,
                modelReference: brCacheItem.modelReference,
                viewMode: viewMode
            };
        else {
            var mr: any = null;

            /*
             Извикваме създаването на BindableReference в runInAction, за да може да се инициализира модела в рамките на Bind,
            който се извиква в рамките на Render - a.
             */
            runInAction("Bind-cr", () => {
                mr = new BindableReference(model, propertyName, modelValidators, false);
            });

            ret = {
                fullHtmlName: fullHtmlName,
                modelReference: mr,
                viewMode: viewMode
            };

            brCacheItem = that.bindableReferencesCache[propertyName] = {
                model: model,
                modelValidators: modelValidators,
                modelReference: mr
            };
        }

        return ret;
    }

    bindArrayElement(selector: (model: TModel) => any, indexes: number[]): BaseProps;
    bindArrayElement(selector: (model: TModel) => any, indexes: number[], htlmName: string, modelValidators?: IBaseValidator<IModelErrors, any>[]): BaseProps;
    bindArrayElement(selector: (model: TModel) => any, indexes: number[], viewMode: ViewMode, htlmName?: string, modelValidators?: IBaseValidator<IModelErrors, any>[]): BaseProps;

    @action bindArrayElement(selector: (model: TModel) => any, indexes: number[], prop1?: any, prop2?: any, prop3?: any): BaseProps {
        var propertyName = this.getPropertyName(selector);

        var indexersKeys = this.getIndexerNamesPropertyName(propertyName);
        for (var i = 0; i < indexersKeys.length; i++) {
            propertyName = propertyName.replace(`[${indexersKeys[i]}]`, `[${indexes[i]}]`);
        }

        return this.bind(propertyName, prop1, prop2, prop3);
    }

    private getPropertyName(prop: any): string {
        if (ObjectHelper.isFunction(prop)) {
            return Helper.getPropertyNameBySelector(prop);
        }

        if (typeof (prop) == "string") {
            return prop;
        }

        return "";
    }

    private getModel(prop1: any): IModelErrors {

        if (prop1 &&
            !ObjectHelper.isFunction(prop1) &&
            typeof (prop1) != "string") {
            return prop1;
        }
        else {
            return this.model;
        }
    }

    private getHtmlName(propertyName: string, prop2: any, prop3: any): string {
        if (prop2 && typeof (prop2) == "string") {
            return prop2;
        }

        if (prop3 && typeof (prop3) == "string") {
            return prop3;
        }

        return propertyName;
    }

    protected getViewMode(prop2: any): ViewMode {
        if (prop2 && typeof (prop2) == "number") {
            return prop2;
        }

        return this.props.viewMode;
    }

    protected getFullHtmlName(htmlName: string): string {
        if (this.props.fullHtmlName && htmlName) {
            return this.props.fullHtmlName + '.' + htmlName;
        }

        if (this.props.fullHtmlName && !htmlName) {
            return this.props.fullHtmlName;
        }

        if (!this.props.fullHtmlName && htmlName) {
            return htmlName;
        }

        if (!this.props.fullHtmlName && !htmlName) {
            return "";
        }
    }

    private getModelValidators(propertyName: string, isModelNew: boolean, prop3: any, prop4: any): IBaseValidator<IModelErrors, any>[] {
        if (prop3 && ObjectHelper.isArray(prop3)) {
            return prop3;
        }

        if (prop4 && ObjectHelper.isArray(prop4)) {
            return prop4;
        }

        if (this.validators && !isModelNew) {
            return this.validators;
        }

        if (this.props.modelReference && !isModelNew) {
            return this.props.modelReference.getValidators();
        }

        return undefined;
    }

    private getIndexerNamesPropertyName(propertyName: string): string[] {
        var indexerNames: string[] = [];

        while (propertyName.indexOf("[") >= 0) {
            indexerNames.push(propertyName.substring(propertyName.indexOf("[") + 1, propertyName.indexOf("]")));
            propertyName = propertyName.substring(0, propertyName.indexOf("[")) + propertyName.substring(propertyName.indexOf("]") + 1);
        }

        return indexerNames;
    }

    //#endregion
}