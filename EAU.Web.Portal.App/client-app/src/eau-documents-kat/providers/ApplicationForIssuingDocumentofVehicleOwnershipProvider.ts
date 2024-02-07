import { IDocumentProvider } from 'eau-documents'
import { ApplicationForIssuingDocumentofVehicleOwnershipManager } from '../form-managers/ApplicationForIssuingDocumentofVehicleOwnershipManager'
import { ApplicationForIssuingDocumentofVehicleOwnershipValidator } from '../validations/forms/ApplicationForIssuingDocumentofVehicleOwnershipValidator'

export const applicationForIssuingDocumentofVehicleOwnershipProvider: IDocumentProvider = {
    getDocumentFormManager:() => new ApplicationForIssuingDocumentofVehicleOwnershipManager(),
    getValidator: () => new ApplicationForIssuingDocumentofVehicleOwnershipValidator()
}