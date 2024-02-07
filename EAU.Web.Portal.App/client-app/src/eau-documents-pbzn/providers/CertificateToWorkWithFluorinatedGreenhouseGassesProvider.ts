import { IDocumentProvider } from "eau-documents";
import { CertificateToWorkWithFluorinatedGreenhouseGassesManager } from "../form-managers/CertificateToWorkWithFluorinatedGreenhouseGassesManager";
import { CertificateToWorkWithFluorinatedGreenhouseGassesValidator } from "../validations/forms/CertificateToWorkWithFluorinatedGreenhouseGassesValidator";

export const certificateToWorkWithFluorinatedGreenhouseGassesProvider: IDocumentProvider = {
    getDocumentFormManager: () => new CertificateToWorkWithFluorinatedGreenhouseGassesManager(),
    getValidator: () => new CertificateToWorkWithFluorinatedGreenhouseGassesValidator()
}