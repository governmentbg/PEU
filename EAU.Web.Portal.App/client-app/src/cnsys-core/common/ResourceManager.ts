import { TypeSystem } from './TypeSystem'
import { Helper } from './Helper'

export interface BaseLocalizationResources {
}

export interface BaseErrorLocalizationResources {
}

export interface IResourceManager {
    getResourceByKey(key: string): string;
    getAllKeys(): string[];
}

export class ApplicationResourceManger implements IResourceManager {
    private defaultMessageForResourceNotFound = 'No resource for key ';
    private resources: any;

    constructor(resources: any) {
        this.resources = resources;
    }

    //#region IResourceManager

    getResourceByKey(key: string): string {
        if (this.resources[key]) {
            return this.resources[key];
        }
        else {
            return (this.defaultMessageForResourceNotFound + key);
        }
    }


    getAllKeys(): string[] {
        var keys: string[] = [];

        for (var key in this.resources) {
            keys.push(key);
        }

        return keys;
    }

    //#endregion

    public getModuleResourceManager(moduleName: string): ModuleResourceManager {
        return new ModuleResourceManager(moduleName, this);
    }
}

class ModuleResourceManager implements IResourceManager {
    private moduleName: string;
    private resourceManager: IResourceManager;

    constructor(moduleName: string, resourceManger: IResourceManager) {
        this.moduleName = moduleName;
        this.resourceManager = resourceManger;
    }

    //#region IResourceManager

    getResourceByKey(key: string): string {
        return this.resourceManager.getResourceByKey(this.getFullKey(key));
    }

    getAllKeys(): string[] {
        var keys: string[] = []
        var fullKeys: string[] = this.resourceManager.getAllKeys();
        var modulePrefix = this.getModulePrefix();

        for (var key of fullKeys) {
            if (key.indexOf(modulePrefix) == 0) {
                keys.push(key.replace(modulePrefix, ""));
            }
        }

        return keys;
    }

    //#endregion 

    //#region Helpers

    private getModulePrefix() {
        var fullKey = this.moduleName + "_";

        fullKey = fullKey[0].toLowerCase() + fullKey.substring(1);

        return fullKey;
    }

    private getFullKey(key: string): string {
        return this.getModulePrefix() + key;
    }

    //#endregion
}

export class TypedModuleResourceManger<TResources extends BaseLocalizationResources, TErrors extends BaseErrorLocalizationResources> {
    private resourceManager: IResourceManager;
    private typedResourceManager: any;

    public get resources(): TResources {
        return this.typedResourceManager;
    }

    public get errors(): TErrors {
        return this.typedResourceManager;
    }

    constructor(resourceManager: IResourceManager) {
        this.resourceManager = resourceManager;
        this.initManager();
    }

    public getResourcesByKey(key: string): string {
        return this.resourceManager.getResourceByKey(key);
    }

    //#region Helpers

    private initManager() {
        var that = this;
        this.typedResourceManager = {};

        if (this.resourceManager) {
            var keys = this.resourceManager.getAllKeys();

            for (let key of keys) {                
                Object.defineProperty(this.typedResourceManager, key, { get: function () { return that.resourceManager.getResourceByKey(key); } });
            }
        }
    }

    //#endregion
}


