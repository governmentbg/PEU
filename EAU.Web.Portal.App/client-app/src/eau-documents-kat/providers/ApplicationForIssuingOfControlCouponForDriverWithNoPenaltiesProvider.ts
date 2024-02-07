import { IDocumentProvider } from 'eau-documents'
import { ApplicationForIssuingOfControlCouponForDriverWithNoPenaltiesManager } from '../form-managers/ApplicationForIssuingOfControlCouponForDriverWithNoPenaltiesManager'
import { ApplicationForIssuingOfControlCouponForDriverWithNoPenaltiesValidator } from '../validations/forms/ApplicationForIssuingOfControlCouponForDriverWithNoPenaltiesValidator'

export const applicationForIssuingOfControlCouponForDriverWithNoPenaltiesProvider: IDocumentProvider = {
    getDocumentFormManager:() => new ApplicationForIssuingOfControlCouponForDriverWithNoPenaltiesManager(),
    getValidator: () => new ApplicationForIssuingOfControlCouponForDriverWithNoPenaltiesValidator()
}