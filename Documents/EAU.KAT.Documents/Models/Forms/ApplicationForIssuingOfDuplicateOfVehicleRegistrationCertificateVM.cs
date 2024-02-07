using EAU.Documents.Models.Forms;

namespace EAU.KAT.Documents.Models.Forms
{
    public class ApplicationForIssuingOfDuplicateOfVehicleRegistrationCertificateVM : ApplicationFormVMBase
    {
        public const string CIRCUMSTANCES = "Circumstances";

        public VehicleDataRequestVM Circumstances { get; set; }        
    }
}
