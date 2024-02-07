import * as React from "react";
import { observable, action, runInAction } from "mobx";
import { observer } from "mobx-react";
import { ObjectHelper, BindableReference } from "cnsys-core";
import { withAsyncFrame, AsyncUIProps, BaseProps } from "cnsys-ui-react";
import { Modal, ModalBody, ModalHeader, ModalFooter } from "reactstrap";
import { EAUBaseComponent, Nomenclatures, DocumentTemplateField, resourceManager } from "eau-core";
import { ApplicationFormManagerProps, withDocumentFormManager } from "./document-forms/DocumentFormManagerProvider";
import { AttachedDocument } from "../models/ModelsManualAdded";

export enum TrigerMode {
    Create = 0,
    Edit = 1
}

interface IHtmlData {
    __html: string;
}

export interface AttachedDocumentTemplateEditUIProps extends BaseProps, AsyncUIProps, ApplicationFormManagerProps {
    trigerTag: TrigerMode;
    onSaveCallback: (attachedDoc: AttachedDocument) => void;
}

@observer class AttachedDocumentTemplateEditUIImpl extends EAUBaseComponent<AttachedDocumentTemplateEditUIProps, AttachedDocument> {
    private htmlEditor: HTMLDivElement;
    private preaperedHtml: IHtmlData;

    @observable private templateFields: DocumentTemplateField[];
    @observable private isOpen: boolean;

    constructor(props: AttachedDocumentTemplateEditUIProps) {
        super(props);

        //Bind
        this.closeModal = this.closeModal.bind(this);
        this.openModal = this.openModal.bind(this);
        this.saveTemplate = this.saveTemplate.bind(this);
        this.onChangeCustomTemplateName = this.onChangeCustomTemplateName.bind(this);
        this.preapareHtml = this.preapareHtml.bind(this);
        this.setHtmlEditor = this.setHtmlEditor.bind(this);
        this.containerHoverOrLeave = this.containerHoverOrLeave.bind(this);

        //Init
        this.isOpen = false;
        this.templateFields = [];
        this.preaperedHtml = undefined;

        let that = this;
        this.props.registerAsyncOperation(Nomenclatures.getDocumentTemplateFields().then(fields => {
            if (fields && fields.length > 0) {
                runInAction(() => {
                    that.templateFields = fields;
                    that.preaperedHtml = { __html: that.preapareHtml(that.model.htmlContent) };
                });
            }
        }));
    }

    render(): JSX.Element {
        if (this.templateFields.length == 0)
            return null;

        return (
            <>
                {this.props.trigerTag == TrigerMode.Create &&
                    <button type="button" className="btn btn-light" onClick={this.openModal}>
                        <i className="ui-icon ui-icon-edit mr-1" aria-hidden="true"></i>
                        {this.getResource('GL_CREATE_ATTACHED_TEMPLATE_L')}
                    </button>}
                {this.props.trigerTag == TrigerMode.Edit &&
                    <button type="button"
                        className="btn btn-light btn-sm"
                        title={this.getResource('GL_EDITING_L')}
                        onClick={this.openModal}
                        onFocus={this.containerHoverOrLeave}
                        onBlur={this.containerHoverOrLeave}>
                        <i className="ui-icon ui-icon-edit" aria-hidden="true" onMouseOver={this.containerHoverOrLeave} onMouseLeave={this.containerHoverOrLeave}></i>
                    </button>}
                <Modal centered={true} backdrop='static' autoFocus={true} isOpen={this.isOpen} onExit={this.closeModal}>
                    <ModalHeader closeAriaLabel={resourceManager.getResourceByKey("GL_CLOSE_L")} toggle={this.closeModal} >
                        {this.model.documentTypeName}
                    </ModalHeader>
                    <ModalBody>
                        <div className="ui-form ui-form--input">
                            <div className="row">
                                <div className="form-group col-12">
                                    <label htmlFor="DESIGNATION" className="form-control-label">{this.getResource('GL_DESIGNATION_L')}</label>
                                    {this.textAreaFor(m => m.description, null, 3, null, this.onChangeCustomTemplateName)}
                                </div>
                            </div>
                            <div className="row">
                                <div className="form-group col-12">
                                    <div className="form-control-box">
                                        <div ref={this.setHtmlEditor} className="document-template" dangerouslySetInnerHTML={this.preaperedHtml}></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ModalBody>
                    <ModalFooter>
                        <div className="button-bar">
                            <div className="right-side">
                                <button type="button" className="btn btn-primary" onClick={this.saveTemplate} data-dismiss="modal">{this.getResource('GL_SAVE_L')}</button>
                            </div>
                            <div className="left-side">
                                <button type="button" className="btn btn-secondary" onClick={this.closeModal} data-dismiss="modal">{this.getResource('GL_CANCEL_L')}</button>
                            </div>
                        </div>
                    </ModalFooter>
                </Modal>
            </>);
    }

    @action private closeModal(): void {
        this.isOpen = false;
    }

    private openModal(): void {
        this.isOpen = true;
    }

    @action private onChangeCustomTemplateName(event: any, value: any, modelReference: BindableReference): void {
        if (ObjectHelper.isStringNullOrEmpty(this.model.description)) {
            this.model.addError('description', this.getResource('GL_INPUT_FIELD_MUST_E'));
        } else {
            this.model.clearErrors();
        }
    }

    private setHtmlEditor(el: HTMLDivElement): void {
        this.htmlEditor = el;
    }

    @action private saveTemplate(e: any) {
        if (this.model.getPropertyErrors('description').length > 0) return;

        $(this.htmlEditor).find('input[type=text], textarea').each(function () {
            $(this).attr('value', $(this).val() as string);

            if (this.tagName === 'TEXTAREA') {
                $(this).text($(this).val() as string);
            }
        });

        this.model.htmlContent = $(this.htmlEditor).html();

        this.props.onSaveCallback(this.model);

        this.preaperedHtml.__html = this.model.htmlContent;

        this.closeModal();
    }

    private preapareHtml(htmlToPreaper: string): string {
        if (this.templateFields && this.templateFields.length > 0) {
            let res: string = htmlToPreaper;

            for (let i: number = 0; i < this.templateFields.length; i++) {
                let currField = this.templateFields[i];
                let fieldValue = this.props.documentFormManager.getTemplateFieldData(currField.key);
                let size = this.props.documentFormManager.getTemplateFieldSize(currField.key);

                while (res.indexOf(currField.key) >= 0) {

                    switch (currField.key) {
                        case '{BLANK_TEXTAREA}':
                            res = res.replace(currField.key, `<textarea class="form-control w-100" rows="3" style="margin-top: 0px; margin-bottom: 10px; height: 90px;" value="${fieldValue ? fieldValue : ''}">${fieldValue ? fieldValue : ''}</textarea>`);
                            break;

                        case '{APPLICANT_AUTHORITY}':
                        case '{APPLICANT_DATE_OF_ISSUE}':
                        case '{APPLICANT_DOCUMENT_NUMBER}':
                        case '{APPLICANT_DOCUMENT_TYPE}':
                        case '{APPLICANT_EGN_LNCH}':
                        case '{APPLICANT_PERMANENT_ADDRESS}':
                        case '{APPLICANT_PERSON_NAME}':
                        case '{COMPANY_NAME}':
                        case '{EIK_BULSTAT_PIK}':
                        case '{TYPE_DOCUMENT_REQUESTED}':
                            res = res.replace(currField.key, fieldValue);
                            break;

                        default:
                            res = res.replace(currField.key, `<input id="${ObjectHelper.newGuid()}" size="${size}" class="form-control" type="text" value="${fieldValue ? fieldValue : ''}"/>`);
                    }
                }
            }

            return res;
        } else {
            return null;
        }
    }

    containerHoverOrLeave(e: any) {
        $("#" + this.model.attachedDocumentID.toString()).toggleClass("interactive-container--focus");
    }
}

export const AttachedDocumentTemplateEditUI = withDocumentFormManager(withAsyncFrame(AttachedDocumentTemplateEditUIImpl));