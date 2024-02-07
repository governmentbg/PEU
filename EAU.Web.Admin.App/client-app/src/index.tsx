import 'react-app-polyfill/stable'; //Трябва да се импортне първо за да сработи полифила за IE
import 'react-datetime/css/react-datetime.css';
import 'assets/css/custom.css';
import 'assets/css/fonts.css';
import 'assets/css/style.css';
import {
    AddUpdateServiceUI, AppParametersUI, AuditUI, Constants, DeclarationFormUI, DeclarationsUI, DocumentTemplateFormUI, DocumentTemplateUI, EpayPaymentsFormUI,
    HomeUI, LabelsUI, LabelTranslationsUI, LanguagesUI, Layout, LimitsUI, PageNotFoundUI, PaymentsObligationsUI, PepPaymentsFormUI, PepPaymentsUI, PredefinedPagesFormUI,
    PredefinedPagesI18nFormUI, PredefinedPagesI18nUI, PredefinedPagesUI, RegistrationUI, ServiceGroupFormUI, ServiceGroupTranslateUI, ServiceGroupUI, ServicesI18nFormUI,
    ServicesI18nUI, ServicesUI, SessionTimeoutUI, UnauthenticatedPageUI, UserProfileFormUI, UsersProfilesUI, NotaryInterestsForPersonOrVehicleUI
} from 'eau-admin';
import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter, Route, Switch } from 'react-router-dom';
import { ApplicationBootstrapper } from './ApplicationBootstrapper';
import { UserInfo } from './cnsys-core';
import { UserPermissions, eauAuthenticationService } from 'eau-core';
import './globals';

function renderApp() {

    ReactDOM.render(<div id="loader" className="loader-overlay load"></div>, document.getElementById("react-app"));

    ApplicationBootstrapper.run().then(() => {

        eauAuthenticationService.getCurrentUser().then(user => {
            let baseUrl = document.getElementsByTagName('base')[0].getAttribute('href')!;

            ReactDOM.render(<BrowserRouter basename={baseUrl}>{getBaseRoutes(user)}</BrowserRouter>, document.getElementById('react-app'));
        })
    });
}

function getBaseRoutes(user: UserInfo): JSX.Element {
    let routes: any[] = [];

    if (user) {
        let hasRoles = user.roles && user.roles.length > 0;

        let hasAdmUsersRole = hasRoles && user.roles.indexOf(UserPermissions[UserPermissions.ADM_USERS]) >= 0
        let hasAdmCMSRole = hasRoles && user.roles.indexOf(UserPermissions[UserPermissions.ADM_CMS]) >= 0
        let hasAdmNomRole = hasRoles && user.roles.indexOf(UserPermissions[UserPermissions.ADM_NOM]) >= 0
        let hasAdmLimitsRole = hasRoles && user.roles.indexOf(UserPermissions[UserPermissions.ADM_PARAM_LIMIT]) >= 0
        let hasAuditRole = hasRoles && user.roles.indexOf(UserPermissions[UserPermissions.ADM_AUDIT]) >= 0

        routes.push(<Route exact key={1} path="/" component={HomeUI} />);

        if (hasAdmUsersRole) {
            //Филтриране на достъпа на администратор с права 'УПРАВЛЕНИЕ НА ПОТРЕБИТЕЛИ'.
            routes.push(<Route key={20} exact path={Constants.PATHS.InternalUsersProfiles} component={UsersProfilesUI} />);
            routes.push(<Route key={21} path={Constants.PATHS.InternalUsersRegister} component={RegistrationUI} />);
            routes.push(<Route key={22} path={Constants.PATHS.InternalUsersProfileEdit} component={UserProfileFormUI} />);
        }

        if (hasAdmCMSRole) {
            //Филтриране на достъпа на администратор с права 'УПРАВЛЕНИЕ НА СЪДЪРЖАНИЕ'.
            routes.push(<Route key={30} exact path={Constants.PATHS.Pages} component={PredefinedPagesUI} />);
            routes.push(<Route key={301} path={Constants.PATHS.PagesEdit} component={PredefinedPagesFormUI} />);
            routes.push(<Route key={31} exact path={Constants.PATHS.DocumentTemplates} component={DocumentTemplateUI} />);
            routes.push(<Route key={311} path={Constants.PATHS.AddDocumentTemplate} component={DocumentTemplateFormUI} />);
            routes.push(<Route key={312} path={Constants.PATHS.EditDocumentTemplate} component={DocumentTemplateFormUI} />);
            routes.push(<Route key={32} exact path={Constants.PATHS.TranslationsPages} component={PredefinedPagesI18nUI} />);
            routes.push(<Route key={321} path={Constants.PATHS.TranslationsPagesTranslate} component={PredefinedPagesI18nFormUI} />);
            routes.push(<Route key={331} path={Constants.PATHS.TranslationsEditService} component={ServicesI18nFormUI} />);
            routes.push(<Route key={33} exact path={Constants.PATHS.TranslationsServices} component={ServicesI18nUI} />);
            routes.push(<Route key={34} path={Constants.PATHS.TranslationsLabels} component={LabelTranslationsUI} />);
            routes.push(<Route key={35} path={Constants.PATHS.TranslationsServiceGroups} component={ServiceGroupTranslateUI} />);
        }

        if (hasAdmNomRole) {
            //Филтриране на достъпа на администратор с права 'УПРАВЛЕНИЕ НА НОМЕНКЛАТУРИ'.
            routes.push(<Route key={40} path={Constants.PATHS.NomLanguages} component={LanguagesUI} />);

            routes.push(<Route key={41} path={Constants.PATHS.NomLabels} component={LabelsUI} />);
            routes.push(<Route key={42} exact path={Constants.PATHS.NomServiceGroups} component={ServiceGroupUI} />);
            routes.push(<Route key={421} path={Constants.PATHS.NomAddServiceGroup} component={ServiceGroupFormUI} />);
            routes.push(<Route key={422} path={Constants.PATHS.NomEditServiceGroup} component={ServiceGroupFormUI} />);

            routes.push(<Route key={43} exact path={Constants.PATHS.NomDeclarations} component={DeclarationsUI} />)
            routes.push(<Route key={431} path={Constants.PATHS.NomAddDeclaration} component={DeclarationFormUI} />)
            routes.push(<Route key={432} path={Constants.PATHS.NomEditDeclaration} component={DeclarationFormUI} />)

            routes.push(<Route key={44} exact path={Constants.PATHS.NomServices} component={ServicesUI} />)
            routes.push(<Route key={441} path={Constants.PATHS.NomAddService} component={AddUpdateServiceUI} />)
            routes.push(<Route key={442} path={Constants.PATHS.NomEditService} component={AddUpdateServiceUI} />)

            routes.push(<Route key={45} path={Constants.PATHS.paymentsEpay} component={EpayPaymentsFormUI} />)
            routes.push(<Route key={46} exact path={Constants.PATHS.paymentsPep} component={PepPaymentsUI} />)
            routes.push(<Route key={461} path={Constants.PATHS.PaymentsAddPep} component={PepPaymentsFormUI} />)
            routes.push(<Route key={462} path={Constants.PATHS.PaymentsEditPep} component={PepPaymentsFormUI} />)
        }

        if (hasAuditRole) {
            //Филтриране на достъпа на администратор с права 'УПРАВЛЕНИЕ НА ОДИТ'
            routes.push(<Route key={50} path={Constants.PATHS.Audit} component={AuditUI} />);
            routes.push(<Route key={51} path={Constants.PATHS.ReportsPayments} component={PaymentsObligationsUI} />);
            routes.push(<Route key={52} path={Constants.PATHS.ReportsNotary} component={NotaryInterestsForPersonOrVehicleUI} />);
        }

        if (hasAdmLimitsRole) {
            //Филтриране на достъпа на администратор с права 'УПРАВЛЕНИЕ НА ПАРАМЕТРИ И ЛИМИТИ'.
            routes.push(<Route key={60} path={Constants.PATHS.AppParameters} component={AppParametersUI} />);
            routes.push(<Route key={61} path={Constants.PATHS.Limits} component={LimitsUI} />);
        }

        routes.push(<Route key={70} path={Constants.PATHS.SessionTimeout} component={SessionTimeoutUI} />);
        routes.push(<Route key={80} component={PageNotFoundUI} />);
    }

    return <Layout user={user}>
        <Switch>
            {user ? routes : <Route key={90} component={UnauthenticatedPageUI} />}
        </Switch>
    </Layout>
}

renderApp();