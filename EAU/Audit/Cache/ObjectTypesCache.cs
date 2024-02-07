using CNSys.Caching;
using EAU.Audit.Models;
using EAU.Nomenclatures;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Audit.Cache
{
    /// <summary>
    /// Интерфейс за работа с кеш на видове обекти.
    /// </summary>
    public interface IObjectTypes : ILoadable
    {
        /// <summary>
        /// Операция за изчитане на видове обекти от кеш.
        /// </summary>
        /// <returns>Видове обекти.</returns>
        IEnumerable<ObjectType> GetObjectTypes();

        /// <summary>
        /// Операция за изчитане на видове обекти от кеш.
        /// </summary>
        /// <param name="lastModifiedDate">Дата на последна редакция.</param>
        /// <returns>Видове обекти.</returns>
        /// 
        IEnumerable<ObjectType> GetObjectTypes(out DateTime? lastModifiedDate);

        /// <summary>
        /// Операция за изчитане на токен за промяна.
        /// </summary>
        /// <returns>токен за промяна.</returns>
        IChangeToken GetChangeToken();
    }

    /// <summary>
    /// Реализация на интерфейс за работа с кеш на видове обекти.
    /// </summary>
    public class ObjectTypes : IObjectTypes
    {
        private readonly IObjectTypesCache _objectTypesCache;

        public ObjectTypes(IObjectTypesCache objectTypesCache)
        {
            _objectTypesCache = objectTypesCache;
        }

        public IEnumerable<ObjectType> GetObjectTypes()
        {
            return _objectTypesCache.Get().Value;
        }

        public IEnumerable<ObjectType> GetObjectTypes(out DateTime? lastModifiedDate)
        {
            var data = _objectTypesCache.Get();

            lastModifiedDate = data.LastModifiedDate;

            return data.Value;
        }

        public IChangeToken GetChangeToken()
        {
            return _objectTypesCache.GetChangeToken();
        }

        public ValueTask EnsureLoadedAsync(CancellationToken cancellationToken)
        {
            return _objectTypesCache.EnsureCreatedAsync(cancellationToken);
        }
    }

    /// <summary>
    /// Интерфейс за работа с кеш на базата данни на видове обекти.
    /// </summary>
    public interface IObjectTypesCache : IDataCacheItem<CachedDataInfo<List<ObjectType>>>
    {
    }
}
