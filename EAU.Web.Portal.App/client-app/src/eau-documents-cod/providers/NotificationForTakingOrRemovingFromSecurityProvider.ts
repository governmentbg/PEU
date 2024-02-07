import { IDocumentProvider } from 'eau-documents'
import { NotificationForTakingOrRemovingFromSecurityManager } from '../form-managers/NotificationForTakingOrRemovingFromSecurityManager'
import { NotificationForTakingOrRemovingFromSecurityValidator } from '../validations/forms/NotificationForTakingOrRemovingFromSecurityValidator'

export const NotificationForTakingOrRemovingFromSecurityProvider: IDocumentProvider = {
    getDocumentFormManager: () => new NotificationForTakingOrRemovingFromSecurityManager(),
    getValidator: () => new NotificationForTakingOrRemovingFromSecurityValidator()
}