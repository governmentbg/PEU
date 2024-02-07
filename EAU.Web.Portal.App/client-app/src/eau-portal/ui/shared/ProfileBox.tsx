import { resourceManager, Constants as coreConstants, eauAuthenticationService, Button } from 'eau-core';
import { UserInfo } from 'cnsys-core';
import * as React from "react";
import { Link } from "react-router-dom";
import { DropdownItem, DropdownMenu, DropdownToggle, UncontrolledDropdown } from "reactstrap";
import { Constants } from '../../Constants';

interface ProfileBoxProps {
    user: UserInfo;
}

export const ProfileBox: React.FC<ProfileBoxProps> = ({ user }) => {

    const loginFormName: string = "__loginForm";

    if (user) {
        return <UncontrolledDropdown a11y={true}>
            <DropdownToggle aria-controls="dropdownMenuUserMenu" id="dropdownMenuUser" type="button" caret className="navbar-top-item" tag="button" data-boundary="window" aria-haspopup={true}>
                <span title={resourceManager.getResourceByKey("GL_USER_PROFILE_L")}>
                    <i className="ui-icon nav-icon-user mr-0 mr-sm-1" aria-hidden="true"></i>
                    <span className="sr-only">{resourceManager.getResourceByKey("GL_USER_PROFILE_L")} {(user.email)}</span>
                    <span className="navbar-top-text-limited d-none d-sm-inline-block" title={user.email} aria-hidden={true}>{user.email}</span>
                </span>
            </DropdownToggle>
            <DropdownMenu aria-labelledby="dropdownMenuUser" id="dropdownMenuUserMenu" positionFixed={true} x-placement="bottom-start">
                <span className="navbar-top-text-limited d-sm-none ml-3 mr-3" title={user.email}>{user.email}</span>
                <div className="dropdown-divider d-sm-none"></div>
                <DropdownItem key={0} tag={Link} to={coreConstants.PATHS.ServiceInstances}>{resourceManager.getResourceByKey(Constants.RESOURCES.MyEServices)}</DropdownItem>
                <DropdownItem key={1} tag={Link} to={Constants.PATHS.MyEPayments}>{resourceManager.getResourceByKey(Constants.RESOURCES.MyEPayments)}</DropdownItem>
                <div className="dropdown-divider"></div>
                <DropdownItem key={2} tag={Link} to={Constants.PATHS.UserProfile}>{resourceManager.getResourceByKey("GL_USER_PROFILE_L")}</DropdownItem>
                <DropdownItem key={3} tag={Link} to={Constants.PATHS.UserAuthentications}>{resourceManager.getResourceByKey("GL_USER_AUTHENTICATIONS_L")}</DropdownItem>
                <DropdownItem key={4} tag={Link} to={Constants.PATHS.ChangePassword}>{resourceManager.getResourceByKey(Constants.RESOURCES.ChangePassword)}</DropdownItem>
                <div className="dropdown-divider"></div>
                <DropdownItem key={5} tag="button" type="button" onClick={(e) => signOut(e)}>{resourceManager.getResourceByKey(Constants.RESOURCES.SignOut)}</DropdownItem>
            </DropdownMenu>
        </UncontrolledDropdown>
    }

    return <UncontrolledDropdown a11y={true}>

        <DropdownToggle aria-controls="dropdownMenuUserMenu" id="dropdownMenuUser" type="button" caret className="navbar-top-item" tag="button" data-boundary="window" aria-haspopup={true}>
            <span title={resourceManager.getResourceByKey(Constants.RESOURCES.Users)}>
                <i className="ui-icon nav-icon-user mr-0 mr-sm-1" aria-hidden="true"></i>
                <span className="d-none d-sm-inline-block">{resourceManager.getResourceByKey(Constants.RESOURCES.Users)}</span>
            </span>
        </DropdownToggle>
        <DropdownMenu aria-labelledby="dropdownMenuUser" id="dropdownMenuUserMenu" positionFixed={true}>
            <DropdownItem key={0} tag="a" href="#" onClick={(e) => signIn(e)}>{resourceManager.getResourceByKey(Constants.RESOURCES.SignIn)}</DropdownItem>
            <DropdownItem key={1} tag={Link} to={Constants.PATHS.ResetPassword}>{resourceManager.getResourceByKey(Constants.RESOURCES.ForgottenPassword)}</DropdownItem>
            <DropdownItem key={2} tag={Link} to={Constants.PATHS.Registration}>{resourceManager.getResourceByKey('GL_REGISTRATION_L')}</DropdownItem>
        </DropdownMenu>
        <form name={loginFormName} action="api/Users/Login" method="post"></form>
    </UncontrolledDropdown>

    function signIn(event: any) {
        event.preventDefault();
        document.forms[loginFormName].submit();
    }

    function signOut(event: any) {
        event.preventDefault();
        eauAuthenticationService.userLogout();
    }
}