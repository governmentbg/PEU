import { UserContext, UserInfo, AuthenticationModes } from 'cnsys-core';

export class PortalUserContext extends UserContext {

    public ensureUser(userProfile: any) {

        if (this._isAuthenticated === true && this._userInfo)
            return;

        if (userProfile) {
            this._userInfo = new UserInfo(userProfile.email);
            this._userInfo.email = userProfile.email;
            this._userInfo.userIdentifiable = userProfile.user_identifiable != undefined && userProfile.user_identifiable == 'True';
            this._userInfo.uic = userProfile.uic;

            if (userProfile && userProfile.amr && userProfile.amr.indexOf("external") >= 0) {
                if (userProfile.idp.indexOf("nra") >= 0) {
                    this._userInfo.authenticationMode = AuthenticationModes.Nra;
                }
                else {
                    this._userInfo.authenticationMode = AuthenticationModes.Windows;
                }

            }
            else if (userProfile && userProfile.amr && userProfile.amr.indexOf("pwd") >= 0) {
                this._userInfo.authenticationMode = AuthenticationModes.UsernameAndPassword;
            }
            else if (userProfile && userProfile.amr && userProfile.amr.indexOf("cert") >= 0) {
                this._userInfo.authenticationMode = AuthenticationModes.Certificate;
            }
            else {
                this._userInfo.authenticationMode = AuthenticationModes.Unknown;
            }
            this._isAuthenticated = true;
        }
        else {
            this._isAuthenticated = false;
        }
    }
}