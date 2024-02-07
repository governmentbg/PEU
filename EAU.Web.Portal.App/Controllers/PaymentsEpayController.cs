using CNSys;
using EAU.Payments.PaymentRequests.Epay;
using EAU.Payments.PaymentRequests.Epay.Models;
using EAU.Web.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Web.Portal.App.Controllers
{
    /// <summary>
    /// Контролер за обратна връзка от Epay
    /// </summary>    
    [Route("Api/Obligations")]
    [Produces("application/json")]
    public class PaymentsEpayController : BaseApiController
    {
        private readonly IEPayPaymentCallbackService _ePayPaymentCallbackService;

        public PaymentsEpayController(IEPayPaymentCallbackService ePayPaymentCallbackService)
        {
            _ePayPaymentCallbackService = ePayPaymentCallbackService;
        }

        /// <summary>
        /// Колбек за обратна връзка от Epay.
        /// </summary>
        /// <param name="registrationDataCin">Клиентски идентификатор на регистационни данни.</param>
        /// <param name="message">Съобщение.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns>Статус.</returns>
        [Route("PaymentRequests/Epay/Callback/{registrationDataCin}")]
        [HttpPost]
        [ProducesResponseType(typeof(Stream), StatusCodes.Status200OK)]
        public async Task<IActionResult> Callback([FromRoute] string registrationDataCin, [FromForm] PaymentsEpayCallbackRequest message, CancellationToken cancellationToken)
        {
            OperationResult<Stream> result = await _ePayPaymentCallbackService.ProcessMessageAsync(registrationDataCin, message, cancellationToken);

            return OperationResult(result);
        }
    }
}