using EAU.KAT.Documents.Domain.Models;

namespace EAU.KAT.Documents.Models
{
    public class VehicleBuyerDataVM
    {
        public PersonEntityDataVM PersonEntityData { get; set; }

        public VehicleOwnerAdditionalCircumstances? VehicleOwnerAdditionalCircumstances { get; set; }

        public string EmailAddress { get; set; }

        public bool ValidateEmail { get; set; }
       
    }
}