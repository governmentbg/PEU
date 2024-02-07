using EAU.Documents.Models.Forms;

namespace EAU.KAT.Documents.Models.Forms
{
    public class ApplicationForIssuingVehicleRegistrationNumberWithCombinationSixLettersOrDigitsVM : ApplicationFormVMBase
    {
        public const string CIRCUMSTANCES = "Circumstances";

        public ApplicationForIssuingVehicleRegistrationNumberWithCombinationSixLettersOrDigitsDataVM Circumstances { get; set; }        
    }
}
