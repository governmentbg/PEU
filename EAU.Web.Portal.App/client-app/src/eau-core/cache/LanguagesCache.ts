import { ItemCacheBase } from 'cnsys-core'
import { NomenclaturesDataService } from '../services/NomenclaturesDataService'
import { Language } from "../models"

export class LanguagesCache extends ItemCacheBase<Language[]> {

    protected generateValue(key: string): Promise<Language[]> {
        var nomenclaturesDataService = new NomenclaturesDataService();

        return nomenclaturesDataService.getLanguages();
    }
}