using CNSys.Caching;
using EAU.Nomenclatures.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Nomenclatures.Cache
{
    public interface IGraoCache : IDataCacheItem<CachedDataInfo<List<Grao>>>
    {

    }

    public class Graos : IGrao
    {
        private readonly IGraoCache _graoCache;

        public Graos(IGraoCache ekatteCache)
        {
            _graoCache = ekatteCache;
        }

        public ValueTask EnsureLoadedAsync(CancellationToken cancellationToken)
        {
            return _graoCache.EnsureCreatedAsync(cancellationToken);
        }

        public IEnumerable<Grao> Search()
        {
            CachedDataInfo<List<Grao>> data = _graoCache.Get();

            return data?.Value;
        }
        public IEnumerable<Grao> Search(out DateTime? lastDateModified)
        {
            CachedDataInfo<List<Grao>> data = _graoCache.Get();

            lastDateModified = data?.LastModifiedDate;

            return data?.Value;
        }
    }
}
