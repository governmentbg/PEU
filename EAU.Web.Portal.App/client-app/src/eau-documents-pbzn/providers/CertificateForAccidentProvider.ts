import { IDocumentProvider } from "eau-documents";
import { CertificateForAccidentManager } from "../form-managers/CertificateForAccidentManager";
import { CertificateForAccidentValidator } from "../validations/forms/CertificateForAccidentValidator";

export const certificateForAccidentProvider: IDocumentProvider = {
    getDocumentFormManager: () => new CertificateForAccidentManager(),
    getValidator: () => new CertificateForAccidentValidator()
}