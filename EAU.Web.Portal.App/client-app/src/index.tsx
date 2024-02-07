import 'react-app-polyfill/stable';
import 'react-datetime/css/react-datetime.css'
import 'assets/css/custom.css';
import 'assets/css/style.css';
import './globals';
import { appConfig, Constants as CoreConstants, TestSignPageUI, eauAuthenticationService } from 'eau-core';
import { Constants, Layout, MyEServicesUI, MyEPaymentsUI, ObligationsUI, MyEServiceInstanceUI, SiteMapUI, CmsUI, SearchsUI, ServicesUI, UsersUI, SessionTimeoutUI, TooManyRequestsUI, UnauthenticatedPageUI, PageNotFoundUI, KATObligationsUI } from 'eau-portal';
import { ApplicationProcessUI, PreviewDocumentProcessUI, ApplicationProcessStartUI } from 'eau-services-document-processes';
import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter, Redirect, Route, Switch, useLocation } from 'react-router-dom';
import { ApplicationBootstrapper } from './ApplicationBootstrapper';

const App = ({ user }) => {

    const { pathname } = useLocation();

    const authorizedPages: { path: any, component: any }[] = [
        { path: CoreConstants.PATHS.PREVIEW_DOCUMENT_PROCESS_BY_FILE, component: PreviewDocumentProcessUI },
        { path: CoreConstants.PATHS.PREVIEW_DOCUMENT_PROCESS_BY_DOC_URI, component: PreviewDocumentProcessUI },
        { path: CoreConstants.PATHS.ServiceInstanceDocumentPreview, component: PreviewDocumentProcessUI },
        { path: CoreConstants.PATHS.ServiceInstance, component: MyEServiceInstanceUI },
        { path: CoreConstants.PATHS.ServiceInstances, component: MyEServicesUI },
        { path: Constants.PATHS.MyEPayments, component: MyEPaymentsUI },
        { path: CoreConstants.PATHS.NotAcknowledgedMessage, component: PreviewDocumentProcessUI },
    ]

    return <Layout user={user}>
        <Switch>
            { /*Статичните страници не могат да бъдат намерени ако завършват с / за това използваме useLocation и прихващаме всички routes, които завършват с / и редиректваме 
             към съшата страница бес / */}
            <Redirect key={0} from="/:url*(/+)" to={pathname.slice(0, -1)} />
            <Redirect key={1} exact from={Constants.PATHS.Home} to={Constants.PATHS.Services} />
            <Route key={2} exact path={Constants.PATHS.Services} component={ServicesUI} />
            <Route key={3} path={Constants.PATHS.Contacts} component={CmsUI} />
            <Route key={4} path={CoreConstants.PATHS.APPLICATION_PROCESSES_START} component={ApplicationProcessStartUI} exact={true} />
            <Route key={5} path={CoreConstants.PATHS.APPLICATION_PROCESSES} component={ApplicationProcessUI} />
            <Route key={6} path={Constants.PATHS.TestSign} component={TestSignPageUI} />
            <Route key={7} path={Constants.PATHS.AccessibilityPolicy} component={CmsUI} />
            <Route key={8} path={Constants.PATHS.SecurityPolicy} component={CmsUI} />
            <Route key={9} path={Constants.PATHS.TermOfUse} component={CmsUI} />
            <Route key={10} path={Constants.PATHS.Cookies} component={CmsUI} />
            <Route key={11} path={Constants.PATHS.PrivacyPolicy} component={CmsUI} />
            <Route key={12} path={Constants.PATHS.News} component={CmsUI} />
            <Route key={13} path={Constants.PATHS.Users} component={UsersUI} />
            <Route key={14} path={Constants.PATHS.Obligations} component={ObligationsUI} />
            <Route key={15} path={Constants.PATHS.KATObligations} component={KATObligationsUI} />
            <Route key={16} path={Constants.PATHS.SiteMap} component={SiteMapUI} />
            <Route key={17} path={Constants.PATHS.VideoLessons} component={CmsUI} />
            <Route key={18} path={Constants.PATHS.AccessFunction} component={CmsUI} />
            <Route key={19} path={CoreConstants.PATHS.DocumentPreview} component={PreviewDocumentProcessUI} />
            <Route key={20} path={Constants.PATHS.TooManyRequestsUI} component={TooManyRequestsUI} />
            <Route key={21} path={Constants.PATHS.Search} component={SearchsUI} />
            {
                authorizedPages.map((route, index) => <Route key={`authorized-${index}`} path={route.path} component={user
                    ? route.component
                    : UnauthenticatedPageUI} />)
            }
            {
                user
                    ? <Redirect key={999} from={Constants.PATHS.SessionTimeout} to={Constants.PATHS.Home} />
                    : <Route key={998} path={Constants.PATHS.SessionTimeout} component={SessionTimeoutUI} />
            }
            <Route key={997} component={PageNotFoundUI} />
        </Switch>
    </Layout>
}

function renderApp() {
    ReactDOM.render(<div id="loader" className="loader-overlay load"></div>, document.getElementById("react-app"));

    ApplicationBootstrapper.run().then(() => {
        const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href')!;
        const reactBaseUrl = `${baseUrl}${(appConfig.clientLanguage == 'bg' ? '' : appConfig.clientLanguage + '/')}`;

        eauAuthenticationService.getCurrentUser().then(user => {
            ReactDOM.render(<BrowserRouter basename={reactBaseUrl}>
                <App user={user} />
            </BrowserRouter>, document.getElementById('react-app')
            );
        })
    });
}
renderApp();