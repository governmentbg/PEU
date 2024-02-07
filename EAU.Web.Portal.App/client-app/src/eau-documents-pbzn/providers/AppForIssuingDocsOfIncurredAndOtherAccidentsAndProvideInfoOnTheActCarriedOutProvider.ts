import { IDocumentProvider } from 'eau-documents'
import { AppForIssuingDocsOfIncurredAndOtherAccidentsAndProvideInfoOnTheActCarriedOutManager } from '../form-managers/AppForIssuingDocsOfIncurredAndOtherAccidentsAndProvideInfoOnTheActCarriedOutManager'
import { AppForIssuingDocsOfIncurredAndOtherAccidentsAndProvideInfoOnTheActCarriedOutValidator } from '../validations/forms/AppForIssuingDocsOfIncurredAndOtherAccidentsAndProvideInfoOnTheActCarriedOutValidator'

export const appForIssuingDocsOfIncurredAndOtherAccidentsAndProvideInfoOnTheActCarriedOutProvider: IDocumentProvider = {
    getDocumentFormManager: () => new AppForIssuingDocsOfIncurredAndOtherAccidentsAndProvideInfoOnTheActCarriedOutManager(),
    getValidator: () => new AppForIssuingDocsOfIncurredAndOtherAccidentsAndProvideInfoOnTheActCarriedOutValidator()
}