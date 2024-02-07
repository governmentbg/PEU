import React from "react";
import { action } from "mobx";
import { observer } from "mobx-react";
import { ObjectHelper } from "cnsys-core";
import { BaseProps } from "cnsys-ui-react";
import { attributesClassFormControlRequiredLabel, attributesClassInlineRadioButtons, attributesClassFormControlReqired, EAUBaseComponent, ResourceHelpers } from "eau-core";
import { PersonIdentifier, PersonIdentifierChoiceType, PersonIdentifierValidator } from "eau-documents";

interface PersonIdentifierKATUIProps extends BaseProps {
}

@observer export class PersonIdentifierKATUI extends EAUBaseComponent<PersonIdentifierKATUIProps, PersonIdentifier> {
    constructor(props?: PersonIdentifierKATUIProps, context?: any) {
        super(props, context);

        //Bind
        this.onIdentifierTypeChange = this.onIdentifierTypeChange.bind(this);
    }

    renderEdit(): JSX.Element {
        return (
            <div className="row">
                <div className="col-12">
                    {this.labelFor(m => m.item, "DOC_KAT_PersonIdentifier_item_L", attributesClassFormControlRequiredLabel)}
                </div>
                <div className="form-group col-6 col-xl-4">
                    {this.textBoxFor(m => m.item, attributesClassFormControlReqired)}
                </div>
                <div className="form-group col-auto">
                    <fieldset className="form-inline">
                        <legend className="sr-only">{this.getResourceByProperty(x => x.itemElementName)}</legend>
                        {this.radioButtonListFor(m => m.itemElementName
                            , ResourceHelpers.getSelectListItemsForEnum(PersonIdentifierChoiceType)
                            , attributesClassInlineRadioButtons, this.onIdentifierTypeChange, null)}
                    </fieldset>
                </div>
            </div>
        );
    }

    renderDisplay(): JSX.Element {
        return (
            <div className="row">
                <div className="form-group col-12">
                    <h4 className="form-control-label">{ResourceHelpers.getResourceByEmun(this.model.itemElementName, PersonIdentifierChoiceType)}</h4>
                    {this.textDisplayFor(m => m.item)}
                </div>
            </div>
        );
    }

    @action private onIdentifierTypeChange(e: any): void {
        this.model.itemElementName = Number(this.model.itemElementName);
        this.model.clearErrors();

        if (!ObjectHelper.isNullOrUndefined(this.model.item)) {
            let validator = new PersonIdentifierValidator();
            validator.validate(this.model);
        }
    }
}