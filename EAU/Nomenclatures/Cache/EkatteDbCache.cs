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
    public class EkatteDbCache : DbDataCacheItem<List<Ekatte>>, IEkatteCache
    {
        private readonly IEkatteRepository _ekatteRepository;

        public EkatteDbCache(
            ILogger<EkatteDbCache> logger,
            IDbCacheInvalidationDispatcher dbCacheInvalidationDispatcher,
            IEkatteRepository ekatteRepository) :
            base(logger,
                dbCacheInvalidationDispatcher,
                null,
                new string[] { "[nom].[n_s_ekatte]" })
        {
            _ekatteRepository = ekatteRepository;
        }

        protected override async Task<CachedDataInfo<List<Ekatte>>> GenerateCacheDataInfoAsync(DateTime? etag, CancellationToken cancellationToken)
        {
            var data = await _ekatteRepository.SearchInfoAsync(new EkatteSearchCriteria(), cancellationToken);

            return new CachedDataInfo<List<Ekatte>>()
            {
                LastModifiedDate = data.LastUpdatedOn,
                Value = data.Data?.ToList()
            };
        }
    }
}
