import { EAUBaseValidator} from "eau-core";
import { ServiceI18nVM } from "../models/ServiceI18nVM";

export class ServiceI18nValidator extends EAUBaseValidator<ServiceI18nVM, any> {

    constructor() {
        super();
        this.ruleFor(x => x.name).length(0, 1000).withMessage(this.getMessage("GL_MAX_LENGTH_EXCEEDED_E"));
    }

    public validate(obj: ServiceI18nVM): boolean {

        let isValid = super.validate(obj);

        return isValid;
    }
}

