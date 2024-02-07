export interface SitemapTree {
    nodes: SitemapTreeNode[]
}

export interface SitemapTreeNode {
    path: string;
    component?: any;
    indexRoute?: any;
    titleKey?: string;
    titleResolver?: (params?: any) => Promise<string>;
    roles?: string[];
    title?: any;
    childrenGenerator?: () => Promise<SitemapTreeNode[]>;
    visibleInPreview?: boolean;
    children?: SitemapTreeNode[];
    onEnter?: (nextState: any, replace: any, callback?: Function) => any;
    requireAuthentication?: boolean;
}

export class Sitemap {
    private tree: SitemapTree;

    constructor(tree: SitemapTree) {
        this.tree = tree;
    }

    get visible(): any {
        return true;
    }

    get routes(): SitemapTreeNode[] {
        return this.tree.nodes;
    }
}