using CNSys.Caching;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Services.Nomenclatures.Cache
{
    public interface IRegistrationNumberProvinceCodesCache : IDataCacheItems<CachedDataInfo<List<string>>, string>
    {
    }

    public class RegistrationNumberProvinceCodes : IRegistrationNumberProvinceCodes
    {
        private readonly IRegistrationNumberProvinceCodesCache _cache;

        public RegistrationNumberProvinceCodes(IRegistrationNumberProvinceCodesCache cache)
        {
            _cache = cache;
        }

        public ValueTask EnsureLoadedAsync(int policeDepartmentID, int vehicleTypeCode, CancellationToken cancellationToken)
        {
            return _cache.GetItem(string.Format("{0}_{1}", policeDepartmentID, vehicleTypeCode)).EnsureCreatedAsync(cancellationToken);
        }

        public IEnumerable<string> Search(int policeDepartmentID, int vehicleTypeCode, out DateTime? lastModifiedDate)
        {
            var data = _cache.GetItem(string.Format("{0}_{1}", policeDepartmentID, vehicleTypeCode)).Get();

            lastModifiedDate = data.LastModifiedDate;

            return data.Value;
        }
    }
}
