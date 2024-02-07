import {LabelTranslationI18nVM} from "../models/LabelTranslationI18nVM";
import {EAUBaseValidator} from "eau-core";

export class LabelTranslationI18nVMValidator extends EAUBaseValidator<LabelTranslationI18nVM, any> {

    constructor() {
        super();
        //this.ruleFor(x => x.value).notEmpty().withMessage(this.getMessage('GL_INPUT_FIELD_MUST_E'));
    }

    public validate(obj: LabelTranslationI18nVM): boolean {
        let isValid = super.validate(obj);

        //if(!obj.value){
            //isValid = false;
        //}

        return isValid;
    }
}