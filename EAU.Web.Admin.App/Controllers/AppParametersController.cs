using EAU.Common;
using EAU.Common.Models;
using EAU.Common.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Web.Admin.App.Controllers
{
    /// <summary>
    /// Контролер реализиращ уеб услуга за работа с параметри.
    /// </summary>
    [Produces("application/json")]
    [AllowAnonymous]
    public class AppParametersController : BaseApiController
    {
        /// <summary>
        /// Операция за изчитане на параметри.
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <param name="_appParameterRepository">Интерфейс за работа с параметри.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns>Списък с параметри.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AppParameter>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] AppParameterSearchCriteria searchCriteria, [FromServices] IAppParameterRepository _appParameterRepository, CancellationToken cancellationToken)
        {
            var res = await _appParameterRepository.SearchInfoAsync(searchCriteria, cancellationToken);
            return Ok(res.Data.ToList());
        }

        /// <summary>
        /// Операция по редакция на параметри.
        /// </summary>
        /// <param name="code">Код</param>
        /// <param name="item">параметър</param>
        /// <param name="appParameterService">Интерфейс за работа с параметри.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        [HttpPut]
        [Route("{code}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromRoute] string code, [FromBody] AppParameter item, [FromServices] IAppParameterService appParameterService, CancellationToken cancellationToken)
        {
            var res = await appParameterService.UpdateAsync(code, item.ValueDateTime, item.ValueInterval, item.ValueString, item.ValueInt, item.ValueHour, cancellationToken);

            return OperationResult(res);
        }
    }
}