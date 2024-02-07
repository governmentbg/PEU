using EAU.Documents.Models;
using EAU.Documents.Models.Forms;

namespace EAU.BDS.Documents.Models.Forms
{
    public class ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensVM : ApplicationFormVMBase
    {
        #region Properties

        public IdentificationPhotoAndSignatureVM IdentificationPhotoAndSignature { get; set; }

        public ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensDataVM Circumstances { get; set; }

        #endregion
    }
}