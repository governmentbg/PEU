using EAU.Documents.Domain.Models;
using EAU.PBZN.Documents.Domain.Models;

namespace EAU.PBZN.Documents.Models
{
    public class ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonDataVM
    {
        public PersonAddress PermanentAddress { get; set; }

        public PersonAddress CurrentAddress { get; set; }

        public string CertificateNumber { get; set; }

        public CertificateType? CertificateType { get; set; }

		public string DiplomaNumber { get; set; }
    }
}