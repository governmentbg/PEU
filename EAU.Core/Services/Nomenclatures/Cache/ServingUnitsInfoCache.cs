using CNSys.Caching;
using EAU.Services.Nomenclatures.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Services.Nomenclatures.Cache
{
    public interface IServingUnitsInfoCache : IDataCacheItems<CachedDataInfo<List<UnitInfo>>, int>
    {
    }

    public class ServingUnitsInfo : IServingUnitsInfo
    {
        private readonly IServingUnitsInfoCache _servingUnitsInfoCache;

        public ServingUnitsInfo(IServingUnitsInfoCache servingUnitsInfoCache)
        {
            _servingUnitsInfoCache = servingUnitsInfoCache;
        }

        public ValueTask EnsureLoadedAsync(int serviceID, CancellationToken cancellationToken)
        {
            return _servingUnitsInfoCache.GetItem(serviceID).EnsureCreatedAsync(cancellationToken);
        }

        public IEnumerable<UnitInfo> Search(int serviceID, out DateTime? lastModifiedDate)
        {
            var data = _servingUnitsInfoCache.GetItem(serviceID).Get();

            lastModifiedDate = data.LastModifiedDate;

            return data.Value;
        }
    }
}