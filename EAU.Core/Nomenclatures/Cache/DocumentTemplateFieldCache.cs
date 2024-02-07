using CNSys.Caching;
using EAU.Nomenclatures.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Nomenclatures.Cache
{
    public interface IDocumentTemplateFieldsCache : IDataCacheItem<CachedDataInfo<List<DocumentTemplateField>>>
    {

    }

    public class DocumentTemplateFields : IDocumentTemplateFields
    {
        private readonly IDocumentTemplateFieldsCache _documentTemplateFieldsCache;

        public DocumentTemplateFields(IDocumentTemplateFieldsCache documentTemplateFieldsCache)
        {
            _documentTemplateFieldsCache = documentTemplateFieldsCache;
        }

        public ValueTask EnsureLoadedAsync(CancellationToken cancellationToken)
        {
            return _documentTemplateFieldsCache.EnsureCreatedAsync(cancellationToken);
        }

        public IEnumerable<DocumentTemplateField> Search()
        {
            return _documentTemplateFieldsCache.Get().Value;
        }

        public IEnumerable<DocumentTemplateField> Search(out DateTime? lastModifiedDate)
        {
            var data = _documentTemplateFieldsCache.Get();

            lastModifiedDate = data.LastModifiedDate;
            return data.Value;
        }
    }
}
