import { allModules, BaseModuleInitializaitonContext, ClientError, Module, ValidatorRegistry } from 'cnsys-core';
import { resourceManager } from './ResourceManager'

var _initContext = new BaseModuleInitializaitonContext();
_initContext.validatorRegistry = new ValidatorRegistry();
_initContext.resourceManager = resourceManager;

export namespace ModuleManager {

    export function registerModule(module: Module): Promise<void> {
        if (allModules.containsKey(module.moduleContext.moduleName)) {
            if (allModules.getValue(module.moduleContext.moduleName) == module) {
                return Promise.resolve();
            }
            else {
                throw new ClientError(`There is other registered module with same name ${module.moduleContext.moduleName}.`);
            }
        }
        else {
            return module.registerModule(_initContext).then(() => { allModules.setValue(module.moduleContext.moduleName, module) });
        }
    }
}
