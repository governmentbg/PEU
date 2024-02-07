import { IDocumentProvider } from 'eau-documents'
import { RequestForApplicationForIssuingDuplicateDrivingLicenseManager } from '../form-managers/RequestForApplicationForIssuingDuplicateDrivingLicenseManager'
import { RequestForApplicationForIssuingDuplicateDrivingLicenseValidator } from '../validations/forms/RequestForApplicationForIssuingDuplicateDrivingLicenseValidator'

export const requestForApplicationForIssuingDuplicateDrivingLicenseProvider: IDocumentProvider = {
    getDocumentFormManager: () => new RequestForApplicationForIssuingDuplicateDrivingLicenseManager(),
    getValidator: () => new RequestForApplicationForIssuingDuplicateDrivingLicenseValidator()
}