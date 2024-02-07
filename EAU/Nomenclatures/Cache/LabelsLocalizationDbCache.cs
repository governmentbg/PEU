using CNSys.Caching;
using EAU.Nomenclatures.Models;
using EAU.Nomenclatures.Repositories;
using EAU.Utilities;
using EAU.Utilities.Caching;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EAU.Nomenclatures.Cache
{
    /// <summary>
    /// Кеш за етикети
    /// </summary>
    public class LabelsLocalizationDbCache : DataCacheItems<CachedDataInfo<Dictionary<string, string>>, string>, ILabelLocalizationsDataCache
    {
        private readonly ILabelRepository _labelRepository;
        private readonly ILanguages _languages;
        private readonly ILogger<LabelsLocalizationDbCache> _logger;
        private readonly IDbCacheInvalidationDispatcher _dbCacheInvalidationDispatcher;
        private readonly string[] _depencyTableNames = new string[] { };

        public LabelsLocalizationDbCache(
            ILabelRepository labelRepository, 
            ILanguages languages, 
            ILogger<LabelsLocalizationDbCache> logger, 
            IDbCacheInvalidationDispatcher dbCacheInvalidationDispatcher)
        {
            _labelRepository = labelRepository;
            _languages = languages;
            _logger = logger;
            _dbCacheInvalidationDispatcher = dbCacheInvalidationDispatcher;
            _depencyTableNames = new string[] { "[nom].[n_d_labels]", "[nom].[n_d_labels_i18n]" };
        }

        protected override IDataCacheItem<CachedDataInfo<Dictionary<string, string>>> CreateCacheItem(string key)
        {
            return new DbDataCacheItem<Dictionary<string, string>>(_logger, _dbCacheInvalidationDispatcher, async (etag, cancellationToken) =>
            {
                await _languages.EnsureLoadedAsync(cancellationToken);

                int? langID = _languages.GetLanguageOrDefault(key).LanguageID;

                var data = (await _labelRepository.SearchInfoAsync(
                    new LabelSearchCriteria() 
                    { 
                        LanguageID = langID, 
                        LoadDecsription = false 
                    }, cancellationToken));

                return new CachedDataInfo<Dictionary<string, string>>()
                {
                    Value = data.Data.ToDictionary((item) => { return item.Code; }, (item) => { return item.Value; }, StringComparer.InvariantCultureIgnoreCase),
                    LastModifiedDate = data.LastUpdatedOn
                };
            }, _depencyTableNames);
        }
    }
}
