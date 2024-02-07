import { UIHelper } from 'cnsys-core';
import { resourceManager, appConfig } from "eau-core";
import React from "react";

export function FontSizeUI(props: any): JSX.Element {
    let onDecreaseFont = function (e: any): void {
        e.preventDefault();
        UIHelper.setFontSize(-1, appConfig.commonCookieDomain);
    };

    let onNormalizeFont = function (e: any): void {
        e.preventDefault();
        UIHelper.setFontSize(0, appConfig.commonCookieDomain);
    };

    let onIncreaseFont = function (e: any): void {
        e.preventDefault();
        UIHelper.setFontSize(1, appConfig.commonCookieDomain);
    };

    UIHelper.initFontSize();

    return (
        <div className="navbar-top-item">
            <button type="button" onClick={(e) => onDecreaseFont(e)}>
                <span title={resourceManager.getResourceByKey("GL_DECREASE_FONT_SIZE_L")}>
                    <i className="ui-icon nav-icon-font-minus" aria-hidden="true"></i>
                    <span className="sr-only">{resourceManager.getResourceByKey("GL_DECREASE_FONT_SIZE_L")}</span>
                </span>
            </button>
            <button type="button" onClick={(e) => onNormalizeFont(e)}>
                <span title={resourceManager.getResourceByKey("GL_NORMAL_FONT_L")}>
                    <i className="ui-icon nav-icon-font" aria-hidden="true"></i>
                    <span className="sr-only">{resourceManager.getResourceByKey("GL_NORMAL_FONT_L")}</span>
                </span>
            </button>
            <button type="button" onClick={(e) => onIncreaseFont(e)}>
                <span title={resourceManager.getResourceByKey("GL_INCREASE_FONT_SIZE_L")}>
                    <i className="ui-icon nav-icon-font-plus" aria-hidden="true"></i>
                    <span className="sr-only">{resourceManager.getResourceByKey("GL_INCREASE_FONT_SIZE_L")}</span>
                </span>
            </button>
        </div>);
}