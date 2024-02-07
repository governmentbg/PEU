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
    /// Контролер реализиращ уеб услуга за работа с видове действия.
    /// </summary>
    [Produces("application/json")]
    [Authorize]
    [ApiParameter(Name = "If-None-Match", Type = typeof(string), Source = "header")]
    [ProducesResponseType(StatusCodes.Status304NotModified)]
    public class ActionTypesController : BaseApiController
    {
        /// <summary>
        /// Операция за изчитане на видове действия.
        /// </summary>
        /// <param name="actionTypesCache">Интерфейс за работа с кеш на видове действия.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns>Видове действия.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ActionType>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromServices] IActionTypes actionTypesCache, CancellationToken cancellationToken)
        {
            await actionTypesCache.EnsureLoadedAsync(cancellationToken);
            var cachedResources = actionTypesCache.GetActionTypes(out DateTime? lastModifiedDate);

            return new ObjectResultWithETag(() =>
            {
                return cachedResources;

            }, lastModifiedDate.Value.FormatForETag());
        }
    }
}