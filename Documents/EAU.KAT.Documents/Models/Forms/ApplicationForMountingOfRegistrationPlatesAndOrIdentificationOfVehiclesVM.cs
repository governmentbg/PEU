using EAU.Documents.Models.Forms;

namespace EAU.KAT.Documents.Models.Forms
{
    public class ApplicationForMountingOfRegistrationPlatesAndOrIdentificationOfVehiclesVM : ApplicationFormVMBase
    {
        public const string CIRCUMSTANCES = "Circumstances";

        public ApplicationForMountingOfRegistrationPlatesAndOrIdentificationOfVehiclesDataVM Circumstances { get; set; }        
    }
}
