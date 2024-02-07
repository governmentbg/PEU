using CNSys.Caching;
using EAU.Nomenclatures.Models;
using EAU.Nomenclatures.Repositories;
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
    public class LabelsDbCache : DataCacheItems<CachedDataInfo<List<Label>>, string>, ILabelsDataCache
    {
        private readonly ILabelRepository _labelRepository;
        private readonly ILanguages _languages;
        private readonly ILogger<LabelsLocalizationDbCache> _logger;
        private readonly IDbCacheInvalidationDispatcher _dbCacheInvalidationDispatcher;
        private readonly string[] _depencyTableNames = new string[] { };

        public LabelsDbCache(
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

        protected override IDataCacheItem<CachedDataInfo<List<Label>>> CreateCacheItem(string key)
        {
            return new DbDataCacheItem<List<Label>>(_logger, _dbCacheInvalidationDispatcher, async (etag, cancellationToken) =>
            {
                await _languages.EnsureLoadedAsync(cancellationToken);

                int? langID = _languages.GetLanguageOrDefault(key).LanguageID;

                var data = (await _labelRepository.SearchInfoAsync(
                    new LabelSearchCriteria()
                    {
                        LanguageID = langID,
                        LoadDecsription = false
                    }, cancellationToken));

                return new CachedDataInfo<List<Label>>()
                {
                    Value = data.Data.ToList(),
                    LastModifiedDate = data.LastUpdatedOn
                };
            }, _depencyTableNames);
        }
    }
}
