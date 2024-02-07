import { ObjectHelper } from "cnsys-core";
import { EAUBaseValidator, ServiceGroup } from "eau-core";

export class ServiceGroupValidator extends EAUBaseValidator<ServiceGroup, any> {

    constructor() {
        super();

        this.ruleFor(x => x.name).notEmpty().withMessage(this.getMessage('GL_MANDATORY_SERVICE_NAME_E'));
        this.ruleFor(x => x.iconName).notEmpty().withMessage(this.getMessage('GL_MANDATORY_IMAGE_SERVICE_GROUP_E'));
        this.ruleFor(x => x.orderNumber).notEmpty().withMessage(this.getMessage('GL_MANDATORY_NUMBER_IN_GROUP_E'));
    }

    public validate(obj: ServiceGroup): boolean {

        let isValid = super.validate(obj);

        if (ObjectHelper.isNullOrUndefined(obj.name)
            || ObjectHelper.isNullOrUndefined(obj.iconName)
            || ObjectHelper.isNullOrUndefined(obj.orderNumber)
            ) {

            obj.addError(this.getMessage('GL_MANDATORY_TYPE_FIELDS_I'));
            isValid = false;
        }

        return isValid;
    }
}

