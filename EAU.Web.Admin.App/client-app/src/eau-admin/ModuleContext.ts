import { BaseModuleInitializaitonContext, TypedModuleContext } from 'cnsys-core';

class ModuleContext extends TypedModuleContext<BaseModuleInitializaitonContext, any, any> {
    public get moduleName(): string {
        return 'eau-admin';
    }
}

export const moduleContext = new ModuleContext();