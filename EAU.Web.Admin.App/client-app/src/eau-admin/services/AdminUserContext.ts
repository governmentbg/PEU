import { UserContext, UserInfo } from 'cnsys-core';

export class AdminUserContext extends UserContext {

    public ensureUser(userProfile: any) {

        if (this._isAuthenticated === true && this._userInfo)
            return;

        if (userProfile) {
            this._userInfo = new UserInfo(userProfile.name);
            this._userInfo.email = userProfile.email;
            this._userInfo.roles = userProfile.roles;
            
            this._isAuthenticated = true;
        }
        else {
            this._isAuthenticated = false;
        }
    }
}