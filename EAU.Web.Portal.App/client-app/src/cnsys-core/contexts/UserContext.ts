import { computed, observable } from "mobx";
import { ArrayHelper } from '../common/ArrayHelper';

export class UserContext {
    @observable protected _isAuthenticated: boolean = false;
    @observable protected _userInfo?: UserInfo;

    constructor() {
    }

    @computed public get isAuthenticated(): boolean {
        return this._isAuthenticated;
    }

    @computed public get user(): UserInfo | undefined {
        return this._userInfo;
    }

    public ensureUser(userProfile: any) {
        // virtual
    }

    public isInRole(role: string): boolean {
        if (this.isAuthenticated
            && this.user
            && this.user.roles
            && this.user.roles.length > 0) {
            let user = this.user;

            if (ArrayHelper.queryable.from(user.roles).count(function (el: string) { return el === role }) == 1) {
                return true;
            }
        }

        return false;
    }
}

export enum AuthenticationModes {
    Unknown = 0,
    UsernameAndPassword = 1,
    Windows = 2,
    Certificate = 3,
    Nra = 4,
}

export class UserInfo {
    private _name: string;
    private _email: string;
    private _roles: string[] = [];
    private _authenticationMode: AuthenticationModes;
    private _userIdentifiable: boolean;
    private _uic: string;

    constructor(name: string) {
        this._name = name;
    }

    public get name(): string {
        return this._name;
    }

    public get email(): string {
        return this._email;
    }
    public set email(value: string) {
        this._email = value;
    }

    public get roles(): string[] {
        return this._roles;
    }
    public set roles(value: string[]) {
        this._roles = value;
    }

    public get authenticationMode(): AuthenticationModes {
        return this._authenticationMode;
    }
    public set authenticationMode(value: AuthenticationModes) {
        this._authenticationMode = value;
    }

    public get userIdentifiable(): boolean {
        return this._userIdentifiable;
    }
    public set userIdentifiable(value: boolean) {
        this._userIdentifiable = value;
    }

    public get uic(): string {
        return this._uic;
    }
    public set uic(value: string) {
        this._uic = value;
    }
}