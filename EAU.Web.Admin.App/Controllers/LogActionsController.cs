using EAU.Audit;
using EAU.Audit.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Web.Admin.App.Controllers
{
    /// <summary>
    /// Контролер реализиращ уеб услуга за работа с одит.
    /// </summary>
    [Produces("application/json")]
    public class LogActionsController : BaseApiController
    {
        /// <summary>
        /// Операция за изчитане на записи в одит.
        /// </summary>
        /// <param name="searchCtriteria">Критерии за търсене</param>
        /// <param name="auditService">Интерфейс за работа с одит.</param>
        /// <param name="cancellationToken">Токен за отказване</param>
        /// <returns>Данни за записи в одит.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Models.LogAction>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] Models.LogActionSearchCriteria searchCtriteria, [FromServices] IAuditService auditService, CancellationToken cancellationToken)
        {
            var state = searchCtriteria.ExtractState();

            IPAddress ipAddress;
            if (!string.IsNullOrEmpty(searchCtriteria.IpAddress)
                && !IPAddress.TryParse(searchCtriteria.IpAddress, out ipAddress))
            {
                List<Models.LogAction> result = new List<Models.LogAction>();
                state.Count = 0;
                return PagedResult(result, state);
            }
            else
            {
                var domainCriteria = Mapper.Map<Audit.Repositories.LogActionSearchCriteria>(searchCtriteria);
                var logActions = await auditService.SearchLogActionsAsync(state, domainCriteria, cancellationToken);

                var mappedRes = Mapper.Map<List<Models.LogAction>>(logActions);

                return PagedResult(mappedRes, state);
            }
        }
    }
}