using EAU.Documents.Domain.Models;
using System.Collections.Generic;

namespace EAU.Documents.Models.Forms
{
    /// <summary>
    ///  Съобщение за неизпълнени условия за подаване на заявление за отказ
    /// </summary>
    public class OutstandingConditionsForWithdrawServiceMessageVM
        : SigningDocumentFormVMBase<DigitalSignatureContainerVM>
    {
        public System.DateTime? DocumentReceiptOrSigningDate { get; set; }

        public ElectronicServiceProviderBasicDataVM ElectronicServiceProviderBasicData { get; set; }

        public ElectronicServiceApplicantVM ElectronicServiceApplicant { get; set; }

        public string OutstandingConditionsForWithdrawServiceMessageHeader { get; set; }

        public AISCaseURIVM AISCaseURI { get; set; }

        public List<OutstandingConditionsForWithdrawServiceMessageGrounds> Grounds { get; set; }

        public string AdministrativeBodyName { get; set; }
    }
}
