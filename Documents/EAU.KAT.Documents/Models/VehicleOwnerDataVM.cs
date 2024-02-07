using EAU.KAT.Documents.Domain.Models;

namespace EAU.KAT.Documents.Models
{
    public class VehicleOwnerDataVM
    {
        public PersonEntityDataVM PersonEntityData { get; set; }

        public VehicleOwnerAdditionalCircumstances? VehicleOwnerAdditionalCircumstances { get; set; }

        public bool? IsSoldBySyndic { get; set; }

        public string SuccessorData { get; set; }
       
    }
}