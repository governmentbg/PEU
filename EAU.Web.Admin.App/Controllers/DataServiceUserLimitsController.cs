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
    /// Контролер реализиращ уеб услуга за работа с лимити за потребители.
    /// </summary>
    [Produces("application/json")]
    public class DataServiceUserLimitsController : BaseApiController
    {
        /// <summary>
        /// Операция за изчитане на лимити за потребители.
        /// </summary>
        /// <param name="dataServiceUserLimitRepository">Интерфейс за работа с лимити.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns>Лимити за потребители</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DataServiceUserLimit>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromServices] IDataServiceUserLimitRepository dataServiceUserLimitRepository, CancellationToken cancellationToken)
        {
            var res = await dataServiceUserLimitRepository.SearchInfoAsync(new DataServiceUserLimitsSearchCriteria(), cancellationToken);
            return Ok(res.Data);
        }

        /// <summary>
        /// Операция по създаване на лимит за потребители.
        /// </summary>
        /// <param name="item">Лимит на потребител.</param>
        /// <param name="dataServiceLimitService">Интерфейс за работа с лимит.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(DataServiceUserLimit item, [FromServices] IDataServiceLimitService dataServiceLimitService, CancellationToken cancellationToken)
        {
            var result = await dataServiceLimitService.CreateDataServiceUserLimitAsync(item, cancellationToken);

            return OperationResult(result);
        }

        /// <summary>
        /// Операция по редакция на лимит за потребители.
        /// </summary>
        /// <param name="userLimitID">Идентификатор на лимит за потребител.</param>
        /// <param name="item">Лимит</param>
        /// <param name="dataServiceLimitService">Интерфейс за работа с лимит.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        [HttpPut]
        [Route("{userLimitID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(int userLimitID, DataServiceUserLimit item, [FromServices]IDataServiceLimitService dataServiceLimitService, CancellationToken cancellationToken)
        {
            var result = await dataServiceLimitService.UpdateDataServiceUserLimitAsync(userLimitID, item.RequestsInterval, item.RequestsNumber, cancellationToken);

            return OperationResult(result);
        }

        /// <summary>
        /// Операция по редакция на лимит за потребители.
        /// </summary>
        /// <param name="userLimitID">Идентификато на лимит</param>
        /// <param name="status">Статус</param>
        /// <param name="dataServiceLimitService">Интерфейс за работа с лимит.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        [HttpPut]
        [Route("{userLimitID}/Status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> StatusChange([FromRoute]int userLimitID, [FromQuery]DataServiceLimitStatus status, [FromServices] IDataServiceLimitService dataServiceLimitService, CancellationToken cancellationToken)
        {
            var result = await dataServiceLimitService.StatusChangeDataServiceUserLimitAsync(userLimitID, status, cancellationToken);

            return OperationResult(result);
        }
    }
}