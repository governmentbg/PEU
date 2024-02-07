import { EAUBaseValidator } from "eau-core";
import { DocumentFormValidationContext } from "..";
import { ReceiptAcknowledgedPaymentForMOIVM } from "../..";

export class ReceiptAcknowledgedPaymentForMOIValidator extends EAUBaseValidator<ReceiptAcknowledgedPaymentForMOIVM, DocumentFormValidationContext> {
    constructor() {
        super();

    }
}