using CNSys.Data;
using EAU.Common.Models;
using EAU.Data;
using EAU.Nomenclatures.Models;
using System.Collections;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Nomenclatures.Repositories
{
    /// <summary>
    /// Критерии за търсене на държавите.
    /// </summary>
    public class CountrySearchCriteria : BasePagedSearchCriteria
    {
    }

    /// <summary>
    /// Интерфeйс за поддържане и съхранение на обекти от тип Country.
    /// </summary>
    public interface ICountryRepository :
        ISearchCollectionInfo2<Country, CountrySearchCriteria>
    {
    }

    /// <summary>
    /// Реализация на интерфейс ICountryRepository за поддържане и съхранение на обекти от тип Country.
    /// </summary>
    internal class CountryRepository : EAURepositoryBase<Country, int?, CountrySearchCriteria, CountryDataContext>, ICountryRepository
    {
        #region Constructors

        public CountryRepository(IDbContextProvider dbContextProvider)
            : base(dbContextProvider)
        {
        }

        #endregion

        #region IDeliveryChannelRepository

        public Task<CollectionInfo<Country>> SearchInfoAsync(CountrySearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return SearchInfoAsync(PagedDataState.CreateMaxPagedDataState(), searchCriteria, cancellationToken);
        }

        public Task<CollectionInfo<Country>> SearchInfoAsync(PagedDataState state, CountrySearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return DoOperationAsync(async (dc, innerToken) =>
            {
                var (data, count, lastUpdated) = await dc.SearchAsync(
                    state.StartIndex,
                    state.PageSize,
                    (state.StartIndex == 1),
                    innerToken);

                state.Count = count ?? state.Count;

                return new CollectionInfo<Country>()
                {
                    Data = data.ToList(),
                    LastUpdatedOn = lastUpdated.GetValueOrDefault()
                };

            }, cancellationToken);
        }

        #endregion

    }
}
