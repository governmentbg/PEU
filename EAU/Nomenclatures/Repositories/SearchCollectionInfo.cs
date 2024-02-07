using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Nomenclatures.Repositories
{
    public class CollectionInfo<T>
    {
        public IEnumerable<T> Data { get; set; }
        public DateTime LastUpdatedOn { get; set; }
    }

    public interface ISearchCollectionInfo2<T, S>
    {
        Task<CollectionInfo<T>> SearchInfoAsync(S searchCriteria, CancellationToken cancellationToken);
        Task<CollectionInfo<T>> SearchInfoAsync(CNSys.Data.PagedDataState state, S searchCriteria, CancellationToken cancellationToken);
    }
}
