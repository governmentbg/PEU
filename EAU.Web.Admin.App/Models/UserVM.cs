using EAU.Users.Models;
using System;

namespace EAU.Web.Admin.App.Models
{
    /// <summary>
    /// Потребител.
    /// </summary>
    public class UserVM
    {
        /// <summary>
        /// Уникален идентификатор на потребителски профил.
        /// </summary>
        public int? UserID { get; set; }

        /// <summary>
        /// Електронна поща.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Потребителско име.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Дата на последна промяна
        /// </summary>
        public DateTime? UpdatedOn { get; set; }

        /// <summary>
        /// Клиентски идентификационен номер.
        /// </summary>
        public int? CIN { get; set; }

        /// <summary>
        /// Статус на профил: 0 -  Непотвърден, 1 - Активен, 2 - Неактивен.
        /// </summary>
        public UserStatuses? Status { get; set; }
    }
}
