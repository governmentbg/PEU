using CNSys.Caching;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using WAIS.Integration.MOI.Nomenclatures;
using WAIS.Integration.MOI.Nomenclatures.Models;

namespace EAU.Services.Nomenclatures.Cache
{
    public class RegistrationNumberProvinceCodesPollingCache : DataCacheItems<CachedDataInfo<List<string>>, string>, IRegistrationNumberProvinceCodesCache
    {
        private readonly INomenclaturesServicesClientFactory _servicesClientFactory;
        private readonly IPollingCacheInfrastructure _pollingCacheInfrastructure;
        private readonly ILogger _logger;       

        public RegistrationNumberProvinceCodesPollingCache(INomenclaturesServicesClientFactory servicesClientFactory,
            IPollingCacheInfrastructure pollingCacheInfrastructure,           
            ILogger<DeliveryUnitsInfoPollingCache> logger)
        {
            _servicesClientFactory = servicesClientFactory;
            _pollingCacheInfrastructure = pollingCacheInfrastructure;
            _logger = logger;
        }

        protected override IDataCacheItem<CachedDataInfo<List<string>>> CreateCacheItem(string key)
        {
            return new PollingDataCacheItem<List<string>>(_logger, _pollingCacheInfrastructure, async (etag, cancellationToken) =>
            {
                var criteria = new RegistrationNumberProvinceCodeSearchCriteria()
                {
                    PoliceDepartmentID = Convert.ToInt32(key.Split('_')[0]),
                    VehicleTypeCode = Convert.ToInt32(key.Split('_')[1])
                };

                var result = await _servicesClientFactory.GetNomenclatureServicesClient().SearchRegistrationNumberProvinceCodesAsync(criteria, CancellationToken.None);

                return new CachedDataInfo<List<string>>()
                {
                    Value = result,
                    LastModifiedDate = DateTime.Now
                };
            });
        }
    }
}
