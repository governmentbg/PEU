import { DocumentFormManagerBase, Section } from "./DocumentFormManager";
import { DocumentFormValidationContext } from "../validations/DocumentFormValidationContext";
import { ResourceHelpers } from "eau-core";
import { ReceiptNotAcknowledgedMessageVM } from "..";
import { ReceiptNotAcknowledgedMessageValidator } from "../validations/forms/ReceiptNotAcknowledgedMessageValidator";
import { ReceiptNotAcknowledgedMessageUI } from "../ui/document-forms/ReceiptNotAcknowledgedMessageUI"

export class ReceiptNotAcknowledgedMessageManager extends DocumentFormManagerBase<ReceiptNotAcknowledgedMessageVM>{

    //#region DocumentFormManager

    createDocument(obj: any): ReceiptNotAcknowledgedMessageVM {
        return new ReceiptNotAcknowledgedMessageVM(obj);
    }

    //#endregion

    protected createSections(validationContext: DocumentFormValidationContext): Section[] {
        var sections = super.createSections(validationContext);

        var main = new Section();
        main.code = "main";
        main.title = ResourceHelpers.getResourceByProperty(m => m, ReceiptNotAcknowledgedMessageVM);
        main.form = this.documentForm;
        main.formUICmp = ReceiptNotAcknowledgedMessageUI;
        main.validator = new ReceiptNotAcknowledgedMessageValidator();
        main.validator.setValidationContext(validationContext);
        main.validate = () => this.validateSection(main);

        sections.push(main);

        return sections;
    }
}