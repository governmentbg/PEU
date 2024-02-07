import { appConfig } from '../common/ApplicationConfig';
import { Country, Declaration, DeliveryChannel, DocumentTemplate, DocumentTemplateField, DocumentType, Ekatte, Grao, Label, Language, Service, ServiceGroup } from '../models';
import { EAUBaseDataService } from './EAUBaseDataService';

export class NomenclaturesDataService extends EAUBaseDataService {

    protected baseUrl(): string {

        return super.baseUrl() + "Nomenclatures";
    }

    public getServices(lang?: string, forceTranslated?: boolean): Promise<Service[]> {

        let queryParams = {};

        if (forceTranslated)
            queryParams = { forceTranslated: true }

        return this.get<Service[]>(`Services/${lang ? lang : appConfig.clientLanguage}`, Service, queryParams, null);
    }

    public getServiceGroups(lang?: string, forceTranslated?: boolean): Promise<ServiceGroup[]> {

        let queryParams = {};

        if (forceTranslated)
            queryParams = { forceTranslated: true }

        return this.get<ServiceGroup[]>(`ServiceGroups/${lang ? lang : appConfig.clientLanguage}`, ServiceGroup, queryParams, null);
    }

    public getDocumentTypes(): Promise<DocumentType[]> {
        return this.get<DocumentType[]>("DocumentTypes", DocumentType);
    }

    public getLanguages(): Promise<Language[]> {
        return this.get<Language[]>("Languages", Language);
    }

    public getEkatte(): Promise<Ekatte[]> {
        return this.get<Ekatte[]>("Ekatte", Ekatte);
    }

    public getGrao(): Promise<Grao[]> {
        return this.get<Grao[]>("Grao", Grao);
    }

    public getLabels(lang?: string): Promise<Label[]> {
        return this.get<Label[]>(`Labels/${lang ? lang : appConfig.clientLanguage}`, Label);
    }

    public getDeclarations(): Promise<Declaration[]> {
        return this.get<Declaration[]>("Declarations", Declaration);
    }

    public getDocumentTemplates(): Promise<DocumentTemplate[]> {
        return this.get<DocumentTemplate[]>("DocumentTemplates", DocumentTemplate);
    }

    public getDocumentTemplateFields(): Promise<DocumentTemplateField[]> {
        return this.get<DocumentTemplateField[]>("DocumentTemplateFields", DocumentTemplateField);
    }

    public getDeliveryChannels(): Promise<DeliveryChannel[]> {
        return this.get<DeliveryChannel[]>("DeliveryChannels", DeliveryChannel);
    }

    public getCountries(): Promise<Country[]> {
        return this.get<Country[]>("Countries", Country);
    }
}