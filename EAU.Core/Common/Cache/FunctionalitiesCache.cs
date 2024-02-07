using CNSys.Caching;
using EAU.Common.Models;
using EAU.Nomenclatures;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Common.Cache
{
    /// <summary>
    /// Интерфейс за кеш за функционалности на системата.
    /// </summary>
    public interface IFunctionalities : ILoadable
    {
        /// <summary>
        /// Операция за изчитане на функционалности от кеш.
        /// </summary>
        /// <returns>Функционалности.</returns>
        IEnumerable<Functionality> GetFunctionalities();

        /// <summary>
        /// Операция за изчитане на функционалности от кеш.
        /// </summary>
        /// <param name="lastModifiedDate">Дата на последна редакция.</param>
        /// <returns>Функционалности.</returns>
        IEnumerable<Functionality> GetFunctionalities(out DateTime? lastModifiedDate);

        /// <summary>
        /// Операция за изчитане на токен за промяна.
        /// </summary>
        /// <returns>токен за промяна.</returns>
        IChangeToken GetChangeToken();
    }

    /// <summary>
    /// Реализация на кеш за функционалности на системата.
    /// </summary>
    public class Functionalities : IFunctionalities
    {
        private readonly IFunctionalitiesCache _functionalitiesCache;

        public Functionalities(IFunctionalitiesCache functionalitiesCache)
        {
            _functionalitiesCache = functionalitiesCache;
        }

        public IEnumerable<Functionality> GetFunctionalities()
        {
            return _functionalitiesCache.Get().Value;
        }

        public IEnumerable<Functionality> GetFunctionalities(out DateTime? lastModifiedDate)
        {
            var data = _functionalitiesCache.Get();

            lastModifiedDate = data.LastModifiedDate;

            return data.Value;
        }

        public IChangeToken GetChangeToken()
        {
            return _functionalitiesCache.GetChangeToken();
        }

        public ValueTask EnsureLoadedAsync(CancellationToken cancellationToken)
        {
            return _functionalitiesCache.EnsureCreatedAsync(cancellationToken);
        }
    }

    /// <summary>
    /// Интерфейс за кеш за база данни за функционалности на системата.
    /// </summary>
    public interface IFunctionalitiesCache : IDataCacheItem<CachedDataInfo<List<Functionality>>>
    {
    }
}
