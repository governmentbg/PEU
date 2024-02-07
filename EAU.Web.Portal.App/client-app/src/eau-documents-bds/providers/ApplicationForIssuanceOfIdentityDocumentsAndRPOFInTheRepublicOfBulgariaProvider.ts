import { IDocumentProvider } from 'eau-documents'
import { ApplicationForIssuanceOfIdentityDocumentsAndRPOFInTheRepublicOfBulgariaManager } from '../form-managers/ApplicationForIssuanceOfIdentityDocumentsAndRPOFInTheRepublicOfBulgariaManager'
import { ApplicationForIssuanceOfIdentityDocumentsAndRPOFInTheRepublicOfBulgariaValidator } from '../validations/forms/ApplicationForIssuanceOfIdentityDocumentsAndRPOFInTheRepublicOfBulgariaValidator'

export const applicationForIssuanceOfIdentityDocumentsAndRPOFInTheRepublicOfBulgariaProvider: IDocumentProvider = {
    getDocumentFormManager: () => new ApplicationForIssuanceOfIdentityDocumentsAndRPOFInTheRepublicOfBulgariaManager(),
    getValidator: () => new ApplicationForIssuanceOfIdentityDocumentsAndRPOFInTheRepublicOfBulgariaValidator()
}