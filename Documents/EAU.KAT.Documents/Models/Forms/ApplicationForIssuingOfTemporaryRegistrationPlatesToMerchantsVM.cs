using EAU.Documents.Models.Forms;
using EAU.KAT.Documents.Domain.Models;

namespace EAU.KAT.Documents.Models.Forms
{
    public class ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchantsVM : ApplicationFormVMBase
    {
        public const string CIRCUMSTANCES = "Circumstances";

        public ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchantsDataVM Circumstances { get; set; }  
        
        public MerchantData MerchantData { get; set; }        
    }
}
