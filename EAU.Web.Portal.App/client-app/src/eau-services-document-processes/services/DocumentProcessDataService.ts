import { EAUBaseDataService } from 'eau-core';
import { AttachedDocument, DocumentFormVMBase, NomenclatureItem } from 'eau-documents';
import { DocumentProcess, NewProcessRequest } from '../models/ModelsManualAdded';

export class DocumentProcessDataService extends EAUBaseDataService {
    protected baseUrl(): string {
        return super.baseUrl() + "Services/DocumentProcesses";
    }

    public getCreatePreviewDocumentProcessURL(): string {
        return this.baseUrl() + '/Preview';
    }

    public getDocumentProcessByServiceID(serviceID: number, removingIrregularitiesInstructionURI: string, additionalApplicationURI: string, caseFileURI: string, withdrawService?: boolean): Promise<DocumentProcess> {
        return this.get<DocumentProcess>(null, DocumentProcess, { serviceID, removingIrregularitiesInstructionURI, additionalApplicationURI, caseFileURI, withdrawService });
    }

    public getDocumentProcessByRequestID(requestID: string): Promise<DocumentProcess> {
        return this.get<DocumentProcess>('Request/' + requestID, DocumentProcess);
    }

    public getDocumentProcess(processID: number, loadAllData: boolean): Promise<DocumentProcess> {
        return this.get<DocumentProcess>(`${processID}`, DocumentProcess, { loadAllData: loadAllData });
    }

    public createDocumentProcess(request: NewProcessRequest): Promise<DocumentProcess> {
        return this.post<DocumentProcess>(null, DocumentProcess, request);
    }

    public deleteDocumentProcess(docProcessID: number): Promise<void> {
        return this.delete(`${docProcessID}`, null);
    }

    public saveDocumentForm(docProcessID: number, form: DocumentFormVMBase): Promise<void> {
        return this.put(`${docProcessID}`, null, form);
    }

    public returnToInProcessStatusAsync(docProcessID: number): Promise<void> {
        return this.put(`ReturnToInProcessStatus/${docProcessID}`, null, null);
    }

    public startSigning(docProcessID: number): Promise<string> {
        return this.post<string>(`${docProcessID}/StartSigning`, null);
    }

    public startSending(docProcessID: number): Promise<void> {
        return this.post<void>(`${docProcessID}/StartSending`, null);
    }

    public getDocumentHtml(processID: number): Promise<string> {
        return this.get<string>(`${processID}/Document/Html`, null);
    }

    public getXmlDownloadUrl(processID: number): string {
        return `${this.baseUrl()}/${processID}/Document/Xml`;
    }

    public getAttachedDocumentDownloadUrl(docProcessID: number, docID: number): Promise<string> {
        let result = `${this.baseUrl()}/${docProcessID}/AttachedDocuments/${docID}`;

        return Promise.resolve(result);
    }

    public getAttachedDocumentUploadUrl(docProcessID: number) {
        return `${this.baseUrl()}/${docProcessID}/AttachedDocuments/Upload`;
    }

    public deleteAttachedDocument(docProcessID: number, docID: number): Promise<void> {
        return this.delete(`${docProcessID}/AttachedDocuments/${docID}`, null);
    }

    public createAttachedDocumentTemplate(docProcessID: number, document: AttachedDocument): Promise<AttachedDocument> {
        return this.post<AttachedDocument>(`${docProcessID}/AttachedDocuments/`, AttachedDocument, document);
    }

    public updateAttachedDocumentTemplate(docProcessID: number, document: AttachedDocument): Promise<void> {
        return this.post<void>(`${docProcessID}/AttachedDocuments/${document.attachedDocumentID}`, AttachedDocument, document);
    }

    public startSigningAttachedDocumentTemplate(docProcessID: number, docID: number): Promise<string> {
        return this.post<string>(`${docProcessID}/AttachedDocuments/${docID}/StartSigning`, null);
    }

    public getAttachedDocuments(docProcessID: number): Promise<AttachedDocument[]> {
        return this.get<AttachedDocument[]>(`${docProcessID}/AttachedDocuments`, AttachedDocument);
    }

    public getNomenclatures(docProcessID: number): Promise<NomenclatureItem[]> {
        return this.get<NomenclatureItem[]>(`${docProcessID}/Nomenclatures`, NomenclatureItem)
    }
}