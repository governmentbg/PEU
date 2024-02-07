using EAU.Signing;
using EAU.Web.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Web.Api.Controllers
{
    /// <summary>
    /// Контролер реализиращ логиката за тестово подписване
    /// </summary>
    public partial class SigningProcessesController : BaseApiController
    {
        /// <summary>
        /// Операция за тестово подписване.
        /// </summary>
        /// <param name="processID">Идентификатор на процес.</param>
        /// <param name="cancellationToken">Токен за отказване.</param>
        /// <param name="testSignService">Интерфейс на услуга за подписване</param>
        /// <param name="logger">Интерфейс за правене на лог</param>
        /// <returns>Флаг указващ дали операцията е завършила успешно.</returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("{processID}/testSign")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> TestSign(Guid processID, CancellationToken cancellationToken, [FromServices]ITestSignService testSignService, [FromServices]ILogger<SigningProcessesController> logger)
        {
            logger.LogInformation(string.Format("RemoteIpAddress: {0}; LocalIpAddress: {1};",
                this.Request.HttpContext.Connection.RemoteIpAddress.ToString()
                , this.Request.HttpContext.Connection.LocalIpAddress.ToString()));

            var result = await testSignService.TestSignAsync(processID, cancellationToken);

            return result.IsSuccessfullyCompleted ? Ok(true) : BadRequest(result);
        }
    }
}
