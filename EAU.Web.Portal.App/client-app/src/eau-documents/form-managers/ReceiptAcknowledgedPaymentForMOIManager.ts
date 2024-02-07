import { ReceiptAcknowledgedPaymentForMOIVM } from "../models";
import { DocumentFormManagerBase, Section } from "./DocumentFormManager";
import { DocumentFormValidationContext } from "../validations/DocumentFormValidationContext";
import { ResourceHelpers } from "eau-core";
import { ReceiptAcknowledgedPaymentForMOIValidator } from "../validations/forms/ReceiptAcknowledgedPaymentForMOIValidator";
import { ReceiptAcknowledgedPaymentForMOIUI } from "../ui"

export class ReceiptAcknowledgedPaymentForMOIManager extends DocumentFormManagerBase<ReceiptAcknowledgedPaymentForMOIVM>{

    //#region DocumentFormManager

    createDocument(obj: any): ReceiptAcknowledgedPaymentForMOIVM {
        return new ReceiptAcknowledgedPaymentForMOIVM(obj);
    }

    //#endregion

    protected createSections(validationContext: DocumentFormValidationContext): Section[] {
        var sections = super.createSections(validationContext);

        var main = new Section();
        main.code = "main";
        main.title = ResourceHelpers.getResourceByProperty(m => m, ReceiptAcknowledgedPaymentForMOIVM);
        main.form = this.documentForm;
        main.formUICmp = ReceiptAcknowledgedPaymentForMOIUI;
        main.validator = new ReceiptAcknowledgedPaymentForMOIValidator();
        main.validator.setValidationContext(validationContext);
        main.validate = () => this.validateSection(main);

        sections.push(main);

        return sections;
    }
}