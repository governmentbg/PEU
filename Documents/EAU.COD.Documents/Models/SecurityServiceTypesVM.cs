using EAU.COD.Documents.Domain.Models;

namespace EAU.COD.Documents.Models.Forms
{
    public class SecurityServiceTypesVM
    {
        public PointOfPrivateSecurityServicesLaw PointOfPrivateSecurityServicesLaw { get; set; }

        public TerritorialScopeOfServicesVM TerritorialScopeOfServices { get; set; }

        public bool IsSelected { get; set; }
    }
}