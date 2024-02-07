import { resourceManager } from 'eau-core';
import { BaseModuleInitializaitonContext, Module } from 'cnsys-core';
import { moduleContext } from './ModuleContext';

export function registerModule(initContext: BaseModuleInitializaitonContext): Promise<void> {
    moduleContext.initializeContext(initContext);

    return resourceManager.loadResourcesByPrefixes([
        'DOC_MIGR',
    ]);
}

export const eauDocumentsMIGRModule: Module = {
    registerModule: registerModule,
    moduleContext: moduleContext
}