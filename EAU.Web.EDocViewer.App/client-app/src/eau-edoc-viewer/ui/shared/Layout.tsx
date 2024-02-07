import { AsyncUIProps, BaseProps, withAsyncFrame } from 'cnsys-ui-react';
import { PageTitleUI } from 'eau-core';
import React from 'react';
import { RouteComponentProps, withRouter } from 'react-router';
import { Header } from './Header';

interface LayoutProps extends BaseProps, AsyncUIProps, RouteComponentProps {
    hideSideNav?: boolean;
}

export class LayoutImpl extends React.Component<LayoutProps, any> {

    render() {
        return (
            <>
                <Header />
                <main className="content-wrapper">
                    <div className="page-header-wrapper section-wrapper section-wrapper--margin-top fixed-content-width">
                        <PageTitleUI currentPath={this.props.location.pathname} />
                    </div>
                    <div className="main-wrapper section-wrapper section-wrapper--margins fixed-content-width">
                        {this.props.children}
                    </div>
                </main>
            </>);
    }

    toggleMenuSize() {
        $("#content-wrapper").toggleClass("mini-navbar-left")
    }
}

export const Layout = withAsyncFrame(withRouter(LayoutImpl))