import moment from 'moment'
import { ArrayHelper, ObjectHelper } from 'cnsys-core';
import { DocumentType, Nomenclatures, Service } from 'eau-core';
import { AttachedDocument, DocumentProviderFactory, IDocumentFormManager, IDocumentProvider } from 'eau-documents';
import { action, computed, observable, runInAction } from 'mobx';
import { DocumentProcessDataService } from '../services/DocumentProcessDataService';
import { DocumentProcess, ProcessStatuses } from '../models/ModelsManualAdded';

export interface IDocumentProcessContext {
    isContextInitialized: boolean;
    status: ProcessStatuses;
    signingGuid: string;
    caseFileUri: string;
    formManager: IDocumentFormManager;
    provider: IDocumentProvider;

    clearContext: () => void;
    saveDocumentForm: () => Promise<void>;

    deleteDocumentProcess: () => Promise<void>;
    startSigning: () => Promise<void>;
    startSending: () => Promise<void>;

    getDocumentHtml(): Promise<string>;
    getXmlDownloadUrl(): string;

    refresh: () => Promise<void>;
    disposeRefresh: () => void;

    getAttachedDocumentUploadURL: () => string;
    getAttachedDocumentDownloadURL: (attachedDocID: number) => Promise<string>;
    deleteAttachedDocument: (document: AttachedDocument) => Promise<void>;
    getDocumentTypeId: () => number;

    saveAttachedDocumentTemplate: (document: AttachedDocument) => Promise<void>;
    startSigningAttachedDocumentTemplate: (attachedDocumentID: number) => Promise<void>;
    refreshAttachedDocuments: () => Promise<void>;

    hasChangesInApplicationsNomenclature: boolean;
    hasChangedApplicant: boolean;
}

export class DocumentProcessContextBase implements IDocumentProcessContext {
    @observable protected _isContextInitialized: boolean;
    protected _formManager: IDocumentFormManager;
    protected _provider: IDocumentProvider;

    protected processDataService = new DocumentProcessDataService();
    protected process: DocumentProcess;
    protected service: Service;
    protected documentType: DocumentType;
    protected docFormLastSavedState: string;
    protected refreshInerval: any;
    protected refreshCounts: number = 1;

    constructor() {

        this.getAttachedDocumentDownloadURL = this.getAttachedDocumentDownloadURL.bind(this);
        this.deleteAttachedDocument = this.deleteAttachedDocument.bind(this);
        this.refreshSecondary = this.refreshSecondary.bind(this);
        this.disposeRefresh = this.disposeRefresh.bind(this);
        this.saveAttachedDocumentTemplate = this.saveAttachedDocumentTemplate.bind(this);
        this.startSigningAttachedDocumentTemplate = this.startSigningAttachedDocumentTemplate.bind(this);
        this.refreshAttachedDocuments = this.refreshAttachedDocuments.bind(this);
    }

    //#region IDocumentProcessContext

    @computed get isContextInitialized(): boolean {
        return this._isContextInitialized
    }

    get status(): ProcessStatuses {
        return this.process.status;
    }

    get signingGuid(): string {
        return this.process.signingGuid;
    }

    get formManager(): IDocumentFormManager {
        return this._formManager;
    }

    get provider(): IDocumentProvider {
        return this._provider;
    }

    get caseFileUri(): string {
        return this.process.caseFileURI;
    }

    get removingIrregularitiesInstructionURI(): string {
        return this.process.additionalData ? this.process.additionalData.removingIrregularitiesInstructionURI : null;
    }

    get caseFileURI(): string {
        return this.process.caseFileURI;
    }

    get notAcknowledgedMessageURI(): string {
        return this.process.notAcknowledgedMessageURI;
    }

    get documentProcessID(): number {
        return this.process.documentProcessID;
    }

    get createdOn(): moment.Moment {
        return this.process.createdOn;
    }

    @action clearContext() {
        this.process = null;
        this._formManager = null;
        this._provider = null;
        this._isContextInitialized = false;
    };

    saveDocumentForm(): Promise<void> {
        var docFormState = JSON.stringify(this.formManager.documentForm);

        if (this.docFormLastSavedState != docFormState) {
            return this.processDataService.saveDocumentForm(this.process.documentProcessID, this.formManager.documentForm).then(() => {
                this.docFormLastSavedState = docFormState
            })
        }

        return Promise.resolve();
    }

    deleteDocumentProcess(): Promise<void> {
        return this.processDataService.deleteDocumentProcess(this.process.documentProcessID).then(() => {
            this.clearContext();
        })
    }

    startSigning(): Promise<void> {
        return this.processDataService.startSigning(this.process.documentProcessID).then(signGuid => {
            runInAction(() => {
                this.process.signingGuid = signGuid;
                this.process.status = ProcessStatuses.Signing;
            })
        })
    }

    startSending(): Promise<void> {
        return this.processDataService.startSending(this.process.documentProcessID).then(() => {
            this.process.status = ProcessStatuses.Sending;
        })
    }

    getDocumentHtml(): Promise<string> {
        return this.processDataService.getDocumentHtml(this.process.documentProcessID);
    }

    getXmlDownloadUrl(): string {
        return this.processDataService.getXmlDownloadUrl(this.process.documentProcessID);
    }

    refresh(isSecondary: boolean = false): Promise<void> {
        if (this.refreshInerval) {
            clearInterval(this.refreshInerval);
            this.refreshInerval = null;
        }

        if (isSecondary) {
            this.refreshCounts += 1;
        }
        else {
            this.refreshCounts = 1;
        }

        return this.processDataService.getDocumentProcess(this.process.documentProcessID, false).bind(this).then(process => {
            runInAction(() => {
                this.process.status = process.status;
                this.process.errorMessage = process.errorMessage;
                this.process.signingGuid = process.signingGuid;
                this.process.caseFileURI = process.caseFileURI;
                this.process.notAcknowledgedMessageURI = process.notAcknowledgedMessageURI;
            })

            if (this.process.status == ProcessStatuses.ReadyForSending) {
                //TODO StartSending ако е необходимо
            }

            if (this.process.status == ProcessStatuses.Sending ||
                this.process.status == ProcessStatuses.Accepted) {
                this.refreshInerval = setInterval(this.refreshSecondary, this.refreshCounts > 100 ? 5000 : 500);
            }
        })
    }

    disposeRefresh() {
        clearInterval(this.refreshInerval)
    }

    getAttachedDocumentUploadURL(): string {
        return this.processDataService.getAttachedDocumentUploadUrl(this.process.documentProcessID);
    }

    getAttachedDocumentDownloadURL(attachedDocID: number): Promise<string> {
        return this.processDataService.getAttachedDocumentDownloadUrl(this.process.documentProcessID, attachedDocID);
    }

    getDocumentTypeId(): number {
        return this.process.documentTypeID;
    }

    deleteAttachedDocument(document: AttachedDocument): Promise<void> {
        return this.processDataService.deleteAttachedDocument(this.process.documentProcessID, document.attachedDocumentID).bind(this).then(() => {
            const index = this.formManager.attachedDocuments.indexOf(document);

            ArrayHelper.removeElement(this.formManager.attachedDocuments, this.formManager.attachedDocuments[index]);
        });
    }

    saveAttachedDocumentTemplate(document: AttachedDocument): Promise<void> {
        let that = this;
        let isCreateOperation: boolean = ObjectHelper.isNullOrUndefined(document.attachedDocumentID);

        if (isCreateOperation) {
            document.documentProcessID = this.process.documentProcessID;

            return this.processDataService.createAttachedDocumentTemplate(this.process.documentProcessID, document).then(ad => {
                if (ad) {
                    runInAction(() => {
                        ad.documentTypeName = document.documentTypeName;
                        that.formManager.attachedDocuments.push(ad);
                    });
                }
            });
        } else {
            return this.processDataService.updateAttachedDocumentTemplate(this.process.documentProcessID, document);
        }
    }

    startSigningAttachedDocumentTemplate(attachedDocumentID: number): Promise<void> {
        let that = this;
        return this.processDataService.startSigningAttachedDocumentTemplate(this.process.documentProcessID, attachedDocumentID)
            .then(signingGuid => {
                runInAction(() => {
                    let docStartSignProcess: AttachedDocument = ArrayHelper.queryable.from(that.formManager.attachedDocuments)
                        .single(el => el.attachedDocumentID == attachedDocumentID);

                    docStartSignProcess.signingGuid = signingGuid;
                });
            });
    }

    refreshAttachedDocuments(): Promise<void> {
        let that = this;
        return this.processDataService.getAttachedDocuments(this.process.documentProcessID).then(docs => {
            if (docs && docs.length > 0) {
                runInAction(() => {
                    for (let i: number = 0; i < docs.length; i++) {
                        let currDoc = docs[i];
                        let attachedDoc = ArrayHelper.queryable.from(that.formManager.attachedDocuments).single(el => el.attachedDocumentID == currDoc.attachedDocumentID);

                        attachedDoc.description = currDoc.description;
                        attachedDoc.documentProcessContentID = currDoc.documentProcessContentID;
                        attachedDoc.fileName = currDoc.fileName;
                        attachedDoc.htmlContent = currDoc.htmlContent;
                        attachedDoc.mimeType = currDoc.mimeType;
                        attachedDoc.signingGuid = currDoc.signingGuid;
                        attachedDoc.attachedDocumentGuid = currDoc.attachedDocumentGuid;
                    }
                });
            }
        });
    }

    get hasChangesInApplicationsNomenclature(): boolean {
        return this.process ? this.process.hasChangesInApplicationsNomenclature : null;
    };

    get hasChangedApplicant(): boolean {
        return this.process ? this.process.hasChangedApplicant : null;
    };

    //#endregion

    //#region Helpers

    protected initDocumentProcessContext(process: DocumentProcess): Promise<void> {
        this.process = process;

        var srvId = process.serviceID ? process.serviceID : this.process.additionalData['ServiceID'] ? Number(this.process.additionalData['ServiceID']) : null;
        var srvPromis = Nomenclatures.getServices(s => s.serviceID == srvId);
        var docPromis = Nomenclatures.getDocumentTypes(d => d.documentTypeID == process.documentTypeID);

        return Promise.all([srvPromis, docPromis]).bind(this).then(result => {
            this.service = result[0][0];
            this.documentType = result[1][0];

            return DocumentProviderFactory.getDocumentProvider(this.documentType.uri).bind(this).then(provider => {
                this._provider = provider;
                this._formManager = this.provider.getDocumentFormManager();

                this._formManager.init(this.process.form, {
                    additionalData: this.process.additionalData,
                    attachedDocuments: this.process.attachedDocuments,
                    service: this.service,
                    documentType: this.documentType,
                    mode: this.process.additionalData.documentInitializationMode,
                    getNomenclatures: () => this.processDataService.getNomenclatures(this.process.documentProcessID)
                }, this.provider);

                this.docFormLastSavedState = JSON.stringify(this.formManager.documentForm);
                this._isContextInitialized = true;

                if (this.process.status == ProcessStatuses.Sending ||
                    this.process.status == ProcessStatuses.Accepted) {
                    this.refreshCounts = 1
                    this.refreshInerval = setInterval(this.refreshSecondary, this.refreshCounts > 100 ? 5000 : 500);
                }
            })
        })
    }

    refreshSecondary() {
        this.refresh(true);
    }

    //#endregion
}