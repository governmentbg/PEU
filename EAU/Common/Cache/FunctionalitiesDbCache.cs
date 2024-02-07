using EAU.Common.Models;
using EAU.Common.Repositories;
using EAU.Nomenclatures.Repositories;
using EAU.Utilities;
using CNSys.Caching;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EAU.Utilities.Caching;

namespace EAU.Common.Cache
{
    /// <summary>
    /// Кеш за функционалности на системата.
    /// </summary>
    public class FunctionalitiesDbCache : DbDataCacheItem<List<Functionality>>, IFunctionalitiesCache
    {
        private readonly IFunctionalityRepository _functionalityRepository;

        public FunctionalitiesDbCache(ILogger<FunctionalitiesDbCache> logger, IDbCacheInvalidationDispatcher dbCacheInvalidationDispatcher, IFunctionalityRepository functionalityRepository) :
            base(logger, dbCacheInvalidationDispatcher, null, new string[] { "[dbo].[n_s_functionalities]" })
        {
            _functionalityRepository = functionalityRepository;
        }

        protected override async Task<CachedDataInfo<List<Functionality>>> GenerateCacheDataInfoAsync(DateTime? etag, CancellationToken cancellationToken)
        {
            CollectionInfo<Functionality> data = await _functionalityRepository.SearchInfoAsync(new FunctionalitySearchCriteria(), cancellationToken);

            return new CachedDataInfo<List<Functionality>>() { Value = data.Data.ToList(), LastModifiedDate = data.LastUpdatedOn };
        }
    }
}
