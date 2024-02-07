import { IDocumentProvider } from 'eau-documents'
import { ApplicationForWithdrawServiceManager } from '../form-managers/ApplicationForWithdrawServiceManager'
import { ApplicationForWithdrawServiceValidator } from '../validations/forms/ApplicationForWithdrawServiceValidator'

export const ApplicationForWithdrawServiceProvider: IDocumentProvider = {
    getDocumentFormManager: () => new ApplicationForWithdrawServiceManager(),
    getValidator: () => new ApplicationForWithdrawServiceValidator()
}