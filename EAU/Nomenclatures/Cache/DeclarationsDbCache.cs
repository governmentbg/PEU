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
    public class DeclarationsDbCache : DbDataCacheItem<List<Declaration>>, IDeclarationsCache
    {
        private readonly IDeclarationRepository _declarationRepository;

        public DeclarationsDbCache(
            ILogger<DeclarationsDbCache> logger,
            IDbCacheInvalidationDispatcher dbCacheInvalidationDispatcher,
            IDeclarationRepository declarationRepository) :
            base(logger,
                dbCacheInvalidationDispatcher,
                null,
                new string[] { "[nom].[n_d_declarations]" })
        {
            _declarationRepository = declarationRepository;
        }

        protected override async Task<CachedDataInfo<List<Declaration>>> GenerateCacheDataInfoAsync(DateTime? etag, CancellationToken cancellationToken)
        {
            var data = await _declarationRepository.SearchInfoAsync(new DeclarationSearchCriteria(), cancellationToken);

            return new CachedDataInfo<List<Declaration>>()
            {
                LastModifiedDate = data.LastUpdatedOn,
                Value = data.Data?.ToList()
            };
        }
    }
}
