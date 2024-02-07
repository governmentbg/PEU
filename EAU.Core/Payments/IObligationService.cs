using CNSys;
using CNSys.Data;
using EAU.Payments.Obligations.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Payments
{
    /// <summary>
    /// Интерфейс за работа със задължения.
    /// </summary>
    public interface IObligationService
    {
        /// <summary>
        /// Операция за търсене на задължения.
        /// </summary>
        /// <param name="state">Състояние за странициране.</param>
        /// <param name="criteria">Критерии за търсене.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns>Задължения.</returns>
        Task<OperationResult<IEnumerable<ObligationSearchResult>>> SearchAsync(PagedDataState state, ObligationSearchCriteria criteria, CancellationToken cancellationToken);

        /// <summary>
        /// Операция за създаване на задължение.
        /// </summary>
        /// <param name="obligationRequest">Заявка за създаване на задължение.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns>Резултат от операцията и създаденото задължение.</returns>
        Task<OperationResult<Obligation>> CreateAsync(ObligationRequest obligationRequest, CancellationToken cancellationToken);

        /// <summary>
        /// Операция за обработване на задължение.
        /// </summary>
        /// <param name="obligationID">Идентификатор на задължение.</param>
        /// <param name="paymentRequestID">Идентификатор на заявка за плащане.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns></returns>
        Task ProcessObligation(long obligationID, long paymentRequestID, CancellationToken cancellationToken);
    }

    /// <summary>
    /// Резултат от търсене на задължения
    /// </summary>
    public class ObligationSearchResult
    {
        public ANDSourceIds? UnitGroup { get; set; }
        public bool ErrorNoDataFound { get; set; }
        public bool ErrorReadingData { get; set; }
        public List<Obligation> Obligations { get; set; }
    }
}
