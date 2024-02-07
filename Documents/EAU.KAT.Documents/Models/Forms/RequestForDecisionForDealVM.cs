using EAU.Documents.Models.Forms;

namespace EAU.KAT.Documents.Models.Forms
{
    /// <summary>
    /// Искане на разрешение за сделка
    /// </summary>
    public class RequestForDecisionForDealVM : ApplicationFormVMBase
    {
        public const string CIRCUMSTANCES = "Circumstances";

        public RequestForDecisionForDealDataVM Circumstances { get; set; }
    }
}
