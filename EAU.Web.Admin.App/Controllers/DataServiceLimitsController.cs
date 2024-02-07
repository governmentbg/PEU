using EAU.ServiceLimits;
using EAU.ServiceLimits.Models;
using EAU.ServiceLimits.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Web.Admin.App.Controllers
{
    /// <summary>
    /// Контролер реализиращ уеб услуга за работа с лимити.
    /// </summary>
    [Produces("application/json")]
    public class DataServiceLimitsController : BaseApiController
    {
        /// <summary>
        /// Операция за изчитане на лимити.
        /// </summary>
        /// <param name="dataServiceLimitRepository">Интерфейс за работа с лимити.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns>Лимити</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DataServiceLimit>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromServices] IDataServiceLimitRepository dataServiceLimitRepository, CancellationToken cancellationToken)
        {
            var res = await dataServiceLimitRepository.SearchInfoAsync(new DataServiceLimitsSearchCriteria(), cancellationToken);
            return Ok(res.Data);
        }

        /// <summary>
        /// Операция за промяна на лимит.
        /// </summary>
        /// <param name="serviceCode">Код на услуга</param>
        /// <param name="limit">Лимит</param>
        /// <param name="dataServiceLimitService">Интерфейс за работа с лимити.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        [HttpPut]
        [Route("{serviceCode}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromRoute] string serviceCode, [FromBody] DataServiceLimit limit, [FromServices] IDataServiceLimitService dataServiceLimitService, CancellationToken cancellationToken)
        {
            var res = await dataServiceLimitService.UpdateDataServiceLimitAsync(serviceCode, limit.RequestsInterval, limit.RequestsNumber, cancellationToken);

            return OperationResult(res);
        }

        /// <summary>
        /// Операция за промяна на статуса на лимит.
        /// </summary>
        /// <param name="serviceCode">Код на услуга</param>
        /// <param name="status">Статус</param>
        /// <param name="dataServiceLimitService">Интерфейс за работа с лимити.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        [HttpPut]
        [Route("{serviceCode}/Status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> StatusChange([FromRoute] string serviceCode, [FromBody] DataServiceLimitStatus status, [FromServices] IDataServiceLimitService dataServiceLimitService, CancellationToken cancellationToken)
        {
            var res = await dataServiceLimitService.StatusChangeDataServiceLimitAsync(serviceCode, status, cancellationToken);

            return OperationResult(res);
        }
    }
}