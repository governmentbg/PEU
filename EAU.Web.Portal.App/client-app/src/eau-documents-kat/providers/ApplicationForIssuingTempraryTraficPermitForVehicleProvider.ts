import { IDocumentProvider } from 'eau-documents'
import { ApplicationForIssuingTempraryTraficPermitForVehicleManager } from '../form-managers/ApplicationForIssuingTempraryTraficPermitForVehicleManager'
import { ApplicationForIssuingTempraryTraficPermitForVehicleValidator } from '../validations/forms/ApplicationForIssuingTempraryTraficPermitForVehicleValidator'

export const ApplicationForIssuingTempraryTraficPermitForVehicleProvider: IDocumentProvider = {
    getDocumentFormManager:() => new ApplicationForIssuingTempraryTraficPermitForVehicleManager(),
    getValidator: () => new ApplicationForIssuingTempraryTraficPermitForVehicleValidator()
}