import { IDocumentProvider } from 'eau-documents'
import { ApplicationForCommissioningTemporarilySuspendedVehicleManager } from '../form-managers/ApplicationForCommissioningTemporarilySuspendedVehicleManager'
import { ApplicationForCommissioningTemporarilySuspendedVehicleValidator } from '../validations/forms/ApplicationForCommissioningTemporarilySuspendedVehicleValidator'

export const ApplicationForCommissioningTemporarilySuspendedVehicleProvider: IDocumentProvider = {
    getDocumentFormManager:() => new ApplicationForCommissioningTemporarilySuspendedVehicleManager(),
    getValidator: () => new ApplicationForCommissioningTemporarilySuspendedVehicleValidator()
}