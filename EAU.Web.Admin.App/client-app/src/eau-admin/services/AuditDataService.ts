import { LogAction } from 'eau-admin/models/LogAction';
import { LogActionSearchCriteria } from 'eau-admin/models/LogActionSearchCriteria';
import { EAUBaseDataService } from 'eau-core';

export class AuditDataService extends EAUBaseDataService {

    protected baseUrl(): string {
        return super.baseUrl();
    }

    /**
     * Взима списък с одитни записи
     * @param searchCriteria 
     */
    public getServiceLimits(searchCriteria: LogActionSearchCriteria) : Promise<LogAction[]>{
        return this.get<LogAction[]>("LogActions", LogAction, searchCriteria).then(function (result: LogAction[]) {
            searchCriteria.count = this.jqXHR.getResponseHeader('count') ? this.jqXHR.getResponseHeader('count') : 0;
            return result;
        });
     }
    
} 