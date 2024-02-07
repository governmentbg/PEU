import { observable } from 'mobx'
import { TypeSystem, BaseDataModel } from 'cnsys-core'
import { moduleContext } from '../ModuleContext'
import { Label } from 'eau-core';

@TypeSystem.typeDecorator('LabelTranslationI18nVM', moduleContext.moduleName)
export class LabelTranslationI18nVM extends Label {

    @observable private _labelCode: string = null;

    @TypeSystem.propertyDecorator('string')
    public set labelCode(val: string){
        this._labelCode = val;
    }

    public get labelCode(): string{
        return this._labelCode;
    }

    @observable private _languageName: string = null;

    @TypeSystem.propertyDecorator('string')
    public set languageName(val: string){
        this._languageName = val;
    }

    public get languageName(): string{
        return this._languageName;
    }

    @observable private _bgValue: string = null;

    @TypeSystem.propertyDecorator('string')
    public set bgValue(val: string){
        this._bgValue = val;
    }

    public get bgValue(): string{
        return this._bgValue;
    }

    constructor(obj?: any){
        super(obj)

        this.copyFrom(obj);
    }
}