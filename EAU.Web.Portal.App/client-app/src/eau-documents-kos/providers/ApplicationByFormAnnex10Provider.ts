import { IDocumentProvider } from 'eau-documents'
import { ApplicationByFormAnnex10Manager } from '../form-managers/ApplicationByFormAnnex10Manager'
import { ApplicationByFormAnnex10Validator } from '../validations/forms/ApplicationByFormAnnex10Validator'

export const applicationByFormAnnex10Provider: IDocumentProvider = {
    getDocumentFormManager: () => new ApplicationByFormAnnex10Manager(),
    getValidator: () => new ApplicationByFormAnnex10Validator()
}