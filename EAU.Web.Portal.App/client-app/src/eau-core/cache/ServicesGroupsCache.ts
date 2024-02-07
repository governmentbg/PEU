import { ItemCacheBase } from 'cnsys-core'
import { NomenclaturesDataService } from '../services/NomenclaturesDataService'
import { ServiceGroup } from "../models"

export class ServicesGroupsCache extends ItemCacheBase<ServiceGroup[]> {

    protected generateValue(key: string): Promise<ServiceGroup[]> {
        var nomenclaturesDataService = new NomenclaturesDataService();

        return nomenclaturesDataService.getServiceGroups();
    }
}