using EAU.Documents.Domain.Models;
using System.Collections.Generic;

namespace EAU.Documents.Models.Forms
{
    /// <summary>
    /// Уведомление за прекратяване на услуга
    /// </summary>
    public class ActionsTakenMessageVM : SigningDocumentFormVMBase<DigitalSignatureContainerVM>
    {        
        public AISCaseURIVM AISCaseURI { get; set; }

        public ElectronicServiceProviderBasicDataVM ElectronicServiceProviderBasicData { get; set; }

        public ElectronicServiceApplicantVM ElectronicServiceApplicant { get; set; }

        public string AdministrativeBodyName { get; set; }

        public string ActionsTakenMessageHeader { get; set; }

        public string ActionsTakenMessageMessage { get; set; }

        public PoliceDepartment PoliceDepartment { get; set; }

        public System.DateTime? DocumentReceiptOrSigningDate { get; set; }
    }
}
