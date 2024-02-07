import { ObjectHelper } from 'cnsys-core';
import { NewProcessRequest } from '../models/ModelsManualAdded';
import { DocumentProcessContextBase } from './IDocumentProcessContext';

export class ApplicationProcessContext extends DocumentProcessContextBase {

    get removingIrregularitiesInstructionURI(): string {
        return this.process.additionalData ? this.process.additionalData.removingIrregularitiesInstructionURI : null;
    }

    get caseFileURI(): string {
        return this.process.caseFileURI;
    }

    get notAcknowledgedMessageURI(): string {
        return this.process.notAcknowledgedMessageURI;
    }

    public tryLoadApplicationProcess(serviceID: number, removingIrregularitiesInstructionURI: string, additionalApplicationURI: string, caseFileURI: string, withdrawService?: boolean): Promise<boolean> {
        this.clearContext();

        return this.processDataService.getDocumentProcessByServiceID(serviceID, removingIrregularitiesInstructionURI, additionalApplicationURI, caseFileURI, withdrawService).then(process => {
            if (process) {
                return this.initDocumentProcessContext(process).bind(this).then(() => true);
            } else {
                return false
            }
        })
    }

    public createApplicationProcess(serviceID: number, removingIrregularitiesInstructionURI: string, additionalApplicationURI: string, caseFileURI: string, withdrawService?: boolean): Promise<void> {
        var request = new NewProcessRequest();
        request.serviceID = Number(serviceID);
        request.removingIrregularitiesInstructionURI = removingIrregularitiesInstructionURI ? removingIrregularitiesInstructionURI : null;
        request.additionalApplicationURI = additionalApplicationURI ? additionalApplicationURI : null;
        request.caseFileURI = caseFileURI ? caseFileURI : null;
        request.withdrawService = !ObjectHelper.isNullOrUndefined(withdrawService) ? withdrawService : null;

        return this.processDataService.createDocumentProcess(request).bind(this).then(process => {
            return this.initDocumentProcessContext(process);
        })
    }
}