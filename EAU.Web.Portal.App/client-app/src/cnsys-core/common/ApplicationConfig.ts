/**Базов интерфайс за конфигурационни на статични парамтри на приложението, конфигурират се в Index.cshtml*/
export interface ApplicationConfig {
    /**Базов път до директория на сайта*/
    baseUrlName: string;
    maxRequestLengthInKB: number,
    acceptedFileExt: string
    gRecaptchaSiteKey: string;
    clientLanguage: string;
    loadingUIText: string;
    defaultPageSize: number;
    allowTestSign: boolean;
    version: string;
}

declare var applicationConfig: ApplicationConfig;

export var appConfig = applicationConfig;