import { IDocumentProvider } from "eau-documents";
import { ReportForChangingOwnershipV2Manager } from "../form-managers/ReportForChangingOwnershipV2Manager";
import { ReportForChangingOwnershipV2Validator } from "../validations/forms/ReportForChangingOwnershipV2Validator";

export const ReportForChangingOwnershipV2Provider: IDocumentProvider = {
    getDocumentFormManager: () => new ReportForChangingOwnershipV2Manager(),
    getValidator: () => new ReportForChangingOwnershipV2Validator()
}