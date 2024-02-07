using CNSys.Xml;
using EAU.Services.DocumentProcesses;
using EAU.Web.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using WAIS.Integration.EPortal.Models;

namespace EAU.Web.Api.Private.Controllers
{
    /// <summary>
    /// Контролер реализиращ уеб услуга за работа с DocumentProcess.
    /// </summary>    
    [Route("Services/DocumentProcesses")]
    [Authorize]
    public class DocumentProcessesController : BaseApiController
    {        
        /// <summary>
        /// Операция за приключва процеса по подписване.
        /// </summary>
        /// <param name="signingGiud">Гуид за подписване.</param>
        /// <param name="userSessionID">Идентификатор на потребителска сесия.</param>
        /// <param name="loginSessionID">Идентификатор на логин сесия.</param>
        /// <param name="ipAddress">IP адрес</param>
        /// <param name="userCIN">КИН на потребител.</param>
        /// <param name="file">Файл.</param>
        /// <param name="service">Клиент за работа с докуемнтен процес.</param>
        /// <param name="cancellationToken">Токен за отказване.</param>
        /// <returns></returns>
        [Route("SigningCompleted")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> SigningCompletedAsync(
            [FromQuery]Guid signingGiud
            , [FromQuery]Guid? userSessionID
            , [FromQuery]Guid? loginSessionID
            , [FromQuery]string ipAddress
            , [FromQuery]int? userCIN
            , IFormFile file
            , [FromServices] IDocumentProcessSigningCallbackService service
            , [FromServices] ILogger<DocumentProcessesController> logger
            , CancellationToken cancellationToken)
        {

            using (var fileContent = file.OpenReadStream())
            using (var ms = new MemoryStream())
            {
                fileContent.CopyTo(ms);

                ms.Position = 0;

                XmlDocument document = XmlHelpers.CreateXmlDocument(ms);

                logger.LogInformation(document.OuterXml);

                ms.Position = 0;

                await service.SigningCompletedAsync(
                    signingGiud
                    , ms
                    , userSessionID
                    , loginSessionID
                    , ipAddress
                    , userCIN
                    , cancellationToken);
            }

            return Ok();
        }

        /// <summary>
        /// Операция за отказване процес по подписване.
        /// </summary>
        /// <param name="signingGiud">Идентификатор на процес по подписване.</param>
        /// <param name="service">Клиент за работа с докуемнтен процес.</param>
        /// <param name="cancellationToken">Токен за отказване.</param>       
        [Route("SigningRejected")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> SigningRejectedAsync(
            [FromQuery]Guid signingGiud
            , [FromServices] IDocumentProcessSigningCallbackService service
            , CancellationToken cancellationToken)
        {
            await service.SigningRejectedAsync(signingGiud, cancellationToken);

            return Ok();
        }

        /// <summary>
        /// Операцията завършва успешно приключил процес по подписване на шаблонна декларация към заявление.
        /// </summary>
        /// <param name="signingGiud">Идентификатор на процес по подписване.</param>
        /// <param name="userSessionID"></param>
        /// <param name="loginSessionID"></param>
        /// <param name="ipAddress"></param>
        /// <param name="userCIN"></param>
        /// <param name="file">Съдържание на подписания документ.</param>
        /// <param name="service">Клиент за работа с прикачени документи.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns></returns>
        [Route("SigningAttachedDocumentTemplateCompleted")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> SigningCompletedAttachedDocumentTemplateAsync(
            [FromQuery]Guid signingGiud
            , [FromQuery]Guid? userSessionID
            , [FromQuery]Guid? loginSessionID
            , [FromQuery]string ipAddress
            , [FromQuery]int? userCIN
            , IFormFile file
            , [FromServices] IDocumentProcessSigningCallbackService service
            , CancellationToken cancellationToken)
        {
            using (var content = file.OpenReadStream())
            {
                await service.SigningAttachedDocumentCompletedAsync(
                    signingGiud
                    , content
                    , userSessionID
                    , loginSessionID
                    , ipAddress
                    , userCIN
                    , cancellationToken);
            }

            return Ok();
        }

        /// <summary>
        /// Операцията завършва отказан процес по подписване на шаблонна декларация към заявление.
        /// </summary>
        /// <param name="signingGiud">Идентификатор на процес по подписване.</param>
        /// <param name="service">Клиент за работа с прикачени документи.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns></returns>
        [Route("SigningAttachedDocumentTemplatRejected")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> SigningRejectedAttachedDocumentTemplateAsync(
            [FromQuery]Guid signingGiud
            , [FromServices] IDocumentProcessSigningCallbackService service
            , CancellationToken cancellationToken)
        {
            await service.SigningAttachedDocumentRejectedAsync(signingGiud, cancellationToken);

            return Ok();
        }

        /// <summary>
        /// Операция за приключва процеса по подаване.
        /// </summary>
        /// <param name="regResponse">Отговор на заявката за регистрация</param>
        /// <param name="service">Клиент за работа с докуемнтен процес.</param>
        /// <param name="cancellationToken">Токен за отказване.</param>
        /// <returns></returns>
        [Route("RegistrationCompleted")]
        [HttpPost]
        [Authorize(Policy = "BackOfficeIntegrationApiPolicy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> RegistrationCompletedAsync(
            [FromBody]DocumentRegistrationResponse regResponse
            , [FromServices] IDocumentProcessCallBackService service
            , CancellationToken cancellationToken)
        {
            var result = await service.RegistrationCompletedAsync(regResponse, cancellationToken);

            return OperationResult(result);
        }        

    }
}
