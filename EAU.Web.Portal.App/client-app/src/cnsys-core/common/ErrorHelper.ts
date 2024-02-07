import { IModelErrors, ErrorInfo, isIModelErrors, ArrayHelper, ObjectHelper } from '../'
import { TypeSystem } from './TypeSystem';

export namespace ErrorHelper {

    export function getErrors(model: IModelErrors): ErrorInfo[] {
        var errors: ErrorInfo[] = [];

        if (!model)
            return errors;

        let mErrors = model.getErrors();

        for (var pErrors of mErrors) {
            for (var error of pErrors.errors) {
                errors.push({ error: error.message, level: error.level, errorContainer: model, propertyName: pErrors.propertyName });
            }
        }

        return errors;
    }

    export function getErrorsRecursive(model: IModelErrors, parentPropName?: string): ErrorInfo[] {
        var errors: ErrorInfo[] = [];

        if (!model)
            return errors;

        errors = getErrors(model);

        if (parentPropName) {
            for (var err of errors) {
                err.propertyName = parentPropName + "." + err.propertyName;
            }
        }

        var obj: any = model;

        for (let prop of TypeSystem.getTypeInfo(obj).properties) {
            if (isIModelErrors(obj[prop.name])) {

                var subModelErrors = this.getErrorsRecursive(obj[prop.name], parentPropName ? `${parentPropName}.${prop.name}` : prop.name);
                ArrayHelper.concat(errors, subModelErrors);
            }
            else if (ObjectHelper.isArray(obj[prop.name])) {
                for (var i = 0; i < obj[prop.name].length; i++) {
                    if (isIModelErrors(obj[prop.name][i])) {
                        var subModelErrors = this.getErrorsRecursive(obj[prop.name][i], parentPropName ? `${parentPropName}.${prop.name}[${i}]` : prop.name);
                        ArrayHelper.concat(errors, subModelErrors);
                    }
                }
            }
        }

        return errors;
    }

    export function excludeErrorsForProps(errors: ErrorInfo[], propNamesToExclude: string[]): ErrorInfo[] {
        if (!errors) {
            return [];
        }

        var newErrors = [];

        for (var err of errors) {
            var propName = removeArrayIndex(err.propertyName)

            if (!propNamesToExclude) {
                newErrors.push(err)
            }
            else {
                var props = propNamesToExclude.filter(pn => propName.indexOf(pn) == 0)

                if (props.length == 0)
                    newErrors.push(err)
            }
        }

        return newErrors;
    }

    export function getErrorsForConcreteProps(errors: ErrorInfo[], propNamesToInclude: string[]): ErrorInfo[] {
        if (!errors) {
            return [];
        }

        var newErrors = [];

        for (var err of errors) {
            var propName = removeArrayIndex(err.propertyName)

            if (propNamesToInclude) {
                var props = propNamesToInclude.filter(pn => propName == pn);

                if (props.length > 0)
                    newErrors.push(err)
            }
        }

        return newErrors;
    }

    function removeArrayIndex(propName: string): string {
        var result = propName;

        while (result.indexOf('[') >= 0) {
            result = result.substring(0, result.indexOf('[')) + result.substring(result.indexOf(']' + 1));
        }

        return result;
    }
}