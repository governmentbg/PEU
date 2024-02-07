import { DocumentProcessContextBase, NewProcessRequest, ProcessStatuses } from 'eau-services-document-processes';

export class DocumentProcessContext extends DocumentProcessContextBase {

    public get errorMessage() {
        return this.process.errorMessage;
    }

    public tryLoadApplicationProcess(processID: number): Promise<boolean> {
        this.clearContext();

        return this.processDataService.getDocumentProcess(processID, true).bind(this).then(process => {
            if (process) {
                return this.initDocumentProcessContext(process).bind(this).then(() => true);
            }
            else {
                return false
            }
        })
    }

    public createApplicationProcess(requestID: string, requestMetadataUrl: string): Promise<void> {
        var request = new NewProcessRequest();
        request.requestID = requestID;
        request.documentMetadataURL = requestMetadataUrl;

        return this.processDataService.createDocumentProcess(request).bind(this).then(process => {
            return this.initDocumentProcessContext(process);
        });
    }

    public async returnToInProcessStatus(): Promise<void> {
        await this.processDataService.returnToInProcessStatusAsync(this.process.documentProcessID);
        this.process.status = ProcessStatuses.InProcess;
    }
}