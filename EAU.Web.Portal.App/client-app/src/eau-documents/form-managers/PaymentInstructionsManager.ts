import { PaymentInstructionsVM } from "../models";
import { DocumentFormManagerBase, Section } from "./DocumentFormManager";
import { DocumentFormValidationContext } from "../validations/DocumentFormValidationContext";
import { ResourceHelpers } from "eau-core";
import { PaymentInstructionsUI } from "../ui"
import { PaymentInstructionsValidator } from "../validations/forms/PaymentInstructionsValidator";

export class PaymentInstructionsManager extends DocumentFormManagerBase<PaymentInstructionsVM>{

    //#region DocumentFormManager

    createDocument(obj: any): PaymentInstructionsVM {
        return new PaymentInstructionsVM(obj);
    }

    //#endregion

    protected createSections(validationContext: DocumentFormValidationContext): Section[] {
        var sections = super.createSections(validationContext);

        var main = new Section();
        main.code = "main";
        main.title = ResourceHelpers.getResourceByProperty(m => m, PaymentInstructionsVM);
        main.form = this.documentForm;
        main.formUICmp = PaymentInstructionsUI;
        main.validator = new PaymentInstructionsValidator();
        main.validator.setValidationContext(validationContext);
        main.validate = () => this.validateSection(main);

        sections.push(main);

        return sections;
    }
}