import { AsyncUIProps, BaseProps, withAsyncFrame } from "cnsys-ui-react";
import { observable } from "mobx";
import { observer } from "mobx-react";
import React from "react";
import { Link } from "react-router-dom";
import { pageRoute } from "../common/PageRoute";
import { EAUBaseComponent } from "./EAUBaseComponent";

export interface BreadcrumpNodesProps extends BaseProps {
    breadcrumbItems: { path: string; text: string; showMainNodeOnly: boolean; isInternal: boolean, disabled?: boolean }[];
}

interface BreadcrumpWrapperProps extends BaseProps, AsyncUIProps {
    currentPath: string;
}

class BreadcrumpNodes extends EAUBaseComponent<BreadcrumpNodesProps, any> {

    render() {
        if (this.props.breadcrumbItems[0].showMainNodeOnly || this.props.breadcrumbItems.length == 1) {
            return <div className="breadcrumbs"></div>;
        }

        var navItems = [];

        for (var i = this.props.breadcrumbItems.length - 1; i > 0; i--) {
            navItems.push(
                this.props.breadcrumbItems[i].disabled
                    ? <li className="breadcrumb-item active" aria-current="page" key={this.props.breadcrumbItems[i].path}>
                        {this.props.breadcrumbItems[i].text}
                    </li>
                    : <li className="breadcrumb-item" key={this.props.breadcrumbItems[i].path}>
                        {
                            this.props.breadcrumbItems[i].isInternal
                                ? <Link to={this.props.breadcrumbItems[i].path}>{this.props.breadcrumbItems[i].text}</Link>
                                : <a href={this.props.breadcrumbItems[i].path}>{}</a>
                        }
                    </li>);
        }
        navItems.push(
            <li className="breadcrumb-item active" aria-current="page" key={this.props.breadcrumbItems[i].path}>
                {this.props.breadcrumbItems[0].text}
            </li>);

        return (
            <div className="breadcrumbs">

                <nav aria-label={this.getResource("GL_CURRENT_PAGE_PATH_I")}>
                        <ol className="breadcrumb">
                            {navItems}
                        </ol>
                    </nav>            
            </div>);
    }
}

@observer class BreadcrumpImpl extends EAUBaseComponent<BreadcrumpWrapperProps, any>{

    @observable breadcrumbItems: { path: string; text: string; showMainNodeOnly: boolean; isInternal: boolean, disabled?: boolean }[] = [];
    private breadcrumbNodesCount = 0;

    constructor(props: BreadcrumpWrapperProps) {
        super(props);

        this.componentDidUpdate = this.componentDidUpdate.bind(this);

        this.props.registerAsyncOperation(pageRoute.getPageRouteItems(this.props.currentPath).bind(this).then(bn => {
            this.breadcrumbNodesCount = pageRoute.pageRouteNodesCount;
            this.breadcrumbItems = bn.some(b => b.skipBreadcrumb == true) ? [] : bn;
        }));
    }

    componentDidUpdate(prevProps: BreadcrumpWrapperProps, prevState: BreadcrumpWrapperProps): void {
        if (prevProps.currentPath != this.props.currentPath ||
            this.breadcrumbNodesCount != pageRoute.pageRouteNodesCount) {

            this.props.registerAsyncOperation(pageRoute.getPageRouteItems(this.props.currentPath).bind(this).then(bn => {
                this.breadcrumbNodesCount = pageRoute.pageRouteNodesCount;
                this.breadcrumbItems = bn.some(b => b.skipBreadcrumb == true) ? [] : bn;
            }));
        }
    }

    render() {

        if (this.breadcrumbItems && this.breadcrumbItems.length > 0)
            return <BreadcrumpNodes breadcrumbItems={this.breadcrumbItems} />

        return null;
    }
}

export const BreadcrumbUI = withAsyncFrame(BreadcrumpImpl);

