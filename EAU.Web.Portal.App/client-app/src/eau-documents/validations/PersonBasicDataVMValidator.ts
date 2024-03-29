﻿import { EAUBaseValidator } from "eau-core";
import { PersonBasicDataVM } from "../models/ModelsAutoGenerated";
import { PersonNamesValidator } from "./PersonNamesValidator";
import { PersonIdentifierValidator } from "./PersonIdentifierValidator";
import { IdentityDocumentBasicDataVMValidator } from "./IdentityDocumentBasicDataVMValidator";
import { DocumentFormValidationContext } from "./DocumentFormValidationContext";

export class PersonBasicDataVMValidator extends EAUBaseValidator<PersonBasicDataVM, DocumentFormValidationContext> {
    constructor(skipIdentityDocumentValidation?: boolean) {
        super();

        this.ruleFor(m => m.names).setValidator(new PersonNamesValidator());
        this.ruleFor(m => m.identifier).setValidator(new PersonIdentifierValidator());

        if (!skipIdentityDocumentValidation)
            this.ruleFor(m => m.identityDocument).setValidator(new IdentityDocumentBasicDataVMValidator());
    }
}