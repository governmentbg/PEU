import { EAUBaseValidator, ErrMsgCodesConstants, ResourceHelpers } from "eau-core";
import { DocumentFormValidationContext } from "..";
import { RefusalVM } from "../..";

export class RefusalValidator extends EAUBaseValidator<RefusalVM, DocumentFormValidationContext> {
    constructor() {
        super();

        this.ruleFor(m => m.refusalContent).notEmpty()
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.DefaultNotEmptyErrorMessage, new RefusalVM(), 'refusalContent'));
    }
}
