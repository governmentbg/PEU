using EAU.BDS.Documents.Domain.Models;
using EAU.Documents.Domain.Models;

namespace EAU.BDS.Documents.Models
{
    public class ForeignIdentityBasicDataVM
    {
        public ForeignCitizenDataVM ForeignCitizenData { get; set; }

        public string EGN { get; set; }

        public string LNCh { get; set; }

        public BIDEyesColor? EyesColor { get; set; }

        public BIDMaritalStatus? MaritalStatus { get; set; }

        public int? Height { get; set; }

        public string Phone { get; set; }
    }
}