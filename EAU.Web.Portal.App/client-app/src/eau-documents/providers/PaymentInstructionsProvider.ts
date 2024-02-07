import { IDocumentProvider } from './DocumentProvider';
import { PaymentInstructionsManager } from '..';
import { PaymentInstructionsValidator } from '../validations/forms/PaymentInstructionsValidator';

export const PaymentInstructionsProvider: IDocumentProvider = {
    getDocumentFormManager: () => new PaymentInstructionsManager(),
    getValidator: () => new PaymentInstructionsValidator()
}