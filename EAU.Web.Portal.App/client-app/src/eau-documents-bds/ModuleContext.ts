import { TypedModuleContext, BaseModuleInitializaitonContext, LocalizationResources, LocalizationErorrs } from 'cnsys-core'

export interface BDSLocalizationErorrs extends LocalizationErorrs {
    ///**В полето "Номер на документа" може да се съдържат точно 9 цифри.*/
    //RegistrationDocumentErrorMessage: string;

    ///**В полето "Номер на документа" може да се съдържат точно буква на кирилица и 6 цифри.*/
    //RegistrationCertificateErrorMessage: string;

    ///**Не е направена проверка за "Притежател"*/
    //HolderErrorMessage: string;

    ///**Не е направена проверка за "Ползвател"*/
    //UserDataErrorMessage: string;

    ///**Ползвател" трябва да е различен от "Притежател"*/
    //UserDataHolderDiffUserErrorMessage: string

    ///**"Ползвател" трябва да е различен от всички нови собственици"*/
    //UserMustBeDifferentFromAllOwnersErrorMessage: string;

    ////**Не са въведени данни за Ползвател*/
    //NotEnteredUserData: string;

    ////**Не са въведени данни за Притежател*/
    //NotEnteredHolderData: string;
}

class ModuleContext extends TypedModuleContext<BaseModuleInitializaitonContext, LocalizationResources, BDSLocalizationErorrs> {    
    public get moduleName(): string {
        return 'eau-documents-bds';
    }    
}

export const moduleContext = new ModuleContext();