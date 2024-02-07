using EAU.Documents.Models;
using EAU.Documents.Models.Forms;

namespace EAU.BDS.Documents.Models.Forms
{
    /// <summary>
    /// Заявление за издаване на удостоверение
    /// </summary>
    public class ApplicationForIssuingDocumentVM : ApplicationFormVMBase
    {
        public const string CIRCUMSTANCES = "Circumstances";

        public const string PersonalInformationData = "PersonalInformation";

        public const string ServiceTermTypeAndApplicant = "ServiceTermTypeAndApplicantReceipt";

        public ApplicationForIssuingDocumentDataVM Circumstances { get; set; }

        public PersonalInformationVM PersonalInformation { get; set; }
    }
}