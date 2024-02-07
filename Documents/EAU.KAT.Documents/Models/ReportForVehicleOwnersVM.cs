using EAU.KAT.Documents.Domain.Models;
using System.Collections.Generic;

namespace EAU.KAT.Documents.Models
{
    public class ReportForVehicleOwnersVM
    {
        public List<EntityData> EntityDataItems { get; set; }
        
        public List<PersonData> PersonDataItems { get; set; }
    }
}
