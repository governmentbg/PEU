import { IDocumentProvider } from "eau-documents";
import { CertificateAttestingToTheRightsAndPenaltiesOfVehicleDriverManager } from "../form-managers/CertificateAttestingToTheRightsAndPenaltiesOfVehicleDriverManager";
import { CertificateAttestingToTheRightsAndPenaltiesOfVehicleDriverValidator } from "../validations/forms/CertificateAttestingToTheRightsAndPenaltiesOfVehicleDriverValidator";

export const certificateAttestingToTheRightsAndPenaltiesOfVehicleDriverProvider: IDocumentProvider = {
    getDocumentFormManager: () => new CertificateAttestingToTheRightsAndPenaltiesOfVehicleDriverManager(),
    getValidator: () => new CertificateAttestingToTheRightsAndPenaltiesOfVehicleDriverValidator()
}