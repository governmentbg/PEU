using EAU.Documents.Domain.Models;
using System.Collections.Generic;

namespace EAU.PBZN.Documents.Models
{
    public class ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityDataVM
    {
        public EntityAddress EntityManagementAddress { get; set; }

        public EntityAddress CorrespondingAddress { get; set; }

        public string DeclaredScopeOfCertification { get; set; }

        public List<CertifiedPersonelVM> AvailableCertifiedPersonnel { get; set; }
    }
}