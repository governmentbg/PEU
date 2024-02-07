using CNSys.Caching;
using EAU.Nomenclatures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Nomenclatures.Cache
{
    public interface IServiceTermsCache : IDataCacheItem<CachedDataInfo<List<ServiceTerm>>>
    {

    }

    public class ServiceTerms : IServiceTerms
    {
        private readonly IServiceTermsCache _serviceTermsCache;

        public ServiceTerms(IServiceTermsCache serviceTermsCache)
        {
            _serviceTermsCache = serviceTermsCache;
        }

        public ValueTask EnsureLoadedAsync(CancellationToken cancellationToken)
        {
            return _serviceTermsCache.EnsureCreatedAsync(cancellationToken);
        }

        public IEnumerable<ServiceTerm> Search()
        {
            return _serviceTermsCache.Get().Value;
        }

        public IEnumerable<ServiceTerm> Search(out DateTime? lastModifiedDate)
        {
            var data = _serviceTermsCache.Get();

            lastModifiedDate = data.LastModifiedDate;
            return data.Value;
        }

        public IEnumerable<ServiceTerm> Search(int? serviceID)
        {
            return Search().Where(st => st.ServiceID == serviceID);
        }
    }
}
