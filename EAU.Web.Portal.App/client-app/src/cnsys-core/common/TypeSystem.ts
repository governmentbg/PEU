import collections from 'typescript-collections';
import moment from 'moment';

let _allTypes: collections.Dictionary<String, TypeInfo> = new collections.Dictionary<String, TypeInfo>();

let _numberType: TypeInfo;
let _stringType: TypeInfo;
let _arrayType: TypeInfo;
let _booleanType: TypeInfo;

export class PropertyInfo {
    private _name: string;
    private _descriptor: PropertyDescriptor;
    private _typeFunc: () => (TypeInfo);

    constructor(name: string, typeFunc: () => TypeInfo, descriptor: PropertyDescriptor) {
        this._name = name;
        this._typeFunc = typeFunc;
        this._descriptor = descriptor;
    }

    get name(): string {
        return this._name;
    }

    get type(): TypeInfo {
        return this._typeFunc();
    }

    get descriptor(): PropertyDescriptor {
        return this._descriptor;
    }
}

interface TypeInfoInitParams {
    constructor?: any;
    name: string;
    moduleName?: string;
    isArray?: boolean;
    elementTypeFunc?: () => TypeInfo | undefined;
    isGeneric?: boolean;
    genericArgumentFuncs?: (() => TypeInfo | undefined)[];
    isEnum?: boolean;
    isPrimitive?: boolean;
    hasCopyConstructor?: boolean;
}

export class TypeInfo {

    private _name: string;
    private _moduleName?: string;
    private _constructor: any;

    private _isArray: boolean = false;
    private _isClass: boolean = false;
    private _isEnum: boolean = false;
    private _isGeneric: boolean = false;
    private _isPrimitive: boolean = false;
    private _genericArgumentFuncs?: (() => TypeInfo | undefined)[];
    private _elementTypeFunc?: () => TypeInfo | undefined;
    private _hasCopyConstructor?: boolean = false;

    private _properties: PropertyInfo[] = [];

    constructor(initParams: TypeInfoInitParams) {
        this._name = initParams.name;
        this._moduleName = initParams.moduleName;
        this._constructor = initParams.constructor;
        this._hasCopyConstructor = initParams.hasCopyConstructor == true ? true : false;

        if (initParams.isArray) {
            this._isArray = true;
            this._name = "Array";
            this._moduleName = undefined;
            this._constructor = Array;
            this._elementTypeFunc = initParams.elementTypeFunc;
        }
        else if (initParams.isGeneric) {
            this._isClass = true;
            this._isGeneric = true;
            this._genericArgumentFuncs = initParams.genericArgumentFuncs;
        }
        else if (initParams.isEnum) {
            this._isEnum = true;
        }
        else if (initParams.isPrimitive) {
            this._isPrimitive = true;
        }
        else if (initParams.constructor && initParams.constructor.prototype) {
            this._isClass = true;
        }
    }

    get name(): string {
        return this._name;
    }

    get moduleName(): (string | undefined) {
        return this._moduleName;
    }

    get fullName(): string {
        return this.moduleName + "." + this._name;
    }

    get ctor(): any {
        return this._constructor;
    }

    get baseType(): (TypeInfo | undefined) {
        if (this.ctor && this.ctor.prototype && this.ctor.prototype.__proto__) {
            return TypeSystem.getTypeInfo(this.ctor.prototype.__proto__);
        }
        else {
            return undefined;
        }
    }

    get isArray(): boolean {
        return this._isArray;
    }

    get isClass(): boolean {
        return this._isClass;
    }

    get isGeneric(): boolean {
        return this._isGeneric;
    }

    get isEnum(): boolean {
        return this._isEnum;
    }

    get isPrimitive(): boolean {
        return this._isPrimitive;
    }

    get hasCopyConstructor(): (boolean | undefined) {
        return this._hasCopyConstructor;
    }

    get properties(): PropertyInfo[] {
        var properties: PropertyInfo[] = [];

        if (this.baseType) {
            properties = this.baseType.properties;
        }

        properties = properties.concat(this._properties);

        return properties;
    }

    getPropertyByName(propName: string): (PropertyInfo | undefined) {
        for (var prop of this.properties) {
            if (prop.name == propName)
                return prop;
        }

        return undefined;
    }

    getGenericArguments(): (TypeInfo | undefined)[] | undefined {
        if (this.isGeneric) {
            if (this._genericArgumentFuncs) {
                return this._genericArgumentFuncs.map(func => { return func(); })
            }
            else {
                return undefined;
            }
        }
        else {
            return undefined;
        }
    }

    getElementType(): (TypeInfo | undefined) {
        if (this.isArray) {
            if (this._elementTypeFunc) {
                return this._elementTypeFunc();
            }
            else {
                return undefined;
            }
        }
        else {
            return undefined;
        }
    }

    addProperty(property: PropertyInfo) {
        this._properties.push(property);
    }

    isSubClassOf(classConstructor: any): boolean {

        if (classConstructor &&
            this._constructor) {

            var classTypeInfo = TypeSystem.getTypeInfo(classConstructor.prototype);

            var baseClass = this._constructor.prototype.__proto__;

            if (baseClass) {
                var typeInfo = TypeSystem.getTypeInfo(baseClass);

                while (typeInfo) {
                    if (typeInfo == classTypeInfo) {
                        return true;
                    }
                    else {
                        var baseClass = baseClass.__proto__;
                        typeInfo = TypeSystem.getTypeInfo(baseClass);
                    }
                }
            }
        }

        return false;
    }
}

export namespace TypeSystem {

    export function registerClassInfo(constructor: any, name: string, moduleName: string): void {

        let typeInfo = new TypeInfo({ constructor: constructor, name: name, moduleName: moduleName });

        if (!_allTypes.containsKey(typeInfo.fullName)) {

            if (constructor.prototype.hasOwnProperty("__typeInfo__")) {
                throw "TypeSystem: Type " + typeInfo.fullName + "is registered not in the system, but in the constructor";
            }

            constructor.prototype.__typeInfo__ = typeInfo;

            _allTypes.setValue(typeInfo.fullName, typeInfo);
        }
        else if (
            !constructor.prototype.hasOwnProperty("__typeInfo__") ||
            constructor.prototype.__typeInfo__ != typeInfo.fullName
        ) {
            throw "TypeSystem: Type " + typeInfo.fullName + "is registered in the system, but not in the constructor";
        }
    }

    export function registerEnumInfo(enumType: any, name: string, moduleName: string): void {

        let typeInfo = new TypeInfo({
            constructor: Number,
            name: name,
            moduleName: moduleName,
            isEnum: true
        });

        if (!_allTypes.containsKey(typeInfo.fullName)) {

            if (enumType.hasOwnProperty("__typeInfo__")) {
                throw "TypeSystem: Type " + typeInfo.fullName + "is registered not in the system, but in the type";
            }

            enumType.__typeInfo__ = typeInfo;

            _allTypes.setValue(typeInfo.fullName, typeInfo);
        }
        else if (
            !enumType.__typeInfo__ ||
            enumType.__typeInfo__ != typeInfo.fullName
        ) {
            throw "TypeSystem: Type " + typeInfo.fullName + "is registered in the system, but not in the type";
        }
    }

    export function registerPropertyInfo(target: any, propertyInfo: PropertyInfo): void {

        if (target.hasOwnProperty("__propertyInfos__") && target.__propertyInfos__.length > 0) {
            target.__propertyInfos__.push(propertyInfo);
        }
        else {
            target.__propertyInfos__ = [propertyInfo];
        }
    }

    export function getTypeInfoByName(fullName: string): TypeInfo {
        var typeInfo = _allTypes.getValue(fullName);

        ensureProperties(typeInfo);

        return typeInfo;
    }

    export function getTypeInfo(object: any): TypeInfo {

        var objectProto = object.__proto__;

        if (objectProto && objectProto.constructor) {
            if (objectProto.constructor == Number)
                return _numberType;
            else if (objectProto.constructor == String)
                return _stringType;
            else if (objectProto.constructor == Array)
                return _arrayType;
            else if (objectProto.constructor == Boolean)
                return _booleanType;
        }

        var typeInfo = object.__typeInfo__;

        if (typeInfo == undefined && object.prototype) {
            typeInfo = object.prototype.__typeInfo__;
        }

        ensureProperties(typeInfo);

        return typeInfo;
    }

    export function getEnumValues(object: any): Array<string> {
        let enums = new Array<string>();
        for (let value in object) {
            if (typeof object[value] === 'string') {
                enums.push(value);
            }
        }

        return enums;
    }

    export function typeDecorator(name: string, moduleName: string) {
        return (constructor: Function) => {
            registerClassInfo(constructor, name, moduleName);
        }
    }

    export function propertyDecorator(type: any) {
        return function (target: any, propertyKey: string, descriptor: PropertyDescriptor) {
            var propertyTypeFunc = getTypeInfoFunc(type);

            registerPropertyInfo(target, new PropertyInfo(propertyKey, propertyTypeFunc, descriptor));
        }
    }

    export function propertyArrayDecorator(elementType: any) {
        return function (target: any, propertyKey: string, descriptor: PropertyDescriptor) {
            let elementTypeFunc = getTypeInfoFunc(elementType);

            var propertyTypeFunc = () => new TypeInfo({
                name: "Array",
                isArray: true,
                elementTypeFunc: elementTypeFunc
            });

            registerPropertyInfo(target, new PropertyInfo(propertyKey, propertyTypeFunc, descriptor));
        }
    }

    export function propertyGenericDecorator(type: any, ...genericArguments: any[]) {
        return function (target: any, propertyKey: string, descriptor: PropertyDescriptor) {

            var propertyTypeFunc = () => {
                let genericArgumentTypeFuncs: (() => TypeInfo | undefined)[] | undefined;

                if (genericArguments) {
                    genericArgumentTypeFuncs = genericArguments.map(elem => getTypeInfoFunc(elem));
                }

                let originalResult: TypeInfo | undefined = getTypeInfoFunc(type)();

                return new TypeInfo({
                    constructor: originalResult ? originalResult.ctor : undefined,
                    name: originalResult ? originalResult.name : '',
                    moduleName: originalResult ? originalResult.moduleName : undefined,
                    isGeneric: true,
                    genericArgumentFuncs: genericArgumentTypeFuncs
                });
            };

            registerPropertyInfo(target, new PropertyInfo(propertyKey, propertyTypeFunc, descriptor));
        }
    }

    function getTypeInfoFunc(type: any): () => TypeInfo {
        let typeInfoFunc: (() => TypeInfo);

        if (typeof (type) == "string") {
            typeInfoFunc = () => TypeSystem.getTypeInfoByName(type);
        }
        else {
            let typeInfo = TypeSystem.getTypeInfo(type)
            if (typeInfo) {
                typeInfoFunc = () => TypeSystem.getTypeInfo(type);
            }
            else {
                typeInfoFunc = () => new TypeInfo({
                    constructor: type,
                    name: type.constructor.name
                });
            }
        }

        return typeInfoFunc;
    }

    function ensureProperties(typeInfo: TypeInfo) {
        if (typeInfo && typeInfo.ctor) {
            var typeInfoDin: any = typeInfo;
            if (typeInfoDin.__propertiesEnsured__) {
                return;
            }
            else {
                var properties = typeInfo.ctor.prototype.hasOwnProperty("__propertyInfos__") ? typeInfo.ctor.prototype.__propertyInfos__ : undefined;

                if (properties) {
                    for (var property of properties) {
                        typeInfo.addProperty(property);
                    }
                }

                if (typeInfo.ctor.prototype.hasOwnProperty("__propertyInfos__")) {
                    typeInfo.ctor.prototype.__propertyInfos__ = undefined;
                }

                typeInfoDin.__propertiesEnsured__ = true;
            }
        }
    }
}

(function initTypeSystem() {

    _numberType = new TypeInfo({ name: "number", isPrimitive: true });
    _stringType = new TypeInfo({ name: "string", isPrimitive: true });
    _booleanType = new TypeInfo({ name: "boolean", isPrimitive: true });
    _arrayType = new TypeInfo({ constructor: Array, name: "array" });

    _allTypes.setValue("string", _stringType);
    _allTypes.setValue("any", new TypeInfo({ name: "any" }));
    _allTypes.setValue("boolean", _booleanType);
    _allTypes.setValue("number", _numberType);
    _allTypes.setValue("moment", new TypeInfo({ constructor: moment, name: "moment", hasCopyConstructor: true }));
    _allTypes.setValue("duration", new TypeInfo({ constructor: moment.duration, name: "duration", hasCopyConstructor: true }));
})();