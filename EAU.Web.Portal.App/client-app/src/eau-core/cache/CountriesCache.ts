import { ItemCacheBase } from 'cnsys-core'
import { NomenclaturesDataService } from '../services/NomenclaturesDataService'
import { Country } from "../models"

export class CountriesCache extends ItemCacheBase<Country[]> {

    protected generateValue(key: string): Promise<Country[]> {
        var nomenclaturesDataService = new NomenclaturesDataService();

        return nomenclaturesDataService.getCountries();
    }
}