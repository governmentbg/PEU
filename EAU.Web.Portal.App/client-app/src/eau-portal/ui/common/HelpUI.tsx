import React from 'react';
import { UrlHelper } from 'cnsys-core';
import { ContextInfoHelper, resourceManager, appConfig } from 'eau-core';

export interface HelpUIProps {
    cssClass: string;
    accessKey?: string
}

export function HelpUI(props: HelpUIProps): JSX.Element {
    let btnClick = function (e: any): void {
        if (window.location.pathname.indexOf('/services/applicationProcesses') >= 0) {
            ContextInfoHelper.getWebHelpUrl('$1').then(url => {
                UrlHelper.openInNewTab(url);
            });
        } if (window.location.pathname.indexOf('/services/obligations') >= 0) {
            ContextInfoHelper.getWebHelpUrl().then(url => {
                UrlHelper.openInNewTab(url);
            });
        } else {
            UrlHelper.openInNewTab(appConfig.webHelpUrl);
        }
    };

    return (
        <button
            accessKey={props.accessKey}
            tabIndex={props.accessKey ? -1 : null}
            type="button"
            className={props.cssClass}
            title={resourceManager.getResourceByKey("GL_PAGE_OPEN_IN_NEW_TAB_L")}
            onClick={btnClick}>{props.accessKey ? `${props.accessKey}-${resourceManager.getResourceByKey("GL_HELP_L")}` : resourceManager.getResourceByKey("GL_HELP_L")}</button>);
}