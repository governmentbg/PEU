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
    /// Интерфейс за поддържане и съхранение на обекти от тип UserAuthentication.
    /// </summary>
    public interface IUsersAuthenticationRepository :
        IRepository<UserAuthentication, long?, UserAuthenticationSearchCriteria>,
        IRepositoryAsync<UserAuthentication, long?, UserAuthenticationSearchCriteria>
    {
    }

    /// <summary>
    /// Реализация на интерфейс IUsersAuthenticationEntity за поддържане и съхранение на обекти от тип UserAuthentication.
    /// </summary>
    internal class UsersAuthenticationRepository : EAURepositoryBase<UserAuthentication, long?, UserAuthenticationSearchCriteria, UsersDataContext>, IUsersAuthenticationRepository
    {
        #region Constructors

        public UsersAuthenticationRepository(IDbContextProvider dbContextProvider)
            : base(dbContextProvider)
        {
        }

        #endregion

        #region IUsersAuthenticationEntity

        protected async override Task CreateInternalAsync(UsersDataContext context, UserAuthentication item, CancellationToken cancellationToken)
        {
            item.AuthenticationID = await context.UsersAuthenticationCreateAsync(
                item.UserID.Value
                , (short)item.AuthenticationType
                , item.PasswordHash
                , item.Username
                , item.CertificateID
                , cancellationToken);
        }

        protected override Task UpdateInternalAsync(UsersDataContext context, UserAuthentication item, CancellationToken cancellationToken)
            => context.UsersAuthenticationUpdateAsync(item.AuthenticationID.Value, item.UserID.Value, (short)item.AuthenticationType, item.PasswordHash, item.Username, item.IsLocked, item.LockedUntil, item.CertificateID, cancellationToken);


        protected override Task DeleteInternalAsync(UsersDataContext context, UserAuthentication item, CancellationToken cancellationToken)
            => context.UsersAuthenticationDeleteAsync(item.AuthenticationID.Value, cancellationToken);

        protected override async Task<IEnumerable<UserAuthentication>> SearchInternalAsync(UsersDataContext context, PagedDataState state, UserAuthenticationSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            var list = await context.UsersAuthenticationSearchAsync(searchCriteria.AuthenticationIDs.ConcatItems(),
                searchCriteria.UserID, (short?)searchCriteria.AuthenticationType, searchCriteria.Username, searchCriteria.CertificateHash, cancellationToken);
            return list.ToList();
        }

        protected override Task<IEnumerable<UserAuthentication>> SearchInternalAsync(UsersDataContext context, UserAuthenticationSearchCriteria searchCriteria, CancellationToken cancellationToken)
            => SearchInternalAsync(context, PagedDataState.CreateMaxPagedDataState(), searchCriteria, cancellationToken);

        #endregion
    }
}
