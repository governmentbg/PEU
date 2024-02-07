import { BaseModuleInitializaitonContext, LocalizationErorrs, LocalizationResources, TypedModuleContext } from 'cnsys-core';

export interface KOSLocalizationErorrs extends LocalizationErorrs {
}

class ModuleContext extends TypedModuleContext<BaseModuleInitializaitonContext, LocalizationResources, KOSLocalizationErorrs> {    
    public get moduleName(): string {
        return 'eau-documents-kos';
    }    
}

export const moduleContext = new ModuleContext();