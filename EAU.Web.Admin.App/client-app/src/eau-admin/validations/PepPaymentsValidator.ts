﻿import { ObjectHelper } from "cnsys-core";
import { EAUBaseValidator } from "eau-core";
import { RegistrationData } from "../models/ModelsAutoGenerated";

export class PepPaymentsValidator extends EAUBaseValidator<RegistrationData, any> {

    constructor() {
        super();

        this.ruleFor(x => x.description).notEmpty().withMessage(this.getMessage('GL_INPUT_FIELD_MUST_E'));
        this.ruleFor(x => x.secretWord).notEmpty().withMessage(this.getMessage('GL_INPUT_FIELD_MUST_E'));
        this.ruleFor(x => x.cin).notEmpty().withMessage(this.getMessage('GL_INPUT_FIELD_MUST_E')); 
        this.ruleFor(x => x.notificationUrl).notEmpty().withMessage(this.getMessage('GL_INPUT_FIELD_MUST_E'));
        this.ruleFor(x => x.portalUrl).notEmpty().withMessage(this.getMessage('GL_INPUT_FIELD_MUST_E'));
        this.ruleFor(x => x.serviceUrl).notEmpty().withMessage(this.getMessage('GL_INPUT_FIELD_MUST_E'));
        this.ruleFor(x => x.validityPeriod).notEmpty().withMessage(this.getMessage('GL_INPUT_FIELD_MUST_E'));
        this.ruleFor(x => x.iban).notEmpty().withMessage(this.getMessage('GL_INPUT_FIELD_MUST_E'));
    }

    public validate(obj: RegistrationData): boolean {

        let isValid = super.validate(obj);

        if (ObjectHelper.isNullOrUndefined(obj.description)
            || ObjectHelper.isNullOrUndefined(obj.secretWord)
            || ObjectHelper.isNullOrUndefined(obj.cin)
            || ObjectHelper.isNullOrUndefined(obj.notificationUrl)
            || ObjectHelper.isNullOrUndefined(obj.portalUrl)
            || ObjectHelper.isNullOrUndefined(obj.serviceUrl)
            || ObjectHelper.isNullOrUndefined(obj.validityPeriod)
            || ObjectHelper.isNullOrUndefined(obj.iban)
            ) {

            obj.addError(this.getMessage("GL_INVALID_MANDATORY_DATA_E"));
            isValid = false;
        }

        return isValid;
    }
}

