import { SelectListItem } from "cnsys-core";
import { BaseProps } from "cnsys-ui-react";
import { attributesClassInlineRadioButtons, EAUBaseComponent } from "eau-core";
import { action } from 'mobx';
import { observer } from "mobx-react";
import * as React from 'react';
import { ApplicationType, DocumentMustServeToVM, ItemChoiceType1 } from '../../models';
import { ApplicationFormManagerProps, withDocumentFormManager } from "../document-forms/DocumentFormManagerProvider";

interface DocumentMustServeToUIProps extends BaseProps, ApplicationFormManagerProps { }

@observer export class DocumentMustServeToUIImpl extends EAUBaseComponent<DocumentMustServeToUIProps, DocumentMustServeToVM> {
    private listItems: SelectListItem[];

    constructor(props: DocumentMustServeToUIProps) {
        super(props);

        //Bind
        this.onRadioChange = this.onRadioChange.bind(this);

        //Init
        this.listItems = [
            new SelectListItem({
                value: ItemChoiceType1.InRepublicOfBulgariaDocumentMustServeTo.toString(),
                text: this.getResourceByProperty(m => m.itemInRepublicOfBulgariaDocumentMustServeTo),
                selected: this.model.itemElementName === ItemChoiceType1.InRepublicOfBulgariaDocumentMustServeTo
            }),
            new SelectListItem({
                value: ItemChoiceType1.AbroadDocumentMustServeTo.toString(),
                text: this.getResourceByProperty(m => m.itemAbroadDocumentMustServeTo),
                selected: this.model.itemElementName === ItemChoiceType1.AbroadDocumentMustServeTo
            })
        ];

        if (!this.model.itemElementName) {
            this.model.itemElementName = ItemChoiceType1.InRepublicOfBulgariaDocumentMustServeTo;
        }
    }

    renderEdit(): JSX.Element {
        return (
            <>
                <div className="form-inline">
                    {
                        this.props.documentFormManager.applicationType == ApplicationType.AppForRemoveInvalidData
                            ? <>
                                <div className="custom-control-inline custom-control custom-radio">
                                    <input type="radio" className="custom-control-input" id={`must-serve-to-${ItemChoiceType1.InRepublicOfBulgariaDocumentMustServeTo}`} name="must-serve-to" disabled
                                        checked={this.model.itemElementName == ItemChoiceType1.InRepublicOfBulgariaDocumentMustServeTo} />
                                    <label className="custom-control-label" htmlFor={`must-serve-to-${ItemChoiceType1.InRepublicOfBulgariaDocumentMustServeTo}`}>{this.getResourceByProperty(m => m.itemInRepublicOfBulgariaDocumentMustServeTo)}</label>
                                </div>
                                <div className="custom-control-inline custom-control custom-radio">
                                    <input type="radio" className="custom-control-input" id={`must-serve-to-${ItemChoiceType1.AbroadDocumentMustServeTo}`} name="must-serve-to" disabled
                                        checked={this.model.itemElementName == ItemChoiceType1.AbroadDocumentMustServeTo} />
                                    <label className="custom-control-label" htmlFor={`must-serve-to-${ItemChoiceType1.AbroadDocumentMustServeTo}`}>{this.getResourceByProperty(m => m.itemAbroadDocumentMustServeTo)}</label>
                                </div>
                            </>
                            : this.radioButtonListFor(m => m.itemElementName, this.listItems, attributesClassInlineRadioButtons, this.onRadioChange)
                    }
                </div>
                {this.model.itemElementName == ItemChoiceType1.AbroadDocumentMustServeTo ?
                    this.textAreaFor(m => m.itemAbroadDocumentMustServeTo) :
                    this.textAreaFor(m => m.itemInRepublicOfBulgariaDocumentMustServeTo)}
            </>
        );
    }

    renderDisplay(): JSX.Element {
        return (
            <>
                {this.model.itemElementName == ItemChoiceType1.AbroadDocumentMustServeTo
                    ? this.labelFor(m => m.itemAbroadDocumentMustServeTo)
                    : this.labelFor(m => m.itemInRepublicOfBulgariaDocumentMustServeTo)}
                {this.model.itemElementName == ItemChoiceType1.AbroadDocumentMustServeTo
                    ? this.textDisplayFor(m => m.itemAbroadDocumentMustServeTo)
                    : this.textDisplayFor(m => m.itemInRepublicOfBulgariaDocumentMustServeTo)}
            </>
        );
    }

    @action private onRadioChange(e: any): void {

        if (e.target.value === ItemChoiceType1.AbroadDocumentMustServeTo.toString()) {
            this.model.itemInRepublicOfBulgariaDocumentMustServeTo = undefined;
            this.props.documentFormManager.changeDocumentMustServeTo?.(ItemChoiceType1.AbroadDocumentMustServeTo);
        } else {
            this.model.itemAbroadDocumentMustServeTo = undefined;
            this.props.documentFormManager.changeDocumentMustServeTo?.(ItemChoiceType1.InRepublicOfBulgariaDocumentMustServeTo);
        }
    }
}

export const DocumentMustServeToUI = withDocumentFormManager(DocumentMustServeToUIImpl);