using EAU.Documents.Models;
using EAU.Documents.Models.Forms;

namespace EAU.KAT.Documents.Models.Forms
{
    public class ApplicationForIssuingDriverLicenseVM : ApplicationFormVMBase
    {
        #region Properties

        /// <summary>
        /// Обстоятелства
        /// </summary>
        public ApplicationForIssuingDriverLicenseDataVM Circumstances { get; set; }

        /// <summary>
        /// Данни за снимката и подписа на заявителя
        /// </summary>
        public IdentificationPhotoAndSignatureVM IdentificationPhotoAndSignature { get; set; }

        #endregion
    }
}
