using CNSys.Caching;
using EAU.Nomenclatures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Nomenclatures.Cache
{
    public interface IServicesCache : IDataCacheItems<CachedDataInfo<List<Service>>, string>
    {

    }

    public class Services : IServices
    {
        private readonly IServicesCache _servicesCache;

        public Services(IServicesCache servicesCache)
        {
            _servicesCache = servicesCache;
        }

        public ValueTask EnsureLoadedAsync(CancellationToken cancellationToken)
        {
            return EnsureLoadedAsync("bg", cancellationToken);
        }

        public ValueTask EnsureLoadedAsync(string lang, CancellationToken cancellationToken)
        {
            return _servicesCache.GetItem(lang).EnsureCreatedAsync(cancellationToken);
        }

        public Service Get(string lang, int serviceID)
        {
            return Search(lang).Single(x => x.ServiceID == serviceID);
        }

        public IEnumerable<Service> Search()
        {
            return Search("bg");
        }

        public IEnumerable<Service> Search(string lang)
        {
            return _servicesCache.GetItem(lang).Get().Value;
        }

        public IEnumerable<Service> Search(string lang, int groupID)
        {
            return Search(lang).Where(x => x.GroupID == groupID);
        }

        public IEnumerable<Service> Search(string lang, out DateTime? lastModifiedDate)
        {
            var data = _servicesCache.GetItem(lang).Get();

            lastModifiedDate = data.LastModifiedDate;
            return data.Value;
        }
    }
}
