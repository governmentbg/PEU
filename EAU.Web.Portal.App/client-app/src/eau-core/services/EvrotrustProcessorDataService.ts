import { EAUBaseDataService } from './EAUBaseDataService';

export class EvrotrustProcessorDataService extends EAUBaseDataService {

    protected baseUrl(): string {
        return super.baseUrl() + 'EvrotrustProcessor';
    }

    public createSignRequest(processID: string, signerID: number, identType: number, ident: string): Promise<void> {
        return this.ajax({
            url: this.getFullUrl('/CreateSignRequest'),
            contentType: 'application/json',
            type: 'POST',
            crossDomain: true,
            data: JSON.stringify({ processID: processID, signerID: signerID, identType: identType, userIdent: ident})
        }, null);
    }

    public signerRejectSigning(processID: string, signerID: number): Promise<void> {
        return this.ajax({
            url: this.getFullUrl(`/${processID}/Signers/${signerID}/RejectSigning`),
            contentType: 'application/json',
            type: 'PUT',
            crossDomain: true
        }, null);
    }
}