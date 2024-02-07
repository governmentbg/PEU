import 'moment/locale/bg';
import moment from "moment";
import numeral from 'numeral';
import { cnsysCoreModule } from 'cnsys-core';
import { eauCoreModule, pageRoute, ModuleManager, resourceManager } from 'eau-core';
import { eauAdminModule, Constants } from 'eau-admin';

export class ApplicationBootstrapper {
    public static run(): Promise<void> {

        var resourcesPromise = this.initResources();
        var modulesPromise = this.bootstrapperModules();

        return Promise.all([resourcesPromise, modulesPromise]).bind(this).then(() => {
            this.initializeBreadcrumb();
        });
    }

    private static initResources(): Promise<void> {
        numeral.locale('bg');
        moment.locale('bg');

        return resourceManager.loadResourcesByPrefixes([
            'GL', //- глобален ресурс за целия сайт
        ]);
    }

    private static bootstrapperModules(): Promise<void> {

        var corePromise = ModuleManager.registerModule(cnsysCoreModule);
        var eauCorePromise = ModuleManager.registerModule(eauCoreModule);
        var portalPromise = ModuleManager.registerModule(eauAdminModule);

        return Promise.all([corePromise, eauCorePromise, portalPromise]).then(args => { return Promise.resolve(); });
    }

    private static initializeBreadcrumb() {

        pageRoute.rootItems.push({
            path: Constants.PATHS.Home,
            text: resourceManager.getResourceByKey("GL_HOME_L"),
            isInternal: true
        })

        pageRoute.addPageRouteNodes([
            { pathPattern: Constants.PATHS.Noms, text: resourceManager.getResourceByKey(Constants.RESOURCES.Noms), disabled: true },
            { pathPattern: Constants.PATHS.NomServices, text: resourceManager.getResourceByKey(Constants.RESOURCES.Services) },
            { pathPattern: Constants.PATHS.NomAddService, text: resourceManager.getResourceByKey(Constants.RESOURCES.AddService) },
            { pathPattern: Constants.PATHS.NomEditService, text: resourceManager.getResourceByKey(Constants.RESOURCES.EditService) },
            { pathPattern: Constants.PATHS.NomDeclarations, text: resourceManager.getResourceByKey(Constants.RESOURCES.Declarations) },
            { pathPattern: Constants.PATHS.NomEditDeclaration, text: resourceManager.getResourceByKey(Constants.RESOURCES.EditDeclaration) },
            { pathPattern: Constants.PATHS.NomAddDeclaration, text: resourceManager.getResourceByKey(Constants.RESOURCES.AddDeclaration) },
            { pathPattern: Constants.PATHS.NomLabels, text: resourceManager.getResourceByKey(Constants.RESOURCES.EditLabels) },
            { pathPattern: Constants.PATHS.NomLanguages, text: resourceManager.getResourceByKey(Constants.RESOURCES.Languages) },
            { pathPattern: Constants.PATHS.NomServiceGroups, text: resourceManager.getResourceByKey(Constants.RESOURCES.ServiceGroups) },
            { pathPattern: Constants.PATHS.NomAddServiceGroup, text: resourceManager.getResourceByKey(Constants.RESOURCES.AddServiceGroup) },
            { pathPattern: Constants.PATHS.NomEditServiceGroup, text: resourceManager.getResourceByKey(Constants.RESOURCES.EditServiceGroup) },
            { pathPattern: Constants.PATHS.DocumentTemplates, text: resourceManager.getResourceByKey(Constants.RESOURCES.DocumentTemplates) },
            { pathPattern: Constants.PATHS.EditDocumentTemplate, text: resourceManager.getResourceByKey(Constants.RESOURCES.EditDocumentTemplates) },
            { pathPattern: Constants.PATHS.AddDocumentTemplate, text: resourceManager.getResourceByKey(Constants.RESOURCES.AddDocumentTemplates) },
            { pathPattern: Constants.PATHS.AppParameters, text: resourceManager.getResourceByKey(Constants.RESOURCES.AppParameters) },
            { pathPattern: Constants.PATHS.Content, text: resourceManager.getResourceByKey(Constants.RESOURCES.Content), disabled: true },
            { pathPattern: Constants.PATHS.Pages, text: resourceManager.getResourceByKey(Constants.RESOURCES.Pages) },
            { pathPattern: Constants.PATHS.PagesEdit, text: resourceManager.getResourceByKey(Constants.RESOURCES.PagesEdit) },

            { pathPattern: Constants.PATHS.Translations, text: resourceManager.getResourceByKey(Constants.RESOURCES.Translations), disabled: true },
            { pathPattern: Constants.PATHS.TranslationsServices, text: resourceManager.getResourceByKey(Constants.RESOURCES.TranslationsServices) },
            { pathPattern: Constants.PATHS.TranslationsEditService, text: resourceManager.getResourceByKey(Constants.RESOURCES.EditService) },
            { pathPattern: Constants.PATHS.TranslationsAddService, text: resourceManager.getResourceByKey(Constants.RESOURCES.AddService) },

            { pathPattern: Constants.PATHS.TranslationsLabels, text: resourceManager.getResourceByKey(Constants.RESOURCES.TranslationsLabels) },
            { pathPattern: Constants.PATHS.TranslationsLanguages, text: resourceManager.getResourceByKey(Constants.RESOURCES.Languages) },
            { pathPattern: Constants.PATHS.TranslationsServiceGroups, text: resourceManager.getResourceByKey(Constants.RESOURCES.TranslationsServiceGroups) },
            { pathPattern: Constants.PATHS.TranslationsPages, text: resourceManager.getResourceByKey(Constants.RESOURCES.TranslationsPages) },
            { pathPattern: Constants.PATHS.TranslationsPagesTranslate, text: resourceManager.getResourceByKey(Constants.RESOURCES.TranslationsPagesEdit) },

            { pathPattern: Constants.PATHS.Users, text: resourceManager.getResourceByKey(Constants.RESOURCES.Users), disabled: true },
            { pathPattern: Constants.PATHS.InternalUsersRegister, text: resourceManager.getResourceByKey(Constants.RESOURCES.InternalUsersRegister) },
            { pathPattern: Constants.PATHS.InternalUsersProfiles, text: resourceManager.getResourceByKey(Constants.RESOURCES.InternalUsersProfiles) },
            { pathPattern: Constants.PATHS.InternalUsersProfileEdit, text: resourceManager.getResourceByKey(Constants.RESOURCES.InternalUsersProfileEdit) },


            { pathPattern: Constants.PATHS.Audit, text: resourceManager.getResourceByKey(Constants.RESOURCES.Audit) },
            { pathPattern: Constants.PATHS.Limits, text: resourceManager.getResourceByKey(Constants.RESOURCES.Limits) },
            { pathPattern: Constants.PATHS.payments, text: resourceManager.getResourceByKey(Constants.RESOURCES.Payments), disabled: true },
            { pathPattern: Constants.PATHS.paymentsEpay, text: resourceManager.getResourceByKey(Constants.RESOURCES.PaymentsEpay) },
            { pathPattern: Constants.PATHS.paymentsPep, text: resourceManager.getResourceByKey(Constants.RESOURCES.PaymentsPep) },
            { pathPattern: Constants.PATHS.PaymentsEditPep, text: resourceManager.getResourceByKey(Constants.RESOURCES.PaymentsAddEditPep) },
            { pathPattern: Constants.PATHS.PaymentsAddPep, text: resourceManager.getResourceByKey(Constants.RESOURCES.PaymentsAddEditPep) },

            { pathPattern: Constants.PATHS.Reports, text: resourceManager.getResourceByKey(Constants.RESOURCES.Reports), disabled: true },
            { pathPattern: Constants.PATHS.ReportsPayments, text: resourceManager.getResourceByKey('GL_REPORTS_PAYMENTS_LONG_TITLE_L') },
            { pathPattern: Constants.PATHS.ReportsNotary, text: resourceManager.getResourceByKey(Constants.RESOURCES.ReportsNotary) },
        ]);
    }
}