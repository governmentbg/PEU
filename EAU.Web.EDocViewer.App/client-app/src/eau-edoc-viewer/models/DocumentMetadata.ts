import { BaseDataModel, TypeSystem } from 'cnsys-core';
import { observable } from 'mobx';
import { moduleContext } from '../ModuleContext';


export enum DocumentViewModes {

    Preview = 1,

    Edit = 2,

    PreviewSign = 3,

    EditSign = 4

}
TypeSystem.registerEnumInfo(DocumentViewModes, 'DocumentViewModes', moduleContext.moduleName)

@TypeSystem.typeDecorator('DocumentMetadata', moduleContext.moduleName)
export class DocumentMetadata extends BaseDataModel {

    //#region viewMode

    @observable private _viewMode: DocumentViewModes = null;

    @TypeSystem.propertyDecorator(DocumentViewModes)
    public set type(val: DocumentViewModes) {
        this._viewMode = val;
    }

    public get type(): DocumentViewModes {
        return this._viewMode;
    } 

    //#endregion viewMode

    //#region documentUrl

    @observable private _documentUrl: string = null;

    @TypeSystem.propertyDecorator('string')
    public set documentUrl(val: string) {
        this._documentUrl = val;
    }

    public get documentUrl(): string {
        return this._documentUrl;
    }

    //#endregion documentUrl

    constructor(obj?: any) {
        super(obj)

        this.copyFrom(obj);
    }
} 
