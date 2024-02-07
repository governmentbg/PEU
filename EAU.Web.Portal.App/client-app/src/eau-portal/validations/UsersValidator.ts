import { ObjectHelper } from "cnsys-core";
import { EAUBaseValidator, ResourceHelpers, resourceManager } from "eau-core";
import { UserInputModel } from "../models/ModelsManualAdded";

export class RegistrationValidator extends EAUBaseValidator<UserInputModel, any> {

    constructor() {
        super();

        this.ruleFor(x => x.email).notEmpty().withMessage(resourceManager.getResourceByKey("GL_INPUT_FIELD_MUST_E"));
        this.ruleFor(m => m.email).emailAddress().withMessage(this.getMessage('GL_INVALID_EMAIL_E'));

        this.ruleFor(x => x.password).notEmpty().withMessage(resourceManager.getResourceByKey("GL_INPUT_FIELD_MUST_E"));
        this.ruleFor(m => m.password).matches('^(?=.*?[A-Z])').withMessage(this.getMessage('GL_CAPITAL_LETTER_VALIDATION_E'));
        this.ruleFor(m => m.password).matches('^(?=.*?[0-9])').withMessage(this.getMessage('GL_DIGIT_VALIDATION_E'));
        this.ruleFor(m => m.password).matches("^[a-zA-Z0-9!@#\$%&\*\.\\-;]+$").withMessage(ResourceHelpers.formatErrorMessage("GL_SPECIAL_SYMBOLS_VALIDATION_E",
            new UserInputModel(), 'password'));
        this.ruleFor(m => m.password).length(8).withMessage(this.getMessage('GL_AT_LEAST_EIGHT_SYMBOLS_VALIDATION_E'));
        this.ruleFor(x => x.confirmPassword).notEmpty().withMessage(resourceManager.getResourceByKey("GL_INPUT_FIELD_MUST_E"));
    }

    public validate(obj: UserInputModel): boolean {

        let isValid = super.validate(obj);

        if (!obj.acceptedTerms) {
            obj.addError(this.getMessage("DOC_GL_NOT_ACCEPTED_TERMS_E"))
            isValid = false;
        }

        if (!ObjectHelper.isStringNullOrEmpty(obj.password) && !ObjectHelper.isStringNullOrEmpty(obj.confirmPassword)) {

            if (obj.password != obj.confirmPassword) {
                obj.addError(this.getMessage("GL_PASSWORD_MATCH_E"))
                isValid = false;
            }
        }

        return isValid;
    }
}

export class ResetPasswordValidator extends EAUBaseValidator<UserInputModel, any> {

    constructor() {
        super();

        this.ruleFor(x => x.email).notEmpty().withMessage(resourceManager.getResourceByKey("GL_INPUT_FIELD_MUST_E"));
        this.ruleFor(m => m.email).emailAddress().withMessage(this.getMessage('GL_INVALID_EMAIL_E'));
    }

    public validate(obj: UserInputModel): boolean {

        let isValid = super.validate(obj);

        return isValid;
    }
}

export class UserProfileValidator extends EAUBaseValidator<UserInputModel, any> {

    constructor() {
        super();

        this.ruleFor(x => x.email).notEmpty().withMessage(this.getMessage("GL_INPUT_FIELD_MUST_E"));
        this.ruleFor(m => m.email).emailAddress().withMessage(this.getMessage('GL_INVALID_EMAIL_E'));
    }

    public validate(obj: UserInputModel): boolean {

        let isValid = super.validate(obj);

        return isValid;
    }
}

export class ForgottenPasswordValidator extends EAUBaseValidator<UserInputModel, any> {

    constructor() {
        super();

        this.ruleFor(x => x.password).notEmpty().withMessage(resourceManager.getResourceByKey("GL_INPUT_FIELD_MUST_E"));
        this.ruleFor(m => m.password).matches('^(?=.*?[A-Z])').withMessage(this.getMessage('GL_CAPITAL_LETTER_VALIDATION_E'));
        this.ruleFor(m => m.password).matches('^(?=.*?[0-9])').withMessage(this.getMessage('GL_DIGIT_VALIDATION_E'));
        this.ruleFor(m => m.password).matches("^[a-zA-Z0-9!@#\$%&\*\.\\-;]+$").withMessage(ResourceHelpers.formatErrorMessage("GL_SPECIAL_SYMBOLS_VALIDATION_E",
            new UserInputModel(), 'password'));
        this.ruleFor(m => m.password).length(8).withMessage(this.getMessage('GL_AT_LEAST_EIGHT_SYMBOLS_VALIDATION_E'));

        this.ruleFor(x => x.confirmPassword).notEmpty().withMessage(resourceManager.getResourceByKey("GL_INPUT_FIELD_MUST_E"));
    }

    public validate(obj: UserInputModel): boolean {

        let isValid = super.validate(obj);

        if (!ObjectHelper.isStringNullOrEmpty(obj.password) && !ObjectHelper.isStringNullOrEmpty(obj.confirmPassword)) {

            if (obj.password != obj.confirmPassword) {
                obj.addError(this.getMessage("GL_PASSWORD_MATCH_E"))
                isValid = false;
            }
        }

        return isValid;
    }
}

export class ChangePasswordValidator extends EAUBaseValidator<UserInputModel, any> {

    constructor() {
        super();

        this.ruleFor(x => x.currentPassword).notEmpty().withMessage(resourceManager.getResourceByKey("GL_INPUT_FIELD_MUST_E"));
        this.ruleFor(x => x.password).notEmpty().withMessage(resourceManager.getResourceByKey("GL_INPUT_FIELD_MUST_E"));
        this.ruleFor(m => m.password).matches('^(?=.*?[A-Z])').withMessage(this.getMessage('GL_CAPITAL_LETTER_VALIDATION_E'));
        this.ruleFor(m => m.password).matches('^(?=.*?[0-9])').withMessage(this.getMessage('GL_DIGIT_VALIDATION_E'));
        this.ruleFor(m => m.password).matches("^[a-zA-Z0-9!@#\$%&\*\.\\-;]+$").withMessage(ResourceHelpers.formatErrorMessage("GL_SPECIAL_SYMBOLS_VALIDATION_E",
            new UserInputModel(), 'password'));
        this.ruleFor(m => m.password).length(8).withMessage(this.getMessage('GL_AT_LEAST_EIGHT_SYMBOLS_VALIDATION_E'));
        this.ruleFor(x => x.confirmPassword).notEmpty().withMessage(resourceManager.getResourceByKey("GL_INPUT_FIELD_MUST_E"));
    }

    public validate(obj: UserInputModel): boolean {

        let isValid = super.validate(obj);

        if (!ObjectHelper.isStringNullOrEmpty(obj.password) && !ObjectHelper.isStringNullOrEmpty(obj.confirmPassword)) {

            if (obj.password != obj.confirmPassword) {
                obj.addError(this.getMessage("GL_PASSWORD_MATCH_E"))
                isValid = false;
            }
        }

        return isValid;
    }
}