using EAU.Documents.Domain.Models;
using EAU.KAT.Documents.Domain.Models;

namespace EAU.KAT.Documents.Models
{
    public class ApplicationForIssuingDocumentofVehicleOwnershipDataVM 
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

        public DocumentFor? DocumentFor
        {
            get;
            set;
        }

        public ApplicationForIssuingDocumentofVehicleOwnershipDataRegistrationAndMakeVM RegistrationAndMake
        {
            get;
            set;
        }

        public OwnershipCertificateReason? OwnershipCertificateReason
        {
            get;
            set;
        }
    }
}
