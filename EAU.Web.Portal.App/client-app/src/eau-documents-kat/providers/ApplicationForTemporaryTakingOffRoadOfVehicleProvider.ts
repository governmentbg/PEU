import { IDocumentProvider } from 'eau-documents'
import { ApplicationForTemporaryTakingOffRoadOfVehicleManager } from '../form-managers/ApplicationForTemporaryTakingOffRoadOfVehicleManager'
import { ApplicationForTemporaryTakingOffRoadOfVehicleValidator } from '../validations/forms/ApplicationForTemporaryTakingOffRoadOfVehicleValidator'

export const ApplicationForTemporaryTakingOffRoadOfVehicleProvider: IDocumentProvider = {
    getDocumentFormManager:() => new ApplicationForTemporaryTakingOffRoadOfVehicleManager(),
    getValidator: () => new ApplicationForTemporaryTakingOffRoadOfVehicleValidator()
}