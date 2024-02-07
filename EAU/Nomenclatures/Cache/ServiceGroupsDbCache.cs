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
    public class ServiceGroupsDbCache : DataCacheItems<CachedDataInfo<List<ServiceGroup>>, string>, IServiceGroupsCache
    {
        private readonly IServiceGroupRepository _serviceGroupRepository;
        private readonly ILanguages _languages;
        private readonly ILogger<ServiceGroupsDbCache> _logger;
        private readonly IDbCacheInvalidationDispatcher _dbCacheInvalidationDispatcher;
        private readonly string[] _depencyTableNames = new string[] { };

        public ServiceGroupsDbCache(
            ILogger<ServiceGroupsDbCache> logger,
            IDbCacheInvalidationDispatcher dbCacheInvalidationDispatcher,
            IServiceGroupRepository serviceGroupRepository,
            ILanguages languages)
        {
            

            _logger = logger;
            _dbCacheInvalidationDispatcher = dbCacheInvalidationDispatcher;
            _serviceGroupRepository = serviceGroupRepository;
            _languages = languages;
            _depencyTableNames = new string[] { "[nom].[n_d_service_groups]", "[nom].[n_d_service_groups_i18n]" };
        }

        protected override IDataCacheItem<CachedDataInfo<List<ServiceGroup>>> CreateCacheItem(string key)
        {
            return new DbDataCacheItem<List<ServiceGroup>>(_logger, _dbCacheInvalidationDispatcher, async (etag, cancellationToken) =>
            {
                await _languages.EnsureLoadedAsync(cancellationToken);

                int? langID = _languages.GetLanguageOrDefault(key).LanguageID;

                var data = (await _serviceGroupRepository.SearchInfoAsync(
                    new ServiceGroupSearchCriteria()
                    {
                        LanguageID = langID,
                    }, cancellationToken));

                return new CachedDataInfo<List<ServiceGroup>>()
                {
                    Value = data.Data.ToList(),
                    LastModifiedDate = data.LastUpdatedOn
                };
            }, _depencyTableNames);
        }

    }
}
