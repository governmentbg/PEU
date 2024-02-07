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
    /// Интерфейс за кеш за параметри на системата.
    /// </summary>
    public interface IAppParameters : ILoadable
    {
        /// <summary>
        /// Операция за изчитане на параметри от кеш.
        /// </summary>
        /// <param name="parameterName">Име на параметър</param>
        /// <returns>Параметри.</returns>
        AppParameter GetParameter(string parameterName);

        /// <summary>
        /// Операция за изчитане на параметри от кеш.
        /// </summary>
        /// <returns>Параметри.</returns>
        IEnumerable<AppParameter> GetAppParameters();

        /// <summary>
        /// Операция за изчитане на параметри от кеш.
        /// </summary>
        /// <param name="lastModifiedDate">Дата на последна редакция.</param>
        /// <returns>Параметри.</returns>
        IEnumerable<AppParameter> GetAppParameters(out DateTime? lastModifiedDate);

        /// <summary>
        /// Операция за изчитане на токен за промяна.
        /// </summary>
        /// <returns>Токен за промяна.</returns>
        IChangeToken GetChangeToken();
    }

    /// <summary>
    /// Реализация на кеш за параметри на системата.
    /// </summary>
    public class AppParameters : IAppParameters
    {
        private readonly IAppParametersCache _appParametersCache;

        public AppParameters(IAppParametersCache appParametersCache)
        {
            _appParametersCache = appParametersCache;
        }

        public AppParameter GetParameter(string parameterName)
        {
            return _appParametersCache.Get().Value[parameterName];
        }

        public IEnumerable<AppParameter> GetAppParameters()
        {
            return _appParametersCache.Get().Value.Values;
        }

        public IEnumerable<AppParameter> GetAppParameters(out DateTime? lastModifiedDate)
        {
            var data = _appParametersCache.Get();

            lastModifiedDate = data.LastModifiedDate;

            return data.Value.Values;
        }

        public IChangeToken GetChangeToken()
        {
            return _appParametersCache.GetChangeToken();
        }

        public ValueTask EnsureLoadedAsync(CancellationToken cancellationToken)
        {
            return _appParametersCache.EnsureCreatedAsync(cancellationToken);
        }
    }

    /// <summary>
    /// Интерфейс за кеш за база данни за параметри на системата.
    /// </summary>
    public interface IAppParametersCache : IDataCacheItem<CachedDataInfo<Dictionary<string, AppParameter>>>
    {
    }
}
