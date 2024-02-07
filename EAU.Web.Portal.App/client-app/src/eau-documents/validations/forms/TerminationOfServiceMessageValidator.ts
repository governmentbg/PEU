import { EAUBaseValidator } from "eau-core";
import { DocumentFormValidationContext } from "..";
import { TerminationOfServiceMessageVM } from "../..";

export class TerminationOfServiceMessageValidator extends EAUBaseValidator<TerminationOfServiceMessageVM, DocumentFormValidationContext> {
    constructor() {
        super();

    }
}