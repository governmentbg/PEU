using EAU.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Users.Models
{
    /// <summary>
    /// Потребител.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Уникален идентификатор на потребителски профил.
        /// </summary>
        [DapperColumn("user_id")]
        public int? UserID { get; set; }

        /// <summary>
        /// Електронна поща.
        /// </summary>
        [DapperColumn("email")]
        public string Email { get; set; }

        /// <summary>
        /// Потребителско име.
        /// </summary>
        [DapperColumn("username")]
        public string Username { get; set; }

        /// <summary>
        /// Дата на последна промяна
        /// </summary>
        [DapperColumn("updated_on")]
        public DateTime? UpdatedOn { get; set; }

        /// <summary>
        /// Клиентски идентификационен номер.
        /// </summary>
        [DapperColumn("cin")]
        public int? CIN { get; set; }

        /// <summary>
        /// Статус на профил: 0 -  Непотвърден, 1 - Активен, 2 - Неактивен.
        /// </summary>
        [DapperColumn("status")]
        public UserStatuses? Status { get; set; }

        /// <summary>
        /// Права на потребителя.
        /// </summary>
        public IEnumerable<UserPermission> UserPermissions { get; set; }
    }
}
