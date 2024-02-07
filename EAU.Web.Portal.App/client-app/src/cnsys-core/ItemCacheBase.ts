import { ObjectHelper } from "./common";
import { Dictionary } from "typescript-collections";
import ObjectCache from 'cnsys-cache';

const cache = new ObjectCache();

class PromiseResolver<TModel> {

    private resolveCallback: (thenableOrResult: TModel | Promise<TModel>) => void;
    private rejectCallback: (error: any) => void;

    constructor(
        resolve: (thenableOrResult: TModel | Promise<TModel>) => void,
        reject: (error: any) => void) {

        this.resolveCallback = resolve;
        this.rejectCallback = reject;
    }

    public resolve(thenableOrResult: TModel | Promise<TModel>): void {
        this.resolveCallback(thenableOrResult);
    }

    public reject(error: any): void {
        this.rejectCallback(error);
    }
}

class ItemCachePendingOperation<TModel> {
    private waitingPromiseResolvers: PromiseResolver<TModel>[];

    constructor() {
        this.waitingPromiseResolvers = [];
    }

    public addWaitingPromiseResolver(
        resolveCallback: (thenableOrResult: TModel | Promise<TModel>) => void,
        rejectCallback: (error: any) => void) {

        this.waitingPromiseResolvers.push(new PromiseResolver<TModel>(resolveCallback, rejectCallback));
    }

    public operationFullfiled(thenableOrResult: TModel | Promise<TModel>): void {
        if (this.waitingPromiseResolvers && this.waitingPromiseResolvers.length > 0) {
            for (var resolver of this.waitingPromiseResolvers) {
                resolver.resolve(thenableOrResult);
            }
        }
    }

    public operationRejected(error: any): void {
        if (this.waitingPromiseResolvers && this.waitingPromiseResolvers.length > 0) {
            for (var resolver of this.waitingPromiseResolvers) {
                resolver.reject(error);
            }
        }
    }
}

export abstract class ItemCacheBase<TModel>{

    private cacheKeyPrefix: string;
    private pendingValueGenerations: Dictionary<string, ItemCachePendingOperation<TModel>>;

    constructor() {
        this.cacheKeyPrefix = ObjectHelper.newGuid();
        this.pendingValueGenerations = new Dictionary<string, ItemCachePendingOperation<TModel>>();
    }

    public getItem(key?: string): Promise<TModel> {

        var cacheKey = this.getfullCacheKey(key);
        var item: TModel = cache.getItem(cacheKey);

        if (item) {
            return Promise.resolve(item);
        }
        else {
            var that = this;

            var pedingOperation = this.pendingValueGenerations.getValue(cacheKey);

            if (pedingOperation != null) {
                return new Promise<TModel>(function (resolve, reject) {
                    pedingOperation.addWaitingPromiseResolver(resolve, reject);
                });
            }
            else {

                pedingOperation = new ItemCachePendingOperation<TModel>();

                this.pendingValueGenerations.setValue(cacheKey, pedingOperation);

                return this.generateValue(key).then(value => {

                    cache.setItem(cacheKey, value, { expirationSliding: 60 * 60 });

                    pedingOperation.operationFullfiled(value);

                    that.pendingValueGenerations.remove(cacheKey);

                    return value;
                }, error => {

                    pedingOperation.operationRejected(error);

                    that.pendingValueGenerations.remove(cacheKey);

                    throw error;
                })
            }
        }
    }

    protected abstract generateValue(key?: string): Promise<TModel>;

    private getfullCacheKey(key?: string): string {
        if (key) {
            return this.cacheKeyPrefix + "_" + key;
        }
        else {
            return this.cacheKeyPrefix;
        }
    }
}