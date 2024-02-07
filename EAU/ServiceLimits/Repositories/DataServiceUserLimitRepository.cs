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
    /// Entity интерфейс за работа с лимит на потребител.
    /// </summary>
    public interface IDataServiceUserLimitRepository :
    IRepository<DataServiceUserLimit, int, DataServiceUserLimitsSearchCriteria>,
    IRepositoryAsync<DataServiceUserLimit, int, DataServiceUserLimitsSearchCriteria>,
    ISearchCollectionInfo2<DataServiceUserLimit, DataServiceUserLimitsSearchCriteria>
    {
    }

    /// <summary>
    /// Реализация на entity интерфейс за работа с лимит на потребител.
    /// </summary>
    internal class DataServiceUserLimitRepository : EAURepositoryBase<DataServiceUserLimit, int, DataServiceUserLimitsSearchCriteria, DataServiceLimitsDataContext>, IDataServiceUserLimitRepository
    {
        public DataServiceUserLimitRepository(IDbContextProvider dbContextProvider) : base(dbContextProvider)
        {

        }

        protected override async Task CreateInternalAsync(DataServiceLimitsDataContext context, DataServiceUserLimit item, CancellationToken cancellationToken)
        {
            int? userLimitID = await context.DataServiceUserLimitCreateAsync(
                                item.ServiceLimitID,
                                item.UserID,
                                item.RequestsIntervalFromStartDate,
                                item.RequestsNumber,
                                (short)item.Status,
                                cancellationToken);

            item.UserLimitID = userLimitID;
        }

        protected override Task UpdateInternalAsync(DataServiceLimitsDataContext context, DataServiceUserLimit item, CancellationToken cancellationToken)
        {
            return context.DataServiceUserLimitUpdateAsync(
                                item.UserLimitID,
                                item.ServiceLimitID,
                                item.UserID,
                                item.RequestsIntervalFromStartDate,
                                item.RequestsNumber,
                                (short)item.Status,
                                cancellationToken);
        }

        public Task<CollectionInfo<DataServiceUserLimit>> SearchInfoAsync(DataServiceUserLimitsSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return SearchInfoAsync(PagedDataState.CreateMaxPagedDataState(), searchCriteria, cancellationToken);
        }

        public Task<CollectionInfo<DataServiceUserLimit>> SearchInfoAsync(PagedDataState state, DataServiceUserLimitsSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return DoOperationAsync(async (dc, innerToken) =>
            {
                var (data, count, lastUpdated) = await dc.DataServiceUserLimitsSearchAsync(
                                    EnumerableExtensions.ToStringNumberCollection(searchCriteria.UserLimitIDs),
                                    searchCriteria.ServiceLimitID,
                                    searchCriteria.UserID,
                                    (short?)searchCriteria.Status,
                                    state.StartIndex,
                                    state.PageSize,
                                    (state.StartIndex == 1),
                                    innerToken);

                state.Count = count ?? state.Count;

                return new CollectionInfo<DataServiceUserLimit>()
                {
                    Data = data.ToList(),
                    LastUpdatedOn = lastUpdated.GetValueOrDefault()
                };

            }, cancellationToken);
        }
    }
}
