
using EAU.Documents.Models.Forms;

namespace EAU.KAT.Documents.Models.Forms
{
    public class DataForPrintSRMPSVM : ApplicationFormVMBase
    {
        public const string CIRCUMSTANCES = "Circumstances";

        public DataForPrintSRMPSDataVM Circumstances { get; set; }
    }
}
