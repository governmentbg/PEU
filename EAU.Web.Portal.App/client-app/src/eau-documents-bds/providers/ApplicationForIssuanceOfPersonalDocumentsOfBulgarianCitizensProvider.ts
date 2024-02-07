import { IDocumentProvider } from 'eau-documents'
import { ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensManager } from '../form-managers/ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensManager'
import { ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensValidator } from '../validations/forms/ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensValidator'

export const applicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensProvider: IDocumentProvider = {
    getDocumentFormManager: () => new ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensManager(),
    getValidator: () => new ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensValidator()
}