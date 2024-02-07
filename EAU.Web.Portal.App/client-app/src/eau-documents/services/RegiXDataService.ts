import { EAUBaseDataService } from 'eau-core';
import { RegiXEntityData } from '../models/ModelsManualAdded';

export class RegiXDataService extends EAUBaseDataService {

    protected baseUrl(): string {
        return super.baseUrl() + "RegiX";
    }

    public getEntityData(identifier: string): Promise<RegiXEntityData> {
        return this.get<RegiXEntityData>(`${identifier}/EntityData`, RegiXEntityData);
    }
}