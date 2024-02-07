import { UserInfo } from 'cnsys-core';
import { resourceManager } from 'eau-core';
import React, { useState } from 'react';
import { NavLink } from 'react-router-dom';
import logo from '../../../assets/images/logo.png';
import { Constants } from '../../Constants';
import { AccessibilityOptionsUI } from "./AccessibilityOptionsUI";
import { FontSizeUI } from "./FontSizeUI";
import { LanguageSelectorUI } from "./LanguageSelectorUI";
import { ProfileBox } from './ProfileBox';
import { HelpUI } from '../common/HelpUI';

interface HeaderProps {
    user?: UserInfo;
}

export const Header: React.FC<HeaderProps> = (props) => {

    const [isMobileMenuOpen, setIsMobileMenuOpen] = useState(false);

    return (
        <div className="header-wrapper">
            <div className="skip-link">
                <a href="#PAGE-CONTENT" className="skip-to-content" data-anchor="PAGE-CONTENT" onClick={skipLinkClick}>
                    {resourceManager.getResourceByKey('GL_SKIP_NAVIGATION_L')}
                </a>
            </div>

            <header className="header">
                <div className="header-bg">
                    <div className="section-wrapper fixed-content-width header-gerb-bg">
                        <div className="row header-container">
                            <div className="col-auto">
                                <img className="mvr-logo" src={logo} alt=""/>
                            </div>
                            <div className="col header-title">
                                <p>{resourceManager.getResourceByKey("GL_REPUBLIC_OF_BULGARIA_L")}<br />
                                    <strong>{resourceManager.getResourceByKey("GL_MVR_L")}</strong>
                                </p>
                                <p className="site-name">{resourceManager.getResourceByKey("GL_PEAU_L")}</p>
                            </div>
                        </div>
                    </div>
                </div>
            </header>

            <nav className="navbar-top"  aria-label={resourceManager.getResourceByKey("GL_MAIN_NAV_L")}>
                <div className="navbar-top-container fixed-content-width">
                    <div className="navbar-top-menu d-lg-none">
                        <button className="navbar-top-item" onClick={toggle} aria-expanded={isMobileMenuOpen} type="button" title={resourceManager.getResourceByKey("GL_SHOW_ALL_NAVIGATION_ITEMS_I")}>
                            <i className="ui-icon nav-icon-menu" aria-hidden="true"></i>
                            <span className="d-none">{resourceManager.getResourceByKey("GL_SHOW_ALL_NAVIGATION_ITEMS_I")}</span>
                        </button>
                    </div>
                    <div className={isMobileMenuOpen ? "navbar-top-container--mobile" : "navbar-top-container--mobile collapse"}>
                        <ul className="navbar-top-container-left">
                            <li>
                                <NavLink className="navbar-top-item" activeClassName={'active'} to={Constants.PATHS.Services}>{resourceManager.getResourceByKey(Constants.RESOURCES.Services)}</NavLink>
                            </li>
                            <li>
                                <HelpUI cssClass="navbar-top-item" />
                            </li>
                            <li>
                                <NavLink className="navbar-top-item"  activeClassName={'active'} to={Constants.PATHS.Contacts}>{resourceManager.getResourceByKey(Constants.RESOURCES.Contacts)}</NavLink>
                            </li>
                        </ul>
                    </div>
                    <ul className="navbar-top-container-right">
                        <li>
                            <NavLink className="navbar-top-item" activeClassName={'active'} to={Constants.PATHS.Search}>
                                <span title={resourceManager.getResourceByKey(Constants.RESOURCES.Search)}>
                                    <i className="ui-icon nav-icon-search mr-0" aria-hidden="true"></i>
                                    <span className="sr-only">{resourceManager.getResourceByKey("GL_SEARCH_L")}</span>
                                </span>
                            </NavLink>
                        </li>

                        <li>
                            <ProfileBox user={props.user} />
                        </li>
                        <li>
                            <AccessibilityOptionsUI />
                        </li>
                        <li>
                            <LanguageSelectorUI />
                        </li>
                        <li className="navbar-top-font-size d-none d-md-inline">
                            <FontSizeUI />
                        </li>
                    </ul>
                </div>
            </nav>
        </div>);

    function toggle() {
        setIsMobileMenuOpen(!isMobileMenuOpen)
    }

    function blureMobileMenu() {
        setIsMobileMenuOpen(false)
    }

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