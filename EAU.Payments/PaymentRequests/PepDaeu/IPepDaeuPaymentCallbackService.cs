using CNSys;
using EAU.Payments.PaymentRequests.PepDaeu.Models;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Payments.PaymentRequests.PepDaeu
{
    /// <summary>
    /// Интерфейс за работа с обратна връзка от PepDaeu.
    /// </summary>
    public interface IPepDaeuPaymentCallbackService
    {
        /// <summary>
        /// Операция за обработане на съобщение от платежен канал.
        /// </summary>
        /// <param name="registrationDataCin">Клиентски идентификатор на регистрационни данни.</param>
        /// <param name="message">Съобщение.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns>Резултат от обратна връзка от PepDaeu.</returns>
        Task<OperationResult<NotificationMessageResponse>> ProcessNotificationMessageAsync(string registrationDataCin, NotificationMessageTransportRequest message, CancellationToken cancellationToken);

        /// <summary>
        /// Операция за връщане след плащане през ПЕП на ДАЕУ.
        /// </summary>
        /// <param name="message">Съобщение.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns>Резултат от плащане през ПЕП на ДАЕУ</returns>
        Task<OperationResult> ProcessVPOSResultMessageAsync(VPOSResultMessageTransportRequest message, CancellationToken cancellationToken);
    }
}