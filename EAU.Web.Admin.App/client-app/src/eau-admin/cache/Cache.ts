﻿import { ArrayHelper, ItemCacheBase } from "cnsys-core";
import { ActionType, ObjectType } from "../models/ModelsAutoGenerated";
import { ActionTypesDataCache } from "./ActionTypesDataCache";
import { ObjectTypesDataCache } from "./ObjectTypesDataCache";


let objectTypesDataCache: ObjectTypesDataCache = new ObjectTypesDataCache();
let actionTypesCache: ActionTypesDataCache = new ActionTypesDataCache();

export namespace Cache {

    export function getObjectTypesDataCache(predicate?: (elem: ObjectType) => boolean): Promise<ObjectType[]> {
        return getCollectionItems(objectTypesDataCache, predicate);
    }

    export function getActionTypesDataCache(predicate?: (elem: ActionType) => boolean): Promise<ActionType[]> {
        return getCollectionItems(actionTypesCache, predicate);
    }
}

export function getCollectionItems<TItem>(cache: ItemCacheBase<TItem[]>, predicate?: (elem: TItem) => boolean, key?: string): Promise<TItem[]> {

    if (predicate) {
        return cache.getItem(key).then(items => {
            return ArrayHelper.queryable.from(items).where(predicate).toArray();
        });
    } else
        return cache.getItem(key);
}