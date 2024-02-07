using EAU.Documents.Models.Forms;

namespace EAU.KOS.Documents.Models.Forms
{
    public class NotificationForNonFirearmVM : ApplicationFormVMBase
    {
        public const string CIRCUMSTANCES = "Circumstances";

        public NotificationForNonFirearmDataVM Circumstances { get; set; }
    }
}