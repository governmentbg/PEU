using CNSys;
using EAU.Payments.PaymentRequests.PepDaeu;
using EAU.Payments.PaymentRequests.PepDaeu.Models;
using EAU.Web.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Web.Portal.App.Controllers
{
    /// <summary>
    /// Контролер за обратна връзка от PEP na DAEU
    /// </summary>    
    [Route("Api/Obligations")]
    [Produces("application/json")]
    public class PaymentsPepDaeuController : BaseApiController
    {
        private readonly IPepDaeuPaymentCallbackService _pepDaeuPaymentCallbackService;
        private readonly ILogger<PaymentsPepDaeuController> _logger;

        public PaymentsPepDaeuController(IPepDaeuPaymentCallbackService pepDaeuPaymentCallbackService,
            ILogger<PaymentsPepDaeuController> logger)
        {
            _pepDaeuPaymentCallbackService = pepDaeuPaymentCallbackService;
            _logger = logger;
        }

        /// <summary>
        /// Колбек за обратна връзка от Пеп на ДАЕУ.
        /// </summary>
        /// <param name="registrationDataCin">Клиентски идентификатор на регистационни данни.</param>
        /// <param name="message">Съобщение.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns>Статус.</returns>
        [Route("PaymentRequests/PepDaeu/Callback/{registrationDataCin}")]
        [HttpPost]
        [ProducesResponseType(typeof(NotificationMessageResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Callback([FromRoute] string registrationDataCin, [FromForm] NotificationMessageTransportRequest message, CancellationToken cancellationToken)
        {
            OperationResult<NotificationMessageResponse> result = await _pepDaeuPaymentCallbackService.ProcessNotificationMessageAsync(registrationDataCin, message, cancellationToken);

            _logger.LogInformation($"response on pep callback call: {result.Result}");
            return OperationResult(result);
        }

        /// <summary>
        /// Колбек за обработване на съобщение след плащане през Пеп на ДАЕУ.
        /// </summary>
        /// <param name="returnURL">URL което да се отвори.</param>
        /// <param name="message">Съобщение.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns>Статус.</returns>
        [Route("PaymentRequests/PepDaeu/VPosResult/{**returnURL}", Name = "PEPDeauOkCancelUrl")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ProcessPepMessage(string returnURL, [FromForm] VPOSResultMessageTransportRequest message, CancellationToken cancellationToken)
        {
            //Ако има грешка пак пренасочваме към подаденият адрес, защото след това получаваме и нотификации.
            await _pepDaeuPaymentCallbackService.ProcessVPOSResultMessageAsync(message, cancellationToken);

            UriBuilder uriBuilder = new UriBuilder();
            uriBuilder.Scheme = Request.Scheme;
            uriBuilder.Host = Request.Host.Host;
            if (Request.Host.Port.HasValue)
            {
                uriBuilder.Port = Request.Host.Port.Value;
            }

            var url = $"{Request.PathBase}/{returnURL}";
            uriBuilder.Path = url.Replace("//", "/").Replace("//", "/");

            return Redirect(uriBuilder.Uri.AbsoluteUri);
        }

        [HttpGet, Route("Hello")]
        public async Task<ActionResult> Test_Hello()
        {
            _logger.LogError("We call test method!!!!");
            return Ok("Its up!");
        }
    }
}