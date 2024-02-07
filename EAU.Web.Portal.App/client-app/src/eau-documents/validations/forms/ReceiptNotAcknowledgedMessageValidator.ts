import { EAUBaseValidator } from "eau-core";
import { DocumentFormValidationContext } from "..";
import { ReceiptNotAcknowledgedMessageVM } from "../..";

export class ReceiptNotAcknowledgedMessageValidator extends EAUBaseValidator<ReceiptNotAcknowledgedMessageVM, DocumentFormValidationContext> {
    constructor() {
        super();

    }
}