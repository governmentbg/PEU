import { IResourceManager, Helper, TypeSystem } from 'cnsys-core'
import { ResourcesDataService } from '../services/ResourcesDataService'

class ResourceManager implements IResourceManager {
    private defaultMessageForResourceNotFound = 'No resource for key ';
    private _resources: any;
    private _resourcePrefixes: string[];
    private _dataService: ResourcesDataService;

    constructor() {
        this._resources = {};
        this._resourcePrefixes = [];
        this._dataService = new ResourcesDataService();
    }

    //#region IResourceManager

    public getResourceByKey(key: string): string {
        var lowerCaseKey = key.toLowerCase();
        if (this._resources[lowerCaseKey]) {
            return this._resources[lowerCaseKey];
        }
        else {
            return (this.defaultMessageForResourceNotFound + key);
        }
    }

    public getAllKeys(): string[] {
        var keys: string[] = [];

        for (var key in this._resources) {
            keys.push(key);
        }

        return keys;
    }

    //#endregion

    public loadResourcesByPrefixes(prefixes: string[]): Promise<void> {
        var newPrefixes: string[] = [];

        for (var prefix of prefixes) {
            if (this._resourcePrefixes.filter(item => prefix.indexOf(item) == 0).length == 0) {
                newPrefixes.push(prefix);
            }
        }

        if (newPrefixes.length > 0) {
            this._resourcePrefixes.push(...newPrefixes);

            return this._dataService.getResources(newPrefixes).bind(this).then(resData => {
                for (var prop in resData) {
                    this._resources[prop.toLowerCase()] = resData[prop];
                }
            })
        }

        return Promise.resolve();
    }

    public get resourcePrefixes(): string[] {
        return this._resourcePrefixes;
    }
}

export const resourceManager = new ResourceManager();
