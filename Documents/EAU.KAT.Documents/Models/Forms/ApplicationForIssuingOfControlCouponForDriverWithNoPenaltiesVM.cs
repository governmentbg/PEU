using EAU.Documents.Models.Forms;

namespace EAU.KAT.Documents.Models.Forms
{
    public class ApplicationForIssuingOfControlCouponForDriverWithNoPenaltiesVM : ApplicationFormVMBase
    {
        public const string CIRCUMSTANCES = "Circumstances";
        public ApplicationForIssuingOfControlCouponForDriverWithNoPenaltiesDataVM Circumstances { get; set; }        
    }
}
