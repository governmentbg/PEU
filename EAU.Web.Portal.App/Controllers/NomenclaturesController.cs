using EAU.Nomenclatures.Models;
using EAU.Services.Nomenclatures;
using EAU.Web.Filters;
using EAU.Web.Mvc;
using EAU.Web.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WAIS.Integration.MOI.Nomenclatures;
using WAIS.Integration.MOI.Nomenclatures.Models;

namespace EAU.Web.Portal.App.Controllers
{
    /// <summary>
    /// Контролер реализиращ уеб услуга за работа с номенклатури.
    /// Методите връщат ObjectResultWithETag, като резултата ТРЯБВА да бъде LINQ заявка, която да не е материализирана,
    /// защото в повечето случаи няма да трябва да се изпълнява. Ще се връща 304!
    /// </summary>    
    [Produces("application/json")]
    [ResponseCache(CacheProfileName = "Nomenclatures")]
    [NoopServiceLimiter]
    [ApiParameter(Name = "If-None-Match", Type = typeof(string), Source = "header")]
    [ProducesResponseType(StatusCodes.Status304NotModified)]
    public class NomenclaturesController : BaseApiController
    {
        /// <summary>
        /// Операция за изчитане на номенклатурата за звена за изпълнители на услугата.
        /// </summary>
        /// <param name="serviceID">Идентификатор на услугата.</param>
        /// <param name="token">Токен за отказване.</param>
        /// <returns>Номенклатура за етикети.</returns>
        [HttpGet]
        [Route("ServingUnitsInfo/{serviceID}")]
        public async Task<IActionResult> SearchServingUnitsInfo(int serviceID, CancellationToken token, [FromServices] IServingUnitsInfo servingUnitsInfo)
        {
            await servingUnitsInfo.EnsureLoadedAsync(serviceID, token);
            var cachedResources = servingUnitsInfo.Search(serviceID, out DateTime? lastModifiedDate);

            /*резултата ТРЯБВА да бъде LINQ заявка, която да не е материализирана, 
             * защото в повечето случаи няма да трябва да се изпълнява. Ще се връща 304!*/
            return new ObjectResultWithETag(() => cachedResources, lastModifiedDate.Value.FormatForETag());
        }


        /// <summary>
        /// Операция за изчитане на номенклатурата за звена за получаване на резултата от услугата.
        /// </summary>
        /// <param name="serviceID">Идентификатор на услугата.</param>
        /// <param name="serviceTermType">Вид на услугата.</param>
        /// <param name="token">Токен за отказване.</param>
        /// <returns>Номенклатура за етикети.</returns>
        [HttpGet]
        [Route("DeliveryUnitsInfo/{serviceID}/{serviceTermType?}")]
        public async Task<IActionResult> SearchDeliveryUnitsInfo(int serviceID, AdmServiceTermType? serviceTermType, CancellationToken token, [FromServices] IDeliveryUnitsInfo deliveryUnitsInfo)
        {
            await deliveryUnitsInfo.EnsureLoadedAsync(serviceID, serviceTermType, token);
            var cachedResources = deliveryUnitsInfo.Search(serviceID, serviceTermType, out DateTime? lastModifiedDate);

            /*резултата ТРЯБВА да бъде LINQ заявка, която да не е материализирана, 
             * защото в повечето случаи няма да трябва да се изпълнява. Ще се връща 304!*/
            return new ObjectResultWithETag(() => cachedResources, lastModifiedDate.Value.FormatForETag());
        }

        /// <summary>
        /// Операция за изчитане на номенклатурата за root звена за получаване на резултата от услугата.
        /// </summary>
        /// <param name="serviceID">Идентификатор на услугата.</param>
        /// <param name="serviceTermType">Вид на услугата.</param>
        /// <param name="token">Токен за отказване.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("DeliveryRootUnitsInfo/{serviceID}/{serviceTermType?}")]
        public async Task<IActionResult> SearchRootDeliveryUnitsInfo(int serviceID, AdmServiceTermType? serviceTermType, CancellationToken token, [FromServices] IDeliveryUnitsRootInfo deliveryUnitsInfo)
        {
            await deliveryUnitsInfo.EnsureLoadedAsync(serviceID, serviceTermType, token);
            var cachedResources = deliveryUnitsInfo.Search(serviceID, serviceTermType, out DateTime? lastModifiedDate);

            /*резултата ТРЯБВА да бъде LINQ заявка, която да не е материализирана, 
             * защото в повечето случаи няма да трябва да се изпълнява. Ще се връща 304!*/
            return new ObjectResultWithETag(() => cachedResources, lastModifiedDate.Value.FormatForETag());
        }

        [HttpGet]
        [Route("КАТ/TerminationOfRegistrationOfVehicle")]
        public async Task<IActionResult> GetTerminationOfRegistrationOfVehicle(CancellationToken cancellationToken, [FromServices] ITerminationOfRegistrationOfVehicleReasons terminationReasons)
        {
            await terminationReasons.EnsureLoadedAsync(cancellationToken);

            var cachedResources = terminationReasons.Search(out DateTime? lastModifiedDate);

            return new ObjectResultWithETag(() => cachedResources, lastModifiedDate.Value.FormatForETag());
        }

        [HttpGet]
        [Route("КАТ/ReasonForIssuingDuplicateOfSRMPS")]
        public async Task<IActionResult> GetReasonForIssuingDuplicateOfSRMPS(CancellationToken cancellationToken, [FromServices] IReasonsForIssuingDuplicateOfSRMPS reasons)
        {
            await reasons.EnsureLoadedAsync(cancellationToken);

            var cachedResources = reasons.Search(out DateTime? lastModifiedDate);

            return new ObjectResultWithETag(() => cachedResources, lastModifiedDate.Value.FormatForETag());
        }

        [HttpGet]
        [Route("KAT/Countries")]
        public async Task<IActionResult> GetCountries(CancellationToken cancellationToken, [FromServices] ICountries countries)
        {
            await countries.EnsureLoadedAsync(cancellationToken);

            var cachedResources = countries.Search(out DateTime? lastModifiedDate);

            return new ObjectResultWithETag(() => cachedResources, lastModifiedDate.Value.FormatForETag());
        }

        [HttpGet]
        [Route("KAT/RegistrationNumberProvinceCodes")]
        public async Task<IActionResult> SearchRegistrationNumberProvinceCodesAsync([FromQuery] RegistrationNumberProvinceCodeSearchCriteria criteria, CancellationToken cancellationToken, [FromServices] IRegistrationNumberProvinceCodes codes)
        {
            await codes.EnsureLoadedAsync(criteria.PoliceDepartmentID, criteria.VehicleTypeCode, cancellationToken);
            var cachedResources = codes.Search(criteria.PoliceDepartmentID, criteria.VehicleTypeCode, out DateTime? lastModifiedDate);

            /*резултата ТРЯБВА да бъде LINQ заявка, която да не е материализирана, 
             * защото в повечето случаи няма да трябва да се изпълнява. Ще се връща 304!*/
            return new ObjectResultWithETag(() => cachedResources, lastModifiedDate.Value.FormatForETag());
        }

        [HttpGet]
        [Route("KAT/VehicleBaseColors")]
        public async Task<IActionResult> GetVehicleBaseColors(CancellationToken cancellationToken, [FromServices] IVehicleBaseColors colors)
        {
            await colors.EnsureLoadedAsync(cancellationToken);

            var cachedResources = colors.Search(out DateTime? lastModifiedDate);

            return new ObjectResultWithETag(() => cachedResources, lastModifiedDate.Value.FormatForETag());
        }
    }
}
