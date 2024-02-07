import { IDocumentProvider } from "eau-documents";
import { ReportForChangingOwnershipManager } from "../form-managers/ReportForChangingOwnershipManager";
import { ReportForChangingOwnershipValidator } from "../validations/forms/ReportForChangingOwnershipValidator";

export const reportForChangingOwnershipProvider: IDocumentProvider = {
    getDocumentFormManager: () => new ReportForChangingOwnershipManager(),
    getValidator: () => new ReportForChangingOwnershipValidator()
}