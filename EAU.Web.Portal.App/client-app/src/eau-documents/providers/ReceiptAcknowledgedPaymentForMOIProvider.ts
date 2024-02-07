import { IDocumentProvider } from './DocumentProvider';
import { ReceiptAcknowledgedPaymentForMOIManager } from '..';
import { ReceiptAcknowledgedPaymentForMOIValidator } from '../validations/forms/ReceiptAcknowledgedPaymentForMOIValidator';

export const ReceiptAcknowledgedPaymentForMOIProvider: IDocumentProvider = {
    getDocumentFormManager: () => new ReceiptAcknowledgedPaymentForMOIManager(),
    getValidator: () => new ReceiptAcknowledgedPaymentForMOIValidator()
}