using CNSys.Caching;
using EAU.CMS.Models;
using EAU.CMS.Repositories;
using EAU.Nomenclatures;
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

namespace EAU.CMS.Cache
{
    public class PagesDbCache : DataCacheItems<CachedDataInfo<List<Page>>, string>, IPagesCache
    {
        private readonly IPageRepository _pageRepository;
        private readonly ILanguages _languages;
        private readonly ILogger<PagesDbCache> _logger;
        private readonly IDbCacheInvalidationDispatcher _dbCacheInvalidationDispatcher;
        private readonly string[] _depencyTableNames = new string[] { };

        public PagesDbCache(
            ILogger<PagesDbCache> logger,
            IDbCacheInvalidationDispatcher dbCacheInvalidationDispatcher,
            IPageRepository pageRepository,
            ILanguages languages)
        {
            

            _logger = logger;
            _dbCacheInvalidationDispatcher = dbCacheInvalidationDispatcher;
            _pageRepository = pageRepository;
            _languages = languages;
            _depencyTableNames = new string[] { "[cms].[pages]", "[cms].[pages_i18n]" };
        }

        protected override IDataCacheItem<CachedDataInfo<List<Page>>> CreateCacheItem(string key)
        {
            return new DbDataCacheItem<List<Page>>(_logger, _dbCacheInvalidationDispatcher, async (etag, cancellationToken) =>
            {
                await _languages.EnsureLoadedAsync(cancellationToken);

                int? langID = _languages.GetLanguageOrDefault(key).LanguageID;

                var data = (await _pageRepository.SearchInfoAsync(
                    new PageSearchCriteria()
                    {
                        LanguageID = langID,
                    }, cancellationToken));

                return new CachedDataInfo<List<Page>>()
                {
                    Value = data.Data.ToList(),
                    LastModifiedDate = data.LastUpdatedOn
                };
            }, _depencyTableNames);
        }

    }


}
