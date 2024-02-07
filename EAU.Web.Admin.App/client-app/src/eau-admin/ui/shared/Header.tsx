import pdf from 'assets/peau_admin_user_guide_v2_0.pdf';
import logo from 'assets/images/logo.png';
import { UIHelper, UserInfo } from 'cnsys-core';
import { AsyncUIProps, withAsyncFrame } from 'cnsys-ui-react';
import { appConfig, eauAuthenticationService, resourceManager } from 'eau-core';
import React, { useEffect, useState } from 'react';

interface HeaderProps extends AsyncUIProps {
    hideIconMenu?: boolean;
}

const HeaderImpl: React.FC<HeaderProps> = (props) => {

    const [user, setUser] = useState<UserInfo>()

    useEffect(() => {
        UIHelper.initFontSize();

        props.registerAsyncOperation(eauAuthenticationService.getCurrentUser().then(authUser => {
            setUser(authUser);
        }))
    }, [user])

    return <div className="header-wrapper">
        <header id="header" className="header">
            <div className="row header-container">
                <div className="col-sm-auto">
                    <img className="registryagency-logo" src={logo} alt="logo" />
                </div>
                <div className="col-sm header-title header-title--register">
                    <h1>{resourceManager.getResourceByKey("GL_PEAU_L")}</h1>
                    <p>{resourceManager.getResourceByKey("GL_ADMIN_MODULE_L")}</p>
                </div>
            </div>
            <div className="brand-line"></div>
        </header>
        <nav className="navbar-top">
            <div className="container-fluid">
                <div className="row">
                    <div className="col-2">
                        {props.hideIconMenu ? null : <a className="navbar-minimalize" onClick={toggleMenuSize}><i className="ui-icon ui-icon-menu" aria-hidden="true"></i></a>}
                    </div>
                    <div className="col-10">

                        <a href={pdf} className="system-button float-right" title={resourceManager.getResourceByKey("GL_HELP_L")} target='_blank'><i className="ui-icon ui-icon-help navbar-top-icon" aria-hidden="true"></i></a>
                        <div className="horizontal-divider float-right"></div>
                        <div className="navbar-top-font-size float-right">
                            <a onClick={e => UIHelper.setFontSize(-1, appConfig.commonCookieDomain)} title={resourceManager.getResourceByKey("GL_FONT_SIZE_DECREASE_L")}>
                                <i className="ui-icon ui-icon-font-size-minus navbar-top-icon" aria-hidden="true"></i>
                            </a>
                            <a onClick={e => UIHelper.setFontSize(0, appConfig.commonCookieDomain)} title={resourceManager.getResourceByKey("GL_FONT_SIZE_NORMAL_L")}>
                                <i className="ui-icon ui-icon-font-size navbar-top-icon" aria-hidden="true"></i>
                            </a>
                            <a onClick={e => UIHelper.setFontSize(1, appConfig.commonCookieDomain)} title={resourceManager.getResourceByKey("GL_FONT_SIZE_INCREASE_L")}>
                                <i className="ui-icon ui-icon-font-size-plus navbar-top-icon" aria-hidden="true"></i>
                            </a>
                        </div>
                        <div className="horizontal-divider float-right"></div>
                        <ProfileBox user={user} />
                    </div>
                </div>
            </div>
        </nav>
    </div>

    function toggleMenuSize() {
        $("#content-wrapper").toggleClass("mini-navbar-left")
    }
}

export const Header = withAsyncFrame(HeaderImpl);

interface ProfileBoxProps {
    user: UserInfo
}

const ProfileBox: React.FC<ProfileBoxProps> = ({ user }) => {

    const loginFormName: string = "__loginForm";

    if (user) {
        return <div id="user-menu" className="dropdown float-right">
            <button className="system-button user-profile dropdown-toggle" onClick={toggleUserMenu}>
                <i className="ui-icon ui-icon-user navbar-top-icon"></i>{user.email}</button>
            <div id="user-menu-dropdown" className="dropdown-menu">
                <a className="dropdown-item" onClick={signOut}>{resourceManager.getResourceByKey("GL_SIGNOUT_L")}</a>
            </div>
        </div>
    }

    return <div id="user-menu" className="dropdown float-right">
        <button className="system-button user-profile dropdown-toggle" onClick={toggleUserMenu}><i className="ui-icon ui-icon-user navbar-top-icon"></i>{resourceManager.getResourceByKey("GL_USER_L")}</button>
        <div id="user-menu-dropdown" className="dropdown-menu">
            <a className="dropdown-item" onClick={(e) => signIn(e)}>{resourceManager.getResourceByKey("GL_SIGNIN_L")}</a>
            <form name={loginFormName} action="api/Users/Login" method="post"></form>
        </div>
    </div>

    function toggleUserMenu() {
        let userMenu = $("#user-menu-dropdown");

        if (userMenu.hasClass("show")) {
            userMenu.removeClass("show");
            document.removeEventListener("click", documentClickDelegat);
        } else {
            userMenu.addClass("show");
            document.addEventListener("click", documentClickDelegat);
        }
    }

    function documentClickDelegat(event: any): void {
        if ($(event.target).parents("#user-menu").length == 0) {
            $("#user-menu-dropdown").removeClass("show");
            document.removeEventListener("click", documentClickDelegat);
        }
    }

    function signIn(event: any) {
        event.preventDefault();
        document.forms[loginFormName].submit();
    }

    function signOut(event: any) {

        eauAuthenticationService.userLogout();
    }
}