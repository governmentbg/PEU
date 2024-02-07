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
    public class DocumentTemplateFieldsDbCache : DbDataCacheItem<List<DocumentTemplateField>>, IDocumentTemplateFieldsCache
    {
        private readonly IDocumentTemplateFieldRepository _documentTemplateFieldRepository;

        public DocumentTemplateFieldsDbCache(
            ILogger<DocumentTemplateFieldsDbCache> logger,
            IDbCacheInvalidationDispatcher dbCacheInvalidationDispatcher,
            IDocumentTemplateFieldRepository documentTemplateFieldRepository) :
            base(logger,
                dbCacheInvalidationDispatcher,
                null,
                new string[] { "[nom].[n_s_document_template_fields]" })
        {
            _documentTemplateFieldRepository = documentTemplateFieldRepository;
        }

        protected override async Task<CachedDataInfo<List<DocumentTemplateField>>> GenerateCacheDataInfoAsync(DateTime? etag, CancellationToken cancellationToken)
        {
            var data = await _documentTemplateFieldRepository.SearchInfoAsync(new DocumentTemplateFieldSearchCriteria(), cancellationToken);

            return new CachedDataInfo<List<DocumentTemplateField>>()
            {
                LastModifiedDate = data.LastUpdatedOn,
                Value = data.Data?.ToList()
            };
        }
    }
}
