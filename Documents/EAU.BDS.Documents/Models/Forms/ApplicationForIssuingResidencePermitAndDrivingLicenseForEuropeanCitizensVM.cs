using EAU.Documents.Models;
using EAU.Documents.Models.Forms;

namespace EAU.BDS.Documents.Models.Forms
{
    /// <summary>
    /// Заявление за издаване на удостоверение за пребиваване и СУМПС на граждани на ЕС по образец - приложение № 4 към чл. 5, ал. 1 от ПИБЛД
    /// </summary>
    public class ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensVM : ApplicationFormVMBase
    {
        #region Properties

        /// <summary>
        /// Основни данни за заявлението
        /// </summary>
        public ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensDataVM Circumstances { get; set; }

        /// <summary>
        /// Данни за снимката и подписа на заявителя
        /// </summary>
        public IdentificationPhotoAndSignatureVM IdentificationPhotoAndSignature { get; set; }

        #endregion
    }
}