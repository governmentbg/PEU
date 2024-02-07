import { appConfig } from 'cnsys-core';
import { EAUBaseDataService } from './EAUBaseDataService';

export class ResourcesDataService extends EAUBaseDataService {

    protected baseUrl(): string {

        return super.baseUrl() + "localization/labels";
    }

    public getResources(prefixes: string[]): Promise<any> {
        return this.get<any>(`${appConfig.clientLanguage}`, null, { prefixes: prefixes ? prefixes.join(',') : null });
    }
}