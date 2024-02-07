using EAU.Documents.Domain.Models;
using EAU.KAT.Documents.Domain.Models;

namespace EAU.KAT.Documents.Models
{
    public class ApplicationForIssuingVehicleRegistrationNumberWithCombinationSixLettersOrDigitsDataVM
    {
        public PoliceDepartment AuthorPoliceDepartment { get; set; }

        public PoliceDepartment IssuingPoliceDepartment { get; set; }

        public string PlatesTypeCode { get; set; }

        public string PlatesTypeName { get; set; }

        public PlatesContentTypes? PlatesContentType { get; set; }

        public string AISKATVehicleTypeCode { get; set; }

        public string AISKATVehicleTypeName { get; set; }

        public int? RectangularPlatesCount { get; set; }

        public int? SquarePlatesCount { get; set; }

        public string ProvinceCode { get; set; }

        public string WishedRegistrationNumber { get; set; }

        public bool? AgreementToReceiveERefusal { get; set; }
    }
}
