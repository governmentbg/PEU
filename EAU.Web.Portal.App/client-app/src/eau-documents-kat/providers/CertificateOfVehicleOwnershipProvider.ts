import { IDocumentProvider } from "eau-documents";
import { CertificateOfVehicleOwnershipManager } from "../form-managers/CertificateOfVehicleOwnershipManager";
import { CertificateOfVehicleOwnershipValidator } from "../validations/forms/CertificateOfVehicleOwnershipValidator";


export const CertificateOfVehicleOwnershipProvider: IDocumentProvider = {
    getDocumentFormManager: () => new CertificateOfVehicleOwnershipManager(),
    getValidator: () => new CertificateOfVehicleOwnershipValidator()
}