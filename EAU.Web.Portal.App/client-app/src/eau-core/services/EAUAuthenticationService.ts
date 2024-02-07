import { UserContext, UserInfo } from 'cnsys-core';
import { EAUBaseDataService } from './EAUBaseDataService';

class EAUAuthenticationService extends EAUBaseDataService {

    private _context: UserContext;

    protected baseUrl(): string {
        return super.baseUrl() + "Users";
    }

    public init(_userContext: UserContext): Promise<Boolean> {
        this._context = _userContext;

        return this.loadCurrentUser().then(user => {

            if (user) {
                this._context.ensureUser(user);
                return true;
            }
            else {
                return false;
            }
        });
    }

    public getCurrentUser(): Promise<UserInfo> {
        return Promise.resolve(this._context.user);
    }

    public loadCurrentUser(): Promise<any> {
        return this.get<any>("current", null).then(r => {
            return r;
        }, e => {
            return null;
        });
    }

    public keepSessionAlive(): Promise<any> {
        return this.get<any>("KeepSessionAlive", null);
    }
}

export const eauAuthenticationService = new EAUAuthenticationService();