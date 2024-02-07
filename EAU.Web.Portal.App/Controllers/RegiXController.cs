using EAU.Web.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using WAIS.Integration.RegiX;
using WAIS.Integration.RegiX.Models;

namespace EAU.Web.Portal.App.Controllers
{
    /// <summary>
    /// Контролер за работа с RegiX
    /// </summary>    
    [Authorize]
    [Route("Api/RegiX")]
    [Produces("application/json")]
    public class RegiXController : BaseApiController
    {
        private readonly IEntityDataServices _entityDataServices;

        public RegiXController(IEntityDataServices entityDataServices)
        {
            _entityDataServices = entityDataServices;
        }

        [Route("{identifier}/EntityData")]
        [HttpGet]
        [ProducesResponseType(typeof(RegiXEntityData), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEntityData([FromRoute] string identifier, CancellationToken cancellationToken)
        {
            var result = await _entityDataServices.GetEntityDataAsync(identifier, cancellationToken);

            return Ok(result.Response);
        }
    }
}