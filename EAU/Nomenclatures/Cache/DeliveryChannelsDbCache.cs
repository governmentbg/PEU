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
    public class DeliveryChannelsDbCache : DbDataCacheItem<List<DeliveryChannel>>, IDeliveryChannelsCache
    {
        private readonly IDeliveryChannelRepository _deliveryChannelRepository;

        public DeliveryChannelsDbCache(
            ILogger<DeliveryChannelsDbCache> logger,
            IDbCacheInvalidationDispatcher dbCacheInvalidationDispatcher,
            IDeliveryChannelRepository deliveryChannelRepository) :
            base(logger,
                dbCacheInvalidationDispatcher,
                null,
                new string[] { "[nom].[n_s_delivery_channels]" })
        {
            _deliveryChannelRepository = deliveryChannelRepository;
        }

        protected override async Task<CachedDataInfo<List<DeliveryChannel>>> GenerateCacheDataInfoAsync(DateTime? etag, CancellationToken cancellationToken)
        {
            var data = await _deliveryChannelRepository.SearchInfoAsync(new DeliveryChannelSearchCriteria(), cancellationToken);

            return new CachedDataInfo<List<DeliveryChannel>>()
            {
                LastModifiedDate = data.LastUpdatedOn,
                Value = data.Data?.ToList()
            };
        }
    }
}
