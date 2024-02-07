import { EAUBaseDataService, ServiceInstanceSearchCriteria } from 'eau-core';
import { CaseFileInfo, ServiceInstance, ServiceInstanceInfo } from '../models/ModelsManualAdded';

export class ServiceInstancesDataService extends EAUBaseDataService {

    protected baseUrl(): string {
        return super.baseUrl() + "Services";
    }

    public searchServiceInstances(criteria: ServiceInstanceSearchCriteria): Promise<ServiceInstance[]> {

        return this.get<ServiceInstance[]>(`Instances`, ServiceInstance, criteria, null).then(function (result: ServiceInstance[]) {
            criteria.count = this.jqXHR.getResponseHeader('count') ? this.jqXHR.getResponseHeader('count') : 0;
            return result;
        });
    }

    public getServiceInstancesInfo(serviceInstanceId: number): Promise<ServiceInstanceInfo> {

        return this.get<ServiceInstanceInfo>(`Instances/${serviceInstanceId}/Info`, ServiceInstanceInfo, null, null).then(function (result: ServiceInstanceInfo) {
            return result;
        });
    }

    public getServiceInstancesDocuments(serviceInstanceId: number): Promise<CaseFileInfo> {

        return this.get<CaseFileInfo>(`Instances/${serviceInstanceId}/CaseFile`, CaseFileInfo, null, null).then(function (result: CaseFileInfo) {
            return result;
        });
    }

    public getServiceInstancesDocumentDownloadUrl(serviceInstanceId: number, documentURI: string): string {
        return this.baseUrl() + `/Instances/${serviceInstanceId}/Documents/${documentURI}`
    }
}