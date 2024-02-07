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
    public class DocumentTemplatesDbCache : DbDataCacheItem<List<DocumentTemplate>>, IDocumentTemplatesCache
    {
        private readonly IDocumentTemplateRepository _documentTemplateRepository;

        public DocumentTemplatesDbCache(
            ILogger<DocumentTemplatesDbCache> logger,
            IDbCacheInvalidationDispatcher dbCacheInvalidationDispatcher,
            IDocumentTemplateRepository documentTemplateRepository) :
            base(logger,
                dbCacheInvalidationDispatcher,
                null,
                new string[] { "[nom].[n_d_document_templates]" })
        {
            _documentTemplateRepository = documentTemplateRepository;
        }

        protected override async Task<CachedDataInfo<List<DocumentTemplate>>> GenerateCacheDataInfoAsync(DateTime? etag, CancellationToken cancellationToken)
        {
            var data = await _documentTemplateRepository.SearchInfoAsync(new DocumentTemplateSearchCriteria(), cancellationToken);

            return new CachedDataInfo<List<DocumentTemplate>>()
            {
                LastModifiedDate = data.LastUpdatedOn,
                Value = data.Data?.ToList()
            };
        }
    }
}
