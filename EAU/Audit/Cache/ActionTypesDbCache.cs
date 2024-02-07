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
    /// Кеш за видове действия.
    /// </summary>
    public class ActionTypesDbCache : DbDataCacheItem<List<ActionType>>, IActionTypesCache
    {
        private readonly IActionTypeRepository _actionTypeRepository;

        public ActionTypesDbCache(ILogger<ActionTypesDbCache> logger, IDbCacheInvalidationDispatcher dbCacheInvalidationDispatcher, IActionTypeRepository actionTypeRepository) :
            base(logger, dbCacheInvalidationDispatcher, null, new string[] { "[audit].[n_s_action_types]" })
        {
            _actionTypeRepository = actionTypeRepository;
        }

        protected override async Task<CachedDataInfo<List<ActionType>>> GenerateCacheDataInfoAsync(DateTime? etag, CancellationToken cancellationToken)
        {
            CollectionInfo<ActionType> data = await _actionTypeRepository.SearchInfoAsync(new ActionTypeSearchCriteria(), cancellationToken);

            return new CachedDataInfo<List<ActionType>>() { Value = data.Data.ToList(), LastModifiedDate = data.LastUpdatedOn };
        }
    }
}
