import { BaseDataModel, TypeSystem } from 'cnsys-core'
import { observable } from 'mobx'
import moment from 'moment'
import { moduleContext } from '../ModuleContext'

@TypeSystem.typeDecorator('CertificateVM', moduleContext.moduleName)
export class CertificateVM extends BaseDataModel {

    //#region certificateID

    @observable private _certificateID: number = null;

    @TypeSystem.propertyDecorator('number')
    public set certificateID(val: number) {
        this._certificateID = val;
    }

    public get certificateID(): number {
        return this._certificateID;
    }

    //#endregion certificateID 

    //#region serialNumber

    @observable private _serialNumber: string = null;

    @TypeSystem.propertyDecorator('string')
    public set serialNumber(val: string) {
        this._serialNumber = val;
    }

    public get serialNumber(): string {
        return this._serialNumber;
    }

    //#endregion serialNumber 

    //#region issuer

    @observable private _issuer: string = null;

    @TypeSystem.propertyDecorator('string')
    public set issuer(val: string) {
        this._issuer = val;
    }

    public get issuer(): string {
        return this._issuer;
    }

    //#endregion issuer 

    //#region subject

    @observable private _subject: string = null;

    @TypeSystem.propertyDecorator('string')
    public set subject(val: string) {
        this._subject = val;
    }

    public get subject(): string {
        return this._subject;
    }

    //#endregion subject 

    //#region validNotBefore

    @observable private _validNotBefore: moment.Moment = null;

    @TypeSystem.propertyDecorator('moment')
    public set validNotBefore(val: moment.Moment) {
        this._validNotBefore = val;
    }

    public get validNotBefore(): moment.Moment {
        return this._validNotBefore;
    }

    //#endregion validNotBefore 

    //#region validNotAfter

    @observable private _validNotAfter: moment.Moment = null;

    @TypeSystem.propertyDecorator('moment')
    public set validNotAfter(val: moment.Moment) {
        this._validNotAfter = val;
    }

    public get validNotAfter(): moment.Moment {
        return this._validNotAfter;
    }

    //#endregion validNotAfter 

    constructor(obj?: any) {
        super(obj)

        this.copyFrom(obj);
    }
} 