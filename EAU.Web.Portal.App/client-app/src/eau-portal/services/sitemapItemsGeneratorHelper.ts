//import { SitemapTreeNode } from 'cnsys-ui-react'
//import { sitemap } from "../UI";
//import { moduleContext } from "../ModuleContext";

export interface SiteMapPreviewItem {
    pathPattern: string;
    text: string;
    children?: SiteMapPreviewItem[];
}

export namespace SitemapItemsGeneratorHelper {
    export function getSiteMapPreviewHierarchy(): Promise<SiteMapPreviewItem[]> {
        var routePromises: Promise<SiteMapPreviewItem>[] = [];

        //for (let route of sitemap.routes) {

        //    if (route.visibleInPreview != false) {
        //        routePromises.push(getPreviewNode(route));

        //    }
        //}

        return Promise.all(routePromises);
    }
}