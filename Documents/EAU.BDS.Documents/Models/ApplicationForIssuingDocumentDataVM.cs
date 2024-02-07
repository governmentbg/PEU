using EAU.BDS.Documents.Domain.Models;

namespace EAU.BDS.Documents.Models
{
    public class ApplicationForIssuingDocumentDataVM 
    {
        public DocumentToBeIssuedForVM DocumentToBeIssuedFor { get; set; }

        public AddressForIssuing AddressForIssuing { get; set; }
    }
}