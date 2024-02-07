import { EAUBaseValidator} from "eau-core";
import { ServiceGroupI18nVM } from "../models/ServiceGroupI18nVM";

export class ServiceGroupI18nValidator extends EAUBaseValidator<ServiceGroupI18nVM, any> {

    constructor() {
        super();
        this.ruleFor(x => x.name).length(0, 1000).withMessage(this.getMessage("GL_MAX_LENGTH_EXCEEDED_E"));
    }

    public validate(obj: ServiceGroupI18nVM): boolean {

        let isValid = super.validate(obj);

        return isValid;
    }
}

