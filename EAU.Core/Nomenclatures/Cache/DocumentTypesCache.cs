using CNSys.Caching;
using EAU.Nomenclatures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Nomenclatures.Cache
{
    public interface IDocumentTypesCache : IDataCacheItem<CachedDataInfo<List<DocumentType>>>
    {

    }

    public class DocumentTypes : IDocumentTypes
    {
        private readonly IDocumentTypesCache _documentTypesCache;

        public DocumentTypes(IDocumentTypesCache documentTypesCache)
        {
            _documentTypesCache = documentTypesCache;
        }
        public DocumentType this[int documentTypeID]
        {
            get
            {
                var documentTypes = _documentTypesCache.Get().Value;

                return documentTypes.Where(dt => dt.DocumentTypeID == documentTypeID).Single();
            }
        }

        public ValueTask EnsureLoadedAsync(CancellationToken cancellationToken)
        {
            return _documentTypesCache.EnsureCreatedAsync(cancellationToken);
        }

        public DocumentType GetByDocumentTypeUri(string uri)
        {
            var documentTypes = _documentTypesCache.Get().Value;

            return documentTypes.Where(dt => dt.Uri == uri).Single();
        }

        public IEnumerable<DocumentType> Search()
        {
            return _documentTypesCache.Get().Value;
        }

        public IEnumerable<DocumentType> Search(out DateTime? lastModifiedDate)
        {
            var data = _documentTypesCache.Get();

            lastModifiedDate = data.LastModifiedDate;
            return data.Value;
        }

        public IEnumerable<DocumentType> Search(IList<int> docTypeIDs)
        {
            return Search().Where(dt => docTypeIDs.Contains(dt.DocumentTypeID.Value));
        }
    }
}
