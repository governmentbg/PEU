import { IDocumentProvider } from './DocumentProvider';
import { DocumentsWillBeIssuedMessageManager } from '..';
import { DocumentsWillBeIssuedMessageValidator } from '../validations/forms/DocumentsWillBeIssuedMessageValidator';

export const DocumentsWillBeIssuedMessageProvider: IDocumentProvider = {
    getDocumentFormManager: () => new DocumentsWillBeIssuedMessageManager(),
    getValidator: () => new DocumentsWillBeIssuedMessageValidator()
}