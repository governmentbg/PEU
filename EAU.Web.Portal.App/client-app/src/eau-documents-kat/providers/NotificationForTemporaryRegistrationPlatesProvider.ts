import { IDocumentProvider } from "eau-documents";
import { NotificationForTemporaryRegistrationPlatesManager } from "../form-managers/NotificationForTemporaryRegistrationPlatesManager";
import { NotificationForTemporaryRegistrationPlatesValidator } from "../validations/forms/NotificationForTemporaryRegistrationPlatesValidator";

export const NotificationForTemporaryRegistrationPlatesProvider: IDocumentProvider = {
    getDocumentFormManager: () => new NotificationForTemporaryRegistrationPlatesManager(),
    getValidator: () => new NotificationForTemporaryRegistrationPlatesValidator()
}