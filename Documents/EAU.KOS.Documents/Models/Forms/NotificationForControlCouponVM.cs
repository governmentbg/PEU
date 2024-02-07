using EAU.Documents.Models.Forms;

namespace EAU.KOS.Documents.Models.Forms
{
    public class NotificationForControlCouponVM : ApplicationFormVMBase
    {
        public const string CIRCUMSTANCES = "Circumstances";

        public NotificationForControlCouponDataVM Circumstances { get; set; }
    }
}
