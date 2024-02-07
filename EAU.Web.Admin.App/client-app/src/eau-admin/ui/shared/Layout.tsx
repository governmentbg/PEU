import { UserInfo } from 'cnsys-core';
import { appConfig, BreadcrumbUI, eauAuthenticationService } from 'eau-core';
import * as JsCookies from "js-cookie";
import React, { useEffect } from 'react';
import IdleTimer from 'react-idle-timer';
import { RouteComponentProps, withRouter } from 'react-router';
import { Constants } from '../../Constants';
import Footer from './Footer';
import { Header } from './Header';
import { SideNav } from './SideNav';
import { PageTitleAdminUI } from '../common/PageTitleAdminUI';

interface LayoutProps extends RouteComponentProps {
    hideSideNav?: boolean;
    user: UserInfo;
}

const LayoutImpl: React.FC<LayoutProps> = (props) => {

    var keepSessionAliveTimeout: any = undefined;
    const userInactivityTimeout = appConfig.userInactivityTimeout ? appConfig.userInactivityTimeout : 10000; //Ако стойността не е сетната слагаме дефолтна 10 мин.

    useEffect(() => {

        if (props.user)
            keepSessionAlivePeriodically();

        return () => {

            if (props.user || keepSessionAliveTimeout) {
                //Зачистваме поддържането на сесията жива.
                clearTimeout(keepSessionAliveTimeout);
            }
        }
    }, [])

    return <>
        {props.user && <IdleTimer
            element={document}
            onIdle={onIdle}
            onAction={onUserAction}
            throttle={2000}
            timeout={userInactivityTimeout} />}

        <Header />
        <div id="content-wrapper" className="content-wrapper">
            <div className="main-wrapper">
                {props.hideSideNav ? null : <SideNav />}
                <div className="page-wrapper">
                    <div className="container-fluid">
                        <BreadcrumbUI currentPath={props.location.pathname} />
                        <PageTitleAdminUI currentPath={props.location.pathname} />
                        {props.children}
                    </div>
                </div>
            </div>
        </div>
        <Footer />
    </>

    function onIdle() {

        let timeout: number = userInactivityTimeout;

        let lastActiveTime: string = JsCookies.get("usr_active_timestamp");

        if (lastActiveTime == null || lastActiveTime == "")
            return Promise.resolve();

        let lastActiveTimestampN: number = parseInt(lastActiveTime);

        if ((Date.now() - lastActiveTimestampN) < timeout)
            return Promise.resolve();

        eauAuthenticationService.userLogout(document.location.origin + (appConfig.baseUrlName + Constants.PATHS.SessionTimeout).replace("//", "/"));
    }

    function onUserAction(e: any) {
        JsCookies.set("usr_active_timestamp", Date.now().toString(), { path: "/", secure: true });
    }

    function keepSessionAlivePeriodically() {

        //keepSessionAliveTimeout поддържането на сесията жива.
        keepSessionAliveTimeout = setTimeout(() => {

            eauAuthenticationService.keepSessionAlive().then(() => {
                keepSessionAlivePeriodically();
            })

            //Поддържаме сесията жива чрез заявки през 1/3 от времето за изтичане на сесията.
        }, Math.floor(userInactivityTimeout / 3));
    }
}

export const Layout = withRouter(LayoutImpl)