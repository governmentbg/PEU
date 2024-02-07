import { action, extendObservable, isObservableProp, observable } from 'mobx';
import moment from 'moment';
import { ArrayHelper, ObjectHelper } from '../';
import { TypeSystem } from '../common/TypeSystem';
import { moduleContext } from '../ModuleContext';

const modelErrorPropName = "a4166ca9-f7df-4c10-ba33-9d32661602c4";

export enum ErrorLevels {
    Error = 1,
    Information = 2,
}

export interface IModelErrors {
    /**Добавя грешка към модела*/
    addError(error: string, errorLevel?: ErrorLevels): void;

    /**Добавя грешка към property от модела*/
    addError(propertyName: string, error: string, errorLevel?: ErrorLevels): void;

    /**Премахва грешка към property на модела ако не е подадена конкретна  грешка маха всичките грешки*/
    removeError(propertyName: string, error?: string): void;

    /**Изчиства всички грешки на модела*/
    clearErrors(recursive?: boolean): void;

    /**Връща всички грешки за дадено проперти*/
    getPropertyErrors(propertyName: string): { message: string, level: ErrorLevels }[];

    /**Връща всички грешки на ниво модел*/
    getModelErrors(): { message: string, level: ErrorLevels }[];

    /**Връща всички грешки на модела*/
    getErrors(): PropertyErrors[];

    /**Връща дали в модела има грешки*/
    hasErrors(): boolean;

    /**На всички грешки слага подадения level*/
    setAllErrorsLevel(level: ErrorLevels): void;
}

export function isIModelErrors(obj: IModelErrors | any): obj is IModelErrors {
    return obj &&
        (<IModelErrors>obj).addError !== undefined &&
        (<IModelErrors>obj).clearErrors !== undefined &&
        (<IModelErrors>obj).getErrors !== undefined &&
        (<IModelErrors>obj).getPropertyErrors !== undefined &&
        (<IModelErrors>obj).hasErrors !== undefined &&
        (<IModelErrors>obj).getModelErrors !== undefined &&
        (<IModelErrors>obj).removeError !== undefined;
}

export interface PropertyErrors {
    propertyName: string;
    errors: { message: string, level: ErrorLevels }[]
}

@TypeSystem.typeDecorator("BaseDataModel", moduleContext.moduleName)
export abstract class BaseDataModel implements IModelErrors {
    @observable private errors: any = {};

    constructor(obj?: any) {
        this.ensureErrors();
    }

    //#region IModelErrors

    addError(error: string, errorLevel?: ErrorLevels): void;

    addError(propertyName: string, error: string, errorLevel?: ErrorLevels): void;

    @action public addError(prop1: string, prop2?: any, prop3?: ErrorLevels): void {
        this.ensureErrors();
        let propertyName = modelErrorPropName;
        let error = prop1;
        let errorLevel = defaultErrorLevel;

        if (!ObjectHelper.isStringNullOrEmpty(prop2) && typeof (prop2) == "string") {
            propertyName = prop1
            error = prop2;
            errorLevel = prop3 ? prop3 : defaultErrorLevel;
        }
        else {
            errorLevel = prop2 ? prop2 : defaultErrorLevel;
        }

        if (!this.errors[propertyName]) {
            this.extendErrorsWithProperty(propertyName);
        }

        this.fireErrorsChange();

        this.errors[propertyName].push({ message: error, level: errorLevel });
    }

    @action public removeError(propertyName: string, errorMessage?: string) {
        this.ensureErrors();

        var propErrors: { message: string, level: ErrorLevels }[] = this.errors[propertyName];

        if (errorMessage) {
            if (propErrors && propErrors.length > 0) {
                var errors = propErrors.filter(err => err.message = errorMessage);

                /*само в случай на изтриванена греша, се вдига събититето за промяна по грешки*/
                if (errors.length > 0) {
                    for (var error of errors) {
                        ArrayHelper.removeAllElements(propErrors, errorMessage)
                    }

                    this.fireErrorsChange();
                }
            }
        }
        else {
            /*само в случай, че има данни в масива, той се зачиства и се вдига събитие за промяна по грешките на обекта.*/
            if (propErrors && propErrors.length > 0) {
                this.fireErrorsChange();

                this.errors[propertyName] = [];
            }
        }
    }

    @action public clearErrors(recursive?: boolean) {
        if (this.hasErrorsInternal()) {
            this.errors = undefined;
            this.ensureErrors();
        }

        if (recursive) {
            var typeInfo = TypeSystem.getTypeInfo(this);

            for (var propInfo of typeInfo.properties) {
                var propValue = (<any>this)[propInfo.name];

                if (isIModelErrors(propValue)) {
                    propValue.clearErrors(recursive);
                } else if (ObjectHelper.isArray(propValue)) {
                    for (var element of propValue) {
                        if (isIModelErrors(element)) {
                            element.clearErrors(recursive);
                        }
                    }
                }
            }
        }
    }

    public getPropertyErrors(propertyName: string): { message: string, level: ErrorLevels }[] {
        this.ensureErrors();
        /** Закачаме се за промяна по пропъртито. Това се прави, за може mobx - да се закачи за промяна по него и съответно да извести компонентите, които викат функцията.
        */
        var change = this.errors.onChange;

        var ret = this.errors[propertyName] as { message: string, level: ErrorLevels }[];

        if (ret == undefined) {
            this.extendErrorsWithProperty(propertyName);
            ret = this.errors[propertyName] as { message: string, level: ErrorLevels }[];
        }

        return ret;
    }

    public getModelErrors(): { message: string, level: ErrorLevels }[] {
        return this.getPropertyErrors(modelErrorPropName);
    }

    public getErrors(): PropertyErrors[] {
        this.ensureErrors();

        var result: PropertyErrors[] = [];

        for (var propName in this.errors) {

            /*изключва се служебното property за промяната*/
            if (propName == "onChange")
                continue;

            var errors = this.errors[propName] as string[];

            if (errors && errors.length > 0) {
                result.push({ propertyName: propName == modelErrorPropName ? "" : propName, errors: this.errors[propName] });
            }
        }
        /** Закачаме се за промяна по пропъртито. Това се прави, за може mobx - да се закачи за промяна по него и съответно да извести компонентите, които викат функцията.
         */
        var change = this.errors.onChange;

        return result;
    }

    public hasErrors(): boolean {
        this.ensureErrors();

        var hasErrors = this.hasErrorsInternal();

        /** Закачаме се за промяна по пропъртито. Това се прави, за може mobx - да се закачи за промяна по него и съответно да извести компонентите, които викат функцията.
         */
        var change = this.errors.onChange;

        return hasErrors;
    }

    @action public setAllErrorsLevel(level: ErrorLevels) {
        this.ensureErrors();

        var propErrors = this.getErrors();

        for (var propError of propErrors) {
            for (var error of propError.errors) {
                error.level = level;
            }
        }

        var model: any = this;

        for (var propName in model) {
            if (propName.indexOf("_") != 0) {
                if (isIModelErrors(model[propName])) {
                    model[propName].setAllErrorsLevel(level);
                }
            }
            else if (ObjectHelper.isArray(model[propName])) {
                for (var i = 0; i < model[propName].length; i++) {
                    if (isIModelErrors(model[propName][i])) {
                        model[propName][i].setAllErrorsLevel(level);
                    }
                }
            }
        }

        this.fireErrorsChange();
    }

    //#endregion

    private hasErrorsInternal(): boolean {
        this.ensureErrors();

        var hasErrors = false;

        if (this.errors) {
            for (var propName in this.errors) {

                /*изключва се служебното property за промяната*/
                if (propName == "onChange")
                    continue;

                var errors = this.errors[propName] as string[];

                if (errors && errors.length > 0) {
                    hasErrors = true;
                    break;
                }
            }
        }

        return hasErrors;
    }

    private fireErrorsChange() {
        this.errors.onChange = ObjectHelper.newGuid();
    }

    private ensureErrors() {
        if (this.errors == undefined) {
            this.errors = observable({ onChange: "" });
        }
        else if (!isObservableProp(this.errors, "onChange")) {
            this.extendErrors({ onChange: "" });
        }
    }

    private extendErrors(obj: any) {
        extendObservable(this.errors, obj);
    }

    private extendErrorsWithProperty(propName: string) {
        var extendObj: any = {};

        extendObj[propName] = [];

        this.extendErrors(extendObj);
    }

    public static isExtendedBy(obj: any): boolean {

        if (obj) {
            var type = TypeSystem.getTypeInfo(obj);

            if (type && type.isSubClassOf(BaseDataModel))
                return true;
            else
                return false;
        }
        else {
            return false;
        }
    }

    public toJSON(): any {
        /**
         * за повече информация: https://developer.mozilla.org/en/docs/Web/JavaScript/Reference/Global_Objects/JSON/stringify
         */
        let source: any = this;
        let ret: any = {};
        let typeInfo = TypeSystem.getTypeInfo(this);

        if (typeInfo && source) {
            for (var propInfo of typeInfo.properties) {
                /*не сериализираме обекта с грешките !*/
                if (propInfo.name != "errors") {
                    if (propInfo.type.name == 'moment' && moment.isMoment(source[propInfo.name])) {
                        ret[propInfo.name] = source[propInfo.name].format('YYYY-MM-DDTHH:mm:ss');
                    }
                    else {
                        ret[propInfo.name] = source[propInfo.name];
                    }
                }
            }
        }
        return ret;
    }

    public copyFrom(source: any) {
        if (source) {
            ObjectHelper.copyFrom(source, this);
        }
    }
}

export function setDefaultErrorLevel(errorLevel: ErrorLevels) {
    defaultErrorLevel = errorLevel;
}

export var defaultErrorLevel: ErrorLevels = ErrorLevels.Error;