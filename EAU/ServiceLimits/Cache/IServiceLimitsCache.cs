using CNSys.Caching;
using EAU.Nomenclatures;
using EAU.ServiceLimits.Models;
using EAU.ServiceLimits.Repositories;
using EAU.Users;
using EAU.Users.Models;
using EAU.Utilities.Caching;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.ServiceLimits.Cache
{

    public interface IServiceLimitsCache : ILoadable
    {
        DataServiceLimit GetDataServiceLimit(string serviceCode);

        IEnumerable<DataServiceLimit> GetDataServiceLimits();

        IEnumerable<DataServiceLimit> GetDataServiceLimits(out DateTime? lastModifiedDate);

        DataServiceUserLimit GetDataServiceUserLimit(int userLimitID);

        IEnumerable<DataServiceUserLimit> GetDataServiceUserLimits();

        IEnumerable<DataServiceUserLimit> GetDataServiceUserLimits(out DateTime? lastModifiedDate);

        IChangeToken GetChangeToken();
    }

    internal class ServiceLimitCacheItem
    {
        public List<DataServiceLimit> DataServiceLimits { get; set; }

        public List<DataServiceUserLimit> DataServiceUserLimits { get; set; }
    }

    internal class ServiceLimitsDbCache : DbDataCacheItem<ServiceLimitCacheItem>, IServiceLimitsCache
    {
        private readonly IDataServiceLimitRepository _dataServiceLimitRepository;
        private readonly IDataServiceUserLimitRepository _dataServiceUserLimitRepository;
        private readonly IUsersSearchService _usersSearchService;

        #region Construcotrs

        public ServiceLimitsDbCache(
            ILogger<ServiceLimitsDbCache> logger,
            IDbCacheInvalidationDispatcher dbCacheInvalidationDispatcher,
            IDataServiceLimitRepository dataServiceLimitRepository,
            IDataServiceUserLimitRepository dataServiceUserLimitRepository,
            IUsersSearchService usersSearchService)
            : base(logger,
                  dbCacheInvalidationDispatcher, 
                  null,
                  new string[] { "[dbo].[data_service_limits]", "[dbo].[data_service_user_limits]" })
        {
            _dataServiceLimitRepository = dataServiceLimitRepository;
            _dataServiceUserLimitRepository = dataServiceUserLimitRepository;
            _usersSearchService = usersSearchService;
        }

        #endregion

        protected async override Task<CachedDataInfo<ServiceLimitCacheItem>> GenerateCacheDataInfoAsync(DateTime? etag, CancellationToken cancellationToken)
        {
            ServiceLimitCacheItem ret = new ServiceLimitCacheItem();

            /*взимаме всички конфигурирани лимити за услуги, независимо дали са активни или не.*/
            var serviceLimitsCol = (await _dataServiceLimitRepository.SearchInfoAsync(new DataServiceLimitsSearchCriteria(), cancellationToken));
            ret.DataServiceLimits = serviceLimitsCol.Data.ToList();

            var serviceUserLimitsCol = (await _dataServiceUserLimitRepository.SearchInfoAsync(new DataServiceUserLimitsSearchCriteria() { Status = DataServiceLimitStatus.Active }, cancellationToken));
            ret.DataServiceUserLimits = serviceUserLimitsCol.Data.ToList();

            var dataServiceLimitsLookup = ret.DataServiceLimits.ToDictionary((item) => item.ServiceLimitID.Value);

            var userIDs = ret.DataServiceUserLimits.Select(item => item.UserID.Value).ToList();

            var users = (await _usersSearchService.SearchUsersAsync(new UserSearchCriteria() { UserIDs = userIDs }, cancellationToken))
                .ToDictionary((item) => item.UserID.Value);


            foreach (var dataServiceUserLimit in ret.DataServiceUserLimits)
            {
                dataServiceUserLimit.ServiceLimit = dataServiceLimitsLookup[dataServiceUserLimit.ServiceLimitID.Value];
                dataServiceUserLimit.User = users[dataServiceUserLimit.UserID.Value];
            }

            return new CachedDataInfo<ServiceLimitCacheItem>()
            {
                Value = ret,
                LastModifiedDate = DateTime.Compare(serviceLimitsCol.LastUpdatedOn, serviceUserLimitsCol.LastUpdatedOn) > 0 ?
                serviceLimitsCol.LastUpdatedOn : serviceUserLimitsCol.LastUpdatedOn
            };
        }

        public ValueTask EnsureLoadedAsync(CancellationToken cancellationToken)
        {
            return this.EnsureCreatedAsync(cancellationToken);
        }

        public DataServiceLimit GetDataServiceLimit(string serviceCode)
        {

            var data = Get();

            return data.Value.DataServiceLimits.Single(t => t.ServiceCode == serviceCode);
        }

        public IEnumerable<DataServiceLimit> GetDataServiceLimits()
        {
            var data = Get();

            return data.Value.DataServiceLimits;
        }        

        public IEnumerable<DataServiceLimit> GetDataServiceLimits(out DateTime? lastModifiedDate)
        {
            var data = Get();

            lastModifiedDate = data.LastModifiedDate;

            return data.Value.DataServiceLimits;
        }

        public DataServiceUserLimit GetDataServiceUserLimit(int userLimitID)
        {
            var data = Get();

            return data.Value.DataServiceUserLimits.Single(t => t.UserLimitID == userLimitID);
        }

        public IEnumerable<DataServiceUserLimit> GetDataServiceUserLimits()
        {
            var data = Get();

            return data.Value.DataServiceUserLimits;
        }

        public IEnumerable<DataServiceUserLimit> GetDataServiceUserLimits(out DateTime? lastModifiedDate)
        {
            var data = Get();

            lastModifiedDate = data.LastModifiedDate;

            return data.Value.DataServiceUserLimits;
        }
    }
}
