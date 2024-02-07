import { ObjectHelper } from "cnsys-core";
import { EAUBaseValidator, DocumentTemplate, resourceManager } from "eau-core";

export class DocumentTemplatesValidator extends EAUBaseValidator<DocumentTemplate, any> {

    constructor() {
        super();

        this.ruleFor(x => x.documentTypeID).notEmpty().withMessage(resourceManager.getResourceByKey("GL_INPUT_FIELD_MUST_E"));
        this.ruleFor(x => x.content).notEmpty().withMessage(resourceManager.getResourceByKey("GL_INPUT_FIELD_MUST_E"));
    }

    public validate(obj: DocumentTemplate): boolean {

        let isValid = super.validate(obj);

        if (ObjectHelper.isNullOrUndefined(obj.content)) {

            obj.addError(resourceManager.getResourceByKey("GL_INVALID_MANDATORY_DATA_E"));
            isValid = false;
        }
        return isValid;
    }
}


