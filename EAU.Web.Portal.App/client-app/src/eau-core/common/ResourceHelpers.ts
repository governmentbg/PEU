import { BaseDataModel, Helper, TypeSystem, SelectListItem, IModelErrors } from "cnsys-core";
import { resourceManager } from "./ResourceManager";

export namespace ResourceHelpers {
    export function formatErrorMessage(errorMsgCode: string, model: any, propertyName: string, ...params: any[]): string {

        if (errorMsgCode) {

            let propertyText = getResourceByProperty(propertyName, model);
            var error = resourceManager.getResourceByKey(errorMsgCode);

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

    export function getErrorMessage(errorMsgCode: string, propertyKey: string): string {
        if(errorMsgCode) {

            let propertyText = resourceManager.getResourceByKey(propertyKey);
            var error = resourceManager.getResourceByKey(errorMsgCode);

            error = error.replace("<Field>", propertyText);
            
            return error;
        }
        else
        return "";
    }

    export function getResourceByModel(model: IModelErrors): string {
        var typeInfo = TypeSystem.getTypeInfo(model);
        var modelName = typeInfo.name;
        var modelNameLastTwoLetters = modelName.substring(modelName.length - 2);
        var prefix = '';

        if (typeInfo.moduleName == "eau-documents") {
            prefix = "DOC_GL_";
        }
        else if (typeInfo.moduleName.indexOf("eau-documents") == 0) {
            prefix = typeInfo.moduleName.replace("eau-documents", "DOC").replace("DOC-", "DOC_").toUpperCase() + "_";
        }
        else {
            prefix = "GL_";
        }

        if (modelNameLastTwoLetters == 'VM') {
            modelName = modelName.substring(0, (modelName.length - 2));
        }

        var propertyKey = `${prefix}${modelName}_L`;

        return resourceManager.getResourceByKey(propertyKey);
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
        var modelName = typeInfo.name;
        var modelNameLastTwoLetters = modelName.substring(modelName.length - 2);
        var prefix = '';

        if (typeInfo.moduleName == "eau-documents") {
            prefix = "DOC_GL_";
        }
        else if (typeInfo.moduleName.indexOf("eau-documents") == 0) {
            prefix = typeInfo.moduleName.replace("eau-documents", "DOC").replace("DOC-", "DOC_").toUpperCase() + "_";           
        }
        else {
            prefix = "GL_";
        }

        var propertyKey = '';

        if (modelNameLastTwoLetters == 'VM') {
            modelName = modelName.substring(0, (modelName.length - 2));
        }

        if (propertyName) {
            propertyKey = `${prefix}${modelName}_${propertyName}_L`;  //propertyKey + "_" + propertyName + '_L';
        }
        else {
            propertyKey = `${prefix}${modelName}_L`; //propertyKey + '_L';
        }

        return resourceManager.getResourceByKey(propertyKey);
    }

    export function getResourceByEmun<T>(value: number, enumObj: T): string {
        if (value == null || value == undefined) {
            return null;
        }

        var enumValue = (<any>enumObj)[value];

        return ResourceHelpers.getResourceByProperty(enumValue, enumObj);
    }

    export function getSelectListItemsForEnum(object: any): SelectListItem[] {
        var enumValues = TypeSystem.getEnumValues(object);

        let selectListItems = new Array<SelectListItem>();

        for (let enumValue of enumValues) {
            let tmpElement = new SelectListItem({ text: ResourceHelpers.getResourceByEmun(Number(enumValue), object), selected: false, value: enumValue });
            selectListItems.push(tmpElement);
        }
        return selectListItems
    }

    export function getPropertyKey<T>(property: string | ((model: T) => any), model: T): string {

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
        var prefix = typeInfo.name;

        if (typeInfo.moduleName == "eau-documents") {
            prefix = "DOC_GL_" + prefix;
        }
        else if (typeInfo.moduleName.indexOf("eau-documents") == 0) {
            prefix = typeInfo.moduleName.replace("eau-documents", "DOC").replace("DOC-", "DOC_").toUpperCase() + "_" + prefix;
        }
        else {
            prefix = "GL_" + prefix;
        }

        var propertyKey = prefix;

        if (propertyName) {
            propertyKey = propertyKey + "_" + propertyName + '_L';
        }
        else {
            propertyKey = propertyKey + '_L';
        }

        return propertyKey;
    }

    export function getPropertyKeyByEnum<T>(value: number, enumObj: T): string {
        if (value == null || value == undefined) {
            return null;
        }

        var enumValue = (<any>enumObj)[value];

        return enumValue;
    }

}