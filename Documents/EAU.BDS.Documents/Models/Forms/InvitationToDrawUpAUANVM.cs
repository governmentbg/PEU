using EAU.Documents.Models;
using EAU.Documents.Models.Forms;
using System;

namespace EAU.BDS.Documents.Models.Forms
{
    public class InvitationToDrawUpAUANVM : SigningDocumentFormVMBase<OfficialVM>
    {
        public ElectronicServiceProviderBasicDataVM ElectronicServiceProviderBasicData { get; set; }

        public AISCaseURIVM AISCaseURI { get; set; }

        public string Title { get; set; }
        
        public string Content { get; set; }

        public string AdministrativeBodyName { get; set; }

        public DateTime? DocumentReceiptOrSigningDate { get; set; }
    }
}
