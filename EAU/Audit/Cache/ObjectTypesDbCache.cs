using EAU.Audit.Models;
using EAU.Audit.Repositories;
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

namespace EAU.Audit.Cache
{
    /// <summary>
    /// Кеш за видове обекти.
    /// </summary>
    public class ObjectTypesDbCache : DbDataCacheItem<List<ObjectType>>, IObjectTypesCache
    {
        private readonly IObjectTypeRepository _objectTypeRepository;

        public ObjectTypesDbCache(ILogger<ObjectTypesDbCache> logger, IDbCacheInvalidationDispatcher dbCacheInvalidationDispatcher, IObjectTypeRepository objectTypeRepository) :
            base(logger, dbCacheInvalidationDispatcher, null, new string[] { "[audit].[n_s_object_types]" })
        {
            _objectTypeRepository = objectTypeRepository;
        }

        protected override async Task<CachedDataInfo<List<ObjectType>>> GenerateCacheDataInfoAsync(DateTime? etag, CancellationToken cancellationToken)
        {
            CollectionInfo<ObjectType> data = await _objectTypeRepository.SearchInfoAsync(new ObjectTypeSearchCriteria(), cancellationToken);

            return new CachedDataInfo<List<ObjectType>>() { Value = data.Data.ToList(), LastModifiedDate = data.LastUpdatedOn };
        }
    }
}
