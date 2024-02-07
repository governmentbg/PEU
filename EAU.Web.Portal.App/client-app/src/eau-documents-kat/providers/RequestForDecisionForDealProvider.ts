import { IDocumentProvider } from 'eau-documents'
import { RequestForDecisionForDealManager } from '../form-managers/RequestForDecisionForDealManager'
import { RequestForDecisionForDealValidator } from '../validations/forms/RequestForDecisionForDealValidator'

export const requestForDecisionForDealProvider: IDocumentProvider = {
    getDocumentFormManager: () => new RequestForDecisionForDealManager(),
    getValidator: () => new RequestForDecisionForDealValidator()
}