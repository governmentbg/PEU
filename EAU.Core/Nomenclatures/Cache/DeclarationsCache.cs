using CNSys.Caching;
using EAU.Nomenclatures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Nomenclatures.Cache
{
    public interface IDeclarationsCache : IDataCacheItem<CachedDataInfo<List<Declaration>>>
    {

    }

    public class Declarations : IDeclarations
    {
        private readonly IDeclarationsCache _declarationsCache;

        public Declarations(IDeclarationsCache declarationsCache)
        {
            _declarationsCache = declarationsCache;
        }

        public ValueTask EnsureLoadedAsync(CancellationToken cancellationToken)
        {
            return _declarationsCache.EnsureCreatedAsync(cancellationToken);
        }

        public IEnumerable<Declaration> Search()
        {
            return _declarationsCache.Get().Value;
        }

        public IEnumerable<Declaration> Search(out DateTime? lastModifiedDate)
        {
            var data = _declarationsCache.Get();

            lastModifiedDate = data.LastModifiedDate;
            return data.Value;
        }

        public IEnumerable<Declaration> Search(IList<int> declarationIDs)
        {
            return Search().Where(d => declarationIDs.Contains(d.DeclarationID.Value));
        }
    }
}
