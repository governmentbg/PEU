import { TypedModuleContext, BaseModuleInitializaitonContext, LocalizationResources, LocalizationErorrs } from 'cnsys-core'

class ModuleContext extends TypedModuleContext<BaseModuleInitializaitonContext, LocalizationResources, LocalizationErorrs> {    
    public get moduleName(): string {
        return 'eau-documents';
    }    
}

export const moduleContext = new ModuleContext();