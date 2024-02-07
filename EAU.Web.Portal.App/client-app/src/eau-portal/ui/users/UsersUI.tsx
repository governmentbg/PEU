import { UserInfo } from 'cnsys-core';
import { AsyncUIProps, BaseProps, withAsyncFrame } from 'cnsys-ui-react';
import { eauAuthenticationService } from 'eau-core';
import React, { useEffect, useState } from 'react';
import { Redirect, Route, Switch } from 'react-router-dom';
import { Constants } from '../../Constants';
import { UnauthenticatedPageUI } from '../shared/UnauthenticatedPageUI';
import { CancelRegistrationUI } from './CancelRegistrationUI';
import { ChangePasswordUI } from './ChangePasswordUI';
import { ConfirmRegistrationUI } from './ConfirmRegistrationUI';
import { ForgottenPasswordUI } from './ForgottenPasswordUI';
import { RegistrationUI } from './RegistrationUI';
import { RegistrationFormCompleteUI } from './RegistrationFormCompleteUI';
import { ResetPasswordUI } from './ResetPasswordUI';
import { UserAuthenticationsUI } from './UserAuthenticationsUI';
import { UserProfileUI } from './UserProfileUI';

interface UsersProps extends BaseProps, AsyncUIProps {
}

const UsersImpl: React.FC<UsersProps> = (props) => {

    const [user, setUser] = useState<UserInfo>();

    useEffect(() => {
        props.registerAsyncOperation(eauAuthenticationService.getCurrentUser().then((user) => {
            setUser(user);
        }))
    }, [user])

    return <>
        <Switch>
            {/*Ако потребителят се е логнал, го редиректваме към началната страница*/}
            {user ? <Redirect from={Constants.PATHS.Registration} to={Constants.PATHS.Home} /> : <Route key={1} path={Constants.PATHS.Registration} component={RegistrationUI} />}
            {user ? <Redirect from={Constants.PATHS.ResetPassword} to={Constants.PATHS.Home} /> : <Route key={2} path={Constants.PATHS.ResetPassword} component={ResetPasswordUI} />}
            {user ? <Redirect from={Constants.PATHS.ForgottenPassword} to={Constants.PATHS.Home} /> : <Route key={3} path={Constants.PATHS.ForgottenPassword} component={ForgottenPasswordUI} />}
            {user ? <Redirect from={Constants.PATHS.ConfirmUserRegistration} to={Constants.PATHS.Home} /> : <Route key={4} path={Constants.PATHS.ConfirmUserRegistration} component={ConfirmRegistrationUI} />}
            {user ? <Redirect from={Constants.PATHS.CancelUserRegistration} to={Constants.PATHS.Home} /> : <Route key={5} path={Constants.PATHS.CancelUserRegistration} component={CancelRegistrationUI} />}
            {user ? <Redirect from={Constants.PATHS.RegistrationFormComplete} to={Constants.PATHS.Home} /> : <Route key={9} path={Constants.PATHS.RegistrationFormComplete} component={RegistrationFormCompleteUI} />}

            {/*Ако нямаме логнат потребител, показваме UnauthenticatedPageUI*/}
            <Route key={6} path={Constants.PATHS.UserProfile} component={user ? UserProfileUI : UnauthenticatedPageUI} />
            <Route key={7} path={Constants.PATHS.ChangePassword} component={user ? ChangePasswordUI : UnauthenticatedPageUI} />
            <Route key={8} path={Constants.PATHS.UserAuthentications} component={user ? UserAuthenticationsUI : UnauthenticatedPageUI} />
        </Switch>
    </>
}

export const UsersUI = withAsyncFrame(UsersImpl);