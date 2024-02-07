import { IDocumentProvider } from './DocumentProvider';
import { TerminationOfServiceMessageManager } from '..';
import { TerminationOfServiceMessageValidator } from '../validations/forms/TerminationOfServiceMessageValidator';

export const TerminationOfServiceMessageProvider: IDocumentProvider = {
    getDocumentFormManager: () => new TerminationOfServiceMessageManager(),
    getValidator: () => new TerminationOfServiceMessageValidator()
}