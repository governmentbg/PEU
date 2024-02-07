
using EAU.Documents.Models.Forms;
using EAU.KAT.Documents.Domain.Models;

namespace EAU.KAT.Documents.Models.Forms
{
    public class DeclarationForLostSRPPSVM : ApplicationFormVMBase
    {
        public const string CIRCUMSTANCES = "Circumstances";

        public DeclarationForLostSRPPSData Circumstances { get; set; }
    }
}
