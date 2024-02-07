using CNSys.Caching;
using EAU.Nomenclatures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Nomenclatures.Cache
{
    public interface IServiceGroupsCache : IDataCacheItems<CachedDataInfo<List<ServiceGroup>>, string>
    {

    }

    public class ServiceGroups : IServiceGroups
    {
        private readonly IServiceGroupsCache _serviceGroupsCache;

        public ServiceGroups(IServiceGroupsCache serviceGroupsCache)
        {
            _serviceGroupsCache = serviceGroupsCache;
        }

        public ValueTask EnsureLoadedAsync(CancellationToken cancellationToken)
        {
            return EnsureLoadedAsync("bg", cancellationToken);
        }

        public ValueTask EnsureLoadedAsync(string lang, CancellationToken cancellationToken)
        {
            return _serviceGroupsCache.GetItem(lang).EnsureCreatedAsync(cancellationToken);
        }

        public ServiceGroup Get(string lang, int id)
        {
            return Search(lang).Single(x => x.GroupID == id);
        }

        public IEnumerable<ServiceGroup> Search(string lang)
        {
            return _serviceGroupsCache.GetItem(lang).Get().Value;
        }

        public IEnumerable<ServiceGroup> Search(string lang, out DateTime? lastModifiedDate)
        {
            var data = _serviceGroupsCache.GetItem(lang).Get();

            lastModifiedDate = data.LastModifiedDate;
            return data.Value;
        }
    }
}
