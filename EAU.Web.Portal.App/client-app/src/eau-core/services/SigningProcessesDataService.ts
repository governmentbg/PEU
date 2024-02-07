import { SigningProcess } from '../models/sign/SigningProcess';
import { EAUBaseDataService } from './EAUBaseDataService';

export class SigningProcessesDataService extends EAUBaseDataService {

    protected baseUrl(): string {
        return super.baseUrl() + 'SigningProcesses';
    }

    public search(processID: string): Promise<SigningProcess[]> {
        //return this.get<SigningProcess[]>(`/${processID}`, SigningProcess, null);
        var settings: JQueryAjaxSettings;

        settings = { method: "GET", type: "GET", url: this.getFullUrl(`/${processID}`), data: null, headers: null, cache: false, dataType: "json" }

        return this.ajax<SigningProcess[]>(settings, SigningProcess);
    }
    
    public rejectSigningProcess(processID: string): Promise<boolean> {
        return this.post<boolean>(`/${processID}/reject`, 'boolean', null);
    }

    public testSign(processID: string): Promise<void> {
        return this.ajax({
            url: this.getFullUrl(`/${processID}/testSign`),
            contentType: 'application/json',
            type: 'POST',
            crossDomain: true
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