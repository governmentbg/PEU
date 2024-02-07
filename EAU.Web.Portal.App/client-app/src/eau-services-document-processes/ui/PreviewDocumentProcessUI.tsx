import { appConfig } from "cnsys-core";
import { AsyncUIProps, BaseRouteParams, BaseRouteProps, BaseRoutePropsExt, withAsyncFrame, withRouter } from "cnsys-ui-react";
import { Constants, EAUBaseComponent, FileUpload, ValidationSummaryErrors, IFileUploadError } from "eau-core";
import { observable } from 'mobx';
import { observer } from "mobx-react";
import React from 'react';
import { DocumentProcess } from "../models/ModelsManualAdded";
import { PreviewDocumentProcessContext } from "../process-contexts/PreviewDocumentProcessContext";
import { DocumentProcessUI } from "./DocumentProcessUI";

interface PreviewDocumentProcessUIRouteParams extends BaseRouteParams {
    documentURI: string,
    caseFileURI: string
    documentProcessId?: string;
    notAcknowledgedMessageURI?: string;
}

interface ApplicationProcessUIProps extends BaseRouteProps<PreviewDocumentProcessUIRouteParams>, AsyncUIProps, BaseRoutePropsExt {
}

//TODO MVREAU2020-210
@observer class PreviewDocumentProcessUIImpl extends EAUBaseComponent<ApplicationProcessUIProps, any> {
    processContext: PreviewDocumentProcessContext;

    @observable errors: string[];

    constructor(props?: ApplicationProcessUIProps) {
        super(props);

        this.documentUploaded = this.documentUploaded.bind(this);
        this.uploadError = this.uploadError.bind(this);
        this.onProcessDeleted = this.onProcessDeleted.bind(this);

        this.processContext = new PreviewDocumentProcessContext();

        if (this.props.match.params.documentURI && this.props.match.params.caseFileURI) {
            this.props.registerAsyncOperation(this.processContext.createPreviewDocumentProcess(this.props.match.params.documentURI, this.props.match.params.caseFileURI));
        } else if (this.props.match.params.notAcknowledgedMessageURI && this.props.match.params.documentProcessId) {
            this.props.registerAsyncOperation(this.processContext.createPreviewDocumentProcess(null, null, parseInt(this.props.match.params.documentProcessId), this.props.match.params.notAcknowledgedMessageURI));
        }
    }

    render() {
        if (this.processContext && this.processContext.isContextInitialized) {
            if (!this.props.match.params.documentURI && !this.props.match.params.caseFileURI
                && !this.props.match.params.notAcknowledgedMessageURI && !this.props.match.params.documentProcessId) {
                return (
                    <div className="preview-document">
                        <DocumentProcessUI context={this.processContext} currentSectionCode={null} onChangeSection={null} onDeleted={this.onProcessDeleted} />
                    </div>);
            } else {
                return <DocumentProcessUI context={this.processContext} currentSectionCode={null} onChangeSection={null} onDeleted={this.onProcessDeleted} />
            }
        }
        else if (!this.props.match.params.documentURI && !this.props.match.params.caseFileURI
            && !this.props.match.params.notAcknowledgedMessageURI && !this.props.match.params.documentProcessId) {
            return this.renderUploadForPreview();
        }
        else {
            return (
                <div className="page-wrapper" id="ARTICLE-CONTENT">
                    <ValidationSummaryErrors asyncErrors={this.props.asyncErrors} />
                    <ValidationSummaryErrors errors={this.errors} />
                </div>);
        }
    }

    renderUploadForPreview() {
        return (
            <div className="page-wrapper" id="ARTICLE-CONTENT">
                <p>
                    {this.getResource("GL_PREVIEW_XML_DOCUMENT_L")}
                </p>
                <ValidationSummaryErrors asyncErrors={this.props.asyncErrors} />
                <ValidationSummaryErrors errors={this.errors} />
                <FileUpload
                    maxFilesizeMB={appConfig.maxRequestLengthInKB / 1024}
                    acceptedFiles={'.xml'}
                    onUploaded={(documentSent, response, dropzone) => this.documentUploaded(documentSent, response, dropzone)}
                    onError={this.uploadError}
                    uploadURL={this.processContext.getCreatePreviewDocumentProcessURL()}
                    params={{}}
                    dictInvalidFileType='GL_APPLICATION_DOCUMENT_ALLOWED_FORMATS_E'
                    dictDefaultMessage='GL_UPLOAD_ERR_E'
                    dictFileTooBig='GL_DOCUMENT_MAX_FILE_SIZE_EXCEEDED_E'
                    dictMaxFilesExceeded='GL_MAX_COUNT_ATTACHMENTS_E'
                    selectFileText={'GL_SELECT_DOCUMENT_L'}
                    addFileText='GL_ADD_DOCUMENT_L'
                    isEnabled={true}
                    idOfParentOfLoadingUI="uploadDoc"
                    accepts={function (file: any, done: any) {
                        if (file.size == 0) {
                            done("GL_UPLOAD_FILE_E");
                        }
                        else { done(); }
                    }}
                />
            </div>);
    }

    onProcessDeleted() {
        if (this.props.match.params.documentURI && this.props.match.params.caseFileURI) {
            var serviceInstacePath = Constants.PATHS.ServiceInstance.replace(':caseFileURI', this.props.match.params.caseFileURI);

            this.props.routerExt.goTo(serviceInstacePath, null);
        }
    }

    documentUploaded(documentSent: any, response: any, dropzone: Dropzone) {
        if (this.errors) {
            this.errors = null;
        }

        var documentProcess = new DocumentProcess(response);
        this.props.registerAsyncOperation(this.processContext.initDocumentProcessContext(documentProcess));
    }

    uploadError(errorMessage: string | IFileUploadError | Error, file: Dropzone.DropzoneFile) {

        // грешките се зачистват на 3 места, а не само в началото на onUploadClick, защото реда на изпълнение на event-ите е различен в IE И Chrome
        if (errorMessage == this.getResource('GL_FILE_FORMAT_DOCUMENT_ALLOWED_FORMATS_E').replace('{FILE_FORMATS}', '.xml')) {
            this.errors = [errorMessage];
        } else if (errorMessage == this.getResource('GL_DOCUMENT_MAX_FILE_SIZE_EXCEEDED_E')) {
            this.errors = [errorMessage.replace('{FILE_SIZE_IN_KB}', (file.size / (1024)).toFixed().toString() + ' kB')
                .replace('{MAX_FILE_SIZE}', appConfig.maxRequestLengthInKB + ' kB')];
        } else if (errorMessage == "GL_UPLOAD_FILE_E") {
            this.errors = [this.getResource(errorMessage)];
        } else if ((errorMessage as IFileUploadError).code == 'GL_CANNOT_PREVIEW_XML_DOCUMENT_E') {
            this.errors = [(errorMessage as IFileUploadError).message];
        } else {
            this.errors = [this.getResource('GL_UPLOAD_ERR_E')];
        }
    }
}

export const PreviewDocumentProcessUI = withRouter(withAsyncFrame(PreviewDocumentProcessUIImpl, false));