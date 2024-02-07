using CNSys.Caching;
using EAU.Nomenclatures.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Nomenclatures.Cache
{
    public interface IEkatteCache : IDataCacheItem<CachedDataInfo<List<Ekatte>>>
    {

    }

    public class Ekattes : IEkatte
    {
        private readonly IEkatteCache _ekatteCache;

        public Ekattes(IEkatteCache ekatteCache)
        {
            _ekatteCache = ekatteCache;
        }

        public ValueTask EnsureLoadedAsync(CancellationToken cancellationToken)
        {
            return _ekatteCache.EnsureCreatedAsync(cancellationToken);
        }

        public IEnumerable<Ekatte> Search()
        {
            CachedDataInfo<List<Ekatte>> data = _ekatteCache.Get();

            return data?.Value;
        }
        public IEnumerable<Ekatte> Search(out DateTime? lastDateModified)
        {
            CachedDataInfo<List<Ekatte>> data = _ekatteCache.Get();

            lastDateModified = data?.LastModifiedDate;

            return data?.Value;
        }
    }
}
