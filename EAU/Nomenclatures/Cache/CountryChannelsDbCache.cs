using CNSys.Caching;
using EAU.Nomenclatures.Models;
using EAU.Nomenclatures.Repositories;
using EAU.Utilities.Caching;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace EAU.Nomenclatures.Cache
{
    public class CountryChannelsDbCache : DbDataCacheItem<List<Country>>, ICountriesCache
    {
        private readonly ICountryRepository _countryRepository;

        public CountryChannelsDbCache(
            ILogger<CountryChannelsDbCache> logger,
            IDbCacheInvalidationDispatcher dbCacheInvalidationDispatcher,
            ICountryRepository countryRepository) :
            base(logger,
                dbCacheInvalidationDispatcher,
                null,
                new string[] { "[nom].[n_s_countries]" })
        {
            _countryRepository = countryRepository;
        }

        protected override async Task<CachedDataInfo<List<Country>>> GenerateCacheDataInfoAsync(DateTime? etag, CancellationToken cancellationToken)
        {
            var data = await _countryRepository.SearchInfoAsync(new CountrySearchCriteria(), cancellationToken);

            return new CachedDataInfo<List<Country>>()
            {
                LastModifiedDate = data.LastUpdatedOn,
                Value = data.Data?.ToList()
            };
        }
    }
}
