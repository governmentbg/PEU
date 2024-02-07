import { IDocumentProvider } from './DocumentProvider';
import { RefusalManager } from '..';
import { RefusalValidator } from '../validations/forms/RefusalValidator';

export const RefusalProvider: IDocumentProvider = {
    getDocumentFormManager: () => new RefusalManager(),
    getValidator: () => new RefusalValidator()
}