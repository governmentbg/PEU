import { BaseModuleInitializaitonContext, Module } from 'cnsys-core';
import { moduleContext } from './ModuleContext';

export function registerModule(initContext: BaseModuleInitializaitonContext): Promise<void> {
    moduleContext.initializeContext(initContext);
    initializeBreadcrumb();

    return Promise.resolve();
}

export const eauEDocViewerModule: Module = {
    registerModule: registerModule,
    moduleContext: moduleContext
}

function initializeBreadcrumb() {

    //load application titles
}