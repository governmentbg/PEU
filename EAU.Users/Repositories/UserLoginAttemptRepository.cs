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
    /// Интерфейс за поддържане и съхранение на обекти от тип UserLoginAttempt.
    /// </summary>
    public interface IUserLoginAttemptRepository :
        IRepository<UserLoginAttempt, int?, UserLoginAttemptSearchCriteria>,
        IRepositoryAsync<UserLoginAttempt, int?, UserLoginAttemptSearchCriteria>
    {
    }

    internal class UserLoginAttemptRepository : EAURepositoryBase<UserLoginAttempt, int?, UserLoginAttemptSearchCriteria, UsersDataContext>, IUserLoginAttemptRepository
    {
        #region Constructors

        public UserLoginAttemptRepository(IDbContextProvider dbContextProvider)
            : base(dbContextProvider)
        {
        }

        #endregion

        protected async override Task CreateInternalAsync(UsersDataContext context, UserLoginAttempt item, CancellationToken cancellationToken)
        {
            item.AttemptID = await context.UserLoginAttemptCreateAsync((short?)item.AuthenticationType, item.LoginName, item.FailedLoginAttempts, cancellationToken);
        }

        protected override Task UpdateInternalAsync(UsersDataContext context, UserLoginAttempt item, CancellationToken cancellationToken)
        {
            return context.UserLoginAttemptUpdateAsync(item.AttemptID.Value, (short)item.AuthenticationType, item.LoginName, item.FailedLoginAttempts, cancellationToken);
        }

        protected override Task DeleteInternalAsync(UsersDataContext context, int? key, CancellationToken cancellationToken)
        {
            if (key == null)
                throw new System.ArgumentNullException();

            return context.UserLoginAttemptDeleteAsync(key.Value, cancellationToken);
        }

        protected override Task DeleteInternalAsync(UsersDataContext context, UserLoginAttempt item, CancellationToken cancellationToken)
        {
            return DeleteInternalAsync(context, item.AttemptID, cancellationToken);
        }

        protected override async Task<IEnumerable<UserLoginAttempt>> SearchInternalAsync(UsersDataContext context, PagedDataState state, UserLoginAttemptSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            var list = await context.UsersLoginAttemptSearchAsync(searchCriteria.AttemptIDs?.ConcatItems(), searchCriteria.LoginName, cancellationToken);
            return list.ToList();
        }

        protected override Task<IEnumerable<UserLoginAttempt>> SearchInternalAsync(UsersDataContext context, UserLoginAttemptSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return SearchInternalAsync(context, PagedDataState.CreateMaxPagedDataState(), searchCriteria, cancellationToken);
        }
    }
}
