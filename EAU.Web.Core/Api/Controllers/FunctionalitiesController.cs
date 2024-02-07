using EAU.Common.Cache;
using EAU.Common.Models;
using EAU.Web.Filters;
using EAU.Web.Mvc;
using EAU.Web.Mvc.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Web.Api.Controllers
{
    /// <summary>
    /// Контролер реализиращ уеб услуга за работа с функционалности.
    /// </summary>
    [Produces("application/json")]
    [Authorize]
    [ApiParameter(Name = "If-None-Match", Type = typeof(string), Source = "header")]
    [ProducesResponseType(StatusCodes.Status304NotModified)]
    [NoopServiceLimiter]
    public class FunctionalitiesController : BaseApiController
    {
        /// <summary>
        /// Операция за изчитане на функционалности.
        /// </summary>
        /// <param name="functionalitiesCache">Интерфейс за работа с кеш на функционалности.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns>Функционалности</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Functionality>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromServices] IFunctionalities functionalitiesCache, CancellationToken cancellationToken)
        {
            await functionalitiesCache.EnsureLoadedAsync(cancellationToken);
            var cachedResources = functionalitiesCache.GetFunctionalities(out DateTime? lastModifiedDate);

            return new ObjectResultWithETag(() =>
            {
                return cachedResources;

            }, lastModifiedDate.Value.FormatForETag());
        }
    }
}