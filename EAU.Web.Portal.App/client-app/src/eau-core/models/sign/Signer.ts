import { observable } from 'mobx';
import { TypeSystem, BaseDataModel } from 'cnsys-core';
import { moduleContext } from '../../ModuleContext';
import { SignerSigningStatuses, SigningChannels } from './SignProcessEnums';

@TypeSystem.typeDecorator('Signer', moduleContext.moduleName)
export class Signer extends BaseDataModel {
    @observable private _signerID: number = null;
    @observable private _name: string = null;
    @observable private _ident: string = null;
    @observable private _order: number = null;
    @observable private _status: SignerSigningStatuses = null;
    @observable private _signingChannel: SigningChannels = null;
    @observable private _rejectReson: string = null;

    constructor(obj?: any) {
        super(obj)

        this.copyFrom(obj);
    }

    @TypeSystem.propertyDecorator('number')
    public set signerID(val: number) {
        this._signerID = val;
    }

    public get signerID(): number {
        return this._signerID;
    }

    @TypeSystem.propertyDecorator('string')
    public set name(val: string) {
        this._name = val;
    }

    public get name(): string {
        return this._name;
    }

    @TypeSystem.propertyDecorator('string')
    public set ident(val: string) {
        this._ident = val;
    }

    public get ident(): string {
        return this._ident;
    }

    @TypeSystem.propertyDecorator('number')
    public set order(val: number) {
        this._order = val;
    }

    public get order(): number {
        return this._order;
    }

    @TypeSystem.propertyDecorator(SignerSigningStatuses ? SignerSigningStatuses : moduleContext.moduleName + '.' + 'SignerSigningStatuses')
    public set status(val: SignerSigningStatuses) {
        this._status = val;
    }

    public get status(): SignerSigningStatuses {
        return this._status;
    }

    @TypeSystem.propertyDecorator(SigningChannels ? SigningChannels : moduleContext.moduleName + '.' + 'SigningChannels')
    public set signingChannel(val: SigningChannels) {
        this._signingChannel = val;
    }

    public get signingChannel(): SigningChannels {
        return this._signingChannel;
    }

    @TypeSystem.propertyDecorator('string')
    public set rejectReson(val: string) {
        this._rejectReson = val;
    }

    public get rejectReson(): string {
        return this._rejectReson;
    }
}