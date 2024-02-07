import { action } from "mobx";
import { observer } from "mobx-react";
import * as React from "react";
import { BaseComponent, BaseProps, ViewMode } from '../BaseComponent';
import { TreeNode } from "./TreeNode";

export interface TreeNodeUIProps extends BaseProps {
    nodeUIComponent: React.ComponentType<TreeNodeComponentUIProps>;
    onNodeSelected?: (nodeId: string, isSelected: boolean) => void;
    childContainerCss?: string;
    skipEmptyNodesOnDisplayMode?: boolean;
}

export interface TreeNodeComponentUIProps extends BaseProps {
    onExtendCallback?: (extend: boolean) => void;
    onNodeSelected?: (nodeId: string, isSelected: boolean) => void;
}

@observer export class TreeNodeUI extends BaseComponent<TreeNodeUIProps, TreeNode> {
    constructor(props: TreeNodeUIProps) {
        super(props);

        //Bind
        this.onExtend = this.onExtend.bind(this);
    }

    render(): JSX.Element {
        return (
            <>
                <this.props.nodeUIComponent {...this.bind(this.model, 'node')} onExtendCallback={this.onExtend} onNodeSelected={this.props.onNodeSelected} />
                {this.model.children && this.model.children.length > 0 ?
                    <div id={`collapsible-content-${this.model.value}`} className={this.props.childContainerCss} style={this.model.isExtended == true ? {} : { display: 'none' }}>
                        <ul>
                            {this.model.children.map((node: TreeNode, idx: number) => {
                                return this.props.skipEmptyNodesOnDisplayMode === true && this.props.viewMode == ViewMode.Display && node.intermediateState !== true && node.selected !== true
                                    ? null
                                    : (<li key={idx}>
                                        <TreeNodeUI {...this.bind(node, `node-${node.value}`)}
                                            nodeUIComponent={this.props.nodeUIComponent}
                                            onNodeSelected={this.props.onNodeSelected}
                                            childContainerCss={this.props.childContainerCss}
                                            skipEmptyNodesOnDisplayMode={this.props.skipEmptyNodesOnDisplayMode}
                                        />
                                    </li>);
                            })}
                        </ul>
                    </div> : null}
            </>);
    }

    @action private onExtend(extend: boolean): void {
        this.model.isExtended = extend;
    }
}