import { observable } from 'mobx'
import { TypeSystem, BaseDataModel } from 'cnsys-core'
import { moduleContext } from '../ModuleContext'
import { Page } from 'eau-core'

@TypeSystem.typeDecorator('PageI18nVM', moduleContext.moduleName)
export class PageI18nVM extends Page { 

    //#region _bgTitle

    @observable private _bgTitle: string = null;

    @TypeSystem.propertyDecorator('string')
    public set bgTitle(val: string){
        this._bgTitle = val;
    }

    public get bgTitle(): string{
        return this._bgTitle;
    } 

    //#endregion _bgTitle

    //#region _bgTitle

    @observable private _bgContent: string = null;

    @TypeSystem.propertyDecorator('string')
    public set bgContent(val: string){
        this._bgContent = val;
    }

    public get bgContent(): string{
        return this._bgContent;
    } 

    //#endregion _bgContent

    @observable private _languageCode: string = null;

    @TypeSystem.propertyDecorator('string')
    public set languageCode(val: string){
        this._languageCode = val;
    }

    public get languageCode(): string{
        return this._languageCode;
    }
}