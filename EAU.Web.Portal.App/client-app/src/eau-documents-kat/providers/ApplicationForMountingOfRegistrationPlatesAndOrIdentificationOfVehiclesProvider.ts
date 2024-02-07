import { IDocumentProvider } from 'eau-documents'
import { ApplicationForMountingOfRegistrationPlatesAndOrIdentificationOfVehiclesManager } from '../form-managers/ApplicationForMountingOfRegistrationPlatesAndOrIdentificationOfVehiclesManager'
import { ApplicationForMountingOfRegistrationPlatesAndOrIdentificationOfVehiclesValidator } from '../validations/forms/ApplicationForMountingOfRegistrationPlatesAndOrIdentificationOfVehiclesValidator'

export const ApplicationForMountingOfRegistrationPlatesAndOrIdentificationOfVehiclesProvider: IDocumentProvider = {
    getDocumentFormManager:() => new ApplicationForMountingOfRegistrationPlatesAndOrIdentificationOfVehiclesManager(),
    getValidator: () => new ApplicationForMountingOfRegistrationPlatesAndOrIdentificationOfVehiclesValidator()
}