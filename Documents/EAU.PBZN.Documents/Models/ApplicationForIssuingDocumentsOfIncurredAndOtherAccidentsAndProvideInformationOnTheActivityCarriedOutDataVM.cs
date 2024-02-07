using EAU.Documents.Domain.Models;
using EAU.Documents.Models;

namespace EAU.PBZN.Documents.Models
{
    public class ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutDataVM
    {
        public EntityAddress EntityManagementAddress { get; set; }

        public EntityAddress CorespondingAddress { get; set; }

        public string PhoneNumber { get; set; }

        public string DocumentCertifyingTheAccidentOccurredOrOtherInformation { get; set; }

        public PoliceDepartment IssuingPoliceDepartment { get; set; }

        public DocumentMustServeToVM DocumentMustServeTo { get; set; }

        public string SunauServiceUri { get; set; }

        public bool? IsRecipientEntity { get; set; }

        public bool? IncludeInformation107 { get; set; }

        public bool? IncludeInformation133 { get; set; }
    }
}