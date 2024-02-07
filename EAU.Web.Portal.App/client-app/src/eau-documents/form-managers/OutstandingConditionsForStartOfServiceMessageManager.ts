import { OutstandingConditionsForStartOfServiceMessageVM } from "../models";
import { DocumentFormManagerBase, Section } from "./DocumentFormManager";
import { DocumentFormValidationContext } from "../validations/DocumentFormValidationContext";
import { ResourceHelpers } from "eau-core";
import { OutstandingConditionsForStartOfServiceMessageValidator } from "../validations/forms/OutstandingConditionsForStartOfServiceMessageValidator";
import { OutstandingConditionsForStartOfServiceMessageUI } from "../ui"


export class OutstandingConditionsForStartOfServiceMessageManager extends DocumentFormManagerBase<OutstandingConditionsForStartOfServiceMessageVM>{

    //#region DocumentFormManager

    createDocument(obj: any): OutstandingConditionsForStartOfServiceMessageVM {
        return new OutstandingConditionsForStartOfServiceMessageVM(obj);
    }

    //#endregion

    protected createSections(validationContext: DocumentFormValidationContext): Section[] {
        var sections = super.createSections(validationContext);

        var main = new Section();
        main.code = "main";
        main.title = ResourceHelpers.getResourceByProperty(m => m, OutstandingConditionsForStartOfServiceMessageVM);
        main.form = this.documentForm;
        main.formUICmp = OutstandingConditionsForStartOfServiceMessageUI;
        main.validator = new OutstandingConditionsForStartOfServiceMessageValidator();
        main.validator.setValidationContext(validationContext);
        main.validate = () => this.validateSection(main);

        sections.push(main);

        return sections;
    }
}