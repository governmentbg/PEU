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
    /// Интерфейс за поддържане и съхранение на обекти от тип UserPermission.
    /// </summary>
    public interface IUsersPermissionRepository :
        IRepository<UserPermission, long?, UserPermissionSearchCriteria>,
        IRepositoryAsync<UserPermission, long?, UserPermissionSearchCriteria>
    {
    }

    /// <summary>
    /// Реализация на интерфейс IUsersPermissionEntity за поддържане и съхранение на обекти от тип UserPermission.
    /// </summary>
    internal class UserPermissionRepository : EAURepositoryBase<UserPermission, long?, UserPermissionSearchCriteria, UsersDataContext>, IUsersPermissionRepository
    {
        #region Constructors

        public UserPermissionRepository(IDbContextProvider dbContextProvider)
            : base(dbContextProvider)
        {
        }

        #endregion

        #region IUsersPermissionEntity

        protected override Task CreateInternalAsync(UsersDataContext context, UserPermission item, CancellationToken cancellationToken)
        {
            return context.UserPermissionCreateAsync(item.UserID.Value, item.PermissionID.Value, cancellationToken);
        }

        protected override Task DeleteInternalAsync(UsersDataContext context, UserPermission item, CancellationToken cancellationToken)
        {
            return context.UserPermissionDeleteAsync(item.UserID.Value, item.PermissionID.Value, cancellationToken);
        }

        protected override async Task<IEnumerable<UserPermission>> SearchInternalAsync(UsersDataContext context, PagedDataState state, UserPermissionSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            var list = await context.UserPermissionsSearchAsync(searchCriteria.UserIDs?.ConcatItems(), cancellationToken);
            return list.ToList();
        }

        protected override Task<IEnumerable<UserPermission>> SearchInternalAsync(UsersDataContext context, UserPermissionSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return SearchInternalAsync(context, PagedDataState.CreateMaxPagedDataState(), searchCriteria, cancellationToken);
        }

        #endregion
    }
}
