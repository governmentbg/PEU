using CNSys.Data;
using EAU.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Web.Protection
{
    /// <summary>
    /// Интерфейс за поддържане и съхранение на обекти от тип DataProtectionKey.
    /// </summary>
    public interface IDataProtectionRepository : IRepository<DataProtectionKey, string, object>
    {
    }

    /// <summary>
    /// Реализация на интерфейс IDataProtectionEntity за поддържане и съхранение на обекти от тип DataProtectionKey.
    /// </summary>
    public class DataProtectionRepository : EAURepositoryBase<DataProtectionKey, string, object, DataProtectionDataContext>, IDataProtectionRepository
    {
        public DataProtectionRepository(IDbContextProvider dbContextProvider)
            : base(dbContextProvider)
        {
        }

        protected override Task<IEnumerable<DataProtectionKey>> SearchInternalAsync(DataProtectionDataContext context, PagedDataState state, object searchCriteria, CancellationToken cancellationToken)
            => context.DataProtectionKeysSearchAsync(cancellationToken);

        protected override IEnumerable<DataProtectionKey> SearchInternal(DataProtectionDataContext context, object searchCriteria)
            => SearchInternal(context, PagedDataState.CreateMaxPagedDataState(), searchCriteria);

        protected override Task CreateInternalAsync(DataProtectionDataContext context, DataProtectionKey item, CancellationToken cancellationToken)
            => context.DataProtectionKeysCreateAsync(item.ID, item.KeyXml, item.CreationDate, item.ActivationDate, item.ExpirationDate, cancellationToken);
    }
}
