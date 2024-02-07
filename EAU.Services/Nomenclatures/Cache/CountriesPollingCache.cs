using CNSys.Caching;
using EAU.Services.Nomenclatures.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WAIS.Integration.MOI.Nomenclatures;
using System.Linq;

namespace EAU.Services.Nomenclatures.Cache
{
    public class CountriesPollingCache : PollingDataCacheItem<List<Nomenclature>>, ICountriesCache
    {
        private readonly INomenclaturesServicesClientFactory _servicesClientFactory;

        public CountriesPollingCache(
            INomenclaturesServicesClientFactory servicesClientFactory,
            IPollingCacheInfrastructure pollingCacheInfrastructure,
            ILogger<CountriesPollingCache> logger)
            : base(logger, pollingCacheInfrastructure, null)
        {
            _servicesClientFactory = servicesClientFactory;
        }

        protected async override Task<CachedDataInfo<List<Nomenclature>>> GenerateCacheDataInfoAsync(DateTime? etag, CancellationToken cancellationToken)
        {
            var result = new List<Nomenclature>();

            var noms = await _servicesClientFactory.GetNomenclatureServicesClient().SearchCountries(CancellationToken.None);

            if (noms != null)
            {
                result.AddRange((from nom in noms
                                 select new Nomenclature()
                                 {
                                     Code = nom.Code,
                                     Name = nom.Name
                                 }).ToList());
            }

            return new CachedDataInfo<List<Nomenclature>>()
            {
                Value = result,
                LastModifiedDate = DateTime.Now
            };
        }
    }
}
