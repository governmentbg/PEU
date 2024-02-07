import { ItemCacheBase } from "cnsys-core";
import { AISKATNomenclatureItem } from "../models/ModelsManualAdded";
import { NomenclaturesDataService } from "../services/NomenclaturesDataService";

export class ReasonForIssuingDuplicateOfSRMPSCache extends ItemCacheBase<AISKATNomenclatureItem[]> {

    protected generateValue(key: string): Promise<AISKATNomenclatureItem[]> {
        let nomenclaturesDataService = new NomenclaturesDataService();

        return nomenclaturesDataService.getReasonForIssuingDuplicateOfSRMPS();
    }
}