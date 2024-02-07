using EAU.Documents.Models.Forms;

namespace EAU.COD.Documents.Models.Forms
{
    /// <summary>
    /// Уведомление за поемане или снемане от охрана на обект при извършване на частна охранителна дейност
    /// </summary>
    public class NotificationForTakingOrRemovingFromSecurityVM : ApplicationFormVMBase
    {
        public const string CIRCUMSTANCES = "Circumstances";

        public NotificationForTakingOrRemovingFromSecurityDataVM Circumstances { get; set; }

        public SecurityObjectsDataVM SecurityObjectsData { get; set; }
    }
}