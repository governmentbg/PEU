import { UserInfo } from 'cnsys-core';
import { eauAuthenticationService, resourceManager, UserPermissions } from 'eau-core';
import React, { useEffect, useState } from 'react';
import { NavLink, RouteComponentProps, withRouter } from 'react-router-dom';
import { Constants } from '../../Constants';

interface SideNavProps extends RouteComponentProps {
}

const SideNavImpl: React.FC<SideNavProps> = (props) => {


    const [user, setUser] = useState<UserInfo>();

    const MENU_IDS = {
        home: { id: "home" },
        users: { id: "users", title: "menu-title-id-users", ul: "menu-ul-id-users" },
        content: { id: "content", title: "menu-title-id-content", ul: "menu-ul-id-content" },
        translations: { id: "translations", title: "menu-title-id-translations", ul: "menu-ul-id-translations" },
        noms: { id: "noms", title: "menu-title-id-noms", ul: "menu-ul-id-noms" },
        limits: { id: "limits" },
        appParameters: { id: "app-parameters" },
        payments: { id: "payments", title: "menu-title-id-payments", ul: "menu-ul-id-payments" },
        reports: { id: "reports", title: "menu-title-id-reports", ul: "menu-ul-id-reports" },
    }

    useEffect(() => {
        initActiveParentMenuItem();

        eauAuthenticationService.getCurrentUser().then(user => setUser(user))
    }, [user])



    for (let key in MENU_IDS) {
        let ulId = MENU_IDS[key].ul;
        if (ulId) {
            $('#' + ulId + ' a').on('click', (e) => {
                //Sub-menu item is clicked
                if ($('#content-wrapper').hasClass("mini-navbar-left")) {
                    //Close the submenu after click
                    $('#' + ulId).removeClass('show');
                }
            });
        }
    }


    return (< div className="nav-wrapper" >
        <nav className="navbar-left">
            <ul>
                <li>
                    <NavLink to="/" activeClassName={'active'} isActive={isBaseRoute}>
                        <i className="nav-icon nav-icon-home"></i>
                        <span>{resourceManager.getResourceByKey(Constants.RESOURCES.Home)}</span>
                    </NavLink>
                </li>
                {
                    user && user.roles && user.roles.length > 0
                        ? <>
                            {
                                //Филтриране на достъпа на администратор с права 'УПРАВЛЕНИЕ НА ПОТРЕБИТЕЛИ' И 'УПРАВЛЕНИЕ НА ОДИТ'.
                                (user.roles.indexOf(UserPermissions[UserPermissions.ADM_USERS]) >= 0 || user.roles.indexOf(UserPermissions[UserPermissions.ADM_AUDIT]) >= 0)
                                && <li>
                                    <a href="#" onClick={e => toggleMenu(e, MENU_IDS.users.id)} className={isActiveUsersSection() ? 'active' : ""}>
                                        <i className="nav-icon nav-icon nav-icon-users"></i>
                                        <span>{resourceManager.getResourceByKey(Constants.RESOURCES.Users)}</span>
                                        <i id={MENU_IDS.users.title} className="ui-icon ui-icon-chevron-right collapse-arrow" aria-hidden="true"></i>
                                    </a>
                                    <ul id={MENU_IDS.users.ul} className="collapse">
                                        <li>
                                            {
                                                (true || user.roles.indexOf(UserPermissions[UserPermissions.ADM_USERS]) >= 0)
                                                && <>
                                                    <NavLink to={Constants.PATHS.InternalUsersRegister} activeClassName="selected">
                                                        <span className="menu-text">{resourceManager.getResourceByKey("GL_REGISTRATION_L")}</span>
                                                    </NavLink>
                                                    <NavLink to={Constants.PATHS.InternalUsersProfiles} activeClassName="selected">
                                                        <span className="menu-text">{resourceManager.getResourceByKey(Constants.RESOURCES.InternalUsersProfiles)}</span>
                                                    </NavLink>
                                                </>
                                            }
                                            {
                                                (true || user.roles.indexOf(UserPermissions[UserPermissions.ADM_AUDIT]) >= 0)
                                                && <NavLink to={Constants.PATHS.Audit} activeClassName="selected">
                                                    <span className="menu-text">{resourceManager.getResourceByKey(Constants.RESOURCES.Audit)}</span>
                                                </NavLink>
                                            }
                                        </li>
                                    </ul>
                                </li>
                            }
                            {
                                //Филтриране на достъпа на администратор с права 'УПРАВЛЕНИЕ НА СЪДЪРЖАНИЕ И ПРЕВОДИ'.
                                user.roles.indexOf(UserPermissions[UserPermissions.ADM_CMS]) >= 0
                                && <>
                                    <li>
                                        <a href="#" onClick={e => toggleMenu(e, MENU_IDS.content.id)} className={isActiveCMSPagesSection() ? 'active' : ""}>
                                            <i className="nav-icon nav-icon nav-icon-documents"></i>
                                            <span>{resourceManager.getResourceByKey(Constants.RESOURCES.Content)}</span>
                                            <i id={MENU_IDS.content.title} className="ui-icon ui-icon-chevron-right collapse-arrow" aria-hidden="true"></i>
                                        </a>
                                        <ul id={MENU_IDS.content.ul} className="collapse">
                                            <li>
                                                <NavLink to={Constants.PATHS.DocumentTemplates} activeClassName="selected">
                                                    <span className="menu-text">{resourceManager.getResourceByKey('GL_DOCUMENT_TEMPLATES_L')}</span>
                                                </NavLink>
                                                <NavLink to={Constants.PATHS.Pages} activeClassName="selected">
                                                    <span className="menu-text">{resourceManager.getResourceByKey(Constants.RESOURCES.PredefinedPages)}</span>
                                                </NavLink>
                                            </li>
                                        </ul>
                                    </li>
                                    <li>
                                        <a href="#" onClick={e => toggleMenu(e, MENU_IDS.translations.id)} className={isActiveTranslationsSection() ? 'active' : ""}>
                                            <i className="nav-icon nav-icon-language"></i>
                                            <span>{resourceManager.getResourceByKey(Constants.RESOURCES.Translations)}</span>
                                            <i id={MENU_IDS.translations.title} className="ui-icon ui-icon-chevron-right collapse-arrow" aria-hidden="true"></i>
                                        </a>
                                        <ul id={MENU_IDS.translations.ul} className="collapse">
                                            <li>
                                                <NavLink to={Constants.PATHS.TranslationsServices} activeClassName="selected" >
                                                    <span className="menu-text">{resourceManager.getResourceByKey(Constants.RESOURCES.Services)}</span>
                                                </NavLink>
                                                <NavLink to={Constants.PATHS.TranslationsLabels} activeClassName="selected">
                                                    <span className="menu-text">{resourceManager.getResourceByKey(Constants.RESOURCES.Labels)}</span>
                                                </NavLink>
                                                <NavLink to={Constants.PATHS.TranslationsServiceGroups} activeClassName="selected" >
                                                    <span className="menu-text">{resourceManager.getResourceByKey(Constants.RESOURCES.ServiceGroups)}</span>
                                                </NavLink>
                                                <NavLink to={Constants.PATHS.TranslationsPages} activeClassName="selected" >
                                                    <span className="menu-text">{resourceManager.getResourceByKey(Constants.RESOURCES.PredefinedPages)}</span>
                                                </NavLink>
                                            </li>
                                        </ul>
                                    </li>
                                </>
                            }
                            {
                                //Филтриране на достъпа на администратор с права 'УПРАВЛЕНИЕ НА НОМЕНКЛАТУРИ'.
                                user.roles.indexOf(UserPermissions[UserPermissions.ADM_NOM]) >= 0
                                && <li>
                                    <a href="#" onClick={e => toggleMenu(e, MENU_IDS.noms.id)} className={isActiveNomenclaturesSection() ? 'active' : ""}>
                                        <i className="nav-icon nav-icon-list"></i>
                                        <span>{resourceManager.getResourceByKey(Constants.RESOURCES.Noms)}</span>
                                        <i id={MENU_IDS.noms.title} className="ui-icon ui-icon-chevron-right collapse-arrow" aria-hidden="true"></i>
                                    </a>
                                    <ul id={MENU_IDS.noms.ul} className="collapse">
                                        <li>
                                            <NavLink to={Constants.PATHS.NomServices} activeClassName="selected" >
                                                <span className="menu-text">{resourceManager.getResourceByKey(Constants.RESOURCES.Services)}</span>
                                            </NavLink>
                                            <NavLink to={Constants.PATHS.NomDeclarations} activeClassName="selected" >
                                                <span className="menu-text">{resourceManager.getResourceByKey("GL_DECLARATIVE_CIRCUMSTANCES_POLICY_L")}</span>
                                            </NavLink>
                                            <NavLink to={Constants.PATHS.NomLabels} activeClassName="selected" >
                                                <span className="menu-text">{resourceManager.getResourceByKey(Constants.RESOURCES.Labels)}</span>
                                            </NavLink>
                                            <NavLink to={Constants.PATHS.NomLanguages} activeClassName="selected">
                                                <span className="menu-text">{resourceManager.getResourceByKey(Constants.RESOURCES.Languages)}</span>
                                            </NavLink>
                                            <NavLink to={Constants.PATHS.NomServiceGroups} activeClassName="selected" >
                                                <span className="menu-text">{resourceManager.getResourceByKey(Constants.RESOURCES.ServiceGroups)}</span>
                                            </NavLink>

                                        </li>
                                    </ul>
                                </li>
                            }
                            {
                                //Филтриране на достъпа на администратор с права 'УПРАВЛЕНИЕ НА ПАРАМЕТРИ И ЛИМИТИ'.
                                user.roles.indexOf(UserPermissions[UserPermissions.ADM_PARAM_LIMIT]) >= 0
                                && <>
                                    <li>
                                        <NavLink to={Constants.PATHS.Limits} activeClassName={'active'}>
                                            <i className="nav-icon nav-icon-limits"></i>
                                            <span>{resourceManager.getResourceByKey(Constants.RESOURCES.Limits)}</span>
                                        </NavLink>
                                    </li>
                                    <li>
                                        <NavLink to={Constants.PATHS.AppParameters} activeClassName={'active'}>
                                            <i className="nav-icon nav-icon-configuration"></i>
                                            <span>{resourceManager.getResourceByKey(Constants.RESOURCES.AppParameters)}</span>
                                        </NavLink>
                                    </li>
                                </>
                            }
                            {
                                //Филтриране на достъпа на администратор с права 'УПРАВЛЕНИЕ НА ПЛАЩАНИЯТА'.
                                user.roles.indexOf(UserPermissions[UserPermissions.ADM_NOM]) >= 0
                                && <li>
                                    <a href="#" onClick={e => toggleMenu(e, MENU_IDS.payments.id)} className={isActivePaymentsSection() ? 'active' : ""}>
                                        <i className="nav-icon nav-icon-payment"></i>
                                        <span>{resourceManager.getResourceByKey(Constants.RESOURCES.Payments)}</span>
                                        <i id={MENU_IDS.payments.title} className="ui-icon ui-icon-chevron-right collapse-arrow" aria-hidden="true"></i>
                                    </a>
                                    <ul id={MENU_IDS.payments.ul} className="collapse">
                                        <li>
                                            <NavLink to={Constants.PATHS.paymentsEpay} activeClassName="selected">
                                                <span className="menu-text">{resourceManager.getResourceByKey(Constants.RESOURCES.PaymentsEpay)}</span>
                                            </NavLink>
                                            <NavLink to={Constants.PATHS.paymentsPep} activeClassName="selected">
                                                <span className="menu-text">{resourceManager.getResourceByKey("GL_PAYMENTS_PEP_L")}</span>
                                            </NavLink>
                                        </li>
                                    </ul>
                                </li>
                            }
                            {
                                //Филтриране на достъпа на администратор с права 'УПРАВЛЕНИЕ НА ПЛАЩАНИЯТА'.
                                user.roles.indexOf(UserPermissions[UserPermissions.ADM_AUDIT]) >= 0
                                && <li>
                                    <a href="#" onClick={e => toggleMenu(e, MENU_IDS.reports.id)} className={isActiveReportsSection() ? 'active' : ""}>
                                        <i className="nav-icon nav-icon-references"></i>
                                        <span>{resourceManager.getResourceByKey(Constants.RESOURCES.Reports)}</span>
                                        <i id={MENU_IDS.reports.title} className="ui-icon ui-icon-chevron-right collapse-arrow" aria-hidden="true"></i>
                                    </a>
                                    <ul id={MENU_IDS.reports.ul} className="collapse">
                                        <li>
                                            <NavLink to={Constants.PATHS.ReportsPayments} activeClassName="selected">
                                                <span className="menu-text">{resourceManager.getResourceByKey(Constants.RESOURCES.ReportsPayments)}</span>
                                            </NavLink>
                                            <NavLink to={Constants.PATHS.ReportsNotary} activeClassName="selected">
                                                <span className="menu-text">{resourceManager.getResourceByKey(Constants.RESOURCES.ReportsNotary)}</span>
                                            </NavLink>
                                        </li>
                                    </ul>
                                </li>
                            }
                        </>
                        : null
                }
            </ul>
        </nav>
    </div>)

    //#region helpers

    function isBaseRoute(match: any, location: any) {
        return location.pathname == '/'
    }

    function toggleMenu(target: any, currentItem: string) {
        target.preventDefault();

        for (let key in MENU_IDS) {

            if (key == currentItem) {
                $(`#${((MENU_IDS as any)[key]).title}`).toggleClass("rotate-90");
                $(`#${((MENU_IDS as any)[key]).ul}`).slideToggle(function () { $(this).css('display', '').toggleClass('show') });
            } else {
                $(`#${((MENU_IDS as any)[key]).title}`).removeClass("rotate-90");
                $(`#${((MENU_IDS as any)[key]).ul}`).slideUp(function () { $(this).css('display', '').removeClass('show') });
            }
        }
    }

    function initActiveParentMenuItem() {
        if (props.location.pathname != "/") {

            for (let key in MENU_IDS) {

                if ($(`#${((MENU_IDS as any)[key]).ul} > li > a.selected`).length > 0) {

                    $(`#${((MENU_IDS as any)[key]).title}`).toggleClass("rotate-90");
                    $(`#${((MENU_IDS as any)[key]).ul}`).slideToggle(function () { $(this).css('display', '').toggleClass('show') });

                    break;
                }
            }
        }
    }

    function isActiveTranslationsSection() {

        return [
            Constants.PATHS.TranslationsServices,
            Constants.PATHS.TranslationsEditService,
            Constants.PATHS.TranslationsAddService,
            Constants.PATHS.TranslationsLabels,
            Constants.PATHS.TranslationsLanguages,
            Constants.PATHS.TranslationsServiceGroups,
            Constants.PATHS.TranslationsEditServiceGroup,
            Constants.PATHS.TranslationsAddServiceGroup,
            Constants.PATHS.TranslationsPages,
            Constants.PATHS.TranslationsPagesTranslate,
        ].includes(props.location.pathname)
    }

    function isActiveNomenclaturesSection() {
        return [
            Constants.PATHS.NomServices,
            Constants.PATHS.NomEditService,
            Constants.PATHS.NomAddService,
            Constants.PATHS.NomDeclarations,
            Constants.PATHS.NomEditDeclaration,
            Constants.PATHS.NomAddDeclaration,
            Constants.PATHS.NomLabels,
            Constants.PATHS.NomLanguages,
            Constants.PATHS.NomServiceGroups,
            Constants.PATHS.NomEditServiceGroup,
            Constants.PATHS.NomAddServiceGroup
        ].includes(props.location.pathname)
    }

    function isActivePaymentsSection() {
        return [
            Constants.PATHS.paymentsEpay,
            Constants.PATHS.paymentsPep,
            Constants.PATHS.PaymentsEditPep,
            Constants.PATHS.PaymentsAddPep
        ].includes(props.location.pathname)
    }

    function isActiveReportsSection() {
        return [
            Constants.PATHS.ReportsPayments,
            Constants.PATHS.ReportsNotary,
        ].includes(props.location.pathname)
    }

    function isActiveCMSPagesSection() {
        return [
            Constants.PATHS.DocumentTemplates,
            Constants.PATHS.AddDocumentTemplate,
            Constants.PATHS.EditDocumentTemplate,
            Constants.PATHS.Pages,
            Constants.PATHS.PagesEdit

        ].includes(props.location.pathname)
    }

    function isActiveUsersSection() {
        return [
            Constants.PATHS.InternalUsersRegister,
            Constants.PATHS.Audit,
        ].includes(props.location.pathname)
    }

    //#endregion
}

export const SideNav = withRouter(SideNavImpl);