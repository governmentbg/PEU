import { BaseModuleInitializaitonContext, Module } from 'cnsys-core';
import { eauAuthenticationService } from 'eau-core';
import { moduleContext } from './ModuleContext';
import { PortalUserContext } from './services/PortalUserContext';

export function registerModule(initContext: BaseModuleInitializaitonContext): Promise<void> {
    moduleContext.initializeContext(initContext);
    initializeBreadcrumb();

    return eauAuthenticationService.init(new PortalUserContext()).then(r => {
        return Promise.resolve();
    });
}

export const eauPortalModule: Module = {
    registerModule: registerModule,
    moduleContext: moduleContext
}

function initializeBreadcrumb() {

    //load application titles
}