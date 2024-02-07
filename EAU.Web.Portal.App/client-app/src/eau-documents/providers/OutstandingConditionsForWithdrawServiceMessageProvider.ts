import { IDocumentProvider } from './DocumentProvider';
import { OutstandingConditionsForWithdrawServiceMessageManager } from '..';
import { OutstandingConditionsForWithdrawServiceMessageValidator } from '../validations/forms/OutstandingConditionsForWithdrawServiceMessageValidator';

export const OutstandingConditionsForWithdrawServiceMessageProvider: IDocumentProvider = {
    getDocumentFormManager: () => new OutstandingConditionsForWithdrawServiceMessageManager(),
    getValidator: () => new OutstandingConditionsForWithdrawServiceMessageValidator()
}