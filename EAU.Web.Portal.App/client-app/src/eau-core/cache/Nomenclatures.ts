import { ArrayHelper, ItemCacheBase } from "cnsys-core";
import {
    Service,
    DocumentType,
    Language,
    Declaration,
    Ekatte,
    Grao,
    ServiceGroup,
    Functionality,
    Label,
    DeliveryChannel,
    Country,
    DocumentTemplate,
    DocumentTemplateField
} from "../models";
import { DocumentTypesCache } from "./DocumentTypesCache";
import { ServicesCache } from "./ServicesCache";
import { LanguagesCache } from "./LanguagesCache";
import { DeclarationsCache } from "./DeclarationsCache";
import { EkatteCache } from "./EkatteCache";
import { GraoCache } from "./GraoCache";
import { ServicesGroupsCache } from "./ServicesGroupsCache";
import { FunctionalitiesCache } from "./FunctionalitiesCache";
import { LabelsCache } from "./LabelsCache";
import { DeliveryChannelCache } from "./DeliveryChannelsCache";
import { CountriesCache } from "./CountriesCache";
import { DocumentTemplatesCache } from "./DocumentTemplatesCache";
import { DocumentTemplateFieldsCache } from "./DocumentTemplateFieldsCache";

let documentTypesCache: DocumentTypesCache = new DocumentTypesCache();
let servicesCache: ServicesCache = new ServicesCache();
let servicesGroupsCache: ServicesGroupsCache = new ServicesGroupsCache();
let languageCache: LanguagesCache = new LanguagesCache();
let declarationsCache: DeclarationsCache = new DeclarationsCache();
let ekatteCache = new EkatteCache();
let graoCache = new GraoCache();
let functionalitiesCache = new FunctionalitiesCache();
let labelsCache = new LabelsCache();
let deliveryChannelsCache = new DeliveryChannelCache();
let countyCache = new CountriesCache();
let docTemplatesCache = new DocumentTemplatesCache();
let docTemplateFieldsCache = new DocumentTemplateFieldsCache();

export namespace Nomenclatures {

    export function getServices(predicate?: (elem: Service) => boolean): Promise<Service[]> {
        return getCollectionItems(servicesCache, predicate);
    }

    export function getServicesGroups(predicate?: (elem: ServiceGroup) => boolean): Promise<ServiceGroup[]> {
        return getCollectionItems(servicesGroupsCache, predicate);
    }

    export function getDocumentTypes(predicate?: (elem: DocumentType) => boolean): Promise<DocumentType[]> {
        return getCollectionItems(documentTypesCache, predicate);
    }

    export function getLanguages(predicate?: (elem: Language) => boolean): Promise<Language[]> {
        return getCollectionItems(languageCache, predicate);
    }

    export function getLabels(predicate?: (elem: Label) => boolean): Promise<Label[]> {
        return getCollectionItems(labelsCache, predicate);
    }

    export function getDeclarations(predicate?: (elem: Declaration) => boolean): Promise<Declaration[]> {
        return getCollectionItems(declarationsCache, predicate);
    }

    export function getEkattes(predicate?: (elem: Ekatte) => boolean): Promise<Ekatte[]> {
        return getCollectionItems(ekatteCache, predicate);
    }

    export function getGrao(predicate?: (elem: Grao) => boolean): Promise<Grao[]> {
        return getCollectionItems(graoCache, predicate);
    }

    export function getFunctionalities(predicate?: (elem: Functionality) => boolean): Promise<Functionality[]> {
        return getCollectionItems(functionalitiesCache, predicate);
    }

    export function getDeliveryChannels(predicate?: (elem: DeliveryChannel) => boolean): Promise<DeliveryChannel[]> {
        return getCollectionItems(deliveryChannelsCache, predicate);
    }

    export function getCountries(predicate?: (elem: Country) => boolean): Promise<Country[]> {
        return getCollectionItems(countyCache, predicate);
    }

    export function getDocumentTemplates(predicate?: (elem: DocumentTemplate) => boolean): Promise<DocumentTemplate[]> {
        return getCollectionItems(docTemplatesCache, predicate);
    }

    export function getDocumentTemplateFields(predicate?: (elem: DocumentTemplateField) => boolean): Promise<DocumentTemplateField[]> {
        return getCollectionItems(docTemplateFieldsCache, predicate);
    }
}

export function getCollectionItems<TItem>(cache: ItemCacheBase<TItem[]>, predicate?: (elem: TItem) => boolean, key?: string): Promise<TItem[]> {
    if (predicate) {
        return cache.getItem(key).then(items => {           
            return ArrayHelper.queryable.from(items).where(predicate).toArray();
        });
    }
    else {
        return cache.getItem(key);
    }
}