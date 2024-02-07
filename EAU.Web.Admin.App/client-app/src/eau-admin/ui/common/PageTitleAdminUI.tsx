import { AsyncUIProps, BaseProps, withAsyncFrame } from "cnsys-ui-react";
import { observable } from "mobx";
import { observer } from "mobx-react";
import React from "react";
import { EAUBaseComponent, pageRoute } from "eau-core";

interface PageTitleAdminProps extends BaseProps, AsyncUIProps {
    currentPath: string;
}

@observer class PageTitleAdminUIImpl extends EAUBaseComponent<PageTitleAdminProps, any>{

    @observable pageRouteItems: { path: string; text: string; showMainNodeOnly: boolean; isInternal: boolean, disabled?: boolean }[] = [];

    constructor(props: PageTitleAdminProps) {
        super(props);

        this.componentDidUpdate = this.componentDidUpdate.bind(this);

        this.props.registerAsyncOperation(pageRoute.getPageRouteItems(this.props.currentPath).bind(this).then(bn => {
            this.pageRouteItems = bn;
        }));
    }

    componentDidUpdate(prevProps: PageTitleAdminProps, prevState: PageTitleAdminProps): void {
        if (prevProps.currentPath != this.props.currentPath)
            this.props.registerAsyncOperation(pageRoute.getPageRouteItems(this.props.currentPath).bind(this).then(bn => this.pageRouteItems = bn));
    }

    render() {

        if (this.pageRouteItems && this.pageRouteItems.length > 0) {
            return (
                <div className="page-title">
                    <h2>{this.pageRouteItems[0].text}</h2>
                </div>);
        } else {
            return null;
        }
    }
}

export const PageTitleAdminUI = withAsyncFrame(PageTitleAdminUIImpl);