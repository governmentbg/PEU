import { ActionsTakenMessageVM } from "../models";
import { DocumentFormManagerBase, Section } from "./DocumentFormManager";
import { DocumentFormValidationContext } from "../validations/DocumentFormValidationContext";
import { ResourceHelpers } from "eau-core";
import { ActionsTakenMessageUI } from "../ui";
import { ActionsTakenMessageValidator } from "../validations/forms/ActionsTakenMessageValidator";


export class ActionsTakenMessageManager extends DocumentFormManagerBase<ActionsTakenMessageVM>{

    //#region DocumentFormManager

    createDocument(obj: any): ActionsTakenMessageVM {
        return new ActionsTakenMessageVM(obj);
    }

    //#endregion

    protected createSections(validationContext: DocumentFormValidationContext): Section[] {
        var sections = super.createSections(validationContext);

        var main = new Section();
        main.code = "main";
        main.title = ResourceHelpers.getResourceByProperty(m => m, ActionsTakenMessageVM);
        main.form = this.documentForm;
        main.formUICmp = ActionsTakenMessageUI;
        main.validator = new ActionsTakenMessageValidator();
        main.validator.setValidationContext(validationContext);
        main.validate = () => this.validateSection(main);

        sections.push(main);

        return sections;
    }
}