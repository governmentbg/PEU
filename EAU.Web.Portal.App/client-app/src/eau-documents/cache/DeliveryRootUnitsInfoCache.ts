﻿import { ItemCacheBase } from "cnsys-core";
import { UnitInfo } from "../models/ModelsAutoGenerated";
import { NomenclaturesDataService } from "../services/NomenclaturesDataService";

export class DeliveryRootUnitsInfoCache extends ItemCacheBase<UnitInfo[]> {

    protected generateValue(key: string): Promise<UnitInfo[]> {
        let nomenclaturesDataService = new NomenclaturesDataService();
        let splitedKey = key.split('_');

        if (splitedKey.length > 1) {
            return nomenclaturesDataService.getDeliveryRootUnitsInfo(Number(splitedKey[0]), Number(splitedKey[1]));
        }

        return nomenclaturesDataService.getDeliveryRootUnitsInfo(Number(splitedKey[0]), null);
    }
}