import { AsyncUIProps, BaseProps, withAsyncFrame } from "cnsys-ui-react";
import { observable } from "mobx";
import { observer } from "mobx-react";
import React from "react";
import { pageRoute } from "../common/PageRoute";
import { EAUBaseComponent } from "./EAUBaseComponent";

interface PageTitleProps extends BaseProps, AsyncUIProps {
    currentPath: string;
}

@observer class PageTitleUIImpl extends EAUBaseComponent<PageTitleProps, any>{

    @observable pageRouteItems: { path: string; text: string; showMainNodeOnly: boolean; isInternal: boolean, disabled?: boolean }[] = [];

    constructor(props: PageTitleProps) {
        super(props);

        this.componentDidUpdate = this.componentDidUpdate.bind(this);

        this.props.registerAsyncOperation(pageRoute.getPageRouteItems(this.props.currentPath).bind(this).then(bn => {
            this.pageRouteItems = bn;
        }));
    }

    componentDidUpdate(prevProps: PageTitleProps, prevState: PageTitleProps): void {
        if (prevProps.currentPath != this.props.currentPath) {
            this.props.registerAsyncOperation(pageRoute.getPageRouteItems(this.props.currentPath).bind(this).then(bn => this.pageRouteItems = bn));
        }
    }

    render() {

        if (this.pageRouteItems && this.pageRouteItems.length > 0) {
            return (
                <div className="page-header" id="PAGE-CONTENT">
                    <div className="row">
                        <div className="col">
                            <h1 className="page-title">{this.pageRouteItems[0].text}</h1>
                        </div>
                    </div>
                </div>);
        } else {
            return null;
        }
    }
}

export const PageTitleUI = withAsyncFrame(PageTitleUIImpl);