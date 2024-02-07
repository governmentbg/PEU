import React from 'react';
import { UrlHelper, ObjectHelper } from 'cnsys-core';
import { ContextInfoHelper, resourceManager } from 'eau-core';

interface SectionTitleProps {
    title: string;
    sectionCode: string;
    additionalWebKeyCode?: string
}

export function SectionTitleUI(props: SectionTitleProps): JSX.Element {
    let btnClick = function (e: any): void {
        let fieldKey: string = undefined;

        switch (props.sectionCode) {
            case 'declarations':
                fieldKey = props.sectionCode;
                break;
            default:
                fieldKey = `$1_${props.sectionCode}`;
                break;
        }

        if (!ObjectHelper.isStringNullOrEmpty(props.additionalWebKeyCode)) {
            fieldKey = `${fieldKey}_${props.additionalWebKeyCode}`;
        }

        ContextInfoHelper.getWebHelpUrl(fieldKey).then(url => {
            UrlHelper.openInNewTab(url);
        });
    };

    return (
        <h2 className="section-title">
            {props.title}&nbsp;
            <button
                type="button"
                className="btn btn-context-help"
                title={resourceManager.getResourceByKey("GL_CONTEXT_HELP_L") + ". " + resourceManager.getResourceByKey("GL_PAGE_OPEN_IN_NEW_TAB_L")}
                onClick={btnClick}></button>
        </h2 >
    );
}