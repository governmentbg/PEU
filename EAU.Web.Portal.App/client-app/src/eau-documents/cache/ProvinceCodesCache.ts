import { ItemCacheBase } from "cnsys-core";
import { NomenclaturesDataService } from "../services/NomenclaturesDataService";

export class ProvinceCodesCache extends ItemCacheBase<string[]> {

    protected generateValue(key: string): Promise<string[]> {
        let nomenclaturesDataService = new NomenclaturesDataService();

        var keys = key.split('_');

        return nomenclaturesDataService.getProvinceCodes(Number(keys[0]), Number(keys[1]));
    }
}