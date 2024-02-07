using EAU.Nomenclatures.Models;
using EAU.Utilities;
using CNSys.Caching;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EAU.Utilities.Caching;
using EAU.Nomenclatures.Repositories;

namespace EAU.Nomenclatures.Cache
{
    /// <summary>
    /// Кеш за езици
    /// </summary>
    public class LanguagesDbCache : DbDataCacheItem<List<Language>>, ILanguagesCache
    {
        private readonly ILanguageRepository _languageRepository;

        public LanguagesDbCache(
            ILogger<LanguagesDbCache> logger, 
            IDbCacheInvalidationDispatcher dbCacheInvalidationDispatcher,
            ILanguageRepository languageRepository) :
            base(logger, 
                dbCacheInvalidationDispatcher, 
                null, 
                new string[] { "[nom].[n_d_languages]" })
        {
            _languageRepository = languageRepository;
        }

        protected override async Task<CachedDataInfo<List<Language>>> GenerateCacheDataInfoAsync(DateTime? etag, CancellationToken cancellationToken)
        {
            var data = await _languageRepository.SearchInfoAsync(new LanguageSearchCriteria(), cancellationToken);

            return new CachedDataInfo<List<Language>>()
            {
                LastModifiedDate = data.LastUpdatedOn,
                Value = data.Data?.ToList()
            };
        }
    }
}
