using CNSys.Caching;
using EAU.Nomenclatures.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Nomenclatures.Cache
{
    public interface ICountriesCache : IDataCacheItem<CachedDataInfo<List<Country>>>
    {

    }

    public class Countries : ICountries
    {
        private readonly ICountriesCache _countriesCache;


        public Countries(ICountriesCache countriesCache)
        {
            _countriesCache = countriesCache;
        }

        public ValueTask EnsureLoadedAsync(CancellationToken cancellationToken)
        {
            return _countriesCache.EnsureCreatedAsync(cancellationToken);
        }

        public IEnumerable<Country> Search()
        {
            return _countriesCache.Get().Value;
        }

        public IEnumerable<Country> Search(out DateTime? lastModifiedDate)
        {
            var data = _countriesCache.Get();

            lastModifiedDate = data.LastModifiedDate;
            return data.Value;
        }
    }
}
