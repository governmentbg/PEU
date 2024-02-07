import { TerminationOfServiceMessageVM } from "../models";
import { DocumentFormManagerBase, Section } from "./DocumentFormManager";
import { DocumentFormValidationContext } from "../validations/DocumentFormValidationContext";
import { ResourceHelpers } from "eau-core";
import { TerminationOfServiceMessageValidator } from "../validations/forms/TerminationOfServiceMessageValidator";
import { TerminationOfServiceMessageUI } from "../ui";


export class TerminationOfServiceMessageManager extends DocumentFormManagerBase<TerminationOfServiceMessageVM>{

    //#region DocumentFormManager

    createDocument(obj: any): TerminationOfServiceMessageVM {
        return new TerminationOfServiceMessageVM(obj);
    }

    //#endregion

    protected createSections(validationContext: DocumentFormValidationContext): Section[] {
        var sections = super.createSections(validationContext);

        var main = new Section();
        main.code = "main";
        main.title = ResourceHelpers.getResourceByProperty(m => m, TerminationOfServiceMessageVM);
        main.form = this.documentForm;
        main.formUICmp = TerminationOfServiceMessageUI;
        main.validator = new TerminationOfServiceMessageValidator();
        main.validator.setValidationContext(validationContext);
        main.validate = () => this.validateSection(main);

        sections.push(main);

        return sections;
    }
}