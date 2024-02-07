using CNSys.Caching;
using EAU.Services.Nomenclatures.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Services.Nomenclatures.Cache
{
    public interface IVehicleBaseColorsCache : IDataCacheItem<CachedDataInfo<List<Nomenclature>>>
    {
    }

    public class VehicleBaseColors : IVehicleBaseColors
    {
        private readonly IVehicleBaseColorsCache _cache;

        public VehicleBaseColors(IVehicleBaseColorsCache cache)
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
