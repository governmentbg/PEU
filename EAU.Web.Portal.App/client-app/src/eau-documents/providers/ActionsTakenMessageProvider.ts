import { IDocumentProvider } from './DocumentProvider';
import { ActionsTakenMessageManager } from '..';
import { ActionsTakenMessageValidator } from '../validations/forms/ActionsTakenMessageValidator';

export const ActionsTakenMessageProvider: IDocumentProvider = {
    getDocumentFormManager: () => new ActionsTakenMessageManager(),
    getValidator: () => new ActionsTakenMessageValidator()
}