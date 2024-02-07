import { OutstandingConditionsForWithdrawServiceMessageVM } from "../models";
import { DocumentFormManagerBase, Section } from "./DocumentFormManager";
import { DocumentFormValidationContext } from "../validations/DocumentFormValidationContext";
import { ResourceHelpers } from "eau-core";
import { OutstandingConditionsForWithdrawServiceMessageValidator } from "../validations/forms/OutstandingConditionsForWithdrawServiceMessageValidator"; 
import { OutstandingConditionsForWithdrawServiceMessageUI } from "../ui"


export class OutstandingConditionsForWithdrawServiceMessageManager extends DocumentFormManagerBase<OutstandingConditionsForWithdrawServiceMessageVM>{

    //#region DocumentFormManager

    createDocument(obj: any): OutstandingConditionsForWithdrawServiceMessageVM {
        return new OutstandingConditionsForWithdrawServiceMessageVM(obj);
    }

    //#endregion

    protected createSections(validationContext: DocumentFormValidationContext): Section[] {
        var sections = super.createSections(validationContext);

        var main = new Section();
        main.code = "main";
        main.title = ResourceHelpers.getResourceByProperty(m => m, OutstandingConditionsForWithdrawServiceMessageVM);
        main.form = this.documentForm;
        main.formUICmp = OutstandingConditionsForWithdrawServiceMessageUI;
        main.validator = new OutstandingConditionsForWithdrawServiceMessageValidator();
        main.validator.setValidationContext(validationContext);
        main.validate = () => this.validateSection(main);

        sections.push(main);

        return sections;
    }
}