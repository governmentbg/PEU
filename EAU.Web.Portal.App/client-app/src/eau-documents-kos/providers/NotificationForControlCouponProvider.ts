import { IDocumentProvider } from 'eau-documents';
import { NotificationForControlCouponManager } from '../form-managers/NotificationForControlCouponManager';
import { NotificationForControlCouponValidator } from '../validations/forms/NotificationForControlCouponValidator';

export const NotificationForControlCouponProvider: IDocumentProvider = {
    getDocumentFormManager: () => new NotificationForControlCouponManager(),
    getValidator: () => new NotificationForControlCouponValidator()
}