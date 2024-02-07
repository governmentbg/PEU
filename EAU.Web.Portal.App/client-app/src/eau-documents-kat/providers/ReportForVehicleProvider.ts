import { IDocumentProvider } from "eau-documents";
import { ReportForVehicleManager } from "../form-managers/ReportForVehicleManager";
import { ReportForVehicleValidator } from "../validations/forms/ReportForVehicleValidator";

export const ReportForVehicleProvider: IDocumentProvider = {
    getDocumentFormManager: () => new ReportForVehicleManager(),
    getValidator: () => new ReportForVehicleValidator()
}