import {ItemCacheBase} from "cnsys-core";
import {Label} from "../models";
import {NomenclaturesDataService} from "../services";



export class LabelsCache extends ItemCacheBase<Label[]>{
    protected generateValue(key: string): Promise<Label[]>{
        var nomenclaturesDataService = new NomenclaturesDataService();

        return nomenclaturesDataService.getLabels();
    }
}