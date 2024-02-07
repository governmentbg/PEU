import { TypedModuleContext, BaseModuleInitializaitonContext } from './contexts/BaseModuleContext'
import { LocalizationResources, LocalizationErorrs } from './LocalizationResources'

class ModuleContext extends TypedModuleContext<BaseModuleInitializaitonContext, LocalizationResources, LocalizationErorrs> {    
    public get moduleName(): string {
        return 'cnsys-core';
    }    
}

export const moduleContext = new ModuleContext();
