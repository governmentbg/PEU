using EAU.Documents.Domain.Models;
using EAU.Documents.Models;
using EAU.Documents.Models.Forms;
using EAU.KAT.Documents.Domain.Models;
using System;

namespace EAU.KAT.Documents.Models.Forms
{
    /// <summary>
    /// Справка за МПС.
    /// </summary>
    public class ReportForVehicleVM : SigningDocumentFormVMBase<DigitalSignatureContainerVM>
    {
        public AISCaseURI AISCaseURI { get; set; }

        public ElectronicServiceApplicant ElectronicServiceApplicant { get; set; }

        public ReportForVehicleRPSSVehicleDataVM RPSSVehicleData { get; set; }

        public ReportForVehicleEUCARISData EUCARISData { get; set; }

        public ReportForVehicleOwnersVM Owners { get; set; }

        public ReportForVehicleGuaranteeFund GuaranteeFund { get; set; }

        public ReportForVehiclePeriodicTechnicalCheck PeriodicTechnicalCheck { get; set; }

        public DateTime? DocumentReceiptOrSigningDate { get; set; }

        public string AdministrativeBodyName { get; set; }

        public XMLDigitalSignature XMLDigitalSignature { get; set; }
    }
}
