import { IDocumentProvider } from 'eau-documents'
import { NotificationForConcludingOrTerminatingEmploymentContractManager } from '../form-managers/NotificationForConcludingOrTerminatingEmploymentContractManager'
import { NotificationForConcludingOrTerminatingEmploymentContractValidator } from '../validations/forms/NotificationForConcludingOrTerminatingEmploymentContractValidator'

export const NotificationForConcludingOrTerminatingEmploymentContractProvider: IDocumentProvider = {
    getDocumentFormManager: () => new NotificationForConcludingOrTerminatingEmploymentContractManager(),
    getValidator: () => new NotificationForConcludingOrTerminatingEmploymentContractValidator()
}