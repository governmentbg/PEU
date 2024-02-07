using CNSys.Data;
using EAU.Data;
using EAU.Payments.Obligations.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Payments.Obligations.Repositories
{
    /// <summary>
    /// Интерфейс IObligationEntity за поддържане и съхранение на обекти от тип Obligation.
    /// </summary>
    public interface IObligationRepository :
        IRepository<Obligation, long?, ObligationRepositorySearchCriteria>,
        IRepositoryAsync<Obligation, long?, ObligationRepositorySearchCriteria>
    {
    }

    /// <summary>
    /// Реализация на интерфейс IObligationEntity за поддържане и съхранение на обекти от тип Obligation.
    /// </summary>
    internal class ObligationRepository : EAURepositoryBase<Obligation, long?, ObligationRepositorySearchCriteria, ObligationDataContext>, IObligationRepository
    {
        #region Constructors

        public ObligationRepository(IDbContextProvider dbContextProvider)
            : base(dbContextProvider)
        {
        }

        #endregion

        #region IObligationEntity

        protected override async Task CreateInternalAsync(ObligationDataContext context, Obligation item, CancellationToken cancellationToken)
        {
            long? paymentID = await context.ObligationCreateAsync(
                                (byte?)item.Status,
                                item.Amount,
                                item.DiscountAmount,
                                item.BankName,
                                item.Bic,
                                item.Iban,
                                item.PaymentReason,
                                item.PepCin,
                                item.ExpirationDate,
                                item.ApplicantID,
                                item.ObligedPersonName,
                                item.ObligedPersonIdent,
                                (byte?)item.ObligedPersonIdentType,
                                item.ObligationDate,
                                item.ObligationIdentifier,
                                (byte?)item.Type,
                                item.ServiceInstanceID,
                                item.ServiceID,
                                item.AdditionalData,
                                (byte?)item.ANDSourceId,
                                cancellationToken);

            item.ObligationID = paymentID;
        }

        protected override Task UpdateInternalAsync(ObligationDataContext context, Obligation item, CancellationToken cancellationToken)
        {
            return context.ObligationUpdateAsync(
                item.ObligationID,
                (byte?)item.Status,
                item.Amount,
                item.DiscountAmount,
                item.BankName,
                item.Bic,
                item.Iban,
                item.PaymentReason,
                item.PepCin,
                item.ExpirationDate,
                item.ApplicantID,
                item.ObligedPersonName,
                item.ObligedPersonIdent,
                (byte?)item.ObligedPersonIdentType,
                item.ObligationDate,
                item.ObligationIdentifier,
                (byte?)item.Type,
                item.ServiceInstanceID,
                item.ServiceID,
                item.AdditionalData,
                (byte?)item.ANDSourceId,
                cancellationToken);
        }

        protected override async Task<IEnumerable<Obligation>> SearchInternalAsync(ObligationDataContext context, PagedDataState state, ObligationRepositorySearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            using (var reader = await context.ObligationSearchAsync(
                                                searchCriteria.ObligationIdentifiersSearchCriteria,
                                                searchCriteria.IsActive,
                                                (byte?)searchCriteria.Type,
                                                EnumerableExtensions.ToStringNumberCollection(searchCriteria.ObligationIDs),
                                                EnumerableExtensions.ToStringNumberCollection(searchCriteria.Statuses),
                                                searchCriteria.ApplicantID,
                                                searchCriteria.IsApplicantAnonimous,
                                                searchCriteria.ServiceInsanceID,
                                                searchCriteria.WithLock,
                                                searchCriteria.LoadOption?.LoadPaymentRequests,
                                                state.StartIndex,
                                                state.PageSize,
                                                (state.StartIndex == 1),
                                                cancellationToken))
            {
                List<Obligation> obligations = (await reader.ReadAsync<Obligation>())?.ToList();

                if (searchCriteria.LoadOption != null
                    && searchCriteria.LoadOption.LoadPaymentRequests)
                {
                    //Зарежда съдържанието
                    List<PaymentRequests.Models.PaymentRequest> paymentRequests = (await reader.ReadAsync<PaymentRequests.Models.PaymentRequest>())?.ToList();

                    if (obligations != null
                        && obligations.Count > 0
                        && paymentRequests != null 
                        && paymentRequests.Any())
                    {
                        foreach (Obligation obl in obligations)
                        {
                            obl.PaymentRequests = paymentRequests.Where(pReq => pReq.ObligationID == obl.ObligationID).ToList();
                        }
                    }
                }

                state.Count = reader.ReadOutParameter<int?>("@p_count") ?? state.Count;

                return obligations;
            }
        }

        protected override Task<IEnumerable<Obligation>> SearchInternalAsync(ObligationDataContext context, ObligationRepositorySearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return SearchInternalAsync(context, PagedDataState.CreateMaxPagedDataState(), searchCriteria, cancellationToken);
        }

        #endregion
    }
}
