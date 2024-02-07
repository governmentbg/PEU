import { BaseErrorLocalizationResources, BaseLocalizationResources, IResourceManager, TypedModuleResourceManger } from '../common';
import { IValidatorFactory, IValidatorRegistry } from '../models';

export interface Module {
    registerModule(initContext: BaseModuleInitializaitonContext): Promise<void>;
    moduleContext: BaseModuleContext;
}

export class BaseModuleInitializaitonContext {
    validatorRegistry: IValidatorRegistry;
    resourceManager: IResourceManager;
}

export abstract class BaseModuleContext {

    protected _validatorRegistry: IValidatorRegistry;
    protected _resourceManager: IResourceManager;

    public initializeContext(initContext: BaseModuleInitializaitonContext) {
        this._validatorRegistry = initContext.validatorRegistry; 
        this._resourceManager = initContext.resourceManager;
    }

    public abstract get moduleName(): string;

    public get validatorFactory(): IValidatorFactory {
        return this._validatorRegistry.getValidatorFactory();
    }

    public get resourceManager(): IResourceManager {
        return this._resourceManager;
    }
}

export abstract class TypedModuleContext<
    TInitContex extends BaseModuleInitializaitonContext,
    TLocalizationResources extends BaseLocalizationResources,
    TErrorLocalizationResources extends BaseErrorLocalizationResources> extends BaseModuleContext {

    protected _typedResourceManager: TypedModuleResourceManger<TLocalizationResources, TErrorLocalizationResources>;

    public initializeContext(initContext: TInitContex) {
        super.initializeContext(initContext);

        this._typedResourceManager = new TypedModuleResourceManger<TLocalizationResources, TErrorLocalizationResources>(initContext.resourceManager);
    }

    public get resources(): TLocalizationResources {
        return this._typedResourceManager.resources;
    }

    public get errors(): TErrorLocalizationResources {
        return this._typedResourceManager.errors;
    }
}
