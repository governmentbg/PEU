using EAU.Services.DocumentProcesses;
using EAU.Web.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Web.EDocViewer.App.Controllers
{
    /// <summary>
    /// Контролер реализиращ уеб услуга за работа с DocumentProcess.
    /// </summary>    
    [Route("api/Services/DocumentProcesses")]
    public class DocumentProcessesController : BaseApiController
    {        
        /// <summary>
        /// Операция за стартира порцеса по изпращане на заявлението..
        /// </summary>
        /// <param name="processID">Идентификатор на процес по заявяване.</param>      
        /// <param name="service">Клиент за работа с докуемнтен процес.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns></returns>
        [Route("{processID}/StartSending")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> StartSendingAsync([FromRoute]long processID, [FromServices] IDocumentProcessService service, CancellationToken cancellationToken)
        {
            var result = await service.StartSendingAsync(processID, cancellationToken);

            return OperationResult(result);
        }
    }
}
