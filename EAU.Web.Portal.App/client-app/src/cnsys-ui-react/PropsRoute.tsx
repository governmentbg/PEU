import { ObjectHelper } from 'cnsys-core';
import React from 'react';
import { Route, RouteProps } from 'react-router-dom';

interface PropsRouteProps extends RouteProps {  
    componentProps?: any;
}

function renderMergedProps(component: any, ...args: any[]): any {
    var finalProps = ObjectHelper.assign({}, ...args);
    return (
        React.createElement(component, finalProps)
    );
}

export function PropsRoute(args: PropsRouteProps): JSX.Element {
    return (
        <Route path={args.path} exact={args.exact} strict={args.strict} render={routeProps => {
            return renderMergedProps(args.component, routeProps, args.componentProps);
        }}/>
    );
}