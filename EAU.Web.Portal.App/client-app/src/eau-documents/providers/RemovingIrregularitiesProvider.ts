import { RemovingIrregularitiesInstructionsValidator } from "../validations/forms/RemovingIrregularitiesInstructionsValidator";
import { IDocumentProvider } from './DocumentProvider';
import { RemovingIrregularitiesManager } from "../form-managers/RemovingIrregularitiesManager";

export const RemovingIrregularitiesProvider: IDocumentProvider = {
    getDocumentFormManager: () => new RemovingIrregularitiesManager(),
    getValidator: () => new RemovingIrregularitiesInstructionsValidator()
}