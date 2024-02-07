using CNSys.Caching;
using EAU.Nomenclatures.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Nomenclatures.Cache
{


    public interface ILabelsDataCache : IDataCacheItems<CachedDataInfo<List<Label>>, string>
    {

    }

    public class Labels : ILabels
    {
        private readonly ILabelsDataCache _labelsCache = null;

        public Labels(ILabelsDataCache labelsDataCache)
        {
            _labelsCache = labelsDataCache;
        }

        public IEnumerable<Label> Search(string lang, out DateTime? lastModifiedDate)
        {
            var data = _labelsCache.GetItem(lang).Get();

            lastModifiedDate = data.LastModifiedDate;

            return data.Value;
        }

        public ValueTask EnsureLoadedAsync(CancellationToken cancellationToken)
        {
            return EnsureLoadedAsync("bg", cancellationToken);
        }

        public ValueTask EnsureLoadedAsync(string lang, CancellationToken cancellationToken)
        {
            return _labelsCache.GetItem(lang).EnsureCreatedAsync(CancellationToken.None);
        }
    }
}
