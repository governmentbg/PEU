using EAU.Common.Models;
using System;
using System.Collections.Generic;

namespace EAU.Users.Models
{
    /// <summary>
    /// Критерии за търсене на потребители.
    /// </summary>
    public class UserSearchCriteria : BasePagedSearchCriteria
    {
        /// <summary>
        /// Идентификатори на потребители.
        /// </summary>
        public List<int> UserIDs { get; set; }

        /// <summary>
        /// КИН.
        /// </summary>
        public int? CIN { get; set; }

        /// <summary>
        /// Имейл.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Потребителско име.
        /// </summary>
        public string Username { get; set; }        

        /// <summary>
        /// Потребителски статуси.
        /// </summary>
        public List<UserStatuses> UserStatuses { get; set; }

        /// <summary>
        /// От дата.
        /// </summary>
        public DateTime? DateFrom { get; set; }

        /// <summary>
        /// До дата.
        /// </summary>
        public DateTime? DateTo { get; set; }

        /// <summary>
        /// Вид автентикация: 1 - потребителско име и парола, 2 - активна директория, 3 - електронен сертификат.
        /// </summary>
        public AuthenticationTypes? AuthenticationType { get; set; }

        /// <summary>
        /// Флаг, указващ дали да зареди ролите на потребителя.
        /// </summary>
        public bool LoadUserPermissions { get; set; }
    }

    /// <summary>
    /// Критерии за търсене на възможни начини за вход на потребител.
    /// </summary>
    public class UserAuthenticationSearchCriteria
    {
        /// <summary>
        /// Идентификатори на автентикация.
        /// </summary>
        public List<int> AuthenticationIDs { get; set; }

        /// <summary>
        /// Уникален идентификатор на потребителя.
        /// </summary>
        public int? UserID { get; set; }

        /// <summary>
        /// Вид автентикация: 1 - потребителско име и парола, 2 - активна директория, 3 - електронен сертификат.
        /// </summary>
        public AuthenticationTypes? AuthenticationType { get; set; }

        /// <summary>
        /// Потребителско име.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Хеш на сертификат.
        /// </summary>
        public string CertificateHash { get; set; }
    }

    /// <summary>
    /// Критерии за търсене на роли на потребители.
    /// </summary>
    public class UserPermissionSearchCriteria
    {
        /// <summary>
        /// Уникални идентификатори на потребители.
        /// </summary>
        public List<int> UserIDs { get; set; }
    }

    /// <summary>
    /// Критерии за търсене на опити за вход на потребител.
    /// </summary>
    public class UserLoginAttemptSearchCriteria
    {
        /// <summary>
        /// Уникални идентификатори на опити за вход.
        /// </summary>
        public List<long> AttemptIDs { get; set; }

        /// <summary>
        /// Потребителско име.
        /// </summary>
        public string LoginName { get; set; }
    }

    /// <summary>
    /// Критерии за търсене на потребителски сесии.
    /// </summary>
    public class UserLoginSessionSearchCriteria
    {
        /// <summary>
        /// Уникални идентификатори на входове в системата.
        /// </summary>
        public Guid[] LoginSessionIDs { get; set; }

        /// <summary>
        /// Уникален идентификатор на потребителска сесия.
        /// </summary>
        public Guid? UserSessionID { get; set; }

        /// <summary>
        /// Уникален идентификатор на потребителя.
        /// </summary>
        public int? UserID { get; set; }

        /// <summary>
        /// Дата, след която търсим.
        /// </summary>
        public DateTime? LoginDateFrom { get; set; }

        /// <summary>
        /// Дата, до която търсим.
        /// </summary>
        public DateTime? LoginDateTo { get; set; }

        /// <summary>
        /// IP адрес.
        /// </summary>
        public byte[] IPAddress { get; set; }

        /// <summary>
        /// Вид автентикация: 1 - потребителско име и парола, 2 - активна директория, 3 - електронен сертификат.
        /// </summary>
        public AuthenticationTypes? AuthenticationType { get; set; }
    }

    public class UserProcessesSearchCriteria
    {
        /// <summary>
        /// Уникални идентификатори на потребители.
        /// </summary>
        public List<int> UserIDs { get; set; }

        /// <summary>
        /// Уникални идентификатори на потребители.
        /// </summary>
        public List<Guid> ProcessGuids { get; set; }
    }
}
