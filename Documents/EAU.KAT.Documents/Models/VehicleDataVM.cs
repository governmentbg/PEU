using System.Collections.Generic;

namespace EAU.KAT.Documents.Models
{
    public class VehicleDataVM
    {
        public VehicleDataItemVM VehicleData { get; set; }

        public List<VehicleDataItemVM> VehicleDataCollection { get; set; }
    }
}
