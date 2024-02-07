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
    /// Интерфейс за поддържане и съхранение на обекти от тип UserLoginSession.
    /// </summary>
    public interface IUserLoginSessionRepository :
        IRepository<UserLoginSession, int?, UserLoginSessionSearchCriteria>,
        IRepositoryAsync<UserLoginSession, int?, UserLoginSessionSearchCriteria>
    {
    }
    /// <summary>
    /// Реализация на интерфейс IUserLoginSessionEntity за поддържане и съхранение на обекти от тип UserLoginSession.
    /// </summary>
    internal class UserLoginSessionRepository : EAURepositoryBase<UserLoginSession, int?, UserLoginSessionSearchCriteria, UsersDataContext>, IUserLoginSessionRepository
    {
        #region Constructors

        public UserLoginSessionRepository(IDbContextProvider dbContextProvider)
            : base(dbContextProvider)
        {
        }

        #endregion

        #region IUsersAuthenticationEntity

        protected override Task CreateInternalAsync(UsersDataContext context, UserLoginSession item, CancellationToken cancellationToken)
        {
            return context.UserLoginSessionCreateAsync(item.LoginSessionID.Value, item.UserSessionID.Value,
                item.UserID.Value, item.LoginDate.Value, item.LogoutDate, item.IpAddress, (short)item.AuthenticationType, item.CertificateID, cancellationToken);
        }

        protected override Task UpdateInternalAsync(UsersDataContext context, UserLoginSession item, CancellationToken cancellationToken)
        {
            return context.UserLoginSessionUpdateAsync(item.LoginSessionID.Value, item.UserSessionID.Value, item.UserID.Value, item.LoginDate.Value, item.LogoutDate, item.IpAddress, (short)item.AuthenticationType, item.CertificateID, cancellationToken);
        }

        protected override async Task<IEnumerable<UserLoginSession>> SearchInternalAsync(UsersDataContext context, PagedDataState state, UserLoginSessionSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            var (data, count) = await context.UsersLoginSessionSearchAsync(searchCriteria.LoginSessionIDs.ConcatItems(), state.StartIndex, state.PageSize, state.StartIndex == 1, cancellationToken);
            state.Count = count ?? state.Count;

            return data.ToList();
        }

        protected override Task<IEnumerable<UserLoginSession>> SearchInternalAsync(UsersDataContext context, UserLoginSessionSearchCriteria searchCriteria, CancellationToken cancellationToken)
            => SearchInternalAsync(context, PagedDataState.CreateMaxPagedDataState(), searchCriteria, cancellationToken);

        #endregion
    }
}
