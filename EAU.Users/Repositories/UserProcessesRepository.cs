using CNSys.Data;
using EAU.Common;
using EAU.Data;
using EAU.Users.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Users.Repositories
{
    /// <summary>
    /// Интерфейс за поддържане и съхранение на обекти от тип UserProcess.
    /// </summary>
    public interface IUsersProcessesRepository :
        IRepository<UserProcess, long?, UserProcessesSearchCriteria>,
        IRepositoryAsync<UserProcess, long?, UserProcessesSearchCriteria>
    {
    }

    /// <summary>
    /// Реализация на интерфейс IUsersProcessesEntity за поддържане и съхранение на обекти от тип UserProcess.
    /// </summary>
    internal class UserProcessesRepository : EAURepositoryBase<UserProcess, long?, UserProcessesSearchCriteria, UsersDataContext>, IUsersProcessesRepository
    {
        #region Constructors

        public UserProcessesRepository(IDbContextProvider dbContextProvider)
            : base(dbContextProvider)
        {
        }

        #endregion

        protected async override Task CreateInternalAsync(UsersDataContext context, UserProcess item, CancellationToken cancellationToken)
        {
            var id = await context.UserProcessesCreateAsync(item.UserID, item.ProcessGuid, (byte)item.ProcessType, item.InvalidAfter, cancellationToken);
            item.ProcessID = id;
        }

        protected override Task UpdateInternalAsync(UsersDataContext context, UserProcess item, CancellationToken cancellationToken)
        {
            return context.UserProcessesUpdateAsync(item.ProcessID.Value, (byte)item.Status, cancellationToken);
        }

        protected override async Task<IEnumerable<UserProcess>> SearchInternalAsync(UsersDataContext context, PagedDataState state, UserProcessesSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            var (data, count) = await context.UserProcessesSearchAsync(null, searchCriteria.ProcessGuids.ConcatItems(), searchCriteria.UserIDs.ConcatItems(), null, state.StartIndex, state.PageSize, state.StartIndex == 1, cancellationToken);
            state.Count = count ?? state.Count;

            return data.ToList();
        }

        protected override Task<IEnumerable<UserProcess>> SearchInternalAsync(UsersDataContext context, UserProcessesSearchCriteria searchCriteria, CancellationToken cancellationToken)
            => SearchInternalAsync(context, PagedDataState.CreateMaxPagedDataState(), searchCriteria, cancellationToken);
    }
}
