import { observable } from 'mobx'
import { TypeSystem, BaseDataModel } from 'cnsys-core'
import { moduleContext } from '../ModuleContext'
import { Service } from 'eau-core'

@TypeSystem.typeDecorator('ServiceI18nVM', moduleContext.moduleName)
export class ServiceI18nVM extends Service { 

    //#region _bgName

    @observable private _bgName: string = null;

    @TypeSystem.propertyDecorator('string')
    public set bgName(val: string){
        this._bgName = val;
    }

    public get bgName(): string{
        return this._bgName;
    } 

    //#endregion _bgName

    //#region _bgDescription

    @observable private _bgDescription: string = null;

    @TypeSystem.propertyDecorator('string')
    public set bgDescription(val: string){
        this._bgDescription = val;
    }

    public get bgDescription(): string{
        return this._bgDescription;
    } 

    //#endregion _bgDescription

    //#region _bgAttachedDocumentsDescription

    @observable private _bgAttachedDocumentsDescription: string = null;

    @TypeSystem.propertyDecorator('string')
    public set bgAttachedDocumentsDescription(val: string){
        this._bgAttachedDocumentsDescription = val;
    }

    public get bgAttachedDocumentsDescription(): string{
        return this._bgAttachedDocumentsDescription;
    } 

    //#endregion _bgAttachedDocumentsDescription

    //#region _languageCode
    @observable private _langCode: string = null;

    @TypeSystem.propertyDecorator('string')
    public set langCode(val: string){
        this._langCode = val;
    }

    public get langCode(): string{
        return this._langCode;
    }

    //#endregion _languageCode
}