import { IDocumentProvider } from 'eau-documents';
import { DeclarationForLostSRPPManager } from '../form-managers/DeclarationForLostSRPPManager';
import { DeclarationForLostSRPPSValidator } from '../validations/forms/DeclarationForLostSRPPSValidator';

export const declarationForLostSRPPProvider: IDocumentProvider = {
    getDocumentFormManager: () => new DeclarationForLostSRPPManager(),
    getValidator: () => new DeclarationForLostSRPPSValidator()
}