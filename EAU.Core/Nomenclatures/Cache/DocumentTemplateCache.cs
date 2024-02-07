using CNSys.Caching;
using EAU.Nomenclatures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Nomenclatures.Cache
{
    public interface IDocumentTemplatesCache : IDataCacheItem<CachedDataInfo<List<DocumentTemplate>>>
    {

    }

    public class DocumentTemplates : IDocumentTemplates
    {
        private readonly IDocumentTemplatesCache _documentTemplatesCache;

        public DocumentTemplates(IDocumentTemplatesCache documentTemplatesCache)
        {
            _documentTemplatesCache = documentTemplatesCache;
        }

        public ValueTask EnsureLoadedAsync(CancellationToken cancellationToken)
        {
            return _documentTemplatesCache.EnsureCreatedAsync(cancellationToken);
        }

        public IEnumerable<DocumentTemplate> Search()
        {
            return _documentTemplatesCache.Get().Value;
        }

        public IEnumerable<DocumentTemplate> Search(out DateTime? lastModifiedDate)
        {
            var data = _documentTemplatesCache.Get();

            lastModifiedDate = data.LastModifiedDate;
            return data.Value;
        }

        public DocumentTemplate Search(int documentTypeID)
        {
            return Search().Where(x => x.DocumentTypeID == documentTypeID).SingleOrDefault();
        }
    }
}
