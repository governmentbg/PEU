using CNSys;
using EAU.Payments.Obligations.Models;
using EAU.Payments.PaymentRequests.Models;
using EAU.Payments.RegistrationsData.Models;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Payments
{
    /// <summary>
    /// Интерфейс за работа с канали за плащане.
    /// </summary>
    public interface IPaymentChannelService
    {
        /// <summary>
        /// Операция за плащане.
        /// </summary>
        /// <param name="paymentRequest">Заявка за плащане.</param>
        /// <param name="obligation">Задължение.</param>
        /// <param name="registrationData">Регистрационни данни.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        Task<OperationResult> PayAsync(PaymentRequest paymentRequest, Obligation obligation, RegistrationData registrationData, CancellationToken cancellationToken);
    }
}
