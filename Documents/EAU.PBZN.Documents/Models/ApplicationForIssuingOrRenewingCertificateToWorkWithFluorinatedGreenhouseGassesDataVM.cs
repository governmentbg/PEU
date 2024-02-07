using EAU.Documents.Domain.Models;
using EAU.PBZN.Documents.Domain.Models;

namespace EAU.PBZN.Documents.Models
{
    public class ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesDataVM
    {
        public string SunauServiceUri { get; set; }

        public EntityOrPerson EntityOrPerson { get; set; }

        public ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityDataVM ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData { get; set; }
        
        public ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonDataVM ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData { get; set; }
        
        public string WorkPhone { get; set; }

        public PoliceDepartment IssuingPoliceDepartment { get; set; }

        public PersonAddress PersonDataPermanentAddress { get; set; }

        public PersonAddress PersonDataCurrentAddress { get; set; }

    }
}