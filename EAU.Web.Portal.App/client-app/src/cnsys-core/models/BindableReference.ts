import { Helper, ObjectHelper, PropertyInfo, PropertyPathItem, TypeInfo, TypeSystem } from '../common';
import { ErrorLevels, IBaseValidator, IModelErrors, isIModelErrors } from '../models';

export interface ErrorInfo {
    error: string;
    level: ErrorLevels,
    errorContainer: any;
    propertyName: string;
}

export class BindableReference {
    private _model: IModelErrors;
    private _propertyName: string;
    private _modelValidators: IBaseValidator<IModelErrors, any>[];
    private _modeAccessInitiated: boolean;

    private _modelToUpdate: IModelErrors;
    private _modelToUpdateValidators?: IBaseValidator<IModelErrors, any>[];
    private _dataAccessor?: PropertyPathItem[];

    constructor(modelReference: BindableReference, selector?: (model: any) => any, modelValidators?: IBaseValidator<IModelErrors, any>[], initModelAccess?: boolean);
    constructor(modelReference: BindableReference, propertyName?: string, modelValidators?: IBaseValidator<IModelErrors, any>[], initModelAccess?: boolean);
    constructor(model: any, selector?: (model: any) => any, modelValidators?: IBaseValidator<IModelErrors, any>[], initModelAccess?: boolean);
    constructor(model: any, propertyName?: string, modelValidators?: IBaseValidator<IModelErrors, any>[], initModelAccess?: boolean );

    constructor(model: any, property: any, modelValidators?: IBaseValidator<IModelErrors, any>[], initModelAccess: boolean = true) {

        var propertyName = this.getPropertyName(property);

        this._model = model;
        this._propertyName = propertyName;
        this._modelValidators = modelValidators;
        this._modeAccessInitiated = false;
        if (initModelAccess)
            this.ensureModelAccess();
    }

    public getValue(): any {

        if (!this._dataAccessor) {
            return this._modelToUpdate;
        }

        var ret: any = this._modelToUpdate;

        for (var prop of this._dataAccessor) {

            if (ret == null ||
                ret == undefined)
                break;

            if (prop.isIndexer && ret.length <= prop.name) {
                ret = undefined;
                break;
            }
            ret = ret[prop.name];
        }

        return ret;
    }

    public setValue(value: any) {
        if (!this._dataAccessor) {
            throw "BindableReference gives direct access to object you can't edit it"
        }

        if (value) {
            var propInfo = this.getPropertyInfo();

            if (propInfo && propInfo.type.name == "number") {
                if (typeof (value) == "string" && !(value as any).endsWith(".") && !(value as any).endsWith(".0") && !(value as any).endsWith(",") && !(value as any).endsWith(",0")) {

                    value = !isNaN(value as any) ? Number(value) : null;
                }
            }
        }

        var modelToSet: any = this._modelToUpdate;

        for (var i = 0; i < this._dataAccessor.length - 1; i++) {
            modelToSet = modelToSet[this._dataAccessor[i].name];
        }

        modelToSet[this._dataAccessor[this._dataAccessor.length - 1].name] = value;

        if (this._modelToUpdateValidators) {
            for (var validator of this._modelToUpdateValidators) {
                validator.validateProperty(this._dataAccessor[0].name, this._modelToUpdate);
            }
        }
    }

    public getErrors(): string[] {
        return this.getErrorsInfo().map((value) => { return value.error; });
    }

    public getErrorsInfo(): ErrorInfo[] {

        var ret: ErrorInfo[] = [];

        var value = this.getValue();

        //Ако референцията сочи обект от който е IModelError връщаме данните за валидацията от конкретния обект
        if (isIModelErrors(value)) {

            for (var pErrors of value.getErrors()) {
                for (var error of pErrors.errors) {
                    ret.push({ error: error.message, level: error.level, errorContainer: value, propertyName: pErrors.propertyName });
                }
            }
        }
        else if (this._dataAccessor && this._dataAccessor.length > 0 && this._modelToUpdate.getPropertyErrors && ObjectHelper.isFunction(this._modelToUpdate.getPropertyErrors)) {

            var propErrors = this._modelToUpdate.getPropertyErrors(this._dataAccessor[0].name);

            if (propErrors && propErrors.length > 0)
                ret = propErrors.map((value) => {
                    if (this._dataAccessor) {
                        return { error: value.message, level: value.level, errorContainer: this._modelToUpdate, propertyName: this._dataAccessor[0].name };
                    }
                    else {
                        throw new Error("Not suported");
                    }
                });
        }

        return ret;
    }

    public hasErrors(): boolean {
        var errors = this.getErrorsInfo();

        return errors && errors.length > 0;
    }

    public getValidators(): IBaseValidator<IModelErrors, any>[] | undefined {
        if (!this._dataAccessor) {
            return this._modelToUpdateValidators;
        }

        //Ако селецтора е за елемент от списък тогава връщма валидатора на обекта
        if (ObjectHelper.isNumber(this._dataAccessor[0].name)) {
            return this._modelToUpdateValidators;
        }

        var validators: IBaseValidator<IModelErrors, any>[] = [];

        if (this._modelToUpdateValidators) {
            for (var modelVal of this._modelToUpdateValidators) {
                var vals = modelVal.getPropertyValidators(this._dataAccessor[0].name);

                if (vals) {
                    validators = validators.concat(vals);
                }
            }
        }

        return validators;
    }

    public ensureModelAccess(): void {
        if (!this._modeAccessInitiated) {

            this.initModelAccess();
            this._modeAccessInitiated = true;
        }
    }

    public initModelAccess(): void {
        var modelToUpdate = this.getModelToUpdate(this._model);
        var propertyName = this._propertyName;
        var modelValidators = this._modelValidators;

        if (modelToUpdate && propertyName) {

            var propertyPath = Helper.getPropertyPath(propertyName);

            if (propertyPath.length <= 0) {
                console.log("Cannot extract propertyPath from = " + propertyName);
            }

            var currentModel = modelToUpdate;
            var currentModelType = TypeSystem.getTypeInfo(currentModel);

            this._modelToUpdate = modelToUpdate;
            this._modelToUpdateValidators = modelValidators;
            this._dataAccessor = propertyPath;

            for (var i = 0; i < propertyPath.length; i++) {
                var prop = propertyPath[i];

                //В новата версия на mobx не се маскира конструктора на "Array"
                if (currentModelType && currentModelType.ctor != Array) {

                    let propType: TypeInfo | undefined;

                    if (currentModelType.isArray) {
                        propType = currentModelType.getElementType();
                    }
                    else {
                        var propInfo = currentModelType.getPropertyByName(prop.name);
                        propType = propInfo ? propInfo.type : undefined;
                    }

                    /*Инициализираме модел да даденито пропърти.*/
                    if (currentModel[prop.name] == undefined
                        && propType
                        && !propType.isPrimitive
                        && !propType.isEnum) {
                        /*За дата от тип момент не инициализираме, тъй като пълни днешна дата*/
                        if ((propType.name) != "moment") {
                            currentModel[prop.name] = new propType.ctor;
                        }

                    }

                    /*намираме последния обект, който е ModelErrors, за да го използваме за достъп и валидация*/
                    if (i < propertyPath.length - 1 &&
                        isIModelErrors(currentModel[prop.name])) {

                        this._modelToUpdate = currentModel[prop.name];
                        this._modelToUpdateValidators = this._modelToUpdateValidators ? this.getValidatorByProperty(this._modelToUpdateValidators, prop.name) : undefined;
                        /*Взимаме пътя до края на селектора. Използва се за достъп до данните от модела*/
                        this._dataAccessor = propertyPath.slice(i + 1);
                    }

                    /*Взимаме типа на текущия модел от типа на пропъртито*/
                    if (propType) {
                        currentModelType = propType;
                    }
                    else {
                        throw new Error("Not suported");
                    }

                    /*Ако текущата стойност на пропартито е от тип наследник на базаовата стоност то типа става като текущия*/
                    if (currentModel[prop.name]) {
                        var currPropType = TypeSystem.getTypeInfo(currentModel[prop.name]);
                        if (currPropType && currPropType.isSubClassOf(currentModelType.ctor)) {
                            currentModelType = currPropType;
                        }
                    }
                }
                else /*ако за текущия модел няма тип, то за следващия се взима от системата за типове, ако има.*/
                    currentModelType = TypeSystem.getTypeInfo(currentModel[prop.name]);

                currentModel = currentModel[prop.name];
            }
        }
        else if (modelToUpdate) {
            this._modelToUpdate = modelToUpdate;
            this._modelToUpdateValidators = modelValidators;
            this._dataAccessor = undefined;
        }
    }

    public invalidateModelAccess(): void {
        this._modeAccessInitiated = false;
    }

    private getModelToUpdate(model: any): any {
        if (model && model.getValue && ObjectHelper.isFunction(model.getValue)) {
            return model.getValue();
        }
        else {
            return model;
        }
    }

    private getPropertyName(property: any): string {
        if (property && ObjectHelper.isFunction(property)) {
            return Helper.getPropertyNameBySelector(property);
        }
        else {
            return property;
        }
    }

    private getValidatorByProperty(validators: IBaseValidator<IModelErrors, any>[], propertyName: string): IBaseValidator<IModelErrors, any>[] | undefined {

        if (validators && validators.length > 0) {
            var ret: IBaseValidator<IModelErrors, any>[] = [];

            for (var validator of validators) {
                ret = ret.concat(validator.getPropertyValidators(propertyName));
            }

            return ret;
        }

        return undefined;
    }

    private getPropertyInfo(): PropertyInfo {
        var model: any = this._modelToUpdate;

        for (var i = 0; i < this._dataAccessor.length - 1; i++) {
            model = model[this._dataAccessor[i].name];
        }

        var modelType = TypeSystem.getTypeInfo(model)

        if (!modelType) {
            return null;
        }

        return modelType.getPropertyByName(this._dataAccessor[this._dataAccessor.length - 1].name);
    }
}