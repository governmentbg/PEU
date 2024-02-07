using CNSys;
using EAU.Payments.PaymentRequests.Epay.Models;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Payments.PaymentRequests.Epay
{
    /// <summary>
    /// Интерфейс за работа с обратна връзка от EPay.
    /// </summary>
    public interface IEPayPaymentCallbackService
    {
        /// <summary>
        /// Операция за обработане на съобщение от платежен канал.
        /// </summary>
        /// <param name="registrationDataCin">Клиентски идентификатор на регистрационни данни.</param>
        /// <param name="message">Съобщение.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns>Резултат от обратна връзка от Epay.</returns>
        Task<OperationResult<Stream>> ProcessMessageAsync(string registrationDataCin, PaymentsEpayCallbackRequest message, CancellationToken cancellationToken);
    }
}
