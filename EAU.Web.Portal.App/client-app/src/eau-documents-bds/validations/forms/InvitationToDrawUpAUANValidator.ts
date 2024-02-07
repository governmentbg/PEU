﻿import { EAUBaseValidator, ResourceHelpers, ErrMsgCodesConstants } from "eau-core";
import { DocumentFormValidationContext } from "eau-documents";
import { InvitationToDrawUpAUANVM } from "../../models/ModelsAutoGenerated";

export class InvitationToDrawUpAUANValidator extends EAUBaseValidator<InvitationToDrawUpAUANVM, DocumentFormValidationContext> {
    constructor() {
        super();

        this.ruleFor(m => m.content).notEmpty()
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.DefaultNotEmptyErrorMessage, new InvitationToDrawUpAUANVM(), 'content'));
    }
}