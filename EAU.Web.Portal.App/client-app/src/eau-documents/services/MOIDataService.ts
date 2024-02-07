import { EAUBaseDataService } from 'eau-core';
import { PersonInfo } from '../models/ModelsManualAdded';

export class MOIDataService extends EAUBaseDataService {

    protected baseUrl(): string {
        return super.baseUrl() + "MOI";
    }

    public getPersonsInfo(pid: string): Promise<PersonInfo> {
        return this.get<PersonInfo>(`/NRBLD/PersonsInfo/${pid}`, PersonInfo);
    }
}