using EAU.Documents.Domain.Models;
using System;
using System.Collections.Generic;

namespace EAU.Documents.Models.Forms
{
    /// <summary>
    /// Отказ
    /// </summary>
    public class RefusalVM : SigningDocumentFormVMBase<OfficialVM>
    {
        public AISCaseURIVM AISCaseURI { get; set; }

        public string AdministrativeBodyName { get; set; }

        public ElectronicServiceProviderBasicDataVM ElectronicServiceProviderBasicData { get; set; }
        
        public ElectronicServiceApplicantVM ElectronicServiceApplicant { get; set; }

        public string RefusalHeader { get; set; }
       
        public string RefusalContent { get; set; }

        public DateTime? DocumentReceiptOrSigningDate { get; set; }
    }
}
