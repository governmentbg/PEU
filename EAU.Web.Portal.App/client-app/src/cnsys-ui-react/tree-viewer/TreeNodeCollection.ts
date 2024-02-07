import { BaseDataModel, moduleContext, TypeSystem } from 'cnsys-core';
import { observable } from 'mobx';
import { TreeNode } from './TreeNode';

@TypeSystem.typeDecorator('TreeNodeCollection', moduleContext.moduleName)
export class TreeNodeCollection extends BaseDataModel {

    @observable private _items: TreeNode[] = null;

    @TypeSystem.propertyArrayDecorator(TreeNode ? TreeNode : moduleContext.moduleName + '.' + 'TreeNode')
    public set items(val: TreeNode[]) {
        this._items = val;
    }

    public get items(): TreeNode[] {
        return this._items;
    }

    public getSelectedTreeNodes(): TreeNode[] {
        return this.getSelectedChildrenNodes(this.items);
    }

    private getSelectedChildrenNodes(treeNodes: TreeNode[]): TreeNode[] {
        let selectedTreeNodes: TreeNode[] = [];

        if (treeNodes && treeNodes.length > 0) {
            for (var i = 0; i < treeNodes.length; i++) {
                let curNode = treeNodes[i];

                if (curNode.selected === true)
                    selectedTreeNodes.push(treeNodes[i]);

                if (curNode.children && curNode.children.length > 0)
                    selectedTreeNodes.push(...this.getSelectedChildrenNodes(curNode.children));
            }
        }

        return selectedTreeNodes;
    }

    constructor(obj?: any) {
        super(obj)

        this.copyFrom(obj);
    }
}