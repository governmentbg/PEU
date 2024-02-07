using EAU.Documents.Models.Forms;

namespace EAU.KAT.Documents.Models.Forms
{
    public class ApplicationForIssuingDocumentofVehicleOwnershipVM : ApplicationFormVMBase
    {
        public const string CIRCUMSTANCES = "Circumstances";
        public ApplicationForIssuingDocumentofVehicleOwnershipDataVM Circumstances { get; set; }        
    }
}
