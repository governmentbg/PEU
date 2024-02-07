using CNSys.Caching;
using EAU.Services.Nomenclatures.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WAIS.Integration.MOI.Nomenclatures;

namespace EAU.Services.Nomenclatures.Cache
{
    public class VehicleBaseColorsPollingCache : PollingDataCacheItem<List<Nomenclature>>, IVehicleBaseColorsCache
    {
        private readonly INomenclaturesServicesClientFactory _servicesClientFactory;

        public VehicleBaseColorsPollingCache(
            INomenclaturesServicesClientFactory servicesClientFactory,
            IPollingCacheInfrastructure pollingCacheInfrastructure,
            ILogger<VehicleBaseColorsPollingCache> logger)
            : base(logger, pollingCacheInfrastructure, null)
        {
            _servicesClientFactory = servicesClientFactory;
        }

        protected async override Task<CachedDataInfo<List<Nomenclature>>> GenerateCacheDataInfoAsync(DateTime? etag, CancellationToken cancellationToken)
        {
            var result = new List<Nomenclature>();

            var noms = await _servicesClientFactory.GetNomenclatureServicesClient().SearchVehicleColors(CancellationToken.None);

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
