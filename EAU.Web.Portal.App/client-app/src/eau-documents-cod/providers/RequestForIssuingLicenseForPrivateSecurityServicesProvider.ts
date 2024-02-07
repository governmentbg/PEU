import { IDocumentProvider } from 'eau-documents'
import { RequestForIssuingLicenseForPrivateSecurityServicesManager } from '../form-managers/RequestForIssuingLicenseForPrivateSecurityServicesManager'
import { RequestForIssuingLicenseForPrivateSecurityServicesValidator } from '../validations/forms/RequestForIssuingLicenseForPrivateSecurityServicesValidator'

export const requestForIssuingLicenseForPrivateSecurityServicesProvider: IDocumentProvider = {
    getDocumentFormManager: () => new RequestForIssuingLicenseForPrivateSecurityServicesManager(),
    getValidator: () => new RequestForIssuingLicenseForPrivateSecurityServicesValidator()
}