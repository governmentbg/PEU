import { IDocumentProvider } from 'eau-documents'
import { NotificationForNonFirearmManager } from '../form-managers/NotificationForNonFirearmManager'
import { NotificationForNonFirearmValidator } from '../validations/forms/NotificationForNonFirearmValidator'

export const NotificationForNonFirearmProvider: IDocumentProvider = {
    getDocumentFormManager: () => new NotificationForNonFirearmManager(),
    getValidator: () => new NotificationForNonFirearmValidator()
}