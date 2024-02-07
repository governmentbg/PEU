using CNSys.Caching;
using EAU.Nomenclatures.Models;
using EAU.Nomenclatures.Repositories;
using EAU.Nomenclatures.Services;
using EAU.Utilities.Caching;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Nomenclatures.Cache
{
    public class ServicesDbCache : DataCacheItems<CachedDataInfo<List<Service>>, string>, IServicesCache
    {
        private readonly IServiceService _serviceService;
        private readonly ILanguages _languages;
        private readonly ILogger<ServicesDbCache> _logger;
        private readonly IDbCacheInvalidationDispatcher _dbCacheInvalidationDispatcher;
        private readonly string[] _depencyTableNames = new string[] { };

        public ServicesDbCache(
            ILogger<ServicesDbCache> logger,
            IDbCacheInvalidationDispatcher dbCacheInvalidationDispatcher,
            IServiceService serviceService,
            ILanguages languages)
        {
            _logger = logger;
            _dbCacheInvalidationDispatcher = dbCacheInvalidationDispatcher;
            _serviceService = serviceService;
            _languages = languages;
            _depencyTableNames = new string[]
            {
                "[nom].[n_d_services]",
                "[nom].[n_d_services_i18n]",
                "[nom].[n_d_service_declarations]",
                "[nom].[n_d_service_delivery_channels]",
                "[nom].[n_d_service_document_types]",
                "[nom].[n_d_service_terms]",
                "[nom].[n_d_declarations]",
                "[nom].[n_s_delivery_channels]",
                "[nom].[n_s_document_types]",
            };
        }

        protected override IDataCacheItem<CachedDataInfo<List<Service>>> CreateCacheItem(string key)
        {
            return new DbDataCacheItem<List<Service>>(_logger, _dbCacheInvalidationDispatcher, async (etag, cancellationToken) =>
            {
                await _languages.EnsureLoadedAsync(cancellationToken);

                int? langID = _languages.GetLanguageOrDefault(key).LanguageID;

                var data = (await _serviceService.SearchInfoAsync(
                    new ServiceSearchCriteria()
                    {
                        LanguageID = langID,
                        LoadDecsription = false
                    }, cancellationToken));

                return new CachedDataInfo<List<Service>>()
                {
                    Value = data.Data.ToList(),
                    LastModifiedDate = data.LastUpdatedOn
                };
            }, _depencyTableNames);
        }
    }
}
