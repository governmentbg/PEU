using CNSys.Caching;
using EAU.Services.Nomenclatures.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Services.Nomenclatures.Cache
{
    public interface IReasonsForIssuingDuplicateOfSRMPSCache : IDataCacheItem<CachedDataInfo<List<Nomenclature>>>
    {
    }

    public class ReasonsForIssuingDuplicateOfSRMPS : IReasonsForIssuingDuplicateOfSRMPS
    {
        private readonly IReasonsForIssuingDuplicateOfSRMPSCache _cache;

        public ReasonsForIssuingDuplicateOfSRMPS(IReasonsForIssuingDuplicateOfSRMPSCache cache)
        {
            _cache = cache;
        }

        public ValueTask EnsureLoadedAsync(CancellationToken cancellationToken)
        {
            return _cache.EnsureCreatedAsync(cancellationToken);
        }

        public IEnumerable<Nomenclature> Search(out DateTime? lastModifiedDate)
        {
            var data = _cache.Get();

            lastModifiedDate = data.LastModifiedDate;

            return data.Value;
        }
    }
}
