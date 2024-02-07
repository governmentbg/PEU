using CNSys.Caching;
using EAU.Nomenclatures;
using EAU.Nomenclatures.Models;
using EAU.Services.Nomenclatures.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using WAIS.Integration.EPortal.Clients;

namespace EAU.Services.Nomenclatures.Cache
{
    public class DeliveryUnitsInfoPollingCache : DataCacheItems<CachedDataInfo<List<UnitInfo>>, string>, IDeliveryUnitsInfoCache
    {
        private readonly IWAISIntegrationServiceClientsFactory _waisIntegrationServiceClientsFactory;
        private readonly IPollingCacheInfrastructure _pollingCacheInfrastructure;
        private readonly ILogger _logger;
        private readonly IServices _services;

        public DeliveryUnitsInfoPollingCache(IWAISIntegrationServiceClientsFactory waisIntegrationServiceClientsFactory,
            IPollingCacheInfrastructure pollingCacheInfrastructure,
            IServices services,
            ILogger<DeliveryUnitsInfoPollingCache> logger)
        {
            _waisIntegrationServiceClientsFactory = waisIntegrationServiceClientsFactory;
            _pollingCacheInfrastructure = pollingCacheInfrastructure;
            _logger = logger;
            _services = services;
        }

        protected override IDataCacheItem<CachedDataInfo<List<UnitInfo>>> CreateCacheItem(string key)
        {
            return new PollingDataCacheItem<List<UnitInfo>>(_logger, _pollingCacheInfrastructure, async (etag, cancellationToken) =>
            {
                var keyParts = key.Split('_');

                var currentService = _services.Search().FirstOrDefault(x => x.ServiceID == int.Parse(keyParts[0]));
                var result = new List<UnitInfo>();

                if (currentService != null)
                {
                    WAIS.Integration.EPortal.Models.ServiceTermTypesSearch? serviceTermTypesSearch = null;

                    if (keyParts.Length > 1  && !string.IsNullOrEmpty(keyParts[1]) && Enum.TryParse(keyParts[1], out AdmServiceTermType termTypeValue))
                    {
                        if (termTypeValue == AdmServiceTermType.Regular) 
                            serviceTermTypesSearch = WAIS.Integration.EPortal.Models.ServiceTermTypesSearch.Regular;
                        if (termTypeValue == AdmServiceTermType.Fast) 
                            serviceTermTypesSearch = WAIS.Integration.EPortal.Models.ServiceTermTypesSearch.Fast;
                    }

                    var data = await _waisIntegrationServiceClientsFactory.GetNomenclaturesClient().GetServiceDeliveryUnitsAsync(currentService.SunauServiceUri, serviceTermTypesSearch, CancellationToken.None);

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