import { IDocumentProvider } from 'eau-documents'
import { ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchantsManager } from '../form-managers/ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchantsManager'
import { ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchantsValidator } from '../validations/forms/ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchantsValidator'

export const ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchantsProvider: IDocumentProvider = {
    getDocumentFormManager:() => new ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchantsManager(),
    getValidator: () => new ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchantsValidator()
}