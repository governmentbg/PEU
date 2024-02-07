import "react-app-polyfill/ie11";
import 'react-app-polyfill/stable';
import 'assets/css/custom.css';
import 'assets/css/style.css';
import { appConfig } from 'eau-core';
import { Constants, EDocViewUI, Layout, EDocLandingPageUI } from 'eau-edoc-viewer';
import React from 'react';
import 'react-datetime/css/react-datetime.css';
import ReactDOM from 'react-dom';
import { BrowserRouter, Redirect, Route, Switch } from 'react-router-dom';
import { ApplicationBootstrapper } from './ApplicationBootstrapper';
import './globals';
function renderApp() {
    ReactDOM.render(<div id="loader" className="loader-overlay load"></div>, document.getElementById("react-app"));

    ApplicationBootstrapper.run().then(() => {
        let baseUrl = document.getElementsByTagName('base')[0].getAttribute('href')!;
        let reactBaseUrl = `${baseUrl}${(appConfig.clientLanguage == 'bg' ? '' : appConfig.clientLanguage + '/')}`;

        ReactDOM.render(
            <BrowserRouter basename={reactBaseUrl}>
                <Layout>
                    <Switch>
                        <Redirect exact from={Constants.PATHS.HOME} to={Constants.PATHS.DOCUMENTS} />
                        <Route key={1} exact path={Constants.PATHS.HOME} component={EDocLandingPageUI} />
                        <Route key={2} path={Constants.PATHS.DOCUMENTS} component={EDocLandingPageUI} />
                        <Route key={3} path={Constants.PATHS.DOCUMENT_PROCESSES} component={EDocViewUI} />
                    </Switch>
                </Layout>
            </BrowserRouter>, document.getElementById('react-app')
        );
    });
}
renderApp();