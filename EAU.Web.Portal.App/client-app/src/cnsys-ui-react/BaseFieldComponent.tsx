import { BindableReference, ObjectHelper } from "cnsys-core";
import { action, runInAction } from "mobx";
import React from "react";
import { CoreProps } from "./BaseComponent";

export interface SelectListItem {
    selected: boolean;
    text: string;
    value: string;
}

export interface BaseFieldProps extends CoreProps {
    onChange?: (event: any, value: any, modelReference: BindableReference) => void
    attributes?: any;
}


export abstract class BaseFieldComponent<TProps extends BaseFieldProps> extends React.Component<TProps, any> {
    protected fieldAttributes: any;
    

    constructor(props?: TProps, context?: any) {
        super(props, context);

        //this.componentDidUpdate = this.componentDidUpdate.bind(this);

        //Не е добре да се променят входни параметри на React Component, затова правим клон на входните атрибути
        //и така добавяме и премахваме "input-error" при невалидна стойност в полето в клонинга.
        this.fieldAttributes = this.props.attributes ? ObjectHelper.assign({}, props.attributes) : null;

        this.handleChange = this.handleChange.bind(this);
        if (this.props.modelReference) {

            var that = this;
            runInAction("BaseFieldComponent:EnsureModelAccess - ctor", () => {
                that.props.modelReference.ensureModelAccess();
            });
        }
    }

    componentWillUpdate(nextProps: TProps, nextState: any, nextContext: any): void {

        if (nextProps && nextProps.modelReference && this.props.modelReference != nextProps.modelReference) {

            runInAction("BaseFieldComponent:EnsureModelAccess - cwu", () => {
                nextProps.modelReference.ensureModelAccess();
            });
        }
    }

    //#region Virtual methods

    render() {
        var isValid = this.props.modelReference && !this.props.modelReference.hasErrors();

        this.prepareFieldAttributes(isValid);

        return this.renderInternal();
    }

    //#endregion

    //#region Abstract Methods  

    protected abstract renderInternal(): JSX.Element;

    //#endregion

    //#region Helpers

    protected getId() {
        return this.props.fullHtmlName.replace(".", "_");
    }

    protected getName() {
        return this.props.fullHtmlName;
    }

    protected getHandleChangeValue(event: any): any {
        return event.target.value == "" ? null : event.target.value;
    }

    @action protected handleChange(event: any) {
        var value = this.getHandleChangeValue(event);

        this.props.modelReference.setValue(value);

        if (this.props && this.props.onChange) {
            this.props.onChange(event, value, this.props.modelReference);
        }
    }

    protected getErrors() {
        var errors: string[];
        var errorsLabel: any[] = [];

        if (this.props.modelReference.getErrors()) {
            errors = this.props.modelReference.getErrors();
        }

        if (errors && errors.length > 0) {
            errors.forEach((err, index) => {
                errorsLabel.push(<label key={index}>{err}</label>);
            })
        }

        return errorsLabel;
    }

    protected prepareFieldAttributes(isValid: boolean) {
        if (!this.fieldAttributes) {
            this.fieldAttributes = {};
        }

        this.fieldAttributes['aria-describedby'] = `${this.getId()}_HELP`;

        if (!isValid) {
            if (!this.fieldAttributes.className) {
                this.fieldAttributes['className'] = "input-error";
            } else {
                if (this.fieldAttributes.className.indexOf("input-error") < 0) {
                    this.fieldAttributes.className += " input-error";
                }
            }

            this.fieldAttributes['aria-invalid'] = "true";
            this.fieldAttributes['aria-describedby'] += ` ${this.getId()}_ERRORS`; 
        } else {
            if (this.fieldAttributes.className && this.fieldAttributes.className.indexOf("input-error") >=0) {
                this.fieldAttributes.className = this.fieldAttributes.className.replace("input-error", "").trim();
            }

            this.fieldAttributes['aria-invalid'] = null;
        }
    }

    //#endregion    
}