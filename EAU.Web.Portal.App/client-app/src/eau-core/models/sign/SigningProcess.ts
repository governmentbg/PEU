import { observable } from 'mobx';
import { TypeSystem, BaseDataModel } from 'cnsys-core';
import { moduleContext } from '../../ModuleContext';
import { Signer } from './Signer';
import { SigningRequestStatuses } from './SignProcessEnums';

@TypeSystem.typeDecorator('SigningProcess', moduleContext.moduleName)
export class SigningProcess extends BaseDataModel {
    @observable private _processID: string = null;   
    @observable private _status: SigningRequestStatuses = undefined;
    @observable private _signers: Signer[] = null;

    constructor(obj?: any) {
        super(obj)

        this.copyFrom(obj);
    }

    @TypeSystem.propertyDecorator('string')
    public set processID(val: string) {
        this._processID = val;
    }

    public get processID(): string {
        return this._processID;
    }

    @TypeSystem.propertyDecorator(SigningRequestStatuses ? SigningRequestStatuses : moduleContext.moduleName + '.' + 'SigningRequestStatuses')
    public set status(val: SigningRequestStatuses) {
        this._status = val;
    }

    public get status(): SigningRequestStatuses {
        return this._status;
    }

    @TypeSystem.propertyArrayDecorator(Signer ? Signer : moduleContext.moduleName + '.' + 'Signer')
    public set signers(val: Signer[]) {
        this._signers = val;
    }

    public get signers(): Signer[] {
        return this._signers;
    }
}