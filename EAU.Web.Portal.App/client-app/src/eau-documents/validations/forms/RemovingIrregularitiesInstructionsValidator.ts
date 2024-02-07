import { EAUBaseValidator, ErrMsgCodesConstants, ResourceHelpers } from "eau-core";
import { DocumentFormValidationContext } from "..";
import { RemovingIrregularitiesInstructionsIrregularitiesVM, RemovingIrregularitiesInstructionsVM } from "../..";

export class RemovingIrregularitiesInstructionsValidator extends EAUBaseValidator<RemovingIrregularitiesInstructionsVM, DocumentFormValidationContext> {
    constructor() {
        super();

        this.ruleFor(m => m.irregularities).setCollectionValidator(new RemovingIrregularitiesInstructionsIrregularityValidator());
    }
}

export class RemovingIrregularitiesInstructionsIrregularityValidator extends EAUBaseValidator<RemovingIrregularitiesInstructionsIrregularitiesVM, DocumentFormValidationContext> {
    constructor() {
        super();

        this.ruleFor(m => m.irregularityType).notEmpty()
            .withMessage(ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.DefaultNotEmptyErrorMessage, new RemovingIrregularitiesInstructionsIrregularitiesVM(), 'irregularityType'));
    }        
}