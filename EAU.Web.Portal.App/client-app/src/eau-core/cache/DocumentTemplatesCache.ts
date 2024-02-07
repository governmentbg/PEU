import { ItemCacheBase } from 'cnsys-core'
import { NomenclaturesDataService } from '../services/NomenclaturesDataService'
import { DocumentTemplate } from "../models"

export class DocumentTemplatesCache extends ItemCacheBase<DocumentTemplate[]> {

    protected generateValue(key: string): Promise<DocumentTemplate[]> {
        var nomenclaturesDataService = new NomenclaturesDataService();

        return nomenclaturesDataService.getDocumentTemplates();
    }
}