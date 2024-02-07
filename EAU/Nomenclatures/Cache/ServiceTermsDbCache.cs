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
    public class ServiceTermsDbCache : DbDataCacheItem<List<ServiceTerm>>, IServiceTermsCache
    {
        private readonly IServiceTermRepository _serviceTermRepository;

        public ServiceTermsDbCache(
            ILogger<ServiceTermsDbCache> logger,
            IDbCacheInvalidationDispatcher dbCacheInvalidationDispatcher,
            IServiceTermRepository serviceTermRepository) :
            base(logger,
                dbCacheInvalidationDispatcher,
                null,
                new string[] { "[nom].[n_d_service_terms]" })
        {
            _serviceTermRepository = serviceTermRepository;
        }

        protected override async Task<CachedDataInfo<List<ServiceTerm>>> GenerateCacheDataInfoAsync(DateTime? etag, CancellationToken cancellationToken)
        {
            var data = await _serviceTermRepository.SearchInfoAsync(new ServiceTermSearchCriteria(), cancellationToken);

            return new CachedDataInfo<List<ServiceTerm>>()
            {
                LastModifiedDate = data.LastUpdatedOn,
                Value = data.Data?.ToList()
            };
        }
    }
}
