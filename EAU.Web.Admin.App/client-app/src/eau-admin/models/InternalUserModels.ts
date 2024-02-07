import { BaseDataModel, TypeSystem } from 'cnsys-core';
import { UserPermissions } from 'eau-core';
import { observable } from 'mobx';
import { moduleContext } from '../ModuleContext';
import moment from 'moment';

@TypeSystem.typeDecorator('InternalUserVM', moduleContext.moduleName)
export class InternalUserVM extends BaseDataModel {

    //#region userID

    @observable private _userID: number = null;

    @TypeSystem.propertyDecorator('number')
    public set userID(val: number) {
        this._userID = val;
    }

    public get userID(): number {
        return this._userID;
    }

    //#endregion 

    //#region userName

    @observable private _username: string = null;

    @TypeSystem.propertyDecorator('string')
    public set username(val: string) {
        this._username = val;
    }

    public get username(): string {
        return this._username;
    }

    //#endregion

    //#region email

    @observable private _email: string = null;

    @TypeSystem.propertyDecorator('string')
    public set email(val: string) {
        this._email = val;
    }

    public get email(): string {
        return this._email;
    }

    //#endregion

    //#region updatedOn

    @observable private _updatedOn: moment.Moment = null;

    @TypeSystem.propertyDecorator('moment')
    public set updatedOn(val: moment.Moment) {
        this._updatedOn = val;
    }

    public get updatedOn(): moment.Moment {
        return this._updatedOn;
    }

    //#endregion 

    //#region cin

    @observable private _cin: number = null;

    @TypeSystem.propertyDecorator('number')
    public set cin(val: number) {
        this._cin = val;
    }

    public get cin(): number {
        return this._cin;
    }

    //#endregion

    //#region isActive

    @observable private _isActive: boolean = false;

    @TypeSystem.propertyDecorator('boolean')
    public set isActive(val: boolean) {
        this._isActive = val;
    }

    public get isActive(): boolean {
        return this._isActive;
    }

    //#endregion

    //#region userPermisions

    @observable private _userPermisions: UserPermissions[] = null;

    @TypeSystem.propertyArrayDecorator(moduleContext.moduleName + '.' + 'UserPermissions')
    public set userPermisions(val: UserPermissions[]) {
        this._userPermisions = val;
    }

    public get userPermisions(): UserPermissions[] {
        return this._userPermisions;
    }

    //#endregion 

    constructor(obj?: any) {
        super(obj)

        this.copyFrom(obj);
    }
}