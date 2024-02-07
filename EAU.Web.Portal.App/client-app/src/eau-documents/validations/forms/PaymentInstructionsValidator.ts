import { EAUBaseValidator, resourceManager } from "eau-core";
import { DocumentFormValidationContext } from "..";
import { PaymentInstructionsVM } from "../..";

export class PaymentInstructionsValidator extends EAUBaseValidator<PaymentInstructionsVM, DocumentFormValidationContext> {
    constructor() {
        super();

        this.ruleFor(m => m.amount).notEmpty()
            .withMessage(resourceManager.getResourceByKey("DOC_GL_PaymentData_AmountRequired_E"));
    }
}