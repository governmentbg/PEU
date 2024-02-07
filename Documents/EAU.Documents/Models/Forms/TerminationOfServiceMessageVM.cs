using EAU.Documents.Domain.Models;
using System.Collections.Generic;

namespace EAU.Documents.Models.Forms
{
    /// <summary>
    /// Уведомление за прекратяване на услуга
    /// </summary>
    public class TerminationOfServiceMessageVM : SigningDocumentFormVMBase<DigitalSignatureContainerVM>
    {
        public System.DateTime? DocumentReceiptOrSigningDate { get; set; }

        public ElectronicServiceProviderBasicDataVM ElectronicServiceProviderBasicData { get; set; }

        public ElectronicServiceApplicantVM ElectronicServiceApplicant { get; set; }

        public string TerminationOfServiceMessageHeader { get; set; }

        public AISCaseURIVM AISCaseURI { get; set; }

        public List<TerminationOfServiceMessageGrounds> Grounds { get; set; }

        public string TerminationDocumentRegistrationNumber { get; set; }

        public System.DateTime? TerminationDate { get; set; }

        public PoliceDepartment PoliceDepartment { get; set; }

        public string AdministrativeBodyName { get; set; }
    }
}
