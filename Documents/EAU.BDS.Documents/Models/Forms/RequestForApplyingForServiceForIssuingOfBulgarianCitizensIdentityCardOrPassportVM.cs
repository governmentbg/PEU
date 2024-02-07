using EAU.Documents.Models.Forms;

namespace EAU.BDS.Documents.Models.Forms
{
    /// <summary>
    /// Искане за издаване на лична карта и/или паспорт на български гражданин
    /// </summary>
    public class RequestForApplyingForServiceForIssuingOfBulgarianCitizensIdentityCardOrPassportVM : ApplicationFormVMBase
    {
        public RequestForApplyingForServiceForIssuingOfBCICardOrPassportDataVM Circumstances { get; set; }
    }
}