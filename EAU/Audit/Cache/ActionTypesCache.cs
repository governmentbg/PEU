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
    /// Интерфейс за работа с кеш на видове действия.
    /// </summary>
    public interface IActionTypes : ILoadable
    {
        /// <summary>
        /// Операция за изчитане на видове действия от кеш.
        /// </summary>
        /// <returns>Видове действия.</returns>
        IEnumerable<ActionType> GetActionTypes();

        /// <summary>
        /// Операция за изчитане на видове действия от кеш.
        /// </summary>
        /// <param name="lastModifiedDate">Дата на последна редакция.</param>
        /// <returns>Видове действия.</returns>
        IEnumerable<ActionType> GetActionTypes(out DateTime? lastModifiedDate);

        /// <summary>
        /// Операция за изчитане на токен за промяна.
        /// </summary>
        /// <returns>токен за промяна.</returns>
        IChangeToken GetChangeToken();
    }

    /// <summary>
    /// Интерфейс за работа с кеш на видове действия.
    /// </summary>
    public class ActionTypes : IActionTypes
    {
        private readonly IActionTypesCache _actionTypesCache;

        public ActionTypes(IActionTypesCache actionTypesCache)
        {
            _actionTypesCache = actionTypesCache;
        }

        public IEnumerable<ActionType> GetActionTypes()
        {
            return _actionTypesCache.Get().Value;
        }

        public IEnumerable<ActionType> GetActionTypes(out DateTime? lastModifiedDate)
        {
            var data = _actionTypesCache.Get();

            lastModifiedDate = data.LastModifiedDate;

            return data.Value;
        }

        public IChangeToken GetChangeToken()
        {
            return _actionTypesCache.GetChangeToken();
        }

        public ValueTask EnsureLoadedAsync(CancellationToken cancellationToken)
        {
            return _actionTypesCache.EnsureCreatedAsync(cancellationToken);
        }
    }

    /// <summary>
    /// Интерфейс за работа с кеш на видове действия.
    /// </summary>
    public interface IActionTypesCache : IDataCacheItem<CachedDataInfo<List<ActionType>>>
    {
    }
}
