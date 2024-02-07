using EAU.Documents.Models.Forms;

namespace EAU.KOS.Documents.Models.Forms
{
    public class ApplicationByFormAnnex10VM : ApplicationFormVMBase
    {
        public const string CIRCUMSTANCES = "Circumstances";

        public ApplicationByFormAnnex10DataVM Circumstances { get; set; }
    }
}
