using EAU.Documents.Domain.Models;
using System.Collections.Generic;

namespace EAU.Documents.Models.Forms
{
    /// <summary>
    ///  Съобщение за неизпълнени условия за предоставяне на услугата
    /// </summary>
    public class OutstandingConditionsForStartOfServiceMessageVM 
        : SigningDocumentFormVMBase<DigitalSignatureContainerVM>
    {
        public System.DateTime? DocumentReceiptOrSigningDate { get; set; }

        public ElectronicServiceProviderBasicDataVM ElectronicServiceProviderBasicData { get; set; }

        public ElectronicServiceApplicantVM ElectronicServiceApplicant { get; set; }

        public string OutstandingConditionsForStartOfServiceMessageHeader { get; set; }

        public AISCaseURIVM AISCaseURI { get; set; }

        public List<OutstandingConditionsForStartOfServiceMessageGrounds> Grounds { get; set; }

        public string AdministrativeBodyName { get; set; }
    }
}
