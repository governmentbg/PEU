import { BaseDataModel, moduleContext, TypeSystem } from 'cnsys-core';
import { observable } from 'mobx';

@TypeSystem.typeDecorator('TreeNode', moduleContext.moduleName)
export class TreeNode extends BaseDataModel {

    @observable private _parentID: string = null;
    @observable private _value: string = null;
    @observable private _text: string = null;
    @observable private _selected: boolean = false;
    @observable private _intermediateState: boolean = false;
    @observable private _isExtended: boolean = false;
    @observable private _children: TreeNode[] = null;

    constructor(obj?: any) {
        super(obj)

        this.copyFrom(obj);
    }

    @TypeSystem.propertyDecorator('string')
    public set parentID(val: string) {
        this._parentID = val;
    }

    public get parentID(): string {
        return this._parentID;
    }

    @TypeSystem.propertyDecorator('string')
    public set value(val: string) {
        this._value = val;
    }

    public get value(): string {
        return this._value;
    }

    @TypeSystem.propertyDecorator('string')
    public set text(val: string) {
        this._text = val;
    }

    public get text(): string {
        return this._text;
    }

    @TypeSystem.propertyDecorator('boolean')
    public set selected(val: boolean) {
        this._selected = val;
    }

    public get selected(): boolean {
        return this._selected;
    }

    @TypeSystem.propertyDecorator('boolean')
    public set intermediateState(val: boolean) {
        this._intermediateState = val;
    }

    public get intermediateState(): boolean {
        return this._intermediateState;
    }

    @TypeSystem.propertyDecorator('boolean')
    public set isExtended(val: boolean) {
        this._isExtended = val;
    }

    public get isExtended(): boolean {
        return this._isExtended;
    }

    @TypeSystem.propertyArrayDecorator(TreeNode ? TreeNode : moduleContext.moduleName + '.' + 'TreeNode')
    public set children(val: TreeNode[]) {
        this._children = val;
    }

    public get children(): TreeNode[] {
        return this._children;
    }
}