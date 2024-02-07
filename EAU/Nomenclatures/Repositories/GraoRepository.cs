using CNSys.Data;
using EAU.Data;
using EAU.Nomenclatures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Nomenclatures.Repositories
{
    public class GraoSearchCriteria
    { }

    /// <summary>
    /// Интерфейс за поддържане и съхранение на обекти от тип Ekatte.
    /// </summary>
    public interface IGraoRepository :
        ISearchCollectionInfo2<Grao, GraoSearchCriteria>        
    {
    }

    /// <summary>
    /// Реализация на интерфейс IEkatteRepository за поддържане и съхранение на обекти от тип Ekatte.
    /// </summary>
    internal class GraoRepository : EAURepositoryBase<Grao, long?, GraoSearchCriteria, GraoDataContext>, IGraoRepository
    {
        #region Constructors

        public GraoRepository(IDbContextProvider dbContextProvider)
            : base(dbContextProvider)
        {
        }

        #endregion


        public Task<CollectionInfo<Grao>> SearchInfoAsync(GraoSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return SearchInfoAsync(PagedDataState.CreateMaxPagedDataState(), searchCriteria, cancellationToken);
        }

        public Task<CollectionInfo<Grao>> SearchInfoAsync(PagedDataState state, GraoSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return DoOperationAsync(async (dc, innerToken) =>
            {
                var (data, count, lastUpdated) = await dc.SearchAsync(
                    state.StartIndex,
                    state.PageSize,
                    (state.StartIndex == 1),
                    innerToken);

                state.Count = count ?? state.Count;

                return new CollectionInfo<Grao>()
                {
                    Data = data.ToList(),
                    LastUpdatedOn = lastUpdated.GetValueOrDefault()
                };

            }, cancellationToken);
        }
    }
}
