export interface IResourcesDataService {
    getResources(langCode: string): Promise<any>;
}