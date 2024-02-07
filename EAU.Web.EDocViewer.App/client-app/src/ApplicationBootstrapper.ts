import moment from "moment";
import 'moment/locale/bg';
import numeral from 'numeral';
import 'numeral/locales';
import { cnsysCoreModule, ArrayHelper } from 'cnsys-core';
import { eauCoreModule, pageRoute, Nomenclatures, appConfig, Constants as coreConstants, ModuleManager, resourceManager } from 'eau-core';
import { eauEDocViewerModule, Constants } from 'eau-edoc-viewer';
import { eauDocumentsModule } from 'eau-documents';
import { eauDocProcessesModule } from 'eau-services-document-processes';

export class ApplicationBootstrapper {
    public static run(): Promise<void> {

        var resourcesPromise = this.initResources();
        var modulesPromise = this.bootstrapperModules();
        var servicesPromise = this.addServices();

        return Promise.all([resourcesPromise, modulesPromise, servicesPromise]).bind(this).then(args => null);
    }

    private static initResources(): Promise<void> {
        let lang = appConfig.clientLanguage == '' ? 'bg' : appConfig.clientLanguage;

        if (lang == 'bg') {

            numeral.locale(lang);
            moment.locale(lang);

            return resourceManager.loadResourcesByPrefixes([
                'GL', //- глобален ресурс за целия сайт
            ]);
        } else {
            let momentLangFileName = this.momentLangCodeMapper(lang);
            return import(/* webpackMode: "eager" */ `moment/locale/${momentLangFileName}`).then(locale => {
                numeral.locale(lang);
                moment.locale(lang);

                return resourceManager.loadResourcesByPrefixes([
                    'GL', //- глобален ресурс за целия сайт
                ]);
            });
        }
    }

    private static bootstrapperModules(): Promise<void> {

        var corePromise = ModuleManager.registerModule(cnsysCoreModule);
        var eauCorePromise = ModuleManager.registerModule(eauCoreModule);
        var portalPromise = ModuleManager.registerModule(eauEDocViewerModule);
        var docProcessesPromise = ModuleManager.registerModule(eauDocProcessesModule);
        var eauDocumentsModulePromise = ModuleManager.registerModule(eauDocumentsModule);

        return Promise.all([corePromise, eauCorePromise, portalPromise, docProcessesPromise, eauDocumentsModulePromise]).then(args => { return Promise.resolve(); });
    }

    private static addServices(): Promise<void> {

        return Nomenclatures.getServicesGroups(i => i.isActive == true).then(groups => {

            let grIds = ArrayHelper.queryable.from(groups).select(gr => gr.groupID).toArray();

            return Nomenclatures.getServices(el => grIds.includes(el.groupID) && el.isActive).then(services => {
                services.forEach(function (item) {
                    var path = coreConstants.PATHS.APPLICATION_PROCESSES.replace(':serviceID', item.serviceID.toString()).replace(':sectionCode', '');
                    pageRoute.addPageRouteNodes([{ pathPattern: path, text: item.name }]);
                })
            });
        })
    }

    private static momentLangCodeMapper(langCode: string): string {
        switch (langCode) {
            case 'en':
                return 'en-gb';
            case 'gom':
                return 'gom-latn';
            case 'hy':
                return 'hy-am';
            case 'pa':
                return 'pa-in';
            case 'tl':
                return 'tl-ph';
            case 'ug':
                return 'ug-cn';
            case 'x':
                return 'x-pseudo';
            case 'zh':
                return 'zh-cn';
            default:
                return langCode;
        }
    }
}