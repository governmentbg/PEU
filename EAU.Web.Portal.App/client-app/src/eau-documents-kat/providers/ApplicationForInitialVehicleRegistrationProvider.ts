import { IDocumentProvider } from 'eau-documents'
import { ApplicationForInitialVehicleRegistrationManager } from '../form-managers/ApplicationForInitialVehicleRegistrationManager'
import { ApplicationForInitialVehicleRegistrationValidator } from '../validations/forms/ApplicationForInitialVehicleRegistrationValidator'

export const ApplicationForInitialVehicleRegistrationProvider: IDocumentProvider = {
    getDocumentFormManager:() => new ApplicationForInitialVehicleRegistrationManager(),
    getValidator: () => new ApplicationForInitialVehicleRegistrationValidator()
}