using CNSys.Caching;
using EAU.Nomenclatures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Nomenclatures.Cache
{
    public interface ILanguagesCache : IDataCacheItem<CachedDataInfo<List<Language>>>
    {

    }

    public class Languages : ILanguages
    {
        private readonly ILanguagesCache _languagesCache;

        public Languages(ILanguagesCache languagesCache)
        {
            _languagesCache = languagesCache;
        }

        public ValueTask EnsureLoadedAsync(CancellationToken cancellationToken)
        {
            return _languagesCache.EnsureCreatedAsync(cancellationToken);
        }

        public Language GetDefault()
        {
            return _languagesCache.Get().Value.FirstOrDefault(lang => { return lang.IsDefault.GetValueOrDefault(); });
        }

        public Language Get(string code)
        {
            var languages = _languagesCache.Get();

            return languages.Value.FirstOrDefault(lang => { return string.Compare(lang.Code, code, true) == 0; });
        }

        public IEnumerable<Language> Search()
        {
            return _languagesCache.Get().Value;
        }

        public IEnumerable<Language> Search(out DateTime? lastModifiedDate)
        {
            var data = _languagesCache.Get();

            lastModifiedDate = data.LastModifiedDate;

            return data.Value;
        }

        public Language GetLanguageOrDefault(string code)
        {
            
            var language = Get(code);

            if (language == null)
                language = GetDefault();

            return language;
        }

        public int GetLanguageID(string code)
        {
            return Get(code).LanguageID.Value;
        }
    }
}
