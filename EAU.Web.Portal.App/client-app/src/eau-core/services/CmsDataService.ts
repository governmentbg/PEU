import { appConfig } from 'cnsys-core';
import { Page } from '../models';
import { EAUBaseDataService } from './EAUBaseDataService';

export class CmsDataService extends EAUBaseDataService {

    protected baseUrl(): string {
        return super.baseUrl() + "Pages";
    }

    public getPages(lang?: string, forceTranslated?: boolean): Promise<Page[]> {
        
        let queryParams = {};

        if (forceTranslated)
            queryParams = {forceTranslated: true}

        let currentLang = lang ? lang : appConfig.clientLanguage;

        return this.get<Page[]>(`/${currentLang}`, Page, queryParams);
    }
}