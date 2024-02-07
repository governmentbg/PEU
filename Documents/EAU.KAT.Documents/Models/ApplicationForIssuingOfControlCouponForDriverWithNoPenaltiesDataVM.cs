using EAU.Documents.Domain.Models;

namespace EAU.KAT.Documents.Models
{
    public class ApplicationForIssuingOfControlCouponForDriverWithNoPenaltiesDataVM
    {
        public PoliceDepartment IssuingPoliceDepartment { get; set; }

        public PersonAddress PermanentAddress { get; set; }
       
        public PersonAddress CurrentAddress { get; set; }

        public bool? AgreementToReceiveERefusal { get; set; }
    }
}
