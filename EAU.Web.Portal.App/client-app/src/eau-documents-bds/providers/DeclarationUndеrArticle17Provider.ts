import { IDocumentProvider } from 'eau-documents';
import { DeclarationUnderArticle17Manager } from '../form-managers/DeclarationUnderArticle17Manager';
import { DeclarationUnderArticle17Validator } from '../validations/forms/DeclarationUnderArticle17Validator';

export const declarationUnderArticle17Provider: IDocumentProvider = {
    getDocumentFormManager: () => new DeclarationUnderArticle17Manager(),
    getValidator: () => new DeclarationUnderArticle17Validator()
}