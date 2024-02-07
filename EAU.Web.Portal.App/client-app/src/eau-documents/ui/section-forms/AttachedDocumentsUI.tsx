import { AsyncUIProps, BaseProps, ConfirmationModal, withAsyncFrame } from "cnsys-ui-react";
import { EAUBaseComponent, SigningProcessUI, ValidationSummaryErrors } from "eau-core";
import { observable } from "mobx";
import { observer } from "mobx-react";
import * as React from "react";
import { AddAttachedDocumentUI } from "../AddAttachedDocumentUI";
import { DocumentFormManagerProps, withDocumentFormManager } from "../document-forms/DocumentFormManagerProvider";
import { FieldFormUI } from "../field-forms/FieldFormUI";
import { AttachedDocumentTemplateEditUI, TrigerMode } from "../AttachedDocumentTemplateEditUI"
import { ObjectHelper, ArrayHelper } from "cnsys-core";
import { AttachedDocument } from "../../models/ModelsManualAdded";

interface AttachedDocumentsUIProps extends BaseProps, DocumentFormManagerProps, AsyncUIProps {
    documentUploadURL?: string;
    getDocumentDownloadUrl: (attachedDocID: number) => Promise<string>;
    onDeleteDocument?: (doc: AttachedDocument) => Promise<void>;
    onSaveAttachedDocumentTemplate?: (document: AttachedDocument) => Promise<void>;
    onStartSigningAttachedDocumentTemplate: (attachedDocumentID: number) => Promise<void>;
    onRefreshAttachedDocuments: () => Promise<void>;
    sectionErrors?: string[];
}

var deleteDocMsgKeys = ['DOC_GL_DELETE_DOCUMENT_MSG_L', 'DOC_GL_DELETE_DOCUMENT_CONFIRM_L'];

@observer class AttachedDocumentsUIImpl extends EAUBaseComponent<AttachedDocumentsUIProps, any> {
    @observable errors: string[];
    @observable warnings: string[];

    constructor(props: AttachedDocumentsUIProps) {
        super(props);

        //Bind
        this.saveAttachedDocumentTemplate = this.saveAttachedDocumentTemplate.bind(this);
        this.ontSartSigningAttachedDocumentTemplate = this.ontSartSigningAttachedDocumentTemplate.bind(this);
        this.downloadDocument = this.downloadDocument.bind(this);
        this.onSigningCompleted = this.onSigningCompleted.bind(this);

        //Init
        this.errors = [];
        this.warnings = [];
        this.documentUploaded = this.documentUploaded.bind(this);
    }

    renderEdit(): JSX.Element {
        return (
            <>
                <FieldFormUI required>
                    <ValidationSummaryErrors errors={this.props.sectionErrors} />
                    {!ObjectHelper.isArrayNullOrEmpty(this.props.documentFormManager.service.attachedDocumentsDescription) ?
                        <div className="alert alert-info white-space-normal">
                            {this.rawHtml(this.props.documentFormManager.service.attachedDocumentsDescription)}
                        </div>
                        : null
                    }
                    {this.renderDocumentsForEdit()}
                    {this.errors && this.errors.length > 0
                        &&
                        <div key="errors" className="alert alert-danger" role="alert">
                            <ul className="list-unstyled">
                            {this.errors.map((err, idx) => { return <li key={idx}>{err}</li> })}
                            </ul>
                        </div>}
                    {this.warnings && this.warnings.length > 0
                        &&
                        <div key="warnings" className="alert alert-warning" role="alert">
                            <ul className="list-unstyled">
                                {this.warnings.map((err, idx) => { return <li key={idx}>{err}</li> })}
                            </ul>
                        </div>}
                    <AddAttachedDocumentUI
                        onDocumentUploaded={this.documentUploaded}
                        uploadURL={this.props.documentUploadURL}
                        errors={this.errors}
                        warnings={this.warnings}
                        docTypes={this.props.documentFormManager.service.attachedDocumentTypes}
                        onSaveAttachedDocumentTemplate={this.saveAttachedDocumentTemplate} />
                </FieldFormUI>
            </>);
    }

    renderDisplay(): JSX.Element {
        return (
            <>
                <FieldFormUI>
                    {this.props.documentFormManager.attachedDocuments.map((doc) => (
                        <div key={doc.attachedDocumentID} className="row">
                            <div className="form-group col-12">
                                <p className="field-text">
                                    {!ObjectHelper.isStringNullOrEmpty(doc.htmlContent)
                                        && ObjectHelper.isStringNullOrEmpty(doc.signingGuid)
                                        && ObjectHelper.isNullOrUndefined(doc.documentProcessContentID) ?
                                        <i className="ui-icon ui-icon-document mr-1" aria-hidden="true"></i> : null}
                                    {ObjectHelper.isNullOrUndefined(doc.documentProcessContentID) ?
                                        <>
                                            {doc.description}
                                        </>
                                        :
                                        <>
                                            <i className="ui-icon ui-icon-download-color mr-1"></i>
                                            <a href="" data-doc-id={doc.attachedDocumentID} onClick={this.downloadDocument}>{doc.description}</a>
                                        </>}
                                </p>
                                {ObjectHelper.isNullOrUndefined(doc.documentProcessContentID) ?
                                    <ul className="invalid-feedback" id="ERR-UNSIGNED-ATTACHED-DOCUMENT-GENERATED-BY-TEMPLATE">
                                        <li>
                                            <i className="ui-icon ui-icon-error mr-1"></i>
                                            {this.getResource('GL_UNSIGNED_ATTACHED_DOCUMENT_GENERATED_BY_TEMPLATE_E')}
                                        </li>
                                    </ul>
                                    : null}
                            </div>
                        </div>))}
                </FieldFormUI>
            </>
        );
    }

    renderDocumentsForEdit() {
        return (
            <ul className="list-filed">
                {this.props.documentFormManager.attachedDocuments.map((doc) => (
                    <li key={doc.attachedDocumentID} aria-live="polite" role="group" aria-label={doc.description}>
                        <div id={doc.attachedDocumentID.toString()} className="interactive-container">
                            <div className="interactive-container__content">
                                <div className="row">
                                    <div className="col-12 form-group">
                                        <p className="field-text">

                                            {ObjectHelper.isNullOrUndefined(doc.documentProcessContentID) ?

                                                <> <i className="ui-icon ui-icon-document mr-1" aria-hidden="true"></i>{doc.description}</>
                                                :
                                                <>
                                                    <i className="ui-icon ui-icon-download-color mr-1"></i>
                                                    <a href="" data-doc-id={doc.attachedDocumentID} onClick={this.downloadDocument}>{doc.description}</a>
                                                </>}
                                        </p>
                                        {!ObjectHelper.isStringNullOrEmpty(doc.signingGuid)
                                            && <SigningProcessUI
                                                signingProcessGuid={doc.signingGuid}
                                                onCompleted={this.onSigningCompleted}
                                                onRejected={() => { return this.props.onRefreshAttachedDocuments(); }} />}
                                    </div>
                                    {!ObjectHelper.isStringNullOrEmpty(doc.htmlContent)
                                        && ObjectHelper.isStringNullOrEmpty(doc.signingGuid)
                                        && ObjectHelper.isNullOrUndefined(doc.documentProcessContentID)
                                        && <div className="col-12 form-group">
                                            <button className="btn btn-light" data-doc-id={doc.attachedDocumentID} onClick={this.ontSartSigningAttachedDocumentTemplate}>
                                                <i className="ui-icon ui-icon-check mr-1" aria-hidden="true"></i>{this.getResource('GL_START_SIGN_L')}
                                            </button>
                                        </div>}
                                </div>
                            </div>
                            <div className="interactive-container__controls">
                                {!ObjectHelper.isStringNullOrEmpty(doc.htmlContent)
                                    && ObjectHelper.isStringNullOrEmpty(doc.signingGuid)
                                    && ObjectHelper.isNullOrUndefined(doc.documentProcessContentID)
                                    &&
                                    <AttachedDocumentTemplateEditUI
                                        trigerTag={TrigerMode.Edit}
                                        {...this.bind(doc, 'docTemplateEditUI')}
                                        onSaveCallback={this.saveAttachedDocumentTemplate} />}
                                <ConfirmationModal modalTitleKey='DOC_GL_DELETE_DOCUMENT_L' modalTextKeys={deleteDocMsgKeys} onSuccess={() => { this.deleteDocument(doc); }} yesTextKey='GL_DELETE_L' noTextKey='GL_CANCEL_L'>
                                    <button name="delete-attached-doc" className="btn btn-light btn-sm"
                                        title={this.getResource("GL_DELETE_L")}
                                        onMouseOver={() => { this.containerHover(doc.attachedDocumentID.toString()); }}
                                        onMouseLeave={() => { this.containerHoverLeave(doc.attachedDocumentID.toString()); }}
                                        onFocus={() => { this.containerHover(doc.attachedDocumentID.toString()); }}
                                        onBlur={() => { this.containerHoverLeave(doc.attachedDocumentID.toString()); }}>
                                        <i className="ui-icon ui-icon-times" aria-hidden="true"></i>
                                    </button>
                                </ConfirmationModal>
                            </div>
                        </div>
                        <hr key={"doted-line-" + doc.attachedDocumentID} className="hr--doted-line"></hr>
                    </li>))}
            </ul>
        );
    }

    //#region Events

    saveAttachedDocumentTemplate(doc: AttachedDocument): void {
        let docId = doc.attachedDocumentID?.toString();
        let that = this;
        this.props.registerAsyncOperation(this.props.onSaveAttachedDocumentTemplate(doc).then(() => {
            if (!docId) {
                if (that.props.documentFormManager.attachedDocuments
                    && that.props.documentFormManager.attachedDocuments.length > 0) {
                    let lastIdx = that.props.documentFormManager.attachedDocuments.length - 1;
                    docId = that.props.documentFormManager.attachedDocuments[lastIdx].attachedDocumentID?.toString();
                }
            }

            window.setTimeout(() => {
                $('button[data-doc-id="' + docId + '"]').focus();
            }, 200);
        }));
    }

    ontSartSigningAttachedDocumentTemplate(e: any): void {
        let attachedDocId: number = $(e.target).data('doc-id');
        this.props.registerAsyncOperation(this.props.onStartSigningAttachedDocumentTemplate(attachedDocId));
    }

    onSigningCompleted(): Promise<void> {
        let that = this;

        return this.props.onRefreshAttachedDocuments()
            .then(() => {
                let attachedDocSection = ArrayHelper.queryable.from(that.props.documentFormManager.sections).singleOrDefault(s => s.code == "attachedDocuments");

                if (attachedDocSection) {
                    attachedDocSection.validate()
                }
            });
    }

    documentUploaded(doc: AttachedDocument) {
        this.props.documentFormManager.attachedDocuments.push(doc);
    }

    deleteDocument(doc: AttachedDocument) {
        this.props.registerAsyncOperation(this.props.onDeleteDocument(doc));
        $("body").removeAttr("style");
    }

    containerHover(containerID: string) {
        $("#" + containerID).addClass("interactive-container--focus");
    }

    containerHoverLeave(containerID: string) {
        $("#" + containerID).removeClass("interactive-container--focus");
    }

    downloadDocument(e: any) {
        e.preventDefault();

        let dataVal = $(e.target).data('doc-id');
        let attachedDocID: number = Number(dataVal);

        this.props.getDocumentDownloadUrl(attachedDocID).then(downloadURL => {

            var pom = document.createElement('a');
            pom.setAttribute('href', downloadURL);
            pom.setAttribute('target', '_blank');

            document.body.appendChild(pom);

            pom.click();

            document.body.removeChild(pom);
        });
    }

    //#endregion
}

export const AttachedDocumentsUI = withDocumentFormManager(withAsyncFrame(AttachedDocumentsUIImpl));