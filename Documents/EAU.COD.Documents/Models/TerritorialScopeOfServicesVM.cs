using EAU.COD.Documents.Domain.Models;
using System.Collections.Generic;

namespace EAU.COD.Documents.Models.Forms
{
    public class TerritorialScopeOfServicesVM
    {
        public ScopeOfCertification? ScopeOfCertification { get; set; }
    
        public List<TerritorialScopeOfServicesDistrictsVM> Districts { get; set; }
    }
}