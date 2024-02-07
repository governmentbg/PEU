import { ApplicationConfig } from 'cnsys-core'

/**Базов интерфейс на EPZEU за конфигурация на статични параметри на приложението, конфигурират се в _Layout.cshtml*/
export interface ApplicationConfigEAU extends ApplicationConfig {    
    /**Период на запазване на черновата на потребителя */
    docSaveIntervalInMs: number;

    /**Домейн адрес за сетване в cookie */
    commonCookieDomain: string;

    /**Пероид на неактивност на потребителска сесия*/
    userInactivityTimeout: number;

    /**Възможи серии за задължения по Фиш */
    possibleKATObligationsFishSeries: string[];

    webHelpUrl: string;

    webHelpConfig: string;

    /**Адрес на сървъра за идентичност */
    idsrvURL: string;
}

declare var applicationConfig: ApplicationConfigEAU;

export var appConfig = applicationConfig;