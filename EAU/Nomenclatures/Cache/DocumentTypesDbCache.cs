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
    public class DocumentTypesDbCache : DbDataCacheItem<List<DocumentType>>, IDocumentTypesCache
    {
        private readonly IDocumentTypeRepository _documentTypeRepository;

        public DocumentTypesDbCache(
            ILogger<DocumentTypesDbCache> logger,
            IDbCacheInvalidationDispatcher dbCacheInvalidationDispatcher,
            IDocumentTypeRepository documentTypeRepository) :
            base(logger,
                dbCacheInvalidationDispatcher,
                null,
                new string[] { "[nom].[n_s_document_types]" })
        {
            _documentTypeRepository = documentTypeRepository;
        }

        protected override async Task<CachedDataInfo<List<DocumentType>>> GenerateCacheDataInfoAsync(DateTime? etag, CancellationToken cancellationToken)
        {
            var data = await _documentTypeRepository.SearchInfoAsync(new DocumentTypeSearchCriteria(), cancellationToken);

            return new CachedDataInfo<List<DocumentType>>()
            {
                LastModifiedDate = data.LastUpdatedOn,
                Value = data.Data?.ToList()
            };
        }
    }
}
