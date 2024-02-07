using EAU.Payments.RegistrationsData;
using EAU.Payments.RegistrationsData.Cache;
using EAU.Payments.RegistrationsData.Models;
using EAU.Payments.RegistrationsData.Repositories;
using EAU.Web.Mvc;
using EAU.Web.Mvc.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Web.Admin.App.Controllers
{
    /// <summary>
    /// Контролер реализиращ уеб услуга за работа с регистрационни данни на ПЕАУ в системата на платежен оператор ePay / ПЕП на ДАЕУ.
    /// </summary>
    [Route("api/RegistrationsDataCache")]
    [Produces("application/json")]
    [Authorize]
    [ApiParameter(Name = "If-None-Match", Type = typeof(string), Source = "header")]
    [ProducesResponseType(StatusCodes.Status304NotModified)]
    public class RegistrationsDataCacheController : BaseApiController
    {
        /// <summary>
        /// Операция за изчитане на регистрационни данни на ПЕАУ в системата на платежен оператор ePay / ПЕП на ДАЕУ.
        /// </summary>
        /// <param name="registrationsDataCache">Интерфейс за работа с кеш на регистрационни данни на ПЕАУ в системата на платежен оператор ePay / ПЕП на ДАЕУ.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns>Регистрационни данни на ПЕАУ в системата на платежен оператор ePay / ПЕП на ДАЕУ.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RegistrationData>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromServices] IRegistrationsData registrationsDataCache, CancellationToken cancellationToken)
        {
            await registrationsDataCache.EnsureLoadedAsync(cancellationToken);
            var cachedResources = registrationsDataCache.GetRegistrationData(out DateTime? lastModifiedDate);

            return new ObjectResultWithETag(() =>
            {
                return cachedResources;

            }, lastModifiedDate.Value.FormatForETag());
        }
    }
}