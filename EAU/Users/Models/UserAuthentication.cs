using EAU.Utilities;
using System;

namespace EAU.Users.Models
{
    /// <summary>
    /// Автентикация на потребителски профил.
    /// </summary>
    public class UserAuthentication
    {
        /// <summary>
        /// Уникален идентификатор на автентикация.
        /// </summary>
        [DapperColumn("authentication_id")]
        public int? AuthenticationID { get; set; }

        /// <summary>
        /// Уникален идентификатор на потребителски профил.
        /// </summary>
        [DapperColumn("user_id")]
        public int? UserID { get; set; }

        /// <summary>
        /// Вид автентикация: 1 - потребителско име и парола, 2 - активна директория, 3 - електронен сертификат.
        /// </summary>
        [DapperColumn("authentication_type")]
        public AuthenticationTypes? AuthenticationType { get; set; }

        /// <summary>
        /// Хеш на парола.
        /// </summary>
        [DapperColumn("password_hash")]
        public string PasswordHash { get; set; }

        /// <summary>
        /// Потребителско име.
        /// </summary>
        [DapperColumn("username")]
        public string Username { get; set; }

        /// <summary>
        /// Идентификатор на потребителски сертификат.
        /// </summary>
        [DapperColumn("certificate_id")]
        public int? CertificateID { get; set; }

        /// <summary>
        /// Флаг, указващ дали автентикацията е заключена.
        /// </summary>
        [DapperColumn("is_locked")]
        public bool IsLocked { get; set; }

        /// <summary>
        /// Дата, до която е заключена автентикацията.
        /// </summary>
        [DapperColumn("locked_until")]
        public DateTime? LockedUntil { get; set; }

        /// <summary>
        /// Флаг указващ дали автентикацията е активна
        /// </summary>
        [DapperColumn("is_active")]
        public bool IsActive { get; set; }
    }
}
