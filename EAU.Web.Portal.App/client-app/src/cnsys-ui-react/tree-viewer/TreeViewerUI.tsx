import { ArrayHelper } from "cnsys-core";
import { BaseComponent, BaseProps, ViewMode } from "cnsys-ui-react";
import { action } from "mobx";
import { observer } from "mobx-react";
import * as React from "react";
import { TreeNode } from "./TreeNode";
import { TreeNodeCollection } from "./TreeNodeCollection";
import { TreeNodeComponentUIProps, TreeNodeUI } from "./TreeNodeUI";
import { TreeViewModes } from "./TreeViewModes";

export interface ITreeViewerProps extends BaseProps {
    cssClass: string;
    nodeChildContainerCss?: string;
    nodeUIComponentType: React.ComponentType<TreeNodeComponentUIProps>;
    onTreeModelChange?: () => void;
    treeMode: TreeViewModes;
    skipEmptyNodesOnDisplayMode?: boolean;
}

@observer export class TreeViewerUI extends BaseComponent<ITreeViewerProps, TreeNodeCollection> {
    constructor(props: ITreeViewerProps) {
        super(props);

        //Bind
        this.getNodeById = this.getNodeById.bind(this);
        this.getNodeByIdReqursive = this.getNodeByIdReqursive.bind(this);
        this.hasSelectedChild = this.hasSelectedChild.bind(this);
        this.selectSingleNode = this.selectSingleNode.bind(this);
        this.ensureSingleNodeSelect = this.ensureSingleNodeSelect.bind(this);
        this.checkUncheckMultiSelectTreeNode = this.checkUncheckMultiSelectTreeNode.bind(this);
        this.multiSelectTreeParentNodeChangeHandler = this.multiSelectTreeParentNodeChangeHandler.bind(this);
        this.treeNodeSelected = this.treeNodeSelected.bind(this);
    }

    render(): JSX.Element {
        if (!this.model || !this.model.items || this.model.items.length == 0) {
            return null;
        }

        return (
            <ul className={this.props.cssClass}>
                {this.model.items.map((node: TreeNode, idx: number) => {
                    return this.props.skipEmptyNodesOnDisplayMode === true && this.props.viewMode == ViewMode.Display && !this.hasSelectedChild(node)
                        ? null
                        : <li key = { idx }>
                            <TreeNodeUI {...this.bind(node, `node-${node.value}`)}
                                nodeUIComponent={this.props.nodeUIComponentType}
                                onNodeSelected={this.treeNodeSelected}
                                childContainerCss={this.props.nodeChildContainerCss}
                                skipEmptyNodesOnDisplayMode={this.props.skipEmptyNodesOnDisplayMode}
                            />
                        </li>
                })}
            </ul>);
    }


    @action private treeNodeSelected(nodeId: string, isSelected: boolean): void {
        if (this.props.treeMode == TreeViewModes.MultiSelectTree) {
            let node = this.getNodeById(nodeId);
            this.checkUncheckMultiSelectTreeNode(node, isSelected);

            let parent = this.getNodeById(node.parentID);
            while (parent) {
                this.multiSelectTreeParentNodeChangeHandler(parent, isSelected);

                parent = parent.parentID ? this.getNodeById(parent.parentID) : undefined;
            }
        }

        if (this.props.treeMode == TreeViewModes.SingleSelectTree) {
            this.selectSingleNode(nodeId);
        }

        if (this.props.treeMode != TreeViewModes.DisplayTree && this.props.onTreeModelChange) {
            this.props.onTreeModelChange();
        }
    }

    //Common helper function code

    private getNodeById(nodeId: string): TreeNode {
        if (!nodeId) return undefined;

        if (this.model && this.model.items && this.model.items.length > 0) {
            for (let i: number = 0; i < this.model.items.length; i++) {
                let curnode = this.model.items[i];
                let searchedNode = this.getNodeByIdReqursive(nodeId, curnode);

                if (searchedNode) return searchedNode;
            }
        }

        return undefined;
    }

    private getNodeByIdReqursive(nodeId: string, node: TreeNode): TreeNode {
        if (node.value === nodeId) return node;

        if (node.children && node.children.length > 0) {
            for (let i: number = 0; i < node.children.length; i++) {
                let curChild = node.children[i];
                let searchedNode = this.getNodeByIdReqursive(nodeId, curChild);

                if (searchedNode) return searchedNode;
            }
        }

        return null;
    }

    private hasSelectedChild(node: TreeNode): boolean {
        if (node.children && node.children.length > 0) {
            for (let i: number = 0; i < node.children.length; i++) {
                let curChild = node.children[i];

                if (curChild.selected)
                    return true;
                else {
                    if (this.hasSelectedChild(curChild)) return true;
                }
            }
        }

        return node.selected;
    }

    //SingleSelectTree helpers

    private selectSingleNode(nodeId: string): void {
        for (let i: number = 0; i < this.model.items.length; i++) {
            this.ensureSingleNodeSelect(nodeId, this.model.items[i]);
        }
    }

    private ensureSingleNodeSelect(nodeId: string, node: TreeNode): void {
        node.selected = node.value == nodeId;

        if (node.children && node.children.length > 0) {
            for (let i: number = 0; i < node.children.length; i++) {
                this.ensureSingleNodeSelect(nodeId, node.children[i]);
            }
        }
    }

    //MultiSelectTree helpers

    private checkUncheckMultiSelectTreeNode(node: TreeNode, check: boolean): void {
        node.selected = check;

        if (check && node.children && node.children.length > 0)
            node.intermediateState = false;

        if (node.children && node.children.length > 0) {
            for (let i: number = 0; i < node.children.length; i++) {
                this.checkUncheckMultiSelectTreeNode(node.children[i], check);
            }
        }
    }

    private multiSelectTreeParentNodeChangeHandler(parent: TreeNode, checked: boolean): void {
        let selectedChild = ArrayHelper.queryable.from(parent.children).count(el => el.selected == true);

        if (checked) {
            parent.selected = selectedChild == parent.children.length;
        } else {
            if (parent.selected == true) {
                parent.selected = false;
            }
        }

        if (selectedChild == parent.children.length) {
            parent.intermediateState = false;
        } else {
            parent.intermediateState = checked === true ? true : this.hasSelectedChild(parent);
        }
    }
}