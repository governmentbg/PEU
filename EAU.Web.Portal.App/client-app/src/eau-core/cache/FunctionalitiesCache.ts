import { ItemCacheBase } from 'cnsys-core';
import { Functionality } from "../models";
import { FunctionalitiesDataService } from '../services/FunctionalitiesDataService';

export class FunctionalitiesCache extends ItemCacheBase<Functionality[]> {

    protected generateValue(key: string): Promise<Functionality[]> {

        let nomenclaturesDataService = new FunctionalitiesDataService();

        return nomenclaturesDataService.getFunctionalities();
    }
}