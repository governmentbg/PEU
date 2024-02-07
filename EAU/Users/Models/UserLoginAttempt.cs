using EAU.Utilities;

namespace EAU.Users.Models
{
    /// <summary>
    /// Опит за вход на потребител.
    /// </summary>
    public class UserLoginAttempt
    {
        /// <summary>
        /// Уникален идентификатор на опита.
        /// </summary>
        [DapperColumn("attempt_id")]
        public int? AttemptID { get; set; }

        /// <summary>
        /// Вид автентикация: 1 - потребителско име и парола, 2 - активна директория, 3 - електронен сертификат.
        /// </summary>
        [DapperColumn("authentication_type")]
        public AuthenticationTypes? AuthenticationType { get; set; }

        /// <summary>
        /// Потребителско име.
        /// </summary>
        [DapperColumn("login_name")]
        public string LoginName { get; set; }

        /// <summary>
        /// Допълнителни данни за опита за вход.
        /// </summary>
        [DapperColumn("additional_data")]
        public string AdditionalData { get; set; }

        /// <summary>
        /// Брой неуспешни опити за вход.
        /// </summary>
        [DapperColumn("failed_login_attempts")]
        public int FailedLoginAttempts { get; set; } = 0;

        /// <summary>
        /// Флаг, указващ дали потребителят е активен.
        /// </summary>
        [DapperColumn("is_active")]
        public bool IsActive { get; set; }
    }
}
