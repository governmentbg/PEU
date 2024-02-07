import { BaseModuleInitializaitonContext, Module } from 'cnsys-core';
import { eauAuthenticationService } from 'eau-core';
import { moduleContext } from './ModuleContext';
import { AdminUserContext } from './services/AdminUserContext';

export function registerModule(initContext: BaseModuleInitializaitonContext): Promise<void> {
    moduleContext.initializeContext(initContext);

    return eauAuthenticationService.init(new AdminUserContext()).then(r => {
        return Promise.resolve();
    });
}

export const eauAdminModule: Module = {
    registerModule: registerModule,
    moduleContext: moduleContext
}