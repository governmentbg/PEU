import { ItemCacheBase } from 'cnsys-core'
import { NomenclaturesDataService } from '../services/NomenclaturesDataService'
import { DocumentType } from "../models"

export class DocumentTypesCache extends ItemCacheBase<DocumentType[]> {

    protected generateValue(key: string): Promise<DocumentType[]> {
        var nomenclaturesDataService = new NomenclaturesDataService();

        return nomenclaturesDataService.getDocumentTypes();
    }
}