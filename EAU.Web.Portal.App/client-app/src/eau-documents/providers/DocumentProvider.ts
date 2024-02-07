import { IDocumentFormManager, DocumentFormValidationContext, DocumentFormVMBase } from "..";
import { EAUBaseValidator } from "eau-core";

export interface IDocumentProvider {
    getDocumentFormManager:()=> IDocumentFormManager,
    getValidator:()=> EAUBaseValidator<DocumentFormVMBase, DocumentFormValidationContext>
}