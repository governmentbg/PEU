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
    /// Интерфейс за поддържане и съхранение на видове обекти.
    /// </summary>
    public interface IObjectTypeRepository :
        IRepositoryAsync<ObjectType, short?, ObjectTypeSearchCriteria>,
        ISearchCollectionInfo2<ObjectType, ObjectTypeSearchCriteria>
    {
    }

    /// <summary>
    /// Реализация на интерфейс IObjectTypeEntity за поддържане и съхранение на видове обекти.
    /// </summary>
    internal class ObjectTypeRepository : EAURepositoryBase<ObjectType, short?, ObjectTypeSearchCriteria, ObjectTypeDataContext>, IObjectTypeRepository
    {
        #region Constructors

        public ObjectTypeRepository(IDbContextProvider dbContextProvider)
            : base(dbContextProvider)
        {
        }


        #endregion

        #region CRUD

        public Task<CollectionInfo<ObjectType>> SearchInfoAsync(ObjectTypeSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return SearchInfoAsync(PagedDataState.CreateMaxPagedDataState(), searchCriteria, cancellationToken);
        }

        public Task<CollectionInfo<ObjectType>> SearchInfoAsync(PagedDataState state, ObjectTypeSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return DoOperationAsync(async (dc, innerToken) =>
            {
                var (data, count, lastUpdated) = await dc.SearchAsync(
                    (short?)searchCriteria.ObjectTypeID,
                    state.StartIndex,
                    state.PageSize,
                    (state.StartIndex == 1),
                    innerToken);

                state.Count = count ?? state.Count;

                return new CollectionInfo<ObjectType>()
                {
                    Data = data.ToList(),
                    LastUpdatedOn = lastUpdated.GetValueOrDefault()
                };

            }, cancellationToken);
        }

        #endregion
    }
}
