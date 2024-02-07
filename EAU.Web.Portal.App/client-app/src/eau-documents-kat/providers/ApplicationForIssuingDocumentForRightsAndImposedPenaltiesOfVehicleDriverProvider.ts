import { IDocumentProvider } from 'eau-documents'
import { ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverManager } from '../form-managers/ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverManager'
import { ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverValidator } from '../validations/forms/ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverValidator'

export const applicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverProvider: IDocumentProvider = {
    getDocumentFormManager: () => new ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverManager(),
    getValidator: () => new ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverValidator()
}