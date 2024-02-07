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
    public class GraoDbCache : DbDataCacheItem<List<Grao>>, IGraoCache
    {
        private readonly IGraoRepository _graoRepository;

        public GraoDbCache(
            ILogger<GraoDbCache> logger,
            IDbCacheInvalidationDispatcher dbCacheInvalidationDispatcher,
            IGraoRepository graoRepository) :
            base(logger,
                dbCacheInvalidationDispatcher,
                null,
                new string[] { "[nom].[n_s_grao]" })
        {
            _graoRepository = graoRepository;
        }

        protected override async Task<CachedDataInfo<List<Grao>>> GenerateCacheDataInfoAsync(DateTime? etag, CancellationToken cancellationToken)
        {
            var data = await _graoRepository.SearchInfoAsync(new GraoSearchCriteria(), cancellationToken);

            return new CachedDataInfo<List<Grao>>()
            {
                LastModifiedDate = data.LastUpdatedOn,
                Value = data.Data?.ToList()
            };
        }
    }
}
