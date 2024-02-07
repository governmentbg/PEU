import { DocumentFormManagerBase, Section } from "./DocumentFormManager";
import { DocumentFormValidationContext } from "../validations/DocumentFormValidationContext";
import { ResourceHelpers } from "eau-core";
import { DocumentsWillBeIssuedMessageVM } from "..";
import { DocumentsWillBeIssuedMessageValidator } from "../validations/forms/DocumentsWillBeIssuedMessageValidator";
import { DocumentsWillBeIssuedMessageUI } from "../ui"

export class DocumentsWillBeIssuedMessageManager extends DocumentFormManagerBase<DocumentsWillBeIssuedMessageVM>{

    //#region DocumentFormManager

    createDocument(obj: any): DocumentsWillBeIssuedMessageVM {
        return new DocumentsWillBeIssuedMessageVM(obj);
    }

    //#endregion

    protected createSections(validationContext: DocumentFormValidationContext): Section[] {
        var sections = super.createSections(validationContext);

        var main = new Section();
        main.code = "main";
        main.title = ResourceHelpers.getResourceByProperty(m => m, DocumentsWillBeIssuedMessageVM);
        main.form = this.documentForm;
        main.formUICmp = DocumentsWillBeIssuedMessageUI;
        main.validator = new DocumentsWillBeIssuedMessageValidator();
        main.validator.setValidationContext(validationContext);
        main.validate = () => this.validateSection(main);

        sections.push(main);

        return sections;
    }
}