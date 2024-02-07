import { IDocumentProvider } from 'eau-documents'
import { ApplicationForChangeRegistrationOfVehiclesManager } from '../form-managers/ApplicationForChangeRegistrationOfVehiclesManager'
import { ApplicationForChangeRegistrationOfVehiclesValidator } from '../validations/forms/ApplicationForChangeRegistrationOfVehiclesValidator'

export const ApplicationForChangeRegistrationOfVehiclesProvider: IDocumentProvider = {
    getDocumentFormManager: () => new ApplicationForChangeRegistrationOfVehiclesManager(),
    getValidator: () => new ApplicationForChangeRegistrationOfVehiclesValidator()
}