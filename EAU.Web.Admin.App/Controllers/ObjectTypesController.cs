using EAU.Audit.Cache;
using EAU.Audit.Models;
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
    /// Контролер реализиращ уеб услуга за работа с видове обекти.
    /// </summary>
    [Produces("application/json")]
    [Authorize]
    [ApiParameter(Name = "If-None-Match", Type = typeof(string), Source = "header")]
    [ProducesResponseType(StatusCodes.Status304NotModified)]
    public class ObjectTypesController : BaseApiController
    {
        /// <summary>
        /// Операция за изчитане на видове обекти.
        /// </summary>
        /// <param name="ObjectTypesCache">Интерфейс за работа с кеш на видове обекти.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns>Видове обекти.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ObjectType>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromServices] IObjectTypes ObjectTypesCache, CancellationToken cancellationToken)
        {
            await ObjectTypesCache.EnsureLoadedAsync(cancellationToken);
            var cachedResources = ObjectTypesCache.GetObjectTypes(out DateTime? lastModifiedDate);

            return new ObjectResultWithETag(() =>
            {
                return cachedResources;

            }, lastModifiedDate.Value.FormatForETag());
        }
    }
}