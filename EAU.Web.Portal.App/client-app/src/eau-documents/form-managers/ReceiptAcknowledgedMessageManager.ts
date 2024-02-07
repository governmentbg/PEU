import { ReceiptAcknowledgedMessageVM } from "../models";
import { DocumentFormManagerBase, Section } from "./DocumentFormManager";
import { DocumentFormValidationContext } from "../validations/DocumentFormValidationContext";
import { ResourceHelpers } from "eau-core";
import { ReceiptAcknowledgedMessageUI } from "../ui/document-forms/ReceiptAcknowledgedMessageUI"
import { ReceiptAcknowledgedMessageValidator } from "../validations/forms/ReceiptAcknowledgedMessageValidator";

export class ReceiptAcknowledgedMessageManager extends DocumentFormManagerBase<ReceiptAcknowledgedMessageVM>{

    //#region DocumentFormManager

    createDocument(obj: any): ReceiptAcknowledgedMessageVM {
        return new ReceiptAcknowledgedMessageVM(obj);
    }

    //#endregion

    protected createSections(validationContext: DocumentFormValidationContext): Section[] {
        var sections = super.createSections(validationContext);

        var main = new Section();
        main.code = "main";
        main.title = ResourceHelpers.getResourceByProperty(m => m, ReceiptAcknowledgedMessageVM);
        main.form = this.documentForm;
        main.formUICmp = ReceiptAcknowledgedMessageUI;
        main.validator = new ReceiptAcknowledgedMessageValidator();
        main.validator.setValidationContext(validationContext);
        main.validate = () => this.validateSection(main);

        sections.push(main);

        return sections;
    }
}