using EAU.Documents.Models;
using EAU.KAT.Documents.Domain.Models;

namespace EAU.KAT.Documents.Models
{
    public class VehicleOwnerInformationItemVM 
    {
        public PersonAndEntityBasicDataVM Owner { get; set; }

        public VehicleOwnerAddress Address { get; set; }
    }
}
