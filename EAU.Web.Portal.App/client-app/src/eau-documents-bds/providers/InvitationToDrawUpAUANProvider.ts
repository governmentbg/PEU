import { IDocumentProvider } from 'eau-documents';
import { InvitationToDrawUpAUANManager } from '../form-managers/InvitationToDrawUpAUANManager';
import { InvitationToDrawUpAUANValidator } from '../validations/forms/InvitationToDrawUpAUANValidator';

export const InvitationToDrawUpAUANProvider: IDocumentProvider = {
    getDocumentFormManager: () => new InvitationToDrawUpAUANManager(),
    getValidator: () => new InvitationToDrawUpAUANValidator()
}