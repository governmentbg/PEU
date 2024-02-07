import { BaseModuleInitializaitonContext, TypedModuleContext } from 'cnsys-core';

class ModuleContext extends TypedModuleContext<BaseModuleInitializaitonContext, any, any> {
    public get moduleName(): string {
        return 'eau-edoc-viewer';
    }
}

export const moduleContext = new ModuleContext();