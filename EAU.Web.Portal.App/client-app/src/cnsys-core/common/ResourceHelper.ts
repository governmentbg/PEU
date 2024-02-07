import { allModules } from '../contexts';
import { BaseDataModel } from '../models';
import { Helper, TypeSystem } from './';

export namespace ResourceHelpers {

    export function format(error: string, ...params: any[]): string {

        if (error) {

            if (params) {
                for (var i = 0; i < params.length; i++) {
                    error = error.replace(`<Param${i + 1}>`, params[i]);
                }
            }

            return error;
        }
        else
            return "";
    }

    export function formatErrorMessage(error: string, model: BaseDataModel, propertyName: string, ...params: any[]): string {

        if (error) {

            let propertyText = getResourceByProperty(propertyName, model);

            error = error.replace("<Field>", propertyText);

            if (params) {
                for (var i = 0; i < params.length; i++) {
                    error = error.replace(`<Param${i + 1}>`, params[i]);
                }
            }

            return error;
        }
        else
            return "";
    }

    export function getResourceByProperty<T>(property: string | ((model: T) => any), model: T): string {
        var propertyName: string;
        var parentModel: any

        if (property && typeof (property) == "string") {
            propertyName = property;
            parentModel = model;
        }
        else {
            let propertySelector = <((model: T) => any)>property;

            parentModel = Helper.getPropertyParentModel(propertySelector, model);

            propertyName = Helper.getPropertyNameBySelector(propertySelector);

            propertyName = propertyName.substring(propertyName.lastIndexOf('.') + 1);
        }

        var typeInfo = TypeSystem.getTypeInfo(parentModel);
        var propertyKey = typeInfo.name + "_" + propertyName;
             
        var module = allModules.getValue(typeInfo.moduleName);

        return module.moduleContext.resourceManager.getResourceByKey(propertyKey);
    }

    export function getResourceByEmun<T>(value: number, enumObj: T): string {
        var enumValue = (<any>enumObj)[value];

        return ResourceHelpers.getResourceByProperty(enumValue, enumObj);
    }
}