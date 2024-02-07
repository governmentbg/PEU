import { resourceManager } from "eau-core";
import { Section } from "eau-documents";
import React from 'react';

interface MenuUIProps {
    sections: Section[]
    currentSectionCode: string
    onChangeSection: (code: string) => void
    collapsed?: boolean
}

export function MenuUI(props: MenuUIProps): JSX.Element {
    return (
        <div className={!props.collapsed ? "nav-wrapper collapse" : "nav-wrapper collapse show"} id = "PAGE-NAV" >
            <div className="skip-link">
                <a href="#ARTICLE-CONTENT" className="skip-to-content" data-anchor="ARTICLE-CONTENT" onClick={skipLinkClick}>
                    {resourceManager.getResourceByKey('GL_SKIP_NAVIGATION_IN_APP_PROCESS_L')}
                </a>
            </div>
            <nav className="page-nav" aria-label={resourceManager.getResourceByKey("GL_SECTIONS_IN_APPLICATION_L")}>
                <ul className="nav-section connected-nav-items">
                    {props.sections.map((item, index) => <li key={index} className="nav-item">
                        <div className="nav-item-state">
                            {item.errors == null || item.errors == undefined
                                ? <i className="ui-icon ui-icon-empty"></i>
                                : item.errors.length > 0
                                    ? <i className="ui-icon ui-icon-error"></i>
                                    : <i className="ui-icon ui-icon-processed"></i>
                            }
                        </div>
                        <div className="nav-item-title">
                            <button onClick={() => { props.onChangeSection(item.code) }} className={"nav-link" + (item.code == props.currentSectionCode ? " active" : "")} type={"button"}>
                                {item.title}
                            </button>
                        </div>
                    </li>)}
                </ul>
            </nav>
        </div>
    );

    function skipLinkClick(e: any): boolean {
        e.preventDefault();

        let anchorId = '#' + $(e.target).data('anchor');
        let anchor = document.querySelector(anchorId);
        $(anchor).attr('tabIndex', -1);
        anchor.scrollIntoView();
        $(anchor).focus();

        return false;
    }
}