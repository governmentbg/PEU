using EAU.Utilities;
using System;

namespace EAU.Users.Models
{
    /// <summary>
    /// Видове права на потребители
    /// </summary>
    public enum UserPermissions
    {
        /// <summary>
        /// Администриране на потребители
        /// </summary>
        ADM_USERS = 1,

        /// <summary>
        /// Управление на съдържанието
        /// </summary>
        ADM_CMS = 2,

        /// <summary>
        /// Управление на номенклатури
        /// </summary>
        ADM_NOM = 3,

        /// <summary>
        /// Управление на одит
        /// </summary>
        ADM_AUDIT = 4,

        /// <summary>
        /// Администриране на параметри и лимити
        /// </summary>
        ADM_PARAM_LIMIT = 5,
    }

    /// <summary>
    /// Права на потребители
    /// </summary>
    public class UserPermission
    {
        /// <summary>
        /// Уникален идентификатор на потребителски профил.
        /// </summary>
        [DapperColumn("user_id")]
        public int? UserID { get; set; }

        /// <summary>
        /// Уникален идентификатор на право.
        /// </summary>
        [DapperColumn("permission_id")]
        public int? PermissionID { get; set; }

        /// <summary>
        /// Право.
        /// </summary>
        public UserPermissions? Permission
        {
            get
            {
                if (Enum.TryParse(PermissionID?.ToString(), out UserPermissions tmp))
                    return tmp;

                return null;
            }
        }
    }
}
