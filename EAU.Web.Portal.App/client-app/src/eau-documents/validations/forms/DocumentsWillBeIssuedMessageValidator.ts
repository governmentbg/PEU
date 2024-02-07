import { EAUBaseValidator } from "eau-core";
import { DocumentFormValidationContext } from "..";
import { DocumentsWillBeIssuedMessageVM } from "../..";

export class DocumentsWillBeIssuedMessageValidator extends EAUBaseValidator<DocumentsWillBeIssuedMessageVM, DocumentFormValidationContext> {
    constructor() {
        super();

    }
}