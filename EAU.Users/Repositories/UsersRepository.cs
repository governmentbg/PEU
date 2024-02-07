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
    /// Интерфейс за поддържане и съхранение на обекти от тип User.
    /// </summary>
    public interface IUsersRepository :
        IRepository<User, long?, UserSearchCriteria>,
        IRepositoryAsync<User, long?, UserSearchCriteria>
    {
        /// <summary>
        /// Изтрива данни за както за самия потребител, така и съпътващите обекти.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task DeleteUserDataAsync(User item, CancellationToken cancellationToken);
    }

    /// <summary>
    /// Реализация на интерфейс IUsersEntity за поддържане и съхранение на обекти от тип User.
    /// </summary>
    internal class UsersRepository : EAURepositoryBase<User, long?, UserSearchCriteria, UsersDataContext>, IUsersRepository
    {
        #region Constructors

        public UsersRepository(IDbContextProvider dbContextProvider)
            : base(dbContextProvider)
        {
        }

        #endregion

        protected async override Task CreateInternalAsync(UsersDataContext context, User item, CancellationToken cancellationToken)
        {
            var (id, version, cin) = await context.UserCreateAsync(item.Email, (byte?)item.Status, cancellationToken);

            item.UserID = id;
            item.CIN = cin;
        }

        protected async override Task UpdateInternalAsync(UsersDataContext context, User item, CancellationToken cancellationToken)
        {
            var version = await context.UserUpdateAsync(item.UserID.Value, item.Email, (byte?)item.Status, cancellationToken);            
        }

        protected override Task DeleteInternalAsync(UsersDataContext context, User item, CancellationToken cancellationToken)
            => context.UserDeleteAsync(item.UserID.Value, cancellationToken);

        protected async override Task<IEnumerable<User>> SearchInternalAsync(UsersDataContext context, PagedDataState state, UserSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            var (data, count) = await context.UsersSearchAsync(
                            searchCriteria.UserIDs?.ConcatItems(), searchCriteria.CIN,
                            searchCriteria.Email, searchCriteria.Username,
                            searchCriteria.UserStatuses?.Cast<int>()?.ConcatItems(),
                            searchCriteria.DateFrom, searchCriteria.DateTo,
                            (short?)searchCriteria.AuthenticationType,
                            state.StartIndex, state.PageSize, null,
                            (state.StartIndex == 1), cancellationToken);

            state.Count = count ?? state.Count;

            return data.ToList();
        }

        protected override Task<IEnumerable<User>> SearchInternalAsync(UsersDataContext context, UserSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return SearchInternalAsync(context, PagedDataState.CreateMaxPagedDataState(), searchCriteria, cancellationToken);
        }

        public Task DeleteUserDataAsync(User item, CancellationToken cancellationToken)
        {
            return DoOperationAsync((context, token) =>
            {
                return context.UserDeleteDataAsync(item.UserID.Value, cancellationToken);
            }, cancellationToken);
        }
    }
}
