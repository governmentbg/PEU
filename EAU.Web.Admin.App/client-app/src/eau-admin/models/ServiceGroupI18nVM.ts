import { observable } from 'mobx'
import { TypeSystem, BaseDataModel, BasePagedSearchCriteria } from 'cnsys-core'
import { moduleContext } from '../ModuleContext'
import { ServiceGroup } from 'eau-core';

@TypeSystem.typeDecorator('ServiceGroupI18nVM', moduleContext.moduleName)
export class ServiceGroupI18nVM extends ServiceGroup { 
   
    //#region bgName

    @observable private _bgName: string = null;

    @TypeSystem.propertyDecorator('string')
    public set bgName(val: string){
        this._bgName = val;
    }

    public get bgName(): string{
        return this._bgName;
    } 

    //#endregion bgName

    constructor(obj?: any){
    super(obj)

    this.copyFrom(obj);
    }
}