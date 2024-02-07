import { Dictionary } from 'typescript-collections';
import { TypeSystem } from '../common';
import { BaseDataModel } from './BaseDataModel';
import { IBaseValidator } from './BaseValidator';

export interface IValidatorFactory {
    getValidator<TModel extends BaseDataModel>(model: TModel): IBaseValidator<TModel, any>;

    getValidator(fullModelName: string): IBaseValidator<BaseDataModel, any>;
}

export interface IValidatorRegistry {
    addValidator(fullModelName: string, validator: IBaseValidator<BaseDataModel, any>): void;
    addValidator(model: any, validator: IBaseValidator<BaseDataModel, any>): void;

    getValidatorFactory(): IValidatorFactory;
}

export class ValidatorRegistry implements IValidatorFactory, IValidatorRegistry {
    private _modelValidators: Dictionary<string, IBaseValidator<BaseDataModel, any>>;

    //#region IValidatorFactory

    getValidator<TModel extends BaseDataModel>(model: TModel): IBaseValidator<TModel, any>;

    getValidator(fullModelName: string): IBaseValidator<BaseDataModel, any>;

    getValidator(model: any): IBaseValidator<BaseDataModel, any> | undefined {
        var fullModelName: string;

        if (typeof (model) == "string") {
            fullModelName = model;
        }
        else {
            var typeInfo = TypeSystem.getTypeInfo(model);
            fullModelName = typeInfo.fullName;
        }

        if (this._modelValidators && this._modelValidators.containsKey(fullModelName)) {
            return this._modelValidators.getValue(fullModelName)
        }
        else {
            return undefined;
        }
    }

    //#endregion

    //#region IValidatorRegistry

    public addValidator(model: any, validator: IBaseValidator<BaseDataModel, any>) {
        var fullModelName: string;

        if (typeof (model) == "string") {
            fullModelName = model;
        }
        else {
            var typeInfo = TypeSystem.getTypeInfo(model);
            fullModelName = typeInfo.fullName;
        }

        if (!this._modelValidators) {
            this._modelValidators = new Dictionary<string, IBaseValidator<BaseDataModel, any>>();
        }
        
        if (this._modelValidators.containsKey(fullModelName)) {
            throw `There is already added validator for ${fullModelName}`
        }

        this._modelValidators.setValue(fullModelName, validator);

    }

    public getValidatorFactory(): IValidatorFactory {
        return this;
    }

    //#endregion
}
