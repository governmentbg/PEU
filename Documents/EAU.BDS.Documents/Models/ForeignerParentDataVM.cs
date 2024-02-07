using EAU.Documents.Domain.Models;
using EAU.Documents.Models;

namespace EAU.BDS.Documents.Models
{
    public class ForeignerParentDataVM
    {
        public bool UnknownParent { get; set; }

        public ForeignCitizenNames Names { get; set; }

        public string BirthDate { get; set; }

        public string EGN { get; set; }

        public string LNCh { get; set; }

        public CitizenshipVM Citizenship { get; set; }
    }
}