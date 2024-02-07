using EAU.Documents.Models;
using EAU.Documents.Models.Forms;

namespace EAU.BDS.Documents.Models.Forms
{
    /// <summary>
    /// Заявление за издаване на лични документи на чужденци в Република България по образец - приложение № 3 към чл. 5, ал. 1 от ПИБЛД
    /// </summary>
    public class ApplicationForIssuanceOfIdentityDocumentsAndRPOFInTheRepublicOfBulgariaVM : ApplicationFormVMBase
    {
        #region Properties

        /// <summary>
        /// Данни за снимката и подписа на заявителя
        /// </summary>
        public IdentificationPhotoAndSignatureVM IdentificationPhotoAndSignature { get; set; }

        public ApplicationForIssuanceOfIdentityDocumentsAndRPOFInTheRepublicOfBulgariaDataVM Circumstances { get; set; }

        #endregion
    }
}