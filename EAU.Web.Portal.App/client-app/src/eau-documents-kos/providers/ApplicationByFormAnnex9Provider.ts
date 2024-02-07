import { IDocumentProvider } from 'eau-documents'
import { ApplicationByFormAnnex9Manager } from '../form-managers/ApplicationByFormAnnex9Manager'
import { ApplicationByFormAnnex9Validator } from '../validations/forms/ApplicationByFormAnnex9Validator'

export const applicationByFormAnnex9Provider: IDocumentProvider = {
    getDocumentFormManager: () => new ApplicationByFormAnnex9Manager(),
    getValidator: () => new ApplicationByFormAnnex9Validator()
}