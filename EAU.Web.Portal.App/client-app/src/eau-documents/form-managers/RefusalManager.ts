import { DocumentFormManagerBase, Section } from "./DocumentFormManager";
import { DocumentFormValidationContext } from "../validations/DocumentFormValidationContext";
import { ResourceHelpers } from "eau-core";
import { RefusalVM } from "..";
import { RefusalValidator } from "../validations/forms/RefusalValidator";
import { RefusalUI } from "../ui"

export class RefusalManager extends DocumentFormManagerBase<RefusalVM>{

    //#region DocumentFormManager

    createDocument(obj: any): RefusalVM {
        return new RefusalVM(obj);
    }

    //#endregion

    protected createSections(validationContext: DocumentFormValidationContext): Section[] {
        var sections = super.createSections(validationContext);

        var main = new Section();
        main.code = "main";
        main.title = ResourceHelpers.getResourceByProperty(m => m, RefusalVM);
        main.form = this.documentForm;
        main.formUICmp = RefusalUI;
        main.validator = new RefusalValidator();
        main.validator.setValidationContext(validationContext);
        main.validate = () => this.validateSection(main);

        sections.push(main);

        return sections;
    }
}