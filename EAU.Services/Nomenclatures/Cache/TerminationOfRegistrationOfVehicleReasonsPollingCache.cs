using CNSys.Caching;
using EAU.Services.Nomenclatures.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using WAIS.Integration.MOI.Nomenclatures;
using System.Threading.Tasks;

namespace EAU.Services.Nomenclatures.Cache
{
    public class TerminationOfRegistrationOfVehicleReasonsPollingCache : PollingDataCacheItem<List<Nomenclature>>, ITerminationOfRegistrationOfVehicleReasonsCache
    {
        private readonly INomenclaturesServicesClientFactory _servicesClientFactory;    

        public TerminationOfRegistrationOfVehicleReasonsPollingCache(
            INomenclaturesServicesClientFactory servicesClientFactory,
            IPollingCacheInfrastructure pollingCacheInfrastructure,
            ILogger<TerminationOfRegistrationOfVehicleReasonsPollingCache> logger)
            : base(logger, pollingCacheInfrastructure, null)
        {
            _servicesClientFactory = servicesClientFactory;
        }

        protected async override Task<CachedDataInfo<List<Nomenclature>>> GenerateCacheDataInfoAsync(DateTime? etag, CancellationToken cancellationToken)
        {
            var result = new List<Nomenclature>();

            var noms = await _servicesClientFactory.GetNomenclatureServicesClient().SearchTerminationOfRegistrationOfVehicleAsync(CancellationToken.None);

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
