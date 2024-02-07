import { action, computed, observable } from "mobx";

export interface IPageRouteNode {
    pathPattern: string;
    text: ((pathParams: any) => Promise<string>) | string;
    showMainNodeOnly?: boolean
    pathRegEx?: string
    pathParamsNames?: string[],
    disabled?: boolean;
    skipBreadcrumb?: boolean;
    skipBreadcrumbItem?: boolean;
}

class PageRoute {
    @observable private pageRouteNodes: IPageRouteNode[] = [];
    rootItems: { path: string; text: string; isInternal: boolean }[] = []

    get getAllPageRouteNodes(): IPageRouteNode[] {
        let nodes: IPageRouteNode[] = this.pageRouteNodes && this.pageRouteNodes.length > 0 ? this.pageRouteNodes : [];

        for (var i = 0; i < this.rootItems.length; i++) {
            nodes.push({
                text: this.rootItems[i].text,
                pathPattern: this.rootItems[i].path
            })
        }

        return nodes;
    }

    @computed get pageRouteNodesCount(): number {
        return this.pageRouteNodes.length;
    }

    @action addPageRouteNodes(nodes: IPageRouteNode[]) {
        if (nodes) {
            for (var node of nodes) {
                this.pageRouteNodes.push(node);
            }
        }
    }

    async getPageRouteItems(currentPath: string): Promise<{ path: string; text: string; showMainNodeOnly: boolean; isInternal: boolean; disabled?: boolean; skipBreadcrumb?: boolean }[]> {
        var nodes = this.getPageRouteNodes(currentPath);
        var pageRouteItems: { path: string; text: string; textPromise: Promise<string>; showMainNodeOnly: boolean; isInternal: boolean; disabled?: boolean; skipBreadcrumb?: boolean  }[] = [];

        if (nodes) {

            for (var node of nodes) {
                if (node.skipBreadcrumbItem) continue;

                pageRouteItems.push({
                    path: node.path,
                    text: null,
                    isInternal: true,
                    textPromise: typeof (node.text) == "string" ? Promise.resolve(node.text) : node.text(node.pathParams),
                    showMainNodeOnly: node.showMainNodeOnly,
                    disabled: node.disabled,
                    skipBreadcrumb: node.skipBreadcrumb
                })
            }

            for (var pageRouteItem of pageRouteItems) {
                pageRouteItem.text = await pageRouteItem.textPromise;
            }

            for (var i = this.rootItems.length - 1; i >= 0; i--) {
                pageRouteItems.push({
                    path: this.rootItems[i].path,
                    text: this.rootItems[i].text,
                    isInternal: this.rootItems[i].isInternal,
                    textPromise: null,
                    showMainNodeOnly: null
                })
            }
        }

        return pageRouteItems;
    }

    private getPageRouteNodes(path: string,
        nodes?: { disabled: boolean; path: string; pathPattern: string; text: ((routeParams: any) => Promise<string>) | string, showMainNodeOnly?: boolean, pathParams: any, skipBreadcrumb?: boolean, skipBreadcrumbItem?: boolean }[]): { disabled: boolean; path: string; pathPattern: string; text: ((routeParams: any) => Promise<string>) | string, showMainNodeOnly?: boolean, pathParams: any, skipBreadcrumb?: boolean, skipBreadcrumbItem?: boolean }[] {
        if (!nodes) {
            nodes = [];
        }

        for (var node of this.pageRouteNodes) {

            if (!node.pathRegEx) {
                var dynamicParams = node.pathPattern.match(/\/:[^/]*/gi);

                if (dynamicParams && dynamicParams.length > 0) {
                    node.pathRegEx = node.pathPattern;
                    node.pathParamsNames = [];

                    for (var dynamicParam of dynamicParams) {
                        if (dynamicParam.indexOf("?") > 0) {
                            node.pathRegEx = node.pathRegEx.replace(dynamicParam, "(/[^/]*)?")
                        }
                        else {
                            node.pathRegEx = node.pathRegEx.replace(dynamicParam, "(/[^/]*)")
                        }

                        node.pathParamsNames.push(dynamicParam.substr(2).replace("?", ""));
                    }
                }
                else {
                    node.pathRegEx = node.pathPattern;
                }
            }

            var regex = new RegExp(node.pathRegEx, 'i');
            var phMatch = path.match(regex);

            if (phMatch && phMatch[0] == path) {
                if (nodes.filter(n => n.pathPattern == node.pathPattern).length == 0) {
                    var pathParams: any = null;
                    if (phMatch.length > 1) {
                        pathParams = {};

                        for (var i = 1; i < phMatch.length; i++) {
                            if (phMatch[i] && phMatch[i].replace("/", "").trim().length > 0) {
                                pathParams[node.pathParamsNames[i - 1]] = phMatch[i].replace("/", "").trim();
                            }
                        }
                    }
                    
                    nodes.push({
                        path: path,
                        pathPattern: node.pathPattern,
                        showMainNodeOnly: node.showMainNodeOnly,
                        text: node.text,
                        pathParams: pathParams,
                        disabled: node.disabled,
                        skipBreadcrumb: node.skipBreadcrumb,
                        skipBreadcrumbItem: node.skipBreadcrumbItem
                    });
                }
                break;
            }
        }

        path = path.substr(0, path.lastIndexOf("/"));

        if (path.length > 0) {
            return this.getPageRouteNodes(path, nodes);
        }

        return nodes.length == 0 ? null : nodes;
    }
}

export const pageRoute = new PageRoute();