using EAU.Services.ServiceInstances;
using EAU.Web.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using WAIS.Integration.EPortal.Models;

namespace EAU.Web.Api.Private.Controllers
{
    /// <summary>
    /// Контролер реализиращ уеб услуга за работа с инстанции на услуги.
    /// </summary>    
    [Route("Services/Instances")]
    [Produces("application/json")]
    [Authorize(Policy = "BackOfficeIntegrationApiPolicy")]
    public class ServiceInstancesController : BaseApiController
    {        
        /// <summary>
        /// Операция по редакция на инстанция на услуги.
        /// </summary>     
        /// <param name="serviceInstance">Инстанция на услуги в Бекенда.</param>
        /// <param name="serviceInstanceService"></param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(
            [FromBody] ServiceInstanceInfo serviceInstance
            , [FromServices]IServiceInstanceService serviceInstanceService
            , CancellationToken cancellationToken)
        {
            var res = await serviceInstanceService.UpdateAsync(serviceInstance, cancellationToken);

            return OperationResult(res);
        }
    }
}
