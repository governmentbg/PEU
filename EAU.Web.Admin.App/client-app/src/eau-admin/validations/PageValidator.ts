import { ObjectHelper } from "cnsys-core";
import { EAUBaseValidator, Page} from "eau-core";

export class PageValidator extends EAUBaseValidator<Page, any> {

    constructor() {
        super();

        this.ruleFor(x => x.title).notEmpty().withMessage(this.getMessage('GL_INPUT_FIELD_MUST_E'));
        this.ruleFor(x => x.content).notEmpty().withMessage(this.getMessage('GL_INPUT_FIELD_MUST_E'));
    }

    public validate(obj: Page): boolean {

        let isValid = super.validate(obj);

        if (ObjectHelper.isNullOrUndefined(obj.title)
            || ObjectHelper.isStringNullOrEmpty(obj.content)
            ) {

                obj.addError(this.getMessage('GL_MANDATORY_TYPE_FIELDS_I'));
            isValid = false;
        }

        return isValid;
    }
}

