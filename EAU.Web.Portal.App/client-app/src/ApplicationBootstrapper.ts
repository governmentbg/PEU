import moment from "moment";
import 'moment/locale/bg';
import numeral from 'numeral';
import  'numeral/locales'
import { cnsysCoreModule, ArrayHelper } from 'cnsys-core';
import { eauCoreModule, pageRoute, Nomenclatures, appConfig, Constants as coreConstants, ModuleManager, resourceManager, WaysToStartService, CmsDataService } from 'eau-core';
import { eauPortalModule, Constants } from 'eau-portal';
import { eauDocumentsModule } from 'eau-documents';
import { eauDocProcessesModule } from 'eau-services-document-processes';

export class ApplicationBootstrapper {
    public static run(): Promise<void> {

        var resourcesPromise = this.initResources();
        var modulesPromise = this.bootstrapperModules();
        var servicesPromise = this.addServices();
        var cmsPagesPromise = this.addCmsPagesRouteNodes();

        return Promise.all([resourcesPromise, modulesPromise, servicesPromise, cmsPagesPromise]).bind(this).then(args => {
            this.initializeBreadcrumb();
        });
    }

    private static initResources(): Promise<void> {
        let lang = appConfig.clientLanguage == '' ? 'bg' : appConfig.clientLanguage;

        //Форматирането на датите е винаги ДД.ММ.ГГГГ, за това сетваме локалето на moment-a на bg. MVREAU2020-366
        moment.locale('bg');
        numeral.locale(lang);

        return resourceManager.loadResourcesByPrefixes([
            'GL', //- глобален ресурс за целия сайт
        ]);
    }

    private static bootstrapperModules(): Promise<void> {

        var corePromise = ModuleManager.registerModule(cnsysCoreModule);
        var eauCorePromise = ModuleManager.registerModule(eauCoreModule);
        var portalPromise = ModuleManager.registerModule(eauPortalModule);
        var docProcessesPromise = ModuleManager.registerModule(eauDocProcessesModule);
        var eauDocumentsModulePromise = ModuleManager.registerModule(eauDocumentsModule);

        return Promise.all([corePromise, eauCorePromise, portalPromise, docProcessesPromise, eauDocumentsModulePromise]).then(args => { return Promise.resolve(); });
    }

    private static initializeBreadcrumb() {

        //pageRoute.rootItems.push({
        //    path: Constants.PATHS.Home,
        //    text: resourceManager.getResourceByKey("GL_HOME_L"),
        //    isInternal: true
        //})

        pageRoute.addPageRouteNodes([
            { pathPattern: Constants.PATHS.Services, text: resourceManager.getResourceByKey(Constants.RESOURCES.Services) },
            { pathPattern: Constants.PATHS.Contacts, text: resourceManager.getResourceByKey(Constants.RESOURCES.Contacts) },
            { pathPattern: Constants.PATHS.Help, text: resourceManager.getResourceByKey(Constants.RESOURCES.Help) },
            { pathPattern: Constants.PATHS.TextVersion, text: resourceManager.getResourceByKey(Constants.RESOURCES.TextVersion) },

            //skipBreadcrumb: Ако се сложи на parent ниво, премахва breadcrumb-а на всички деца. Ако не се цели махането на всики, както е в users/...
            //се слага на конкретния краен route.
            { pathPattern: Constants.PATHS.Users, text: resourceManager.getResourceByKey(Constants.RESOURCES.Users), disabled: true, skipBreadcrumb: true },
            { pathPattern: Constants.PATHS.TooManyRequestsUI, text: '' },
            { pathPattern: Constants.PATHS.Registration, text: resourceManager.getResourceByKey(Constants.RESOURCES.Registration) },
            { pathPattern: Constants.PATHS.RegistrationFormComplete, text: resourceManager.getResourceByKey(Constants.RESOURCES.Registration) },
            { pathPattern: Constants.PATHS.UserProfile, text: resourceManager.getResourceByKey(Constants.RESOURCES.UserProfile) },
            { pathPattern: Constants.PATHS.ForgottenPassword, text: resourceManager.getResourceByKey(Constants.RESOURCES.ChangeForgottenPassword) },
            { pathPattern: Constants.PATHS.ResetPassword, text: resourceManager.getResourceByKey(Constants.RESOURCES.ResetPassword) },
            { pathPattern: Constants.PATHS.UserAuthentications, text: resourceManager.getResourceByKey(Constants.RESOURCES.UserAuthentications) },
            { pathPattern: Constants.PATHS.ChangePassword, text: resourceManager.getResourceByKey(Constants.RESOURCES.ChangePassword) },
            { pathPattern: Constants.PATHS.ConfirmUserRegistration, text: resourceManager.getResourceByKey(Constants.RESOURCES.ConfirmUserRegistration) },
            { pathPattern: Constants.PATHS.CancelUserRegistration, text: resourceManager.getResourceByKey(Constants.RESOURCES.CancelUserRegistration) },

            { pathPattern: Constants.PATHS.Search, text: resourceManager.getResourceByKey(Constants.RESOURCES.Search) },
            { pathPattern: Constants.PATHS.TestSign, text: resourceManager.getResourceByKey(Constants.RESOURCES.TestSign) },
            { pathPattern: coreConstants.PATHS.ServiceInstances, text: resourceManager.getResourceByKey(Constants.RESOURCES.MyEServices), showMainNodeOnly: true },
            { pathPattern: coreConstants.PATHS.NotAcknowledgedMessage, text: resourceManager.getResourceByKey("DOC_GL_ReceiptNotAcknowledgedMessage_L"), skipBreadcrumb: true },
            { pathPattern: Constants.PATHS.MyEPayments, text: resourceManager.getResourceByKey(Constants.RESOURCES.MyEPayments) },
            { pathPattern: Constants.PATHS.Obligations, text: resourceManager.getResourceByKey(Constants.RESOURCES.Obligations) },
            { pathPattern: Constants.PATHS.KATObligations, text: resourceManager.getResourceByKey(Constants.RESOURCES.KatObligations) },
            { pathPattern: coreConstants.PATHS.DocumentPreview, text: resourceManager.getResourceByKey('GL_E_DOCUMENT_PREVIEW_L'), skipBreadcrumb: true },
            { pathPattern: coreConstants.PATHS.ServiceInstance, text: resourceManager.getResourceByKey('GL_CASE_FILE_L') },
            { pathPattern: coreConstants.PATHS.ServiceInstanceDocumentPreview, text: resourceManager.getResourceByKey('GL_E_DOCUMENT_PREVIEW_L') },
            { pathPattern: Constants.PATHS.SiteMap, text: resourceManager.getResourceByKey(Constants.RESOURCES.SiteMap) }
        ]);
    }

    private static addServices(): Promise<void> {
        
        return Nomenclatures.getServicesGroups(i => i.isActive == true).then(groups => {

            let grIds = ArrayHelper.queryable.from(groups).select(gr => gr.groupID).toArray();

            return Nomenclatures.getServices(el => grIds.includes(el.groupID) && el.isActive).then(services => {
                services.filter(srv => srv.initiationTypeID == WaysToStartService.ByAplication).forEach(function (item) {

                    var pathStart = coreConstants.PATHS.APPLICATION_PROCESSES_START.replace(':serviceID', item.serviceID.toString()).replace(':sectionCode', '');
                    pageRoute.addPageRouteNodes([{ pathPattern: pathStart, text: item.name, skipBreadcrumbItem: true }]);

                    var path = coreConstants.PATHS.APPLICATION_PROCESSES.replace(':serviceID', item.serviceID.toString()).replace(':sectionCode', '');
                    pageRoute.addPageRouteNodes([{ pathPattern: path, text: item.name }]);                    
                })
            });
        })
    }

    private static addCmsPagesRouteNodes(): Promise<void> {
        return new CmsDataService().getPages(appConfig.clientLanguage).then((pages) => {
            if (pages) {
                for (let i: number = 0; i < pages.length; i++) {
                    let page = pages[i];
                    pageRoute.addPageRouteNodes([{ pathPattern: `/${page.code.toLowerCase()}`, text: page.title }]);
                }
            }
        });
    }
}