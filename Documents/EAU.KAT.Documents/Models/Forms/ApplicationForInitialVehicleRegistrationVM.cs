using EAU.Documents.Models.Forms;

namespace EAU.KAT.Documents.Models.Forms
{
    /// <summary>
    /// Заявление за извършване на първоначална регистрация на ППС
    /// </summary>
    public class ApplicationForInitialVehicleRegistrationVM : ApplicationFormVMBase
    {
        public const string CIRCUMSTANCES = "Circumstances";

        public ApplicationForInitialVehicleRegistrationDataVM Circumstances { get; set; }
    }
}
