import * as React from "react";
import { BaseProps, withAsyncFrame, AsyncUIProps } from "cnsys-ui-react";
import { observable, runInAction } from "mobx";
import { observer } from "mobx-react";
import { ObjectHelper } from "cnsys-core";
import { EAUBaseComponent, DocumentType, appConfig, AutoCompleteUI, attributesClassFormControl, IAutoCompleteItem, Nomenclatures, resourceManager, FileUpload, IFileUploadError } from "eau-core";
import { action } from "mobx";
import { AttachedDocumentTemplateEditUI, TrigerMode } from "./AttachedDocumentTemplateEditUI";
import { AttachedDocument } from "../models/ModelsManualAdded";

interface AddAttachedDocumentUIProps extends BaseProps, AsyncUIProps {
    onDocumentUploaded: (doc: AttachedDocument) => void;
    uploadURL: string;
    docTypes: DocumentType[]
    errors: string[];
    warnings: string[];
    onSaveAttachedDocumentTemplate: (document: AttachedDocument) => void;
}

@observer class AddAttachedDocumentUIImpl extends EAUBaseComponent<AddAttachedDocumentUIProps, AttachedDocument> {
    @observable onlyFromTemplate: boolean;

    constructor(props: AddAttachedDocumentUIProps) {
        super(props);

        //Bind
        this.documentTypeSelected = this.documentTypeSelected.bind(this);
        this.renderDocTemplateEditor = this.renderDocTemplateEditor.bind(this);
        this.onSaveAttachedDocumentTemplateInternal = this.onSaveAttachedDocumentTemplateInternal.bind(this);
        this.uploadError = this.uploadError.bind(this);
        this.uploadClick = this.uploadClick.bind(this);

        //Init
        this.onlyFromTemplate = false;
        this.model = new AttachedDocument();
    }

    render(): JSX.Element {
        return (
            <div id="uploadDoc" className="loader-overlay-local">
                <div className="interactive-container">
                    <div className="interactive-container__content">
                        <fieldset>
                            <legend className="sr-only">{resourceManager.getResourceByKey("DOC_GL_AttachedDocumentsCollection_L")}</legend>
                            <div className="row">
                                <div className="form-group col-12">
                                    {this.labelFor(m => m.documentTypeID, "GL_DOCUMENT_TYPE_L", { className: "form-control-label"})}
                                    <AutoCompleteUI
                                        {...this.bind(m => m.documentTypeID)}
                                        triggerLength={0}
                                        attributes={attributesClassFormControl}
                                        dataSourceSearchDelegat={val => Promise.resolve(this.props.docTypes
                                            .filter(dt => !val || dt.name.toLowerCase().indexOf(val.toLowerCase()) >= 0)
                                            .map((dt) => {
                                                return {
                                                    id: dt.documentTypeID,
                                                    text: dt.name
                                                }
                                            }))}
                                        onChangeCallback={this.documentTypeSelected}
                                    />
                                </div>
                                <div className="form-group col-12">
                                    {this.labelFor(m => m.description, "GL_DESIGNATION_L", { className: "form-control-label" })}
                                    {this.textAreaFor(m => m.description, null, 3)}
                                </div>
                                <div className="form-group col-12">
                                    <div className="row">
                                        {this.onlyFromTemplate == true ?
                                            null
                                            :
                                            <div className="col-auto form-group">
                                                <span onClickCapture={this.uploadClick}>
                                                    <FileUpload
                                                        maxFilesizeMB={appConfig.maxRequestLengthInKB / 1024}
                                                        acceptedFiles={appConfig.acceptedFileExt.replace(' ', '')}
                                                        onUploaded={(documentSent, response, dropzone) => this.documentUploaded(documentSent, response, dropzone)}
                                                        onError={this.uploadError}
                                                        uploadURL={this.props.uploadURL}
                                                        params={this.model}
                                                        dictInvalidFileType='GL_FILE_FORMAT_DOCUMENT_ALLOWED_FORMATS_E'
                                                        dictDefaultMessage='GL_UPLOAD_ERR_E'
                                                        dictFileTooBig='GL_DOCUMENT_MAX_FILE_SIZE_EXCEEDED_E'
                                                        dictMaxFilesExceeded='GL_MAX_COUNT_ATTACHMENTS_E'
                                                        selectFileText={'GL_SELECT_FILE_L'}
                                                        addFileText='GL_ADD_DOCUMENT_L'
                                                        isEnabled={this.model.documentTypeID ? true : false}
                                                        idOfParentOfLoadingUI="uploadDoc"
                                                        accepts={function (file: any, done: any) {
                                                            if (file.size == 0) {
                                                                done("GL_UPLOAD_FILE_E");
                                                            }
                                                            else { done(); }
                                                        }}
                                                    />
                                                </span>
                                            </div>}

                                        {this.renderDocTemplateEditor()}
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                </div>
            </div>);
    }

    renderDocTemplateEditor(): JSX.Element {
        if (ObjectHelper.isStringNullOrEmpty(this.model.htmlContent))
            return null;

        let componentModel: AttachedDocument = new AttachedDocument({
            documentTypeID: this.model.documentTypeID,
            documentTypeName: this.model.documentTypeName,
            description: this.model.description,
            htmlContent: this.model.htmlContent
        });

        return (
            <div className="col-auto form-group">
                <AttachedDocumentTemplateEditUI
                    trigerTag={TrigerMode.Create}
                    {...this.bind(componentModel, 'docTemplateEditUI')}
                    onSaveCallback={this.onSaveAttachedDocumentTemplateInternal} />
            </div>);
    }

    //#region Events

    @action documentTypeSelected(item: IAutoCompleteItem) {
        if (item) {
            var docType = this.props.docTypes.filter(dt => dt.documentTypeID == item.id)[0];
            this.model.description = docType.name;
            this.model.documentTypeName = docType.name;

            let that = this;
            this.props.registerAsyncOperation(Nomenclatures.getDocumentTemplates(el => el.documentTypeID == docType.documentTypeID)
                .then(dt => {
                    if (dt && dt.length == 1) {
                        runInAction(() => {
                            that.model.htmlContent = dt[0].content;
                            that.onlyFromTemplate = dt[0].isSubmittedAccordingToTemplate;
                        });
                    }
                }));
        } else {
            this.model.htmlContent = undefined;
            this.onlyFromTemplate = false;
            this.model.description = undefined;
            this.model.documentTypeName = undefined;
        }
    }

    onSaveAttachedDocumentTemplateInternal(document: AttachedDocument): void {
        this.clear();
        this.props.onSaveAttachedDocumentTemplate(document);
    }

    @action uploadClick() {
        if (!this.model.documentTypeID) {
            this.props.errors.splice(0, this.props.errors.length);

            // грешките се зачистват на 3 места, а не само в началото на onUploadClick, защото реда на изпълнение на event-ите е различен в IE И Chrome
            this.props.errors.push(this.getResource('GL_NO_SELECT_DOCUMENT_TYPE_E'));
        } else {
            if (this.model.description == null || this.model.description == undefined || this.model.description.trim() == "")
                this.model.description = this.props.docTypes.filter(dt => dt.documentTypeID == this.model.documentTypeID)[0].name;
        }
        let tabContent = document.querySelector('#documentTypeID');
        tabContent.scrollIntoView();
        $(tabContent).focus();
    }

    documentUploaded(documentSent: any, response: any, dropzone: Dropzone) {
        this.props.errors.splice(0, this.props.errors.length);

        // грешките се зачистват на 3 места, а не само в началото на onUploadClick, защото реда на изпълнение на event-ите е различен в IE И Chrome
        var attachedDocument = new AttachedDocument(response);

        this.props.onDocumentUploaded(attachedDocument);

        let tabContent = document.querySelector('#documentTypeID');
        tabContent.scrollIntoView();

        $('button[name*="delete-attached-doc"]').last().focus();
        
        this.clear();
    }

    uploadError(errorMessage: string | IFileUploadError | Error, file: Dropzone.DropzoneFile) {
        this.props.errors.splice(0, this.props.errors.length);

        let fileExt = appConfig.acceptedFileExt.replace(' ', '');
        let notAllowedFileErr = this.getResource('GL_FILE_FORMAT_DOCUMENT_ALLOWED_FORMATS_E').replace('{FILE_FORMATS}', fileExt);

        // грешките се зачистват на 3 места, а не само в началото на onUploadClick, защото реда на изпълнение на event-ите е различен в IE И Chrome
        if (errorMessage == notAllowedFileErr) {
            this.props.errors.push(errorMessage);
        } else if (errorMessage == this.getResource('GL_DOCUMENT_MAX_FILE_SIZE_EXCEEDED_E')) {
            this.props.errors.push(errorMessage.replace('{FILE_SIZE_IN_KB}', (file.size / (1024)).toFixed().toString() + ' kB')
                .replace('{MAX_FILE_SIZE}', appConfig.maxRequestLengthInKB + ' kB'));
        } else if (errorMessage == "GL_UPLOAD_FILE_E") {
            this.props.errors.push(this.getResource(errorMessage));
        } else if (((errorMessage as IFileUploadError).code == 'SERVER_ERROR_FILE_MAX_SIZE_EXCEEDED')
            || ((errorMessage as IFileUploadError).code == 'APPLICATION_MAX_SIZE_LIMIT_CODE_WITHOUT_LABEL')) {
            this.props.errors.push((errorMessage as IFileUploadError).message);
        }
        else if ((errorMessage as IFileUploadError).code == 'GL_TOO_MANY_REQUESTS_E') {
            this.props.warnings.push(this.getResource('GL_TOO_MANY_REQUESTS_E'))
        }
        else {
            this.props.errors.push(this.getResource('GL_UPLOAD_ERR_E'));
        }
    }

    //#endregion

    //#region Helpers

    @action clear() {
        this.model.attachedDocumentGuid = null;
        this.model.attachedDocumentID = null;
        this.model.description = null;
        this.model.documentTypeID = null;
        this.model.documentTypeName = null;
        this.model.fileName = null;
        this.model.mimeType = null;
        this.model.htmlContent = null;
        this.model.signingGuid = null;
        this.onlyFromTemplate = false;
    }

    //#endregion

}

export const AddAttachedDocumentUI = withAsyncFrame(AddAttachedDocumentUIImpl);