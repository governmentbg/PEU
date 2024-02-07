import { ItemCacheBase } from 'cnsys-core';
import { Declaration } from '../models/ModelsManualAdded';
import { NomenclaturesDataService } from '../services/NomenclaturesDataService';

export class DeclarationsCache extends ItemCacheBase<Declaration[]> {

    protected generateValue(key: string): Promise<Declaration[]> {

        let nomenclaturesDataService = new NomenclaturesDataService();

        return nomenclaturesDataService.getDeclarations();
    }
}