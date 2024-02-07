using CNSys;
using EAU.Payments.PaymentRequests.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Payments
{
    /// <summary>
    /// Интерфейс за работа със заявки за плащане.
    /// </summary>
    public interface IPaymentRequestService
    {
        /// <summary>
        /// Операция за стартиране на плащане.
        /// </summary>
        /// <param name="obligaionID">идентификатор на задължение.</param>
        /// <param name="request">заявка за стартиране на плащане.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns>Създадената заявка.</returns>
        Task<OperationResult<PaymentRequest>> StartPaymentAsync(long obligaionID, StartPaymentRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Операция за търсене заявки за плащане.
        /// </summary>
        /// <param name="criteria">Критерии за търсене.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns>Заявки за плащане.</returns>
        Task<IEnumerable<PaymentRequest>> SearchAsync(PaymentRequestSearchCriteria criteria, CancellationToken cancellationToken);

        /// <summary>
        /// Операция за маркиране на плащане като платено.
        /// </summary>
        /// <param name="request">Заявка за плащане.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns>Заявка за плащане</returns>
        Task<OperationResult<PaymentRequest>> PaymentPaid(PaymentRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Операция за маркиране на плащане като отказано.
        /// </summary>
        /// <param name="request">Заявка за плащане.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns>Заявка за плащане</returns>
        Task<OperationResult<PaymentRequest>> PaymentCancelled(PaymentRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Операция за маркиране на плащане като изтекло.
        /// </summary>
        /// <param name="request">Заявка за плащане.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns>Заявка за плащане</returns>
        Task<OperationResult<PaymentRequest>> PaymentExpired(PaymentRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Операция за маркиране на плащане като дублирано .
        /// </summary>
        /// <param name="request">Заявка за плащане.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns>Заявка за плащане</returns>
        Task<OperationResult<PaymentRequest>> PaymentDuplicate(PaymentRequest request, CancellationToken cancellationToken);
    }
}