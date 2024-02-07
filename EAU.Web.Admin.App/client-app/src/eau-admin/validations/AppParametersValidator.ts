import { moduleContext } from "cnsys-core";
import { AppParameter, AppParameterSearchCriteria, AppParameterTypes, EAUBaseValidator } from "eau-core";

export class AppParameterSearchCriteriaValidator extends EAUBaseValidator<AppParameterSearchCriteria, any> {

    constructor() {
        super();
    }

    public validate(obj: AppParameterSearchCriteria): boolean {

        let isValid = super.validate(obj);

        return isValid;
    }
}

export class AppParameterResultValidator extends EAUBaseValidator<AppParameter, any> {

    constructor() {
        super();

        this.ruleFor(x => x.valueInt).matches('^[0-9]{0,9}$').when(x => x.parameterType == AppParameterTypes.Integer)
            .withMessage(moduleContext.resourceManager.getResourceByKey("GL_INCORRECT_FORMAT_E"));

        this.ruleFor(x => x.valueInt).notEmpty().when(x => x.parameterType == AppParameterTypes.Integer)
            .withMessage(moduleContext.resourceManager.getResourceByKey("GL_INPUT_FIELD_MUST_E"));
    }

    public validate(obj: AppParameter): boolean {

        let isValid = super.validate(obj);

        return isValid;
    }
}
