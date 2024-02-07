using CNSys.Caching;
using EAU.Nomenclatures;
using EAU.Services.Nomenclatures.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using WAIS.Integration.EPortal.Clients;

namespace EAU.Services.Nomenclatures.Cache
{
    public class ServingUnitsInfoPollingCache : DataCacheItems<CachedDataInfo<List<UnitInfo>>, int>, IServingUnitsInfoCache
    {
        private readonly IWAISIntegrationServiceClientsFactory _waisIntegrationServiceClientsFactory;
        private readonly IPollingCacheInfrastructure _pollingCacheInfrastructure;
        private readonly ILogger _logger;
        private readonly IServices _services;

        public ServingUnitsInfoPollingCache(IWAISIntegrationServiceClientsFactory waisIntegrationServiceClientsFactory,
            IPollingCacheInfrastructure pollingCacheInfrastructure,
            IServices services,
            ILogger<ServingUnitsInfoPollingCache> logger)
        {
            _waisIntegrationServiceClientsFactory = waisIntegrationServiceClientsFactory;
            _pollingCacheInfrastructure = pollingCacheInfrastructure;
            _logger = logger;
            _services = services;
        }

        protected override IDataCacheItem<CachedDataInfo<List<UnitInfo>>> CreateCacheItem(int key)
        {
            return new PollingDataCacheItem<List<UnitInfo>>(_logger, _pollingCacheInfrastructure, async (etag, cancellationToken) =>
            {
                var currentService = _services.Search().FirstOrDefault(x => x.ServiceID == key);
                var result = new List<UnitInfo>();

                if (currentService != null)
                {
                    var data = await _waisIntegrationServiceClientsFactory.GetNomenclaturesClient().GetServiceServingUnitsAsync(currentService.SunauServiceUri, CancellationToken.None);

                    if (data != null)
                    {
                        result.AddRange((from unit in data
                                         select new UnitInfo()
                                         {
                                             UnitID = unit.UnitInfoID,
                                             HasChildUnits = unit.HasChildUnits,
                                             Name = unit.Name,
                                             ParentUnitID = unit.ParentUnitInfoID
                                         }).OrderBy(u => u.Name).ToList());
                    }
                }

                return new CachedDataInfo<List<UnitInfo>>()
                {
                    Value = result,
                    LastModifiedDate = DateTime.Now
                };
            });
        }
    }
}