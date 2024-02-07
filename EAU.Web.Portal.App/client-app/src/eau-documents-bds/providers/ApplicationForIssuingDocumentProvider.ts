import { IDocumentProvider } from 'eau-documents'
import { ApplicationForIssuingDocumentManager } from '../form-managers/ApplicationForIssuingDocumentManager'
import { ApplicationForIssuingDocumentValidator } from '../validations/forms/ApplicationForIssuingDocumentValidator'

export const applicationForIssuingDocumentProvider: IDocumentProvider = {
    getDocumentFormManager: () => new ApplicationForIssuingDocumentManager(),
    getValidator: () => new ApplicationForIssuingDocumentValidator()
}