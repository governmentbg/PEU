import { moduleContext, ObjectHelper, UIHelper, UserInfo } from 'cnsys-core';
import { appConfig, BreadcrumbUI, Constants as CoreConstants, eauAuthenticationService, PageTitleUI, resourceManager } from 'eau-core';
import * as JsCookies from "js-cookie";
import { action, observable } from "mobx";
import { observer } from "mobx-react";
import React from 'react';
import IdleTimer from 'react-idle-timer';
import { matchPath, RouteComponentProps, withRouter } from 'react-router';
import { Link } from 'react-router-dom';
import { Constants } from '../../Constants';
import Footer from './Footer';
import { Header } from './Header';

interface LayoutProps extends RouteComponentProps {
    hideSideNav?: boolean;
    user: UserInfo;
}

@observer export class LayoutImpl extends React.Component<LayoutProps, any> {

    private keepSessionAliveTimeout: any;
    private userInactivityTimeout = appConfig.userInactivityTimeout ? appConfig.userInactivityTimeout : 10000; //Ако стойността не е сетната слагаме дефолтна 10 мин.

    @observable errors: string[];
    @observable warnings: string[];

    constructor(props: LayoutProps) {
        super(props);

        this.onIdle = this.onIdle.bind(this);

        //Init
        this.errors = [];
        this.warnings = [];
        this.handleScroll = this.handleScroll.bind(this);
        this.initUserInLocalStorage();
    }

    componentDidMount() {
        window.addEventListener('scroll', this.handleScroll);
        window.addEventListener("storage", this.onLocalStorageChange)

        if (this.props.user)
            this.keepSessionAlivePeriodically();
    }

    componentWillUnmount() {
        window.removeEventListener('scroll', this.handleScroll);
        if (this.props.user || this.keepSessionAliveTimeout)
            clearTimeout(this.keepSessionAliveTimeout); //Зачистваме поддържането на сесията жива.

        window.removeEventListener("storage", this.onLocalStorageChange)
    }

    render() {
        return (
            <>
                {this.props.user && <IdleTimer
                    element={document}
                    onIdle={this.onIdle}
                    onAction={this.onUserAction}
                    throttle={2000}
                    timeout={this.userInactivityTimeout} />}

                <Header user={this.props.user} />
                <main className="content-wrapper">
                    <div className="page-header-wrapper section-wrapper section-wrapper--margin-top fixed-content-width">
                        <BreadcrumbUI currentPath={this.props.location.pathname} />
                        {
                            //Ако адреса, на който сме не е на заявление, показваме PageTitle.
                            this.showPageTitle() && <PageTitleUI currentPath={this.props.location.pathname} />
                        }
                        {
                            this.errors && this.errors.length > 0 && <div key="errors" className="alert alert-danger">
                                <ul className="list-unstyled">
                                    {this.errors.map((err, idx) => { return <li key={idx}>{err}</li> })}
                                </ul>
                            </div>
                        }
                        {
                            this.warnings && this.warnings.length > 0 && <div key="warnings" className="alert alert-warning">
                                <ul className="list-unstyled">
                                    {this.warnings.map((err, idx) => { return <li key={idx}>{err}</li> })}
                                </ul>
                            </div>
                        }
                    </div>
                    <div className="main-wrapper section-wrapper section-wrapper--margins fixed-content-width">
                        {this.props.children}
                    </div>
                </main>
                <div id="topcontrol" className="scroll-to-top" onClick={this.scrollToTop} title={resourceManager.getResourceByKey("GL_GOTOTOP_I")} style={{ position: "fixed", bottom: "15px", right: "15px", display: "none" }}  ><i className="ui-icon ui-icon-scroll-to-top"></i></div>
                <Footer />
                {this.renderCookieSection()}
            </>);
    }

    private renderCookieSection() {
        if (ObjectHelper.isStringNullOrEmpty(UIHelper.getCookie('cookiePrivacyAccept'))) {
            return <aside id="cookieInfo" className="cookie-info">
                <div className="fixed-content-width">
                    <h5>{resourceManager.getResourceByKey('GL_USING_COOKIES_L')}</h5>
                    <p>
                        {resourceManager.getResourceByKey('GL_USING_COOKIES_INFO_L')} <Link to={Constants.PATHS.Cookies} className="text-white"><b>{resourceManager.getResourceByKey('GL_COOKIES_L')}</b></Link>
                    </p>
                    <div className="button-bar">
                        <div className="right-side">
                            <button type="button" className="btn btn-outline-light" id="COOKIE_ACCEPT" onClick={this.onCookieAccept}>{resourceManager.getResourceByKey('GL_ACCEPT_COOKIE_L')}</button>
                        </div>
                        <div className="left-side">
                        </div>
                    </div>
                </div>
            </aside>
        }

        return null;
    }

    private onCookieAccept() {
        UIHelper.setCookiePrivacyAccept(appConfig.commonCookieDomain);
        $('#cookieInfo').addClass('d-none');
    }

    private scrollToTop(): void {
        const scrollToTop = () => {
            const c = document.documentElement.scrollTop || document.body.scrollTop;
            if (c > 0) {
                window.requestAnimationFrame(scrollToTop);
                window.scrollTo(0, c - c / 8);
            }
        };
        scrollToTop();
    }

    private handleScroll(): void {
        $(window).scroll(function () {
            $("main").each(function () {
                if ($(window).scrollTop() > $(this).offset().top - 100) {
                    $("#topcontrol").fadeIn("slow", function () {
                        $("#topcontrol").css("display", "block");
                    });
                }
                else {
                    $("#topcontrol").fadeOut("slow", function () {
                        $("#topcontrol").css("display", "none");
                    });
                }
            });
        })
    }

    private onIdle() {

        let lastActiveTime: string = JsCookies.get("usr_active_timestamp");
        if (lastActiveTime == null || lastActiveTime == "")
            return;

        let lastActiveTimestampN: number = parseInt(lastActiveTime);
        let timeout: number = this.userInactivityTimeout;

        if ((Date.now() - lastActiveTimestampN) < timeout)
            return;

        eauAuthenticationService.userLogout(document.location.origin + (appConfig.baseUrlName + Constants.PATHS.SessionTimeout).replace("//", "/"));
    }

    private onUserAction(e: any) {
        JsCookies.set("usr_active_timestamp", Date.now().toString(), { path: "/", secure: true });
    }

    @action private keepSessionAlivePeriodically() {
        let that = this;

        that.errors = [];
        that.warnings = [];

        //keepSessionAliveTimeout поддържането на сесията жива.
        this.keepSessionAliveTimeout = setTimeout(() => {

            eauAuthenticationService.keepSessionAlive().then(() => {
                that.keepSessionAlivePeriodically();
            }).catch((ex: any): any => {

                if (ex.httpStatusCode == 429) {
                    that.warnings.push(moduleContext.resourceManager.getResourceByKey('GL_TOO_MANY_REQUESTS_E'))
                }
            })

            //Поддържаме сесията жива чрез заявки през 1/3 от времето за изтичане на сесията.
        }, Math.floor(this.userInactivityTimeout / 3));
    }

    private showPageTitle(): boolean {
        if (matchPath(this.props.location.pathname, { path: CoreConstants.PATHS.APPLICATION_PROCESSES })
            || matchPath(this.props.location.pathname, { path: CoreConstants.PATHS.APPLICATION_PROCESSES_START })
            || matchPath(this.props.location.pathname, { path: CoreConstants.PATHS.ServiceInstanceDocumentPreview })
            || matchPath(this.props.location.pathname, { path: CoreConstants.PATHS.NotAcknowledgedMessage })
            //|| matchPath(this.props.location.pathname, { path: Constants.PATHS.Contacts })
            //|| matchPath(this.props.location.pathname, { path: Constants.PATHS.AccessibilityPolicy })
            //|| matchPath(this.props.location.pathname, { path: Constants.PATHS.SecurityPolicy })
            //|| matchPath(this.props.location.pathname, { path: Constants.PATHS.TermOfUse })
            //|| matchPath(this.props.location.pathname, { path: Constants.PATHS.VideoLessons })
            //|| matchPath(this.props.location.pathname, { path: Constants.PATHS.Cookies })
            //|| matchPath(this.props.location.pathname, { path: Constants.PATHS.PrivacyPolicy })
            //|| matchPath(this.props.location.pathname, { path: Constants.PATHS.News })
        )
            return false;

        return true;
    }

    private initUserInLocalStorage() {
        if (this.props.user && window.localStorage.getItem("active-user") != "on")
            window.localStorage.setItem("active-user", "on")
    }

    onLocalStorageChange(e: any) {
        if (e.key === 'active-user') {
            window.location.reload();
        }
    }
}

export const Layout = withRouter(LayoutImpl)