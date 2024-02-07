import { IDocumentProvider } from 'eau-documents'
import { ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensManager } from '../form-managers/ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensManager'
import { ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensValidator } from '../validations/forms/ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensValidator'

export const applicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensProvider: IDocumentProvider = {
    getDocumentFormManager: () => new ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensManager(),
    getValidator: () => new ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensValidator()
}