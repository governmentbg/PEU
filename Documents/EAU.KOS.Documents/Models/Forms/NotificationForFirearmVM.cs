using EAU.Documents.Models.Forms;

namespace EAU.KOS.Documents.Models.Forms
{
    public class NotificationForFirearmVM : ApplicationFormVMBase
    {
        public const string CIRCUMSTANCES = "Circumstances";

        public NotificationForFirearmDataVM Circumstances { get; set; }
    }
}