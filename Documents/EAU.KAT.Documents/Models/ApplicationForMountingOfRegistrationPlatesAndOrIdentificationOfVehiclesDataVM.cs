using EAU.Documents.Domain.Models;

namespace EAU.KAT.Documents.Models
{
    public class ApplicationForMountingOfRegistrationPlatesAndOrIdentificationOfVehiclesDataVM
    {
        public PoliceDepartment PoliceDepartment { get; set; }

        public string ApplicationText { get; set; }

        public string Phone { get; set; }
    }
}