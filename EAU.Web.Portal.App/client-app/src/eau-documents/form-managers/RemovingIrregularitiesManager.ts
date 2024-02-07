import { RemovingIrregularitiesInstructionsVM } from "../models";
import { DocumentFormManagerBase, Section } from "./DocumentFormManager";
import { DocumentFormValidationContext } from "../validations/DocumentFormValidationContext";
import { ResourceHelpers } from "eau-core";
import { RemovingIrregularitiesInstructionsUI } from "../ui/document-forms/RemovingIrregularitiesInstructionsUI"
import { RemovingIrregularitiesInstructionsValidator } from "../validations/forms/RemovingIrregularitiesInstructionsValidator";

export class RemovingIrregularitiesManager extends DocumentFormManagerBase<RemovingIrregularitiesInstructionsVM>{

    //#region DocumentFormManager

    createDocument(obj: any): RemovingIrregularitiesInstructionsVM {
        return new RemovingIrregularitiesInstructionsVM(obj);
    }

    //#endregion

    protected createSections(validationContext: DocumentFormValidationContext): Section[] {
        var sections = super.createSections(validationContext);

        var main = new Section();
        main.code = "main";
        main.title = ResourceHelpers.getResourceByProperty(m => m, RemovingIrregularitiesInstructionsVM);
        main.form = this.documentForm;
        main.formUICmp = RemovingIrregularitiesInstructionsUI;
        main.validator = new RemovingIrregularitiesInstructionsValidator();
        main.validator.setValidationContext(validationContext);
        main.validate = () => this.validateSection(main);

        sections.push(main);

        return sections;
    }
}