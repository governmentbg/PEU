import { observable } from 'mobx';
import { appConfig } from '../common/ApplicationConfig';
import { TypeSystem } from '../common/TypeSystem';
import { moduleContext } from '../ModuleContext';
import { BaseDataModel } from './BaseDataModel';

@TypeSystem.typeDecorator('BasePagedSearchCriteria', moduleContext.moduleName)
export class BasePagedSearchCriteria extends BaseDataModel {
    @observable private _page: number = 0;
    @observable private _pageSize: number = 0;
    @observable private _count: number = 0;

    @TypeSystem.propertyDecorator('number')
    public set page(val: number) {
        this._page = val;
    }

    public get page(): number {
        return this._page;
    }

    @TypeSystem.propertyDecorator('number')
    public set pageSize(val: number) {
        this._pageSize = val;
    }

    public get pageSize(): number {
        return this._pageSize;
    }

    @TypeSystem.propertyDecorator('number')
    public set count(val: number) {
        this._count = val;
    }

    public get count(): number {
        return this._count;
    }

    constructor(obj?: any) {
        super(obj)

        this.copyFrom(obj);

        this.page = 1;
        this.pageSize = appConfig.defaultPageSize ? appConfig.defaultPageSize : 10;
    }

    public getPagesCount(): number {
        return Math.ceil(this.count / this.pageSize);
    }

    public clone(): any {
        let t: any = null;
        let typeInfo = TypeSystem.getTypeInfo(this);

        t = new typeInfo.ctor(JSON.parse(JSON.stringify(this, function (k, v) { return v === null ? undefined : v; })));

        return t;
    }
}
