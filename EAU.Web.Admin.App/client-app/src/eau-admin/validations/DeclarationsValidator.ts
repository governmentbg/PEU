import { ObjectHelper } from "cnsys-core";
import { EAUBaseValidator, Declaration, resourceManager } from "eau-core";

export class DeclarationsValidator extends EAUBaseValidator<Declaration, any> {

    constructor() {
        super();

        this.ruleFor(x => x.code).notEmpty().withMessage(resourceManager.getResourceByKey("GL_MANDATORY_CODE_E"));
        this.ruleFor(x => x.content).notEmpty().withMessage(resourceManager.getResourceByKey("GL_MANDATORY_CONTENT_E"));
        this.ruleFor(x => x.description).notEmpty().withMessage(resourceManager.getResourceByKey("GL_MANDATORY_DESCRIPTION_E"));
    }

    public validate(obj: Declaration): boolean {

        let isValid = super.validate(obj);

        if (ObjectHelper.isNullOrUndefined(obj.code)
            ||ObjectHelper.isNullOrUndefined(obj.content)
            || ObjectHelper.isNullOrUndefined(obj.description)) {

            obj.addError(resourceManager.getResourceByKey("GL_INVALID_MANDATORY_DATA_E"));
            isValid = false;
        }

        return isValid;
    }
}


