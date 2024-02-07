using CNSys.Data;
using EAU.Common.Models;
using EAU.Data;
using EAU.Nomenclatures.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Common.Repositories
{
    /// <summary>
    /// Интерфейс за поддържане и съхранение на параметри на системата.
    /// </summary>
    public interface IAppParameterRepository :
        IRepositoryAsync<AppParameter, long?, AppParameterSearchCriteria>,
        ISearchCollectionInfo2<AppParameter, AppParameterSearchCriteria>
    {
    }

    /// <summary>
    /// Реализация на интерфейс IAppParameterEntity за поддържане и съхранение на параметри на системата.
    /// </summary>
    internal class AppParameterRepository : EAURepositoryBase<AppParameter, long?, AppParameterSearchCriteria, AppParameterDataContext>, IAppParameterRepository
    {
        #region Constructors

        public AppParameterRepository(IDbContextProvider dbContextProvider)
            : base(dbContextProvider)
        {
        }


        #endregion

        #region CRUD

        protected override Task UpdateInternalAsync(AppParameterDataContext context, AppParameter item, CancellationToken cancellationToken)
        {
            return context.AppParameterUpdateAsync(
                                item.Code,
                                item.ValueDateTime,
                                item.ValueIntervalFromStartDate,
                                item.ValueString,
                                item.ValueInt,
                                item.ValueHour,
                                cancellationToken);
        }

        public Task<CollectionInfo<AppParameter>> SearchInfoAsync(AppParameterSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return SearchInfoAsync(PagedDataState.CreateMaxPagedDataState(), searchCriteria, cancellationToken);
        }

        public Task<CollectionInfo<AppParameter>> SearchInfoAsync(PagedDataState state, AppParameterSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return DoOperationAsync(async (dc, innerToken) =>
            {
                var (data, count, lastUpdated) = await dc.SearchAsync(
                    (short?)searchCriteria.FunctionalityID,
                    searchCriteria.Code,
                    searchCriteria.CodeIsExact,
                    searchCriteria.Description,
                    searchCriteria.IsSystem,
                    state.StartIndex,
                    state.PageSize,
                    (state.StartIndex == 1),
                    innerToken);

                state.Count = count ?? state.Count;

                return new CollectionInfo<AppParameter>()
                {
                    Data = data.ToList(),
                    LastUpdatedOn = lastUpdated.GetValueOrDefault()
                };

            }, cancellationToken);
        }

        #endregion
    }
}
