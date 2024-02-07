import { ApiError, ArrayHelper, ClientError, handleErrorLog, moduleContext, ObjectHelper, TypeSystem } from "cnsys-core";
import { action, observable, runInAction } from "mobx";
import { observer } from "mobx-react";
import React from "react";
import { BaseProps } from "./BaseComponent";

interface AsyncFrameProps extends BaseProps {
}

//Fix на проблем Type '{ registerAsyncOperation: <P>(promise: Promise<P>, forceShowLoadingUI?: boolean) => void; asyncErrors: (ApiError | ClientError | Error)[]; ... 9 more ...; fullHtmlName?: string; }' is not assignable to type 'LibraryManagedAttributes<C, Readonly<any> & Readonly<{ children?: ReactNode; }>>'.	C:\Projects\github\EAU\EAU.Web.Portal.App\ClientApp (tsconfig or jsconfig project)	C:\Projects\github\EAU\EAU.Web.Portal.App\ClientApp\src\Cnsys.UI.React\AsyncFrameUI.tsx	226	Active
type P = React.ComponentProps<any>;
type IntrinsicProps = JSX.IntrinsicAttributes &
    JSX.LibraryManagedAttributes<any, { children?: React.ReactNode }>;
type WrappedProps = P & IntrinsicProps & {};

export interface AsyncUIProps {
    asyncErrorMessages?: string[];
    asyncErrors?: (ApiError | ClientError | Error)[];
    /*The operation registers the promise in the async UI. If the promise is pending for long period, then the loading UI is shown*/
    registerAsyncOperation?: (promise: Promise<any>, forceShowLoadingUI?: boolean) => void;
    clearErrors?: () => void;
    drawErrors?: () => any;
    drawWarnings?: () => any;
}

var asyncOperationCount: number = 0;
var globalLoaderID: string = ObjectHelper.newGuid();

@observer class AsyncFrameBaseUI extends React.Component<AsyncFrameProps, any> {
    @observable private _pedingAsyncOperations: any[];
    @observable private _showLoadingUI: boolean;
    @observable protected _errors: (ApiError | ClientError | Error)[];
    @observable protected _withLocalLoader: boolean;

    constructor(props?: AsyncFrameProps) {
        super(props);

        //Binding
        this.registerAsyncOperation = this.registerAsyncOperation.bind(this);
        this.clearUIErrors = this.clearUIErrors.bind(this);
        this.hasWarnings = this.hasWarnings.bind(this);
        this.getWarnings = this.getWarnings.bind(this);
        this.checkToAddGlobalLoader = this.checkToAddGlobalLoader.bind(this);

        //Init
        this._pedingAsyncOperations = [];
        this._errors = [];
        this._showLoadingUI = false;
    }

    checkFreezeUI(): boolean {
        return this._pedingAsyncOperations.length > 0;
    }

    checkShowLoadingUI(): boolean {
        return this.checkFreezeUI() && this._showLoadingUI;
    }

    @action registerAsyncOperation<P>(promise: Promise<P>, forceShowLoadingUI?: boolean): void {
        var that = this;

        /*Init errors list with the first operation.*/
        if (this._pedingAsyncOperations.length == 0) {
            this._errors = [];
            this._showLoadingUI = false;
        }

        if (forceShowLoadingUI === true) {
            this._showLoadingUI = true;
        }

        /*If the promise is not pending, then do nothing or show error if there is any*/
        if (promise.isPending()) {
            var newPromise = promise.catch((ex: any) => {

                console.error(ex);

                this.handleError(ex);

                return null;

            }).finally(() => {
                /* Премахваме операцията от списъка с чакащи асинхронни операции. */
                ArrayHelper.removeElement(that._pedingAsyncOperations, newPromise);

                if (that._withLocalLoader === false) {
                    asyncOperationCount -= 1;

                    if (asyncOperationCount == 0) {
                        let globelLoaderDiv = document.getElementById(globalLoaderID);
                        if (globelLoaderDiv)
                            document.body.removeChild(globelLoaderDiv);
                    }
                }
            });

            /* Добавяме асинхронната операция към списъка с чакащи асинхронни операции. */
            this._pedingAsyncOperations.push(newPromise);

            if (this._withLocalLoader === false) {
                asyncOperationCount += 1;
                this.checkToAddGlobalLoader();
            }

            if (ObjectHelper.isNullOrUndefined(forceShowLoadingUI) || forceShowLoadingUI === false) {
                if (this._withLocalLoader === false && asyncOperationCount > 0)
                    document.getElementById(globalLoaderID).setAttribute('class', 'loader-overlay freeze');

                Promise.delay(100).then(() => {
                    /*Ако все още има чакащи операции, то трябва да се покаже loadingUI*/
                    runInAction(() => {
                        if (that._pedingAsyncOperations.length > 0) {
                            that._showLoadingUI = true;

                            if (that._withLocalLoader === false)
                                document.getElementById(globalLoaderID).setAttribute('class', 'loader-overlay load');
                        }
                    })
                });
            }
        }
        else if (promise.isRejected()) {
            this.handleError(promise.reason());
        }
    }

    checkToAddGlobalLoader(): void {
        let globelLoaderDiv = document.getElementById(globalLoaderID);

        if (!globelLoaderDiv) {
            globelLoaderDiv = document.createElement('div');
            globelLoaderDiv.setAttribute('id', globalLoaderID);
            globelLoaderDiv.setAttribute('class', 'loader-overlay');

            document.body.appendChild(globelLoaderDiv);
        }
    }

    @action clearUIErrors(): void {
        if (this._errors && this._errors.length > 0)
            this._errors = [];
    }

    handleError(error: ApiError | ClientError | Error) {
        this._errors.push(error);
        handleErrorLog(error);
    }

    getMessageFromError(error: ApiError | ClientError | Error): string {

        if (this.isApiError(error)) {
            return (error.message && error.message != '') ? error.message : moduleContext.errors.Error;
        }
        else {
            return moduleContext.errors.Error;
        }
    }

    getInnerErrors(errors: ApiError[] | ClientError[] | Error[]): ApiError[] {
        var innerErrors: ApiError[] = [];

        if (errors) {
            for (let error of errors) {
                if (this.isApiError(error))
                    if (error.innerErrors && error.innerErrors.length > 0) {
                        innerErrors.push(...error.innerErrors);
                    }
            }
        }

        return innerErrors;
    }

    private isApiError(error: Error): error is ApiError {
        var exType = TypeSystem.getTypeInfo(error);

        if (exType) {
            return exType.ctor == ApiError;
        }
        else {
            return false;
        }
    }

    protected hasWarnings(): boolean {
        return this._errors && this._errors.length > 0 && ArrayHelper.queryable.from(this._errors).count(err => this.isApiError(err) && err.treatAsWarning === true) > 0;
    }

    protected getWarnings(): (ApiError | ClientError | Error)[] {
        return ArrayHelper.queryable.from(this._errors).where(err => this.isApiError(err) && err.treatAsWarning === true).toArray();
    }

    protected hasErrors(): boolean {
        return this._errors && this._errors.length > 0 && ArrayHelper.queryable.from(this._errors).count(err => !this.isApiError(err) || (this.isApiError(err) && err.treatAsWarning === false)) > 0;
    }

    protected getErrorsOnly(): (ApiError | ClientError | Error)[] {
        return ArrayHelper.queryable.from(this._errors).where(err => !this.isApiError(err) || (this.isApiError(err) &&  err.treatAsWarning === false)).toArray();
    }
}

export function withAsyncFrame<C extends React.ComponentClass<any> | React.FC<any>>(Component: C, showError: boolean = true, withLocalLoader: boolean = false): C {
    var asyncErrorMessagesEmpty: string[] = [];

    @observer class withAsyncFrameWrapper extends AsyncFrameBaseUI {

        constructor(props: any) {
            super(props);

            //Bind
            this.renderErrors = this.renderErrors.bind(this);
            this.renderWarnings = this.renderWarnings.bind(this);

            //Init
            this._withLocalLoader = withLocalLoader;
        }

        render() {
            let innerErrors = this.getInnerErrors(this._errors);
            let asyncErrMessages: string[] = null;

            let errMessages = this._errors && this._errors.length > 0 ? this._errors.map(err => this.getMessageFromError(err)) : null;
            let innerErrMessages = innerErrors && innerErrors.length > 0 ? innerErrors.map(err => this.getMessageFromError(err)) : null;

            if (innerErrMessages && innerErrMessages.length > 0)
                asyncErrMessages = innerErrMessages;
            else if (errMessages && errMessages.length > 0)
                asyncErrMessages = errMessages;

            return (
                <>
                    {showError && this._errors && this._errors.length > 0 ? <>{this.renderErrors()}{this.renderWarnings()}</> : null}
                    {this._withLocalLoader === false ?
                        <Component {...((this.props as unknown) as WrappedProps)}
                            registerAsyncOperation={this.registerAsyncOperation}
                            asyncErrors={this._errors}
                            /*TODO да се прегледа дали се ползват asyncErrors. В момента има разминаване между това какво влиза в AsyncErrors и AsyncErrorMessages. 
                             asyncErrorMessagesEmpty се ползва за да не се подават различни масиви при всеки render и да е еднотипно с подаването на другия параметър на масив от string */
                            asyncErrorMessages={asyncErrMessages || asyncErrorMessagesEmpty}
                            clearErrors={this.clearUIErrors}
                            drawErrors={this.renderErrors}
                            drawWarnings={this.renderWarnings}                          
                        />
                        :
                        <div className={this.checkShowLoadingUI() ? "loader-overlay-local load" : this.checkFreezeUI() ? "loader-overlay-local freeze" : "loader-overlay-local"}>
                            <Component {...((this.props as unknown) as WrappedProps)}
                                registerAsyncOperation={this.registerAsyncOperation}
                                asyncErrors={this._errors}
                                /*TODO да се прегледа дали се ползват asyncErrors. В момента има разминаване между това какво влиза в AsyncErrors и AsyncErrorMessages. 
                                 asyncErrorMessagesEmpty се ползва за да не се подават различни масиви при всеки render и да е еднотипно с подаването на другия параметър на масив от string */
                                asyncErrorMessages={asyncErrMessages || asyncErrorMessagesEmpty}
                                clearErrors={this.clearUIErrors}
                                drawErrors={this.renderErrors}
                                drawWarnings={this.renderWarnings} />
                        </div>}
                </>
            );
        }

        renderErrors(): any {
            let errors = this.getErrorsOnly();

            if (ObjectHelper.isNullOrUndefined(errors) || errors.length == 0)
                return null;

            return (
                <div className="alert alert-danger" role="alert">
                    {errors.map((err, idx) => {
                        return (<p key={idx}>{this.getMessageFromError(err)}</p>);
                    })}
                </div>);
        }

        renderWarnings(): any {
            let warnings = this.getWarnings();

            if (ObjectHelper.isNullOrUndefined(warnings) || warnings.length == 0)
                return null;

            return (
                <div className="alert alert-warning" role="alert">
                    {warnings.map((err, idx) => {
                        return (<p key={idx}>{this.getMessageFromError(err)}</p>);
                    })}
                </div>);
        }
    };

    (withAsyncFrameWrapper as any).displayName = `withAsyncFrame(${Component.displayName || (Component as any).name || "Component"})`;

    return withAsyncFrameWrapper as any;
}