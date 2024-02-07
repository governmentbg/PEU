import { EAUBaseValidator  } from "eau-core";
import { DocumentFormValidationContext } from "..";
import { OutstandingConditionsForStartOfServiceMessageVM } from "../..";

export class OutstandingConditionsForStartOfServiceMessageValidator extends EAUBaseValidator<OutstandingConditionsForStartOfServiceMessageVM, DocumentFormValidationContext> {
    constructor() {
        super();

    }
}