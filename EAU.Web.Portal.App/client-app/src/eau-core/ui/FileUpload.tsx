import { appConfig, ObjectHelper, UrlHelper } from "cnsys-core";
import { observable, runInAction } from "mobx";
import { observer } from "mobx-react";
import React from "react";
import { moduleContext } from '../ModuleContext';

interface FileUploadProps {
    onUploaded?: (fileInfo: any, response: any, dropzone: Dropzone) => void,
    onError?: (error: any, file: Dropzone.DropzoneFile) => void,
    uploadURL: string,
    acceptedFiles: string,
    maxFilesizeMB: number,
    params: any,
    dictInvalidFileType: string,
    dictDefaultMessage: string,
    dictFileTooBig: string,
    dictMaxFilesExceeded: string,
    selectFileText: string,
    addFileText: string,
    isEnabled: boolean,
    timeout?: number,
    idOfParentOfLoadingUI?: string,
    accepts?: any,
    maxFiles?: number
}

export interface IFileUploadError {
    code: string;
    message: string;
}

@observer export class FileUpload extends React.Component<FileUploadProps, {}>{
    private myDropzone: Dropzone;
    private dropzoneId: string;
    @observable private isDropZoneLoaded: boolean;
    @observable private showLoadingUI: boolean;

    constructor(props?: any, context?: any) {
        super(props, context);

        this.dropzoneId = ObjectHelper.newGuid();
        this.isDropZoneLoaded = false;
    }

    componentDidMount() {
        let that = this;

        import(/* webpackChunkName: "dropzone" */ 'dropzone').then((Dropzone) => {
            runInAction(() => {
                let options: Dropzone.DropzoneOptions = {
                    url: UrlHelper.generateContentUrl(that.props.uploadURL),
                    //method:"default is POST"
                    maxFilesize: this.props.maxFilesizeMB, //in MB
                    filesizeBase: 1024,
                    //uploadMultiple:"boolean",
                    //previewsContainer:"HTMLElement or a CSS selector",
                    acceptedFiles: this.props.acceptedFiles,
                    maxFiles: this.props.maxFiles,
                    timeout: that.props.timeout ? that.props.timeout : 1000000,
                    addRemoveLinks: true,
                    autoProcessQueue: false, // if hat.props.instantUpload is true, we call processQueue in the addedfile method AFTER we get token (it may have expired!)
                    previewsContainer: "#preview-file",
                    previewTemplate: document.querySelector("#preview-template").innerHTML,
                    dictInvalidFileType: moduleContext.resourceManager.getResourceByKey(that.props.dictInvalidFileType).replace('{FILE_FORMATS}', that.props.acceptedFiles),
                    dictDefaultMessage: moduleContext.resourceManager.getResourceByKey(that.props.dictDefaultMessage),
                    dictFileTooBig: moduleContext.resourceManager.getResourceByKey(that.props.dictFileTooBig),
                    dictRemoveFile: '',
                    dictCancelUpload: '',
                    params: that.getDropzoneParams(),
                    dictMaxFilesExceeded: moduleContext.resourceManager.getResourceByKey(that.props.dictMaxFilesExceeded),
                    headers: {
                        'Content-Type': undefined,
                        'encType': 'multipart/form-data'
                    },
                    accept: this.props.accepts
                }

                that.myDropzone = new (Dropzone as any).default(document.getElementById('my-dropzone' + that.dropzoneId), options)
                that.isDropZoneLoaded = true;

                let parentLoadingDiv = ObjectHelper.isStringNullOrEmpty(that.props.idOfParentOfLoadingUI) ? undefined : document.getElementById(that.props.idOfParentOfLoadingUI);

                //#region Events

                that.myDropzone.on("queuecomplete", function () {
                    //that.removeAllFiles();
                });

                that.myDropzone.on("maxfilesexceeded", function (file: any) {
                    this.removeAllFiles();
                    this.addFile(file);
                })

                that.myDropzone.on("error", function (file: Dropzone.DropzoneFile, message: string | IFileUploadError | Error, xhr: XMLHttpRequest) {
                    that.showLoadingUI = false;
                    if (parentLoadingDiv) {
                        parentLoadingDiv.setAttribute('class', 'loader-overlay-local');
                    }

                    if (xhr && (xhr.status == 403)) {
                        message = options.dictInvalidFileType;
                    }

                    if (xhr && (xhr.status == 413)) {
                        message = {
                            code: 'SERVER_ERROR_FILE_MAX_SIZE_EXCEEDED',
                            message: (message as string)
                        }
                    }

                    if (xhr && (xhr.status == 429)) {
                        message = {
                            code: 'GL_TOO_MANY_REQUESTS_E',
                            message: (message as string)
                        }
                    }

                    that.props.onError && that.props.onError(message, file);
                    this.removeAllFiles();
                })

                that.myDropzone.on("addedfile", function (file: any) {
                    that.showLoadingUI = true;

                    if (parentLoadingDiv) {
                        parentLoadingDiv.setAttribute('class', 'loader-overlay-local load');
                    }

                    // TODO да се види това дали да не се взима от appParameter
                    //ако до 20 минути не се качи файла, прекъсваме качването
                    Promise.delay(1200000).then(() => {
                        if (that.showLoadingUI) {
                            that.showLoadingUI = false;
                            if (parentLoadingDiv) {
                                parentLoadingDiv.setAttribute('class', 'loader-overlay-local');
                            }
                            that.myDropzone.removeAllFiles(true);
                        }
                    });

                    (that.myDropzone as any).options.headers = {
                        'Content-Type': undefined,
                        'encType': 'multipart/form-data'
                    };

                    //TODO: Да се помисли как да се оправи без този delay
                    Promise.delay(100).then(() => {
                        that.myDropzone.processQueue();
                    })
                })

                that.myDropzone.on("removedfile", (file: Dropzone.DropzoneFile) => {
                })

                that.myDropzone.on("success", (file: any, response: any) => {
                    that.showLoadingUI = false;

                    if (parentLoadingDiv) {
                        parentLoadingDiv.setAttribute('class', 'loader-overlay-local');
                    }

                    that.props.onUploaded(file, response, this.myDropzone);
                })

                this.myDropzone.on("canceled", (file: any) => {
                    that.showLoadingUI = false;

                    if (parentLoadingDiv) {
                        parentLoadingDiv.setAttribute('class', 'loader-overlay-local');
                    }
                })

                if (!that.props.isEnabled)
                    that.myDropzone.disable();

                //#endregion Events
            });
        });
    }

    componentDidUpdate(prevProps: FileUploadProps, prevState: any, snapshot: any): void {
        if (this.isDropZoneLoaded) {
            if (this.props.isEnabled) {
                this.myDropzone.enable();
            } else {
                this.myDropzone.disable();
            }

            (this.myDropzone as any).options.headers = {
                'Content-Type': undefined,
                'encType': 'multipart/form-data'
            };

            (this.myDropzone as any).options.url = UrlHelper.generateContentUrl(this.props.uploadURL);
            (this.myDropzone as any).options.params = this.getDropzoneParams();
        }
    }

    componentWillUnmount() {
        if (this.isDropZoneLoaded) {
            this.myDropzone.destroy();
        }
    }

    render() {
        return (
            <>
                <div className={this.showLoadingUI && ObjectHelper.isStringNullOrEmpty(this.props.idOfParentOfLoadingUI) ? "loader-overlay-local load" : ""}>
                    <button className="btn btn-light" type="button" id={"my-dropzone" + this.dropzoneId}>
                        <i className="ui-icon ui-icon-upload-color mr-1" aria-hidden="true"></i>
                        {moduleContext.resourceManager.getResourceByKey(this.props.selectFileText)}
                    </button>
                    <span id="preview-file" style={{ display: "none" }}>
                    </span>
                    <span id="preview-template" style={{ display: "none" }}>
                        <span data-dz-remove></span>
                    </span>


                </div>
            </>);
    }

    private handleClick(e: Event) {
        e.preventDefault();
    }

    /** Ако в multipart/form-data някое поле е null или undefined при десерилизацията му в string се десерилизира като "null" или "undefined" */
    getDropzoneParams(): any {
        var params = JSON.parse(JSON.stringify(this.props.params));
        var result: any = {};

        for (var propName in params) {
            if (params[propName]) {
                result[propName] = params[propName];
            }
        }

        return result;
    }
}
