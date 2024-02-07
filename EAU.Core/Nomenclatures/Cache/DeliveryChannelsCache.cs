using CNSys.Caching;
using EAU.Nomenclatures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Nomenclatures.Cache
{
    public interface IDeliveryChannelsCache : IDataCacheItem<CachedDataInfo<List<DeliveryChannel>>>
    {

    }

    public class DeliveryChannels : IDeliveryChannels
    {
        private readonly IDeliveryChannelsCache _deliveryChannelsCache;


        public DeliveryChannels(IDeliveryChannelsCache deliveryChannelsCache)
        {
            _deliveryChannelsCache = deliveryChannelsCache;
        }

        public ValueTask EnsureLoadedAsync(CancellationToken cancellationToken)
        {
            return _deliveryChannelsCache.EnsureCreatedAsync(cancellationToken);
        }

        public IEnumerable<DeliveryChannel> Search()
        {
            return _deliveryChannelsCache.Get().Value;
        }

        public IEnumerable<DeliveryChannel> Search(out DateTime? lastModifiedDate)
        {
            var data = _deliveryChannelsCache.Get();

            lastModifiedDate = data.LastModifiedDate;
            return data.Value;
        }

        public IEnumerable<DeliveryChannel> Search(IList<short> deliveryChannelIDs)
        {
            return Search().Where(dc => deliveryChannelIDs.Contains(dc.DeliveryChannelID.Value));
        }
    }
}
