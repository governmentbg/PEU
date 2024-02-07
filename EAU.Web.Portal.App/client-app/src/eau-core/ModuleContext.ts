import { BaseModuleInitializaitonContext, LocalizationErorrs, LocalizationResources, TypedModuleContext } from 'cnsys-core';

class ModuleContext extends TypedModuleContext<BaseModuleInitializaitonContext, LocalizationResources, LocalizationErorrs> {    
    public get moduleName(): string {
        return 'eau-core';
    }    
}

export const moduleContext = new ModuleContext();