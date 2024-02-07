import { BaseModuleInitializaitonContext } from './contexts/BaseModuleContext';
import { moduleContext } from './ModuleContext';
import { Module } from './contexts'

function registerModule(initContext: BaseModuleInitializaitonContext): Promise<void> {
    if (initContext && initContext.validatorRegistry) {
        //add validators to validatorRegistry hear
        //Example: initParams.validatorRegistry.addValidator('fullModelName', new TestValidator())           
    }

    moduleContext.initializeContext(initContext);

    return Promise.resolve();
}

export const cnsysCoreModule: Module = {
    registerModule: registerModule,
    moduleContext: moduleContext
}