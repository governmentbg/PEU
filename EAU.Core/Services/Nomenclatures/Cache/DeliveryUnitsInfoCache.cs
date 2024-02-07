using CNSys.Caching;
using EAU.Nomenclatures.Models;
using EAU.Services.Nomenclatures.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Services.Nomenclatures.Cache
{
    public interface IDeliveryUnitsInfoCache : IDataCacheItems<CachedDataInfo<List<UnitInfo>>, string>
    {
    }

    public class DeliveryUnitsInfo : IDeliveryUnitsInfo
    {
        private readonly IDeliveryUnitsInfoCache _deliveryUnitsInfoCache;

        public DeliveryUnitsInfo(IDeliveryUnitsInfoCache deliveryUnitsInfoCache)
        {
            _deliveryUnitsInfoCache = deliveryUnitsInfoCache;
        }

        public ValueTask EnsureLoadedAsync(int serviceID, CancellationToken cancellationToken) => EnsureLoadedAsync(serviceID, null, cancellationToken);

        public ValueTask EnsureLoadedAsync(int serviceID, AdmServiceTermType? serviceTermType, CancellationToken cancellationToken)
        {
            var key = serviceTermType != null ? $"{serviceID}_{serviceTermType.Value}" : serviceID.ToString();
            return _deliveryUnitsInfoCache.GetItem(key).EnsureCreatedAsync(cancellationToken);
        }

        public IEnumerable<UnitInfo> Search(int serviceID, out DateTime? lastModifiedDate) => Search(serviceID, null, out lastModifiedDate);

        public IEnumerable<UnitInfo> Search(int serviceID, AdmServiceTermType? serviceTermType, out DateTime? lastModifiedDate)
        {
            var key = serviceTermType != null ? $"{serviceID}_{serviceTermType.Value}" : serviceID.ToString();

            var data = _deliveryUnitsInfoCache.GetItem(key).Get();

            lastModifiedDate = data.LastModifiedDate;

            return data.Value;
        }
    }
}