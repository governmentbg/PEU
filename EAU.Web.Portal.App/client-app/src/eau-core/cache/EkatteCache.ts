import { ItemCacheBase } from "cnsys-core";
import { NomenclaturesDataService } from "../services/NomenclaturesDataService";
import { Ekatte } from "../models";

export class EkatteCache extends ItemCacheBase<Ekatte[]> {

    protected generateValue(key: string): Promise<Ekatte[]> {
        var nomenclaturesDataService = new NomenclaturesDataService();

        return nomenclaturesDataService.getEkatte();
    }
}