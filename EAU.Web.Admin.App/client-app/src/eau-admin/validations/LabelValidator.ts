import {Label} from "eau-core";
import {EAUBaseValidator} from "eau-core";

export class LabelValidator extends EAUBaseValidator<Label, any> {

    constructor() {
        super();
        this.ruleFor(x => x.value).notEmpty().withMessage(this.getMessage('GL_INPUT_FIELD_MUST_E'));
        this.ruleFor(x => x.description).notEmpty().withMessage(this.getMessage('GL_INPUT_FIELD_MUST_E'));
    }

    public validate(obj: Label): boolean {
        let isValid = super.validate(obj);

        if(!obj.code || !obj.value || !obj.description){
            isValid = false;
        }

        return isValid;
    }
}

