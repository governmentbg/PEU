import { IDocumentProvider } from 'eau-documents'
import { ApplicationForIssuingDriverLicenseManager } from '../form-managers/ApplicationForIssuingDriverLicenseManager'
import { ApplicationForIssuingDriverLicenseValidator } from '../validations/forms/ApplicationForIssuingDriverLicenseValidator'

export const ApplicationForIssuingDriverLicenseProvider: IDocumentProvider = {
    getDocumentFormManager: () => new ApplicationForIssuingDriverLicenseManager(),
    getValidator: () => new ApplicationForIssuingDriverLicenseValidator()
}