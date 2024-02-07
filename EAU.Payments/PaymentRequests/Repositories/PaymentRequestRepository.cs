using CNSys.Data;
using EAU.Data;
using EAU.Payments.PaymentRequests.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace EAU.Payments.PaymentRequests.Repositories
{
    //class PaymentRequestRepository
    /// <summary>
    /// Интерфейс IPaymentRequestEntity за поддържане и съхранение на обекти от тип Models.PaymentRequest.
    /// </summary>
    public interface IPaymentRequestRepository :
        IRepository<Models.PaymentRequest, long?, PaymentRequestSearchCriteria>,
        IRepositoryAsync<Models.PaymentRequest, long?, PaymentRequestSearchCriteria>
    {
    }

    /// <summary>
    /// Реализация на интерфейс IPaymentRequestEntity за поддържане и съхранение на обекти от тип Models.PaymentRequest.
    /// </summary>
    internal class PaymentRequestRepository : EAURepositoryBase<Models.PaymentRequest, long?, PaymentRequestSearchCriteria, PaymentRequestDataContext>, IPaymentRequestRepository
    {
        #region Constructors

        public PaymentRequestRepository(IDbContextProvider dbContextProvider)
            : base(dbContextProvider)
        {
        }

        #endregion

        #region IPaymentRequestEntity

        protected override async Task CreateInternalAsync(PaymentRequestDataContext context, Models.PaymentRequest item, CancellationToken cancellationToken)
        {
            long? paymentID = await context.PaymentRequestCreateAsync(
                                item.RegistrationDataID,
                                (byte?)item.RegistrationDataType,
                                (byte?)item.Status,
                                item.ObligationID,
                                item.ObligedPersonName,
                                item.ObligedPersonIdent,
                                (byte?)item.ObligedPersonIdentType,
                                item.PayerIdent,
                                (byte?)item.PayerIdentType,
                                item.SendDate,
                                item.PayDate,
                                item.ExternalPortalPaymentNumber,
                                item.Amount,
                                item.AdditionalData,
                                cancellationToken);

            item.PaymentRequestID = paymentID;
        }

        protected override Task UpdateInternalAsync(PaymentRequestDataContext context, Models.PaymentRequest item, CancellationToken cancellationToken)
        {
            return context.PaymentRequestUpdateAsync(
                item.PaymentRequestID,
                item.RegistrationDataID,
                (byte?)item.RegistrationDataType,
                (byte?)item.Status,
                item.ObligationID,
                item.ObligedPersonName,
                item.ObligedPersonIdent,
                (byte?)item.ObligedPersonIdentType,
                item.PayerIdent,
                (byte?)item.PayerIdentType,
                item.SendDate,
                item.PayDate,
                item.ExternalPortalPaymentNumber,
                item.Amount,
                item.AdditionalData,
                cancellationToken);
        }

        protected override Task DeleteInternalAsync(PaymentRequestDataContext context, Models.PaymentRequest item, CancellationToken cancellationToken)
        {
            return DeleteInternalAsync(context, item.PaymentRequestID, cancellationToken);
        }

        protected override Task DeleteInternalAsync(PaymentRequestDataContext context, long? key, CancellationToken cancellationToken)
        {
            if (key == null)
                throw new ArgumentNullException();

            return context.PaymentRequestDeleteAsync(key, cancellationToken);
        }


        protected override async Task<IEnumerable<Models.PaymentRequest>> SearchInternalAsync(PaymentRequestDataContext context, PagedDataState state, PaymentRequestSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            var (data, count) = await context.PaymentRequestSearchAsync(
                                                EnumerableExtensions.ToStringNumberCollection(searchCriteria.PaymentRequestIDs),
                                                EnumerableExtensions.ToStringNumberCollection(searchCriteria.ObligationIDs),
                                                searchCriteria.RegistrationDataID,
                                                (byte?)searchCriteria.PaymentChannel,
                                                searchCriteria.ExternalPortalNumber,
                                                searchCriteria.WithLock,
                                                state.StartIndex,
                                                state.PageSize,
                                                (state.StartIndex == 1),
                                                cancellationToken);

            state.Count = count ?? state.Count;

            return data.ToList();
        }

        protected override Task<IEnumerable<Models.PaymentRequest>> SearchInternalAsync(PaymentRequestDataContext context, PaymentRequestSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return SearchInternalAsync(context, PagedDataState.CreateMaxPagedDataState(), searchCriteria, cancellationToken);
        }

        #endregion
    }
}
