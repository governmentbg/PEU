import { IDocumentProvider } from './DocumentProvider';
import { ReceiptAcknowledgedMessageManager } from '..';
import { ReceiptAcknowledgedMessageValidator } from '../validations/forms/ReceiptAcknowledgedMessageValidator';

export const ReceiptAcknowledgedMessageProvider: IDocumentProvider = {
    getDocumentFormManager: () => new ReceiptAcknowledgedMessageManager(),
    getValidator: () => new ReceiptAcknowledgedMessageValidator()
}