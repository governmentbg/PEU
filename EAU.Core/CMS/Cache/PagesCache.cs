using CNSys.Caching;
using EAU.CMS.Models;
using EAU.Nomenclatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.CMS.Cache
{
    /// <summary>
    /// Интерфейс за работа с кеш на номеклатура на групи услуги по направление на дейност в МВР
    /// </summary>
    public interface IPages : ILoadable
    {
        Page Get(string lang, int id);
        IEnumerable<Page> Search(string lang);
        IEnumerable<Page> Search(string lang, out DateTime? lastModifiedDate);
        ValueTask EnsureLoadedAsync(string lang, CancellationToken cancellationToken);
    }

    public interface IPagesCache : IDataCacheItems<CachedDataInfo<List<Page>>, string>
    {

    }

    public class Pages : IPages
    {
        private readonly IPagesCache _pagesCache;

        public Pages(IPagesCache pagesCache)
        {
            _pagesCache = pagesCache;
        }

        public ValueTask EnsureLoadedAsync(CancellationToken cancellationToken)
        {
            return EnsureLoadedAsync("bg", cancellationToken);
        }

        public ValueTask EnsureLoadedAsync(string lang, CancellationToken cancellationToken)
        {
            return _pagesCache.GetItem(lang).EnsureCreatedAsync(cancellationToken);
        }

        public Page Get(string lang, int id)
        {
            return Search(lang).Single(x => x.PageID == id);
        }

        public IEnumerable<Page> Search(string lang)
        {
            return _pagesCache.GetItem(lang).Get().Value;
        }

        public IEnumerable<Page> Search(string lang, out DateTime? lastModifiedDate)
        {
            var data = _pagesCache.GetItem(lang).Get();

            lastModifiedDate = data.LastModifiedDate;
            return data.Value;
        }
    }
}
