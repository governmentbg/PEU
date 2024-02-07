import { TypeSystem } from "./TypeSystem";
import { isObservableArray } from "mobx";
import moment from "moment";

export namespace ObjectHelper {
    ///**The Object.assign() method is used to copy the values of all enumerable own properties from one or more source objects to a target object. It will return the target object.*/
    //export function assign(target: any, source1: any): any;

    ///**The Object.assign() method is used to copy the values of all enumerable own properties from one or more source objects to a target object. It will return the target object.*/
    //export function assign(target: any, source1: any, source2: any): any;

    ///**The Object.assign() method is used to copy the values of all enumerable own properties from one or more source objects to a target object. It will return the target object.*/
    //export function assign(target: any, source1: any, source2: any, source3: any): any;

    /**The Object.assign() method is used to copy the values of all enumerable own properties from one or more source objects to a target object. It will return the target object.*/
    export function assign(target: any, ...sources: any[]): any {
        return (<any>Object).assign.apply(Object, arguments);
    }

    export function newGuid(): string {
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        });
    }

    export function isFunction(func: any): boolean {
        return typeof func == 'function' || false;
    }

    export function isArrayNullOrEmpty(arr: any): boolean {
        return arr == undefined || arr == null || arr.length == 0;
    };

    export function isStringNullOrEmpty(str: any): boolean {

        return str == undefined
            || str == null
            || str.toString() == ""
            || str.toString().trim() == '';
    };

    export function isArray(obj: any): obj is any[] {
        //за обектите от тип Mobx observable list Array.isArray не работи коректно
        if (obj && (Array.isArray(obj) || isObservableArray(obj))) {
            return true;
        }
        else {
            return false;
        }
    }

    export function isNumber(obj: any): obj is number {
        if ((obj === '') || (obj === null))
            return false;
        //Ако не успее да парсне връща NaN, ако е различно от NaN => obj = number
        return !isNaN(Number(obj));
    }

    export function hashCode(obj: string): number {
        var hash: number = 0,
            i: number,
            chr: number,
            len: number;

        if (obj.length === 0) return hash;
        for (i = 0, len = obj.length; i < len; i++) {
            chr = obj.charCodeAt(i);
            hash = ((hash << 5) - hash) + chr;
            hash |= 0; // Convert to 32bit integer
        }
        return hash;
    };

    export function toEnum(enumType: any, stringValue: string): number {
        return enumType[stringValue];
    }

    export function getEnumValues(enumType: any): number[] {
        const keys = Object.keys(enumType).filter(k => typeof enumType[k as any] === "number"); // ["A", "B"]
        const values = keys.map(k => enumType[k as any]); // [0, 1]

        return values
    }

    export function isSubClassOf(target: any, classToCheckAgainst: any): boolean {

        if (target &&
            classToCheckAgainst) {
            var currentTarget = target;

            while (currentTarget != undefined) {

                if (currentTarget === classToCheckAgainst.prototype)
                    return true;

                currentTarget = currentTarget.__proto__;
            }
        }

        return false;
    }

    /**Инициализира всяко нов обект на всяко пропърти, като взима информацията от системата с типове TypeSystem. */
    export function inittializeObject(object: any, initRecursive: boolean = false) {

        if (object) {
            var typeInfo = TypeSystem.getTypeInfo(object);

            if (typeInfo) {
                for (var prop of typeInfo.properties) {
                    if (object[prop.name] == undefined &&
                        !prop.type.isPrimitive && !prop.type.isEnum) {

                        object[prop.name] = new prop.type.ctor;

                        if (initRecursive)
                            inittializeObject(object[prop.name], initRecursive);
                    }
                }
            }
        }
    }

    export function compareObjects(o: any, p: any) {
        var i: any,
            keysO = Object.keys(o).sort(),
            keysP = Object.keys(p).sort();
        if (keysO.length !== keysP.length)
            return false;
        if (keysO.join('') !== keysP.join(''))
            return false;
        for (i = 0; i < keysO.length; ++i) {
            if (o[keysO[i]] instanceof Array) {
                if (!(p[keysO[i]] instanceof Array))
                    return false;
                if (p[keysO[i]].sort().join('') !== o[keysO[i]].sort().join(''))
                    return false;
                else {
                    for (var j = 0; j < p[keysO[i]].length; j++) {
                        if (compareObjects(o[keysO[i]][j], p[keysO[i]][j]) === false)
                            return false;
                    }
                }
            }
            else if (o[keysO[i]] instanceof Date) {
                if (!(p[keysO[i]] instanceof Date))
                    return false;
                if (('' + o[keysO[i]]) !== ('' + p[keysO[i]]))
                    return false;
            }
            else if (o[keysO[i]] instanceof Function) {
                if (!(p[keysO[i]] instanceof Function))
                    return false;
            }
            else if (o[keysO[i]] instanceof String) {
                if (!(p[keysO[i]] instanceof String))
                    return false;
                if (o[keysO[i]] !== p[keysO[i]])
                    return false;
                if (o[keysO[i]].length != p[keysO[i]].length)
                    return false;
            }
            else if (typeof o[keysO[i]] === "string") {
                if (!(typeof o[keysO[i]] === "string"))
                    return false;
                if (o[keysO[i]] !== p[keysO[i]])
                    return false;
            }
            else if (o[keysO[i]] instanceof Object) {
                if (!(p[keysO[i]] instanceof Object))
                    return false;
                if (o[keysO[i]] === o) {
                    if (p[keysO[i]] !== p)
                        return false;
                }
                else if (compareObjects(o[keysO[i]], p[keysO[i]]) === false)
                    return false;
            }
            //if (o[keysO[i]] !== p[keysO[i]])
            //    return false;
        }
        return true;
    }

    export function copyFrom(from: any, to: any) {

        if (!(from && to)) {
            throw new Error("arguments from and to must not be null!");
        }

        let typeInfo = TypeSystem.getTypeInfo(to);

        if (!typeInfo) {
            throw new Error("There is no type registered for 'to'!");
        }

        for (var propInfo of typeInfo.properties) {
            if (from[propInfo.name] != undefined) {
                var constructor = propInfo.type ? propInfo.type.ctor : undefined;

                if (propInfo.type.isPrimitive ||
                    propInfo.type.isEnum ||
                    propInfo.type.name == "any") {
                    if (canEditProperty(from, propInfo.name)) {
                        if (propInfo.type.name == "number") {
                            to[propInfo.name] = from[propInfo.name] === "" ? null : Number(from[propInfo.name]);

                        } else if (propInfo.type.name == "boolean") {
                            if (from[propInfo.name] == "true")
                                to[propInfo.name] = true;
                            else if (from[propInfo.name] == "false")
                                to[propInfo.name] = false;
                            else
                                to[propInfo.name] = from[propInfo.name];
                        }
                        else {
                            to[propInfo.name] = from[propInfo.name];
                        }
                    }
                }
                else if (propInfo.type.name == "moment" || propInfo.type.name == "duration") {
                    if (canEditProperty(from, propInfo.name)) {

                        if (propInfo.type.name == "moment") {
                            //С parseZone и local(true) запазваме часа и датата, като променяме зоната към локалната.
                            //На сървъра пазим датата със часовата зона, където се намира сървъра.
                            //На клиента изпращаме датата като променяме само зоната към локалната.
                            //Има връзка с toJSON в BaseDataModel
                            to[propInfo.name] = new constructor(moment.parseZone(from[propInfo.name]).local(true));
                        } else {
                            to[propInfo.name] = new constructor(from[propInfo.name]);
                        }
                    }
                }
                else if (to[propInfo.name] &&
                    !propInfo.type.isArray) {
                    /*If the type is array we create new array every time. */

                    /*copy data from the object*/
                    copyFrom(from[propInfo.name], to[propInfo.name]);
                }
                else if (constructor) {

                    var newObj = null;

                    if (propInfo.type.hasCopyConstructor)
                        newObj = new constructor(from[propInfo.name]);
                    else {

                        newObj = new constructor();

                        if (propInfo.type.isArray) {

                            let elemType = propInfo.type.getElementType();

                            if (elemType) {
                                for (var elem of from[propInfo.name]) {

                                    if (elemType.isEnum ||
                                        elemType.isPrimitive ||
                                        elemType.name == "any") {
                                        newObj.push(elem);
                                    }
                                    else if (elemType.name == "moment") {
                                        newObj.push(new elemType.ctor(elem));
                                    }
                                    else {
                                        var newElem = new elemType.ctor();

                                        copyFrom(elem, newElem);

                                        newObj.push(newElem);
                                    }
                                }
                            }
                        }
                        else {
                            copyFrom(from[propInfo.name], newObj);
                        }
                    }
                    if (canEditProperty(from, propInfo.name)) {
                        to[propInfo.name] = newObj;
                    }
                }
                else
                    throw new Error("Not Implemented copy from");
            }
            else {
                if (canEditProperty(from, propInfo.name)) {
                    to[propInfo.name] = undefined;
                }
            }
        }
    }

    export function padLeft(nr: string, n: number, str: string): string {
        return Array(n - String(nr).length + 1).join(str || '0') + nr;
    }

    export function canEditProperty(object: any, propertyName: any): boolean {

        var propDescriptor = Object.getOwnPropertyDescriptor(object, propertyName);

        if (propDescriptor) {
            if (propDescriptor.writable || propDescriptor.set) {
                return true;
            }
            else {
                return false;
            }
        }
        else {
            return false;
        }
    }

    export function isNullOrUndefined(object: any): boolean {
        return object === null || object === undefined;
    }

    /**Клонира подадения обект. */
    export function clone(src: any, dst: any): void {
        let srcClean = JSON.parse(JSON.stringify(src));

        copyFrom(srcClean, dst);
    }
}