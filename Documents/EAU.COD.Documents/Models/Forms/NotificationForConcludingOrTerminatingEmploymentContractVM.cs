using EAU.Documents.Models.Forms;

namespace EAU.COD.Documents.Models.Forms
{
    /// <summary>
    /// Уведомление за сключване или прекратяване на трудов договор между лице, получило лиценз за извършване на частна охранителна дейност, и служител от неговия специализиран персонал
    /// </summary>
    public class NotificationForConcludingOrTerminatingEmploymentContractVM : ApplicationFormVMBase
    {
        public const string CIRCUMSTANCES = "Circumstances";

        public NotificationForConcludingOrTerminatingEmploymentContractDataVM Circumstances { get; set; }
    }
}