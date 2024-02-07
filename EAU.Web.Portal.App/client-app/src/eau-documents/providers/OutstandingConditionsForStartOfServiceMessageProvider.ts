import { IDocumentProvider } from './DocumentProvider';
import { OutstandingConditionsForStartOfServiceMessageManager } from '..';
import { OutstandingConditionsForStartOfServiceMessageValidator } from '../validations/forms/OutstandingConditionsForStartOfServiceMessageValidator';

export const OutstandingConditionsForStartOfServiceMessageProvider: IDocumentProvider = {
    getDocumentFormManager: () => new OutstandingConditionsForStartOfServiceMessageManager(),
    getValidator: () => new OutstandingConditionsForStartOfServiceMessageValidator()
}