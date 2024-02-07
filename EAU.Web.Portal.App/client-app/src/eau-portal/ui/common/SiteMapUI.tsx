import {
    AsyncUIProps,
    BaseRouteParams,
    BaseRouteProps,
    BaseRoutePropsExt,
    withAsyncFrame
} from "cnsys-ui-react";
import * as React from 'react';
import { observer } from "mobx-react";
import { EAUBaseComponent, pageRoute, appConfig, Constants as coreConstants } from "eau-core";
import { withRouter } from "react-router";
import { Link } from "react-router-dom";
import { ObjectHelper, ArrayHelper, UIHelper } from "cnsys-core";
import { action, observable } from "mobx";
import { Constants } from '../../Constants';


interface SiteMapUIRouteProps extends BaseRouteParams {
}

interface SiteMapUIProps extends BaseRouteProps<SiteMapUIRouteProps>, AsyncUIProps, BaseRoutePropsExt {

}

@observer
class SiteMapImplUI extends EAUBaseComponent<SiteMapUIProps, any> {

    @observable private pathTrees = [];

    constructor(props: SiteMapUIProps) {
        super(props);

        this.pathTrees = [];
        this.arrangeIntoTrees();
    }

    render(): JSX.Element {
        return <div className="page-wrapper" id="ARTICLE-CONTENT">
            <div className="site-map">
                <ul>
                    {this.pathTrees.length > 0 && this.pathTrees.map((currentTree) => {
                        return currentTree;
                    })}
                </ul>
            </div>
        </div>
    }

    addToPathTree(currentRoute) {
        this.pathTrees.push(this.getCurrentTree(currentRoute));
    }

    getCurrentTree(currentRoute) {

        let elementId = ObjectHelper.newGuid();
        let linkId = ObjectHelper.newGuid();
        let listId = ObjectHelper.newGuid();

        if (currentRoute.children && currentRoute.children.length > 0) {
            return (
                <li key={elementId}>
                    {currentRoute.disabled === true ?
                        <React.Fragment key={linkId}>{currentRoute.name}</React.Fragment>
                        :
                        <Link key={linkId} to={currentRoute.path}>{currentRoute.name}</Link>}
                    {currentRoute.showMainNodeOnly === true ?
                        null
                        :
                        <ul key={listId}>
                            {currentRoute.children.map(child => this.getCurrentTree(child))}
                        </ul>}
                </li>);
        } else {
            switch (currentRoute.path) {
                case Constants.PATHS.Help:
                    return (<li key={elementId}><a href={appConfig.webHelpUrl} target="_blank">{currentRoute.name}</a></li>);
                case Constants.PATHS.TextVersion:
                    return (<li key={elementId}><a href='#' onClick={(e) => { e.preventDefault(); UIHelper.HideSiteCss(); }}>{currentRoute.name}</a></li>);
                case Constants.PATHS.ForgottenPassword:
                case Constants.PATHS.ConfirmUserRegistration:
                case Constants.PATHS.CancelUserRegistration:
                    return null;
                default:
                    return (<li key={elementId}><Link to={currentRoute.path}>{currentRoute.name}</Link></li>)
            }
        }
    }

    @action
    async arrangeIntoTrees() {
        let correctOrder: string[] = [Constants.PATHS.Services, Constants.PATHS.Contacts, Constants.PATHS.Help, Constants.PATHS.Search, Constants.PATHS.AccessFunction, Constants.PATHS.TextVersion, Constants.PATHS.Users, Constants.PATHS.MyEServices, Constants.PATHS.MyEPayments, Constants.PATHS.News, Constants.PATHS.TestSign, coreConstants.PATHS.DocumentPreview, Constants.PATHS.PrivacyPolicy, Constants.PATHS.AccessibilityPolicy, Constants.PATHS.SecurityPolicy, Constants.PATHS.TermOfUse, Constants.PATHS.Cookies, Constants.PATHS.VideoLessons];
        let pathsCorrectOrder: { name: string, path: string, children: [], disabled: boolean, showMainNodeOnly: boolean }[] = [];
        let paths = [];

        for (let routeNode of pageRoute.getAllPageRouteNodes) {

            let currentPath = await pageRoute.getPageRouteItems(routeNode.pathPattern);

            paths.push(currentPath);
        }

        for (let i = 0; i < paths.length; i++) {
            let currentRoutePath = paths[i];

            let rootRoute = currentRoutePath[currentRoutePath.length - 1];

            for (let j = currentRoutePath.length - 1; j >= 0; j--) {

                let currentRoot = pathsCorrectOrder.filter(p => p.name == rootRoute.text)[0];

                if (!pathsCorrectOrder.map(p => p.name).includes(rootRoute.text)) {
                    pathsCorrectOrder.push({ name: currentRoutePath[j].text, path: currentRoutePath[j].path, children: [], disabled: currentRoutePath[j].disabled, showMainNodeOnly: currentRoutePath[j].showMainNodeOnly })

                } else if (currentRoutePath[j].name !== rootRoute.text && !this.containsNode(currentRoot, currentRoutePath[j])) {

                    let parentNode = this.findNodeByName(currentRoot, currentRoutePath[j + 1]);

                    parentNode.children.push({ name: currentRoutePath[j].text, path: currentRoutePath[j].path, children: [] });
                }
            }
        }

        for (let k = 0; k < correctOrder.length; k++) {
            let currentPath = ArrayHelper.queryable.from(pathsCorrectOrder).singleOrDefault(el => el.path === correctOrder[k]);

            if (!ObjectHelper.isNullOrUndefined(currentPath)) {
                this.addToPathTree(currentPath);
            }
        }
    }

    private findNodeByName(currentNode: any, nodeToFind) {
        let resultNode;

        if (ObjectHelper.isNullOrUndefined(currentNode) || ObjectHelper.isNullOrUndefined(nodeToFind)) {
            resultNode = undefined;
        }
        else if (currentNode.name == nodeToFind.text) {
            resultNode = currentNode;
        }
        else if (currentNode.children && currentNode.children.length > 0) {
            for (const currentNodeChild of currentNode.children) {
                resultNode = this.findNodeByName(currentNodeChild, nodeToFind)
            }
        }

        return resultNode;
    }

    private containsNode(currentNode: any, currentRoutePathElement: any): boolean {

        let result = false;

        if (ObjectHelper.isNullOrUndefined(currentNode) || ObjectHelper.isNullOrUndefined(currentRoutePathElement)) {
            result = false;
        }
        else if (currentNode.name == currentRoutePathElement.text) {
            result = true;
        } else if (currentNode.children && currentNode.children.length > 0) {
            for (const currentNodeChild of currentNode.children) {
                result = this.containsNode(currentNodeChild, currentRoutePathElement)
            }
        }

        return result;
    }
}

export const SiteMapUI = withRouter(withAsyncFrame(SiteMapImplUI));