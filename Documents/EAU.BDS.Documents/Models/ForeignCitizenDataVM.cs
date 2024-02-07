using EAU.Documents.Domain.Models;
using EAU.Documents.Models;

namespace EAU.BDS.Documents.Models
{
    public class ForeignCitizenDataVM
    {
        public ForeignCitizenNames ForeignCitizenNames { get; set; }

        public string GenderName { get; set; }

        public string GenderCode { get; set; }

        public string BirthDate { get; set; }

        public PlaceOfBirthAbroad PlaceOfBirthAbroad { get; set; }

        public CitizenshipVM Citizenship { get; set; }
    }
}