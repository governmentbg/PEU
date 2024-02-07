using EAU.Documents.Domain.Models;
using EAU.KAT.Documents.Domain.Models;

namespace EAU.KAT.Documents.Models
{
    public class ApplicationForIssuingOfDuplicateOfDrivingLicenseControlCouponDataVM
    {
        public PoliceDepartment IssuingPoliceDepartment { get; set; }

        public PersonAddress PermanentAddress { get; set; }

        public PersonAddress CurrentAddress { get; set; }

        public bool? AgreementToReceiveERefusal { get; set; }

        public CouponDuplicateIssuensReason? CouponDuplicateIssuensReason { get; set; }
    }
}