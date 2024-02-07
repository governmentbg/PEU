using CNSys.Caching;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Nomenclatures.Cache
{
    public interface ILabelLocalizationsDataCache : IDataCacheItems<CachedDataInfo<Dictionary<string, string>>, string>
    {

    }

    public class LabelLocalizations : ILabelLocalizations
    {
        private readonly ILabelLocalizationsDataCache _labelsCache = null;

        public LabelLocalizations(ILabelLocalizationsDataCache labelsDataCache)
        {
            _labelsCache = labelsDataCache;
        }
        public string Get(string lang, string labelCode)
        {
            var cache = _labelsCache.GetItem(lang);

            if (cache.Get().Value.TryGetValue(labelCode, out string value))
                return value;
            else
                return default(string);
        }

        public IDictionary<string, string> Search(string lang, out DateTime? lastModifiedDate)
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
