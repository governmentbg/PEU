import { IDocumentProvider } from 'eau-documents'
import { ApplicationForIssuingOfDuplicateOfDrivingLicenseControlCouponManager } from '../form-managers/ApplicationForIssuingOfDuplicateOfDrivingLicenseControlCouponManager'
import { ApplicationForIssuingOfDuplicateOfDrivingLicenseControlCouponValidator } from '../validations/forms/ApplicationForIssuingOfDuplicateOfDrivingLicenseControlCouponValidator'

export const applicationForIssuingOfDuplicateOfDrivingLicenseControlCouponProvider: IDocumentProvider = {
    getDocumentFormManager:() => new ApplicationForIssuingOfDuplicateOfDrivingLicenseControlCouponManager(),
    getValidator: () => new ApplicationForIssuingOfDuplicateOfDrivingLicenseControlCouponValidator()
}