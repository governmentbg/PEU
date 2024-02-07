import { BaseModuleInitializaitonContext, Module } from 'cnsys-core';
import { moduleContext } from './ModuleContext';

export function registerModule(initContext: BaseModuleInitializaitonContext): Promise<void> {
    moduleContext.initializeContext(initContext);

    return Promise.resolve();
}

export const eauDocProcessesModule: Module = {
    registerModule: registerModule,
    moduleContext: moduleContext
}