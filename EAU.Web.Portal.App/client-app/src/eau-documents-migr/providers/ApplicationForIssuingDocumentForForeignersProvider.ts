import { IDocumentProvider } from 'eau-documents'
import { ApplicationForIssuingDocumentForForeignersManager } from '../form-managers/ApplicationForIssuingDocumentForForeignersManager'
import { ApplicationForIssuingDocumentForForeignersValidator } from '../validations/forms/ApplicationForIssuingDocumentForForeignersValidator'

export const applicationForIssuingDocumentForForeignersProvider: IDocumentProvider = {
    getDocumentFormManager: () => new ApplicationForIssuingDocumentForForeignersManager(),
    getValidator: () => new ApplicationForIssuingDocumentForForeignersValidator()
}