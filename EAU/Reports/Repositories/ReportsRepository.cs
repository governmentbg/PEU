using CNSys.Data;
using EAU.Common;
using EAU.Data;
using EAU.Reports.NotaryInterestsForPersonOrVehicle.Models;
using EAU.Reports.PaymentsObligations.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Reports.Repositories
{
    /// <summary>
    /// Интерфейс PaymentsObligationsRepository за справка за извършено плащане
    /// </summary>
    public interface IReportsRepository :
        ISearchAsync<PaymentsObligationsData, PaymentsObligationsSearchCriteria>,
        ISearchAsync<DocumentAccessedDataGroupedRow, DocumentAccessDataReportSearchCriteria>
    {
    }

    /// <summary>
    /// Реализация на интерфейс PaymentsObligationsRepository.
    /// </summary>
    internal class ReportsRepository : EAURepositoryBase<PaymentsObligationsData, long?, PaymentsObligationsSearchCriteria, ReportsDataContext>, IReportsRepository
    {
        #region Constructors

        public ReportsRepository(IDbContextProvider dbContextProvider)
            : base(dbContextProvider)
        {
        }
        
        #endregion

        protected override async Task<IEnumerable<PaymentsObligationsData>> SearchInternalAsync(ReportsDataContext context, PagedDataState state, PaymentsObligationsSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            var (data, count) = await context.PaymentsObligationsSearchAsync(
                                                searchCriteria.DebtorIdentifier,
                                                searchCriteria.DateFrom,
                                                searchCriteria.DateTo,
                                                state.StartIndex,
                                                state.PageSize,
                                                (state.StartIndex == 1),
                                                cancellationToken);

            state.Count = count ?? state.Count;

            return data.ToList();
        }

        protected override Task<IEnumerable<PaymentsObligationsData>> SearchInternalAsync(ReportsDataContext context, PaymentsObligationsSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return SearchInternalAsync(context, PagedDataState.CreateMaxPagedDataState(), searchCriteria, cancellationToken);
        }


        public Task<IEnumerable<DocumentAccessedDataGroupedRow>> SearchAsync(DocumentAccessDataReportSearchCriteria searchCriteria)
        {
            return SearchAsync(PagedDataState.CreateMaxPagedDataState(), searchCriteria, CancellationToken.None);
        }

        public Task<IEnumerable<DocumentAccessedDataGroupedRow>> SearchAsync(DocumentAccessDataReportSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return SearchAsync(PagedDataState.CreateMaxPagedDataState(), searchCriteria, cancellationToken);
        }

        public Task<IEnumerable<DocumentAccessedDataGroupedRow>> SearchAsync(PagedDataState state, DocumentAccessDataReportSearchCriteria searchCriteria)
        {
            return SearchAsync(state, searchCriteria, CancellationToken.None);
        }

        public Task<IEnumerable<DocumentAccessedDataGroupedRow>> SearchAsync(PagedDataState state, DocumentAccessDataReportSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return DoOperationAsync(async (ctx, token) => {

                var (data, count) = await ctx.DocumentAccessDataReportSearchAsync(
                                                searchCriteria.DataType,
                                                searchCriteria.DataValue,
                                                searchCriteria.DateFrom,
                                                searchCriteria.DateTo,
                                                searchCriteria.DocumentTypeId,
                                                searchCriteria.DataTypesInResult?.ConcatItems(),
                                                state.StartIndex,
                                                state.PageSize,
                                                (state.StartIndex == 1),
                                                cancellationToken);

                state.Count = count ?? state.Count;

                IEnumerable<DocumentAccessedDataGroupedRow> result = data.ToList();

                return result;

            }, cancellationToken);            
        }
    }
}