using EAU.KOS.Documents.Domain.Models;
using EAU.Security;
using EAU.Web.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using WAIS.Integration.MOI.KOS;

namespace EAU.Web.Portal.App.Controllers
{
    /// <summary>
    /// Контролер за работа с ЕАР КОС
    /// </summary>    
    [Authorize]
    [Route("Api/EARKOS")]
    [Produces("application/json")]
    public class EARKOSController : BaseApiController
    {
        private readonly IKOSServices _kOSServices;
        private readonly IEAUUserAccessor _eAUUserAccessor;

        public EARKOSController(IKOSServices kOSServices, IEAUUserAccessor eAUUserAccessor)
        {
            _kOSServices = kOSServices;
            _eAUUserAccessor = eAUUserAccessor;
        }

        /// <summary>
        /// Интеграция с ЕАР КОС за получаване на информация за разрешение по неговия номер.
        /// </summary>
        /// <param name="number">Номер на разрешение.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Route("{number}")]
        [HttpGet]
        [ProducesResponseType(typeof(LicenseInfo), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetLicenseAsync([FromRoute] string number, CancellationToken cancellationToken)
        {
            var merchantRes = await _kOSServices.MerchantAsync(_eAUUserAccessor.User.UIC, cancellationToken);

            if(!merchantRes.IsSuccessfullyCompleted)
            {
                var errCode = merchantRes.Errors[0].Code;

                switch (errCode)
                {
                    case "400":
                        //Грешка при валидация на заявката. Невалиден ЕИК.
                        return BadRequest("DOC_KOS_InvalidEIK_E ", "DOC_KOS_InvalidEIK_E ");
                    case "404":
                        //Няма информация за валиден търговец с това ЕИК.
                        return BadRequest("DOC_KOS_NOINFO_4LicensedWeaponTrader_E", "DOC_KOS_NOINFO_4LicensedWeaponTrader_E");
                    default:
                        return BadRequest("GL_SYSTEM_UNAVAILABLE_E", "GL_SYSTEM_UNAVAILABLE_E");
                }
            }

            var licenseRes = await _kOSServices.LicenseAsync(number, Media.Electronic, true, merchantRes.Response.MerchantValidityToken, cancellationToken);

            if(licenseRes.IsSuccessfullyCompleted)
            {
                if(licenseRes.Response.ValidityPeriodEnd.HasValue && licenseRes.Response.ValidityPeriodEnd.Value.DateTime < DateTime.Now)
                {
                    //Срокът на валидност на разрешението с въведения номер е изтекъл.
                    return BadRequest("DOC_KOS_ValidWeaponAcquisitionPerimit_Expired_E", "DOC_KOS_ValidWeaponAcquisitionPerimit_Expired_E");
                }

                var licenseInfo = new LicenseInfo()
                {
                    PermitNumber = number,
                    PermitType = licenseRes.Response.Type,
                    PermitTypeName = licenseRes.Response.TypeName,
                    Reason = licenseRes.Response.Reason,
                    ReasonName = licenseRes.Response.ReasonName,
                    ValidityPeriodStart = licenseRes.Response.ValidityPeriodStart.DateTime,
                    ValidityPeriodEnd = licenseRes.Response.ValidityPeriodEnd.HasValue ? licenseRes.Response.ValidityPeriodEnd.Value.DateTime : null,
                    HolderName = licenseRes.Response.HolderName,
                    HolderIdentifier = licenseRes.Response.HolderId,
                    IssuingPoliceDepartment = new Documents.Domain.Models.PoliceDepartment()
                    {
                        PoliceDepartmentCode = licenseRes.Response.OrganizationUnit,
                        PoliceDepartmentName = licenseRes.Response.OrganizationUnitName,
                    },
                    Content = licenseRes.Response.Content,
                };

                return Ok(licenseInfo);
            }
            else
            {
                var errCode = licenseRes.Errors[0].Code;

                switch (errCode)
                {
                    case "400":
                        //Грешка при валидация на заявката. Невалиден номер.
                        return BadRequest("DOC_KOS_NotValidPermitNumber_E", "DOC_KOS_NotValidPermitNumber_E");
                    case "404":
                        //Няма информация за валидно разрешение с този номер
                        return BadRequest("DOC_KOS_NOINFO_4ValidWeaponAcquisitionPermit_E", "DOC_KOS_NOINFO_4ValidWeaponAcquisitionPermit_E");
                    case "409":
                        //Има валидно разрешение, но то вече е използвано!
                        return BadRequest("DOC_KOS_ValidWeaponAcquisitionPermit_Used_E", "DOC_KOS_ValidWeaponAcquisitionPermit_Used_E");
                    default:
                        return BadRequest("GL_SYSTEM_UNAVAILABLE_E", "GL_SYSTEM_UNAVAILABLE_E");
                }
            }
        }
    }
}