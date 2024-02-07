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
    public class EkatteSearchCriteria
    { }

    /// <summary>
    /// Интерфейс за поддържане и съхранение на обекти от тип Ekatte.
    /// </summary>
    public interface IEkatteRepository :
        ISearchCollectionInfo2<Ekatte, EkatteSearchCriteria>
        
    {
    }

    /// <summary>
    /// Реализация на интерфейс IEkatteRepository за поддържане и съхранение на обекти от тип Ekatte.
    /// </summary>
    internal class EkatteRepository : EAURepositoryBase<Ekatte, long?, EkatteSearchCriteria, EkatteDataContext>, IEkatteRepository
    {
        #region Constructors

        public EkatteRepository(IDbContextProvider dbContextProvider)
            : base(dbContextProvider)
        {
        }

        #endregion


        public Task<CollectionInfo<Ekatte>> SearchInfoAsync(EkatteSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return SearchInfoAsync(PagedDataState.CreateMaxPagedDataState(), searchCriteria, cancellationToken);
        }

        public Task<CollectionInfo<Ekatte>> SearchInfoAsync(PagedDataState state, EkatteSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return DoOperationAsync(async (dc, innerToken) =>
            {
                var (data, count, lastUpdated) = await dc.SearchAsync(
                    state.StartIndex,
                    state.PageSize,
                    (state.StartIndex == 1),
                    innerToken);

                state.Count = count ?? state.Count;

                return new CollectionInfo<Ekatte>()
                {
                    Data = data.ToList(),
                    LastUpdatedOn = lastUpdated.GetValueOrDefault()
                };

            }, cancellationToken);
        }       
    }
}
