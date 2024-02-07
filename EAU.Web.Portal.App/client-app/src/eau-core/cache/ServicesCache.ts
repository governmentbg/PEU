import { ItemCacheBase } from 'cnsys-core'
import { NomenclaturesDataService } from '../services/NomenclaturesDataService'
import { Service } from "../models"

export class ServicesCache extends ItemCacheBase<Service[]> {

    protected generateValue(key: string): Promise<Service[]> {
        var nomenclaturesDataService = new NomenclaturesDataService();

        return nomenclaturesDataService.getServices();
    }
}