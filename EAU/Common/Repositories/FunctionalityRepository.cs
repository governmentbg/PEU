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
    /// Интерфейс за поддържане и съхранение на функционалности на системата.
    /// </summary>
    public interface IFunctionalityRepository :
        IRepositoryAsync<Functionality, short?, FunctionalitySearchCriteria>,
        ISearchCollectionInfo2<Functionality, FunctionalitySearchCriteria>
    {
    }

    /// <summary>
    /// Реализация на интерфейс IFunctionalityEntity за поддържане и съхранение на функционалности на системата.
    /// </summary>
    internal class FunctionalityRepository : EAURepositoryBase<Functionality, short?, FunctionalitySearchCriteria, FunctionalityDataContext>, IFunctionalityRepository
    {
        #region Constructors

        public FunctionalityRepository(IDbContextProvider dbContextProvider)
            : base(dbContextProvider)
        {
        }


        #endregion

        #region CRUD

        public Task<CollectionInfo<Functionality>> SearchInfoAsync(FunctionalitySearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return SearchInfoAsync(PagedDataState.CreateMaxPagedDataState(), searchCriteria, cancellationToken);
        }

        public Task<CollectionInfo<Functionality>> SearchInfoAsync(PagedDataState state, FunctionalitySearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return DoOperationAsync(async (dc, innerToken) =>
            {
                var (data, count, lastUpdated) = await dc.SearchAsync(
                    (short?)searchCriteria.FunctionalityID,
                    state.StartIndex,
                    state.PageSize,
                    (state.StartIndex == 1),
                    innerToken);

                state.Count = count ?? state.Count;

                return new CollectionInfo<Functionality>()
                {
                    Data = data.ToList(),
                    LastUpdatedOn = lastUpdated.GetValueOrDefault()
                };

            }, cancellationToken);
        }

        #endregion
    }
}
