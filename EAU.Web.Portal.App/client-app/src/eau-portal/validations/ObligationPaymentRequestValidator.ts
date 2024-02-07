import { EAUBaseValidator, ObligedPersonIdentTypes } from "eau-core";
import { ObligationPaymentRequest } from "../models/ModelsManualAdded";

export class ObligationPaymentRequestValidator extends EAUBaseValidator<ObligationPaymentRequest, any> {

    constructor() {
        super();

        this.ruleFor(m => m.obligedPersonName).notEmpty().withMessage(this.getMessage("GL_INPUT_FIELD_MUST_E"));
        this.ruleFor(m => m.obligedPersonName).length(null, 26).withMessage(this.getMessage('GL_OBLIGATED_PERSON_NAME_MAX_LENGTH_E'));
        this.ruleFor(m => m.obligedPersonName).matches('^[a-zа-я0-9\\s-,.]+$', 'i').withMessage(this.getMessage('GL_ENTER_VALID_OBLIGATED_PERSON_NAME_E'));

        this.ruleFor(m => m.obligedPersonIdent).notEmpty().withMessage(this.getMessage("GL_INPUT_FIELD_MUST_E"))
            .when(m => m.obligedPersonIdentType == ObligedPersonIdentTypes.EGN
                || m.obligedPersonIdentType == ObligedPersonIdentTypes.LNC
                || m.obligedPersonIdentType == ObligedPersonIdentTypes.BULSTAT);

        this.ruleFor(m => m.obligedPersonIdent).isValidEGNLNCh().withMessage(this.getMessage("GL_INVALID_IDENT_E"))
            .when(m => m.obligedPersonIdentType == ObligedPersonIdentTypes.EGN
                || m.obligedPersonIdentType == ObligedPersonIdentTypes.LNC);

        this.ruleFor(m => m.obligedPersonIdent).isValidBULSTAT().withMessage(this.getMessage("GL_INVALID_IDENT_E"))
            .when(m => m.obligedPersonIdentType == ObligedPersonIdentTypes.BULSTAT);

        this.ruleFor(m => m.payerIdent).notEmpty().withMessage(this.getMessage("GL_INPUT_FIELD_MUST_E"));

        this.ruleFor(m => m.payerIdent).isValidEGNLNCh().withMessage(this.getMessage("GL_INVALID_IDENT_E"))
            .when(m => m.payerIdentType == ObligedPersonIdentTypes.EGN || m.payerIdentType == ObligedPersonIdentTypes.LNC);

        this.ruleFor(m => m.payerIdent).isValidBULSTAT().withMessage(this.getMessage("GL_INVALID_IDENT_E"))
            .when(m => m.payerIdentType == ObligedPersonIdentTypes.BULSTAT);
    }

    public validate(obj: ObligationPaymentRequest): boolean {
        let isValid = super.validate(obj);

        return isValid;
    }
}