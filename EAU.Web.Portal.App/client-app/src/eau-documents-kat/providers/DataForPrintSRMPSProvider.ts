import { IDocumentProvider } from 'eau-documents'
import { DataForPrintSRMPSManager } from '../form-managers/DataForPrintSRMPSManager'
import { DataForPrintSRMPSValidator } from '../validations/forms/DataForPrintSRMPSValidator'

export const dataForPrintSRMPSProvider: IDocumentProvider = {
    getDocumentFormManager: () => new DataForPrintSRMPSManager(),
    getValidator: () => new DataForPrintSRMPSValidator()
}