using EAU.KAT.Documents.Domain.Models;
using System.Collections.Generic;

namespace EAU.KAT.Documents.Models
{
    public class ReportForVehicleRPSSVehicleDataVM
    {
        public VehicleRegistrationData VehicleRegistrationData { get; set; }
        
        public List<ReportForVehicleRPSSVehicleDataOwnersVM> Owners { get; set; }

        public List<Status> Statuses { get; set; }
    }
}
