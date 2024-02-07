import {ItemCacheBase} from "cnsys-core";
import { NomenclaturesDataService } from '../services/NomenclaturesDataService'
import {DeliveryChannel} from "../models"


export class DeliveryChannelCache extends ItemCacheBase<DeliveryChannel[]> {

    protected generateValue(key: string): Promise<DeliveryChannel[]> {
        var nomenclaturesDataService = new NomenclaturesDataService();

        return nomenclaturesDataService.getDeliveryChannels();
    }
}