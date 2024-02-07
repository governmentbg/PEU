using CNSys.Caching;
using EAU.Nomenclatures.Repositories;
using EAU.Payments.RegistrationsData.Models;
using EAU.Payments.RegistrationsData.Repositories;
using EAU.Utilities.Caching;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Payments.RegistrationsData.Cache
{
    public class RegistrationsDataDbCache : DbDataCacheItem<List<RegistrationData>>, IRegistrationsDataCache
    {
        private readonly IRegistrationDataRepository _eAURegistrationDataRepository;

        public RegistrationsDataDbCache(
            ILogger<RegistrationsDataDbCache> logger,
            IDbCacheInvalidationDispatcher dbCacheInvalidationDispatcher,
            IRegistrationDataRepository eAURegistrationDataRepository) : base(logger, dbCacheInvalidationDispatcher, null, new string[] { "[pmt].[n_d_registration_data]" })
        {


            _eAURegistrationDataRepository = eAURegistrationDataRepository;
        }


        protected override async Task<CachedDataInfo<List<RegistrationData>>> GenerateCacheDataInfoAsync(DateTime? etag, CancellationToken cancellationToken)
        {
            CollectionInfo<RegistrationData> data = await _eAURegistrationDataRepository.SearchInfoAsync(new RegistrationDataSearchCriteria(), cancellationToken);

            return new CachedDataInfo<List<RegistrationData>>() { Value = data.Data.ToList(), LastModifiedDate = data.LastUpdatedOn };
        }
    }

    /// <summary>
    /// Реализация на интерфейс за работа с кеш на регистрационни данни на ПЕАУ в системата на платежен оператор ePay / ПЕП на ДАЕУ.
    /// </summary>
    public class RegistrationsData : IRegistrationsData
    {
        private readonly IRegistrationsDataCache _registrationsDataCache;

        public RegistrationsData(IRegistrationsDataCache registrationsDataCache)
        {
            _registrationsDataCache = registrationsDataCache;
        }

        public IEnumerable<RegistrationData> GetRegistrationData()
        {
            return _registrationsDataCache.Get().Value;
        }

        public IEnumerable<RegistrationData> GetRegistrationData(out DateTime? lastModifiedDate)
        {
            var data = _registrationsDataCache.Get();

            lastModifiedDate = data.LastModifiedDate;

            return data.Value;
        }

        public IChangeToken GetChangeToken()
        {
            return _registrationsDataCache.GetChangeToken();
        }

        public ValueTask EnsureLoadedAsync(CancellationToken cancellationToken)
        {
            return _registrationsDataCache.EnsureCreatedAsync(cancellationToken);
        }
    }
}
