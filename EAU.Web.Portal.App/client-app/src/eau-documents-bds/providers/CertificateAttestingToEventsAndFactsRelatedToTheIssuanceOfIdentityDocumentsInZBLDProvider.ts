import { IDocumentProvider } from "eau-documents";
import { CertificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDManager } from "../form-managers/CertificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDManager";
import { CertificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDValidator } from "../validations/forms/CertificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDValidator";


export const certificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDProvider: IDocumentProvider = {
    getDocumentFormManager: () => new CertificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDManager(),
    getValidator: () => new CertificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDValidator()
}