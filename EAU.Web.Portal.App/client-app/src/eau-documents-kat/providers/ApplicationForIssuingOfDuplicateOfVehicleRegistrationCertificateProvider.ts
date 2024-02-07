import { IDocumentProvider } from 'eau-documents'
import { ApplicationForIssuingOfDuplicateOfVehicleRegistrationCertificateManager } from '../form-managers/ApplicationForIssuingOfDuplicateOfVehicleRegistrationCertificateManager'
import { ApplicationForIssuingOfDuplicateOfVehicleRegistrationCertificateValidator } from '../validations/forms/ApplicationForIssuingOfDuplicateOfVehicleRegistrationCertificateValidator'

export const ApplicationForIssuingOfDuplicateOfVehicleRegistrationCertificateProvider: IDocumentProvider = {
    getDocumentFormManager:() => new ApplicationForIssuingOfDuplicateOfVehicleRegistrationCertificateManager(),
    getValidator: () => new ApplicationForIssuingOfDuplicateOfVehicleRegistrationCertificateValidator()
}