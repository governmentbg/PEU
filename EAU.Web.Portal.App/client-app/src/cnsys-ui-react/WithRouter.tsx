import { appConfig } from 'cnsys-core';
import { parse, stringify } from 'query-string';
import React from "react";
import { RouteComponentProps, withRouter as withRouterBase } from 'react-router-dom';
import { BaseProps } from './';

//Fix на проблем Type '{ registerAsyncOperation: <P>(promise: Promise<P>, forceShowLoadingUI?: boolean) => void; asyncErrors: (ApiError | ClientError | Error)[]; ... 9 more ...; fullHtmlName?: string; }' is not assignable to type 'LibraryManagedAttributes<C, Readonly<any> & Readonly<{ children?: ReactNode; }>>'.	C:\Projects\github\EAU\EAU.Web.Portal.App\ClientApp (tsconfig or jsconfig project)	C:\Projects\github\EAU\EAU.Web.Portal.App\ClientApp\src\Cnsys.UI.React\AsyncFrameUI.tsx	226	Active
type P = React.ComponentProps<any>;
type IntrinsicProps = JSX.IntrinsicAttributes &
    JSX.LibraryManagedAttributes<any, { children?: React.ReactNode }>;
type WrappedProps = P & IntrinsicProps & {};

export interface BaseRouteProps<TParams> extends BaseProps, RouteComponentProps<TParams> {
}

export interface BaseRouteParams {
}

export interface BaseRoutePropsExt {
    routerExt?: IRouterExt;
}

export interface IRouterExt {
    goTo(path: string, params: any): void;
    changeParams(params: any): void;
    mergeParamsTo(obj: any): void;
    hasParams(): any;
    getParams(): any;
    hasParamsChanged(obj: any): boolean;
    haveDifferences(obj: any, query: any): boolean;
    getOnBackEventName(): string;
}

export function withRouter<C extends React.ComponentClass<any>>(Component: C): C {

    var wrapper = class extends React.Component<BaseRouteProps<BaseRouteParams>, any> {
        constructor(props: BaseRouteProps<BaseRouteParams>) {
            super(props);
        }

        render() {
            return <Component {...((this.props as unknown) as WrappedProps) } routerExt={this} />
        }

        goTo(path: string, params: any): void {
            //Защото искаме да вземем само публичните пропъртита и е описано в ToJSON
            var paramsNew = JSON.parse(JSON.stringify(params))
            var hash = null;

            if (path.indexOf("#") >= 0) {
                hash = path.substring(path.indexOf("#") + 1);
                path = path.substring(0, path.indexOf("#"));
            }

            for (var key in paramsNew) {
                if (paramsNew[key] == null)
                    paramsNew[key] = undefined;
            }

            this.props.history.push({
                pathname: path,
                search: stringify(paramsNew, null),
                hash: hash
            });
        }

        changeParams(params: any): void {
            let path: string = this.props.location.pathname.replace(appConfig.baseUrlName, "");
            this.goTo(path, params);
        }

        mergeParamsTo(obj: any) {
            obj.copyFrom(parse(this.props.location.search))
        }

        hasParams(): any {
            return Object.keys(this.getParams()).length > 0;
        }

        getParams(): any {
            return parse(this.props.location.search);
        }

        hasParamsChanged(obj: any): boolean {
            var query = parse(this.props.location.search);
            for (var qProp in query) {
                for (var objProp in obj) {
                    if (qProp == objProp) {
                        if (query[qProp] != obj[qProp]) {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        haveDifferences(obj: any, query: any): boolean {
            for (var qProp in query) {
                for (var objProp in obj) {
                    if (qProp == objProp) {
                        if (query[qProp] != obj[qProp]) {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        getOnBackEventName() {
            return "popstate"
        }
    };

    return withRouterBase(wrapper) as any;;
}
