import { EAUBaseValidator } from "eau-core";
import { DocumentFormValidationContext } from "..";
import { ReceiptAcknowledgedMessageVM } from "../..";

export class ReceiptAcknowledgedMessageValidator extends EAUBaseValidator<ReceiptAcknowledgedMessageVM, DocumentFormValidationContext> {
    constructor() {
        super();

    }
}