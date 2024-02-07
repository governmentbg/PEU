import { EAUBaseValidator } from "eau-core";
import { DocumentFormValidationContext } from "..";
import { ActionsTakenMessageVM } from "../..";

export class ActionsTakenMessageValidator extends EAUBaseValidator<ActionsTakenMessageVM, DocumentFormValidationContext> {
    constructor() {
        super();

    }
}