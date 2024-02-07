using CNSys;
using CNSys.Data;
using EAU.Payments.Obligations.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Payments
{
    /// <summary>
    /// Интерфейс за работа с на данни за задължения от външни системи.
    /// </summary>
    public interface IObligationChannelService
    {
        /// <summary>
        /// Операця за изчиане на данни за задължения от външни системи.
        /// </summary>
        /// <param name="criteria">Критерии за търсене</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns></returns>
        Task<OperationResult<List<ObligationSearchResult>>> SearchAsync(ObligationChannelSearchCriteria criteria, CancellationToken cancellationToken);

        /// <summary>
        /// Операця за обработване на данни от външни системи.
        /// </summary>
        /// <param name="obligation">Задължение.</param>
        /// <param name="activePaymentRequestID">Идентификатор на заявка за плащане за която се извършва обработването.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        Task<OperationResult> ProcessObligation(Obligation obligation, long activePaymentRequestID, CancellationToken cancellationToken);
    }
}