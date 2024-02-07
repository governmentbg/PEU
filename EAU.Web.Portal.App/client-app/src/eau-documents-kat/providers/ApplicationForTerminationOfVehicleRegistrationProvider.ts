import { IDocumentProvider } from 'eau-documents'
import { ApplicationForTerminationOfVehicleRegistrationManager } from '../form-managers/ApplicationForTerminationOfVehicleRegistrationManager'
import { ApplicationForTerminationOfVehicleRegistrationValidator } from '../validations/forms/ApplicationForTerminationOfVehicleRegistrationValidator'

export const ApplicationForTerminationOfVehicleRegistrationProvider: IDocumentProvider = {
    getDocumentFormManager:() => new ApplicationForTerminationOfVehicleRegistrationManager(),
    getValidator: () => new ApplicationForTerminationOfVehicleRegistrationValidator()
}