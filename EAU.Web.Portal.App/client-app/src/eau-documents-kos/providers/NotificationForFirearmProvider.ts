import { IDocumentProvider } from 'eau-documents'
import { NotificationForFirearmManager } from '../form-managers/NotificationForFirearmManager'
import { NotificationForFirearmValidator } from '../validations/forms/NotificationForFirearmValidator'

export const NotificationForFirearmProvider: IDocumentProvider = {
    getDocumentFormManager: () => new NotificationForFirearmManager(),
    getValidator: () => new NotificationForFirearmValidator()
}