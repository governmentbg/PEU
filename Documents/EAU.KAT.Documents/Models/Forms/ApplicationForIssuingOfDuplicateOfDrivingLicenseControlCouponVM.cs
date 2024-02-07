using EAU.Documents.Models.Forms;

namespace EAU.KAT.Documents.Models.Forms
{
    public class ApplicationForIssuingOfDuplicateOfDrivingLicenseControlCouponVM : ApplicationFormVMBase
    {
        public const string CIRCUMSTANCES = "Circumstances";
        public ApplicationForIssuingOfDuplicateOfDrivingLicenseControlCouponDataVM Circumstances { get; set; }        
    }
}
