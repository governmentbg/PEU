using EAU.Documents.Models.Forms;

namespace EAU.KOS.Documents.Models.Forms
{
    public class ApplicationByFormAnnex9VM : ApplicationFormVMBase
    {
        public const string CIRCUMSTANCES = "Circumstances";

        public ApplicationByFormAnnex9DataVM Circumstances { get; set; }
    }
}