using EAU.Documents.Models.Forms;

namespace EAU.KAT.Documents.Models.Forms
{
    /// <summary>
    /// Промяна в регистрацията на пътно превозно средство (ППС)
    /// </summary>
    public class ApplicationForChangeRegistrationOfVehiclesVM : ApplicationFormVMBase
    {
        public const string CIRCUMSTANCES = "Circumstances";

        public ApplicationForChangeRegistrationOfVehiclesDataVM Circumstances { get; set; }
    }
}
