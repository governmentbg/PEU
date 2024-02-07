import { ObjectHelper } from '../';
import { TypeSystem } from '../common/TypeSystem';
import { moduleContext } from '../ModuleContext';

@TypeSystem.typeDecorator('ApiError', moduleContext.moduleName)
export class ApiError implements Error {

    private _name: string = "ApiError";
    private _message: string = null;
    private _httpStatusCode: number = 0;
    private _code: string = null;
    private _stackTrace: string = null;
    private _innerErros: ApiError[] = null;
    private _treatAsWarning: boolean = null;


    constructor(message: string);
    constructor(message: string, httpStatusCode: number);
    constructor(message: string, httpStatusCode: number, code: string);
    constructor(message: string, httpStatusCode: number, code: string, stackTrace: string);
    constructor(obj: any);
    constructor(arg1?: any, arg2?: any, arg3?: any, arg4?: any) {

        if (arguments.length == 1 && typeof (arg1) == "object") {
            ObjectHelper.copyFrom(arg1, this);
        }
        else {
            this.message = arg1;
            this.httpStatusCode = arg2;
            this.code = arg3;
            this.stackTrace = arg4;
        }

        //TODO: да се ревизира.
        if (!ObjectHelper.isNullOrUndefined(this.httpStatusCode)) {
            //Определяме дали грешката да се третира като предупреждение.
            switch (this.httpStatusCode) {
                case 429:
                    this.treatAsWarning = true;
                    break;
                default:
                    this.treatAsWarning = false;
                    break;
            }
        }

        this.name = "ApiError";
    }

    public get stack(): string { return this.stackTrace };

    @TypeSystem.propertyDecorator('string')
    public set name(val: string) {
        this._name = val;
    }

    public get name(): string {
        return this._name;
    }

    @TypeSystem.propertyDecorator('string')
    public set message(val: string) {
        this._message = val;
    }

    public get message(): string {
        return this._message;
    }

    @TypeSystem.propertyDecorator('number')
    public set httpStatusCode(val: number) {
        this._httpStatusCode = val;
    }

    public get httpStatusCode(): number {
        return this._httpStatusCode;
    }

    @TypeSystem.propertyDecorator('string')
    public set code(val: string) {
        this._code = val;
    }

    public get code(): string {
        return this._code;
    }

    @TypeSystem.propertyDecorator('string')
    public set stackTrace(val: string) {
        this._stackTrace = val;
    }

    public get stackTrace(): string {
        return this._stackTrace;
    }

    @TypeSystem.propertyArrayDecorator(ApiError ? ApiError : moduleContext.moduleName + '.' + 'ApiError')
    public set innerErrors(val: ApiError[]) {
        this._innerErros = val;
    }

    public get innerErrors(): ApiError[] {
        return this._innerErros;
    }

    @TypeSystem.propertyDecorator('boolean')
    public set treatAsWarning(val: boolean) {
        this._treatAsWarning = val;
    }

    public get treatAsWarning(): boolean {
        return this._treatAsWarning;
    }
}

@TypeSystem.typeDecorator('ClientError', moduleContext.moduleName)
export class ClientError implements Error {
    private _name: string = "ClientError";
    private _message: string = null;
    private _stackTrace: string = null;

    constructor(message: string);
    constructor(message: string, stackTrace: string);
    constructor(obj: any);
    constructor(arg1?: any, arg2?: any) {

        if (arguments.length == 1 && typeof (arg1) == "object") {
            ObjectHelper.copyFrom(arg1, this);
        }
        else {
            this.message = arg1;
            this.stackTrace = arg2;
        }

        this.name = "ClientError";
    }

    @TypeSystem.propertyDecorator('string')
    public set name(val: string) {
        this._name = val;
    }

    public get name(): string {
        return this._name;
    }

    @TypeSystem.propertyDecorator('string')
    public set message(val: string) {
        this._message = val;
    }

    public get message(): string {
        return this._message;
    }

    @TypeSystem.propertyDecorator('string')
    public set stackTrace(val: string) {
        this._stackTrace = val;
    }

    public get stackTrace(): string {
        return this._stackTrace;
    }
}

export const isApiError = (error: Error): error is ApiError => {
    var exType = TypeSystem.getTypeInfo(error);

    if (exType) {
        return exType.ctor == ApiError;
    }
    else {
        return false;
    }
}