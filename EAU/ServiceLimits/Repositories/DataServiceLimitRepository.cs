using CNSys.Data;
using EAU.Data;
using EAU.Nomenclatures.Repositories;
using EAU.ServiceLimits.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.ServiceLimits.Repositories
{
    /// <summary>
    /// Entity интерфейс за работа с лимит.
    /// </summary>
    public interface IDataServiceLimitRepository :
        IRepository<DataServiceLimit, int, DataServiceLimitsSearchCriteria>,
        IRepositoryAsync<DataServiceLimit, int, DataServiceLimitsSearchCriteria>,
        ISearchCollectionInfo2<DataServiceLimit, DataServiceLimitsSearchCriteria>
    {
    }

    /// <summary>
    /// Реализация на entity интерфейс за работа с лимит.
    /// </summary>
    internal class DataServiceLimitRepository : EAURepositoryBase<DataServiceLimit, int, DataServiceLimitsSearchCriteria, DataServiceLimitsDataContext>, IDataServiceLimitRepository
    {
        public DataServiceLimitRepository(IDbContextProvider dbContextProvider) : base(dbContextProvider)
        {
        }

        protected override Task UpdateInternalAsync(DataServiceLimitsDataContext context, DataServiceLimit item, CancellationToken cancellationToken)
        {
            return context.DataServiceLimitUpdateAsync(
                                item.ServiceCode,
                                item.RequestsIntervalFromStartDate,
                                item.RequestsNumber,
                                (short)item.Status,
                                cancellationToken);
        }

        public Task<CollectionInfo<DataServiceLimit>> SearchInfoAsync(DataServiceLimitsSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return SearchInfoAsync(PagedDataState.CreateMaxPagedDataState(), searchCriteria, cancellationToken);
        }

        public Task<CollectionInfo<DataServiceLimit>> SearchInfoAsync(PagedDataState state, DataServiceLimitsSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return DoOperationAsync(async (dc, innerToken) =>
            {
                var (data, count, lastUpdated) = await dc.DataServiceLimitsSearchAsync(
                                    EnumerableExtensions.ToStringNumberCollection(searchCriteria.ServiceLimitIDs),
                                    searchCriteria.ServiceCode,
                                    searchCriteria.ServiceName,
                                    (short?)searchCriteria.Status,
                                    state.StartIndex,
                                    state.PageSize,
                                    (state.StartIndex == 1),
                                    innerToken);

                state.Count = count ?? state.Count;

                return new CollectionInfo<DataServiceLimit>()
                {
                    Data = data.ToList(),
                    LastUpdatedOn = lastUpdated.GetValueOrDefault()
                };

            }, cancellationToken);
        }
    }
}
