using CNSys.Data;
using EAU.Audit.Models;
using EAU.Data;
using EAU.Nomenclatures.Repositories;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Audit.Repositories
{
    /// <summary>
    /// Интерфейс за поддържане и съхранение на видове действия.
    /// </summary>
    public interface IActionTypeRepository :
        IRepositoryAsync<ActionType, short?, ActionTypeSearchCriteria>,
        ISearchCollectionInfo2<ActionType, ActionTypeSearchCriteria>
    {
    }

    /// <summary>
    /// Реализация на интерфейс IActionTypeEntity за поддържане и съхранение на видове действия.
    /// </summary>
    internal class ActionTypeRepository : EAURepositoryBase<ActionType, short?, ActionTypeSearchCriteria, ActionTypeDataContext>, IActionTypeRepository
    {
        #region Constructors

        public ActionTypeRepository(IDbContextProvider dbContextProvider)
            : base(dbContextProvider)
        {
        }


        #endregion

        #region CRUD

        public Task<CollectionInfo<ActionType>> SearchInfoAsync(ActionTypeSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return SearchInfoAsync(PagedDataState.CreateMaxPagedDataState(), searchCriteria, cancellationToken);
        }

        public Task<CollectionInfo<ActionType>> SearchInfoAsync(PagedDataState state, ActionTypeSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return DoOperationAsync(async (dc, innerToken) =>
            {
                var (data, count, lastUpdated) = await dc.SearchAsync(
                    (short?)searchCriteria.ActionTypeID,
                    state.StartIndex,
                    state.PageSize,
                    (state.StartIndex == 1),
                    innerToken);

                state.Count = count ?? state.Count;

                return new CollectionInfo<ActionType>()
                {
                    Data = data.ToList(),
                    LastUpdatedOn = lastUpdated.GetValueOrDefault()
                };

            }, cancellationToken);
        }

        #endregion
    }
}
