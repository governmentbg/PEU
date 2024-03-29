﻿import { EAUBaseValidator } from "eau-core";
import { DataServiceLimit } from "../models/ModelsAutoGenerated";
import moment from "moment";

export class LimitsValidator extends EAUBaseValidator<DataServiceLimit, any> {

    constructor() {
        super();
        this.ruleFor(x => x.requestsNumber).notEmpty().withMessage(this.getMessage('GL_INPUT_FIELD_MUST_E'));
        this.ruleFor(x => x.requestsNumber).greaterThanOrEqualTo(1).withMessage(this.getMessage('GL_INPUT_FIELD_MUST_GREATER_THAN_ZERO_E'));
        this.ruleFor(x => x.requestsInterval).must(x=>moment.duration(x.requestsInterval).asMilliseconds() != 0).withMessage(this.getMessage('GL_INTERVAL_MUST_GREATER_THAN_ZERO_E'));
    }

    public validate(obj: DataServiceLimit): boolean {

        let isValid = super.validate(obj);

        return isValid;
    }
}
