import { IDocumentProvider } from './DocumentProvider';
import { ReceiptNotAcknowledgedMessageManager } from '..';
import { ReceiptNotAcknowledgedMessageValidator } from '../validations/forms/ReceiptNotAcknowledgedMessageValidator';

export const ReceiptNotAcknowledgedMessageProvider: IDocumentProvider = {
    getDocumentFormManager: () => new ReceiptNotAcknowledgedMessageManager(),
    getValidator: () => new ReceiptNotAcknowledgedMessageValidator()
}