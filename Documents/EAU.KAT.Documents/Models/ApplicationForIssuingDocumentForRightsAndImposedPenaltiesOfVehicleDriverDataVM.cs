using EAU.Documents.Domain.Models;
using EAU.KAT.Documents.Domain.Models;

namespace EAU.KAT.Documents.Models
{
    public class ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverDataVM 
    {
        public PersonAddress PermanentAddress
        {
            get;
            set;
        }
        public PersonAddress CurrentAddress
        {
            get;
            set;
        }
        public ANDCertificateReason? ANDCertificateReason
        {
            get;
            set;
        }
    }
}
