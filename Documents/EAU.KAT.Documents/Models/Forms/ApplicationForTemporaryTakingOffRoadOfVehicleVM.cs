using EAU.Documents.Models.Forms;

namespace EAU.KAT.Documents.Models.Forms
{
    public class ApplicationForTemporaryTakingOffRoadOfVehicleVM : ApplicationFormVMBase
    {
        public const string CIRCUMSTANCES = "Circumstances";

        public VehicleDataRequestVM Circumstances { get; set; }        
    }
}
