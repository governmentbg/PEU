import { TypedModuleContext, BaseModuleInitializaitonContext, LocalizationResources, LocalizationErorrs } from 'cnsys-core'

export interface CodLocalizationErorrs extends LocalizationErorrs {

}

class ModuleContext extends TypedModuleContext<BaseModuleInitializaitonContext, LocalizationResources, CodLocalizationErorrs> {

    public get moduleName(): string {
        return 'eau-documents-pbzn';
    }
}

export const moduleContext = new ModuleContext();