import { ItemCacheBase } from "cnsys-core";
import { NomenclaturesDataService } from "../services/NomenclaturesDataService";
import { Grao } from "../models";

export class GraoCache extends ItemCacheBase<Grao[]> {

    protected generateValue(key: string): Promise<Grao[]> {
        var nomenclaturesDataService = new NomenclaturesDataService();

        return nomenclaturesDataService.getGrao();
    }
}