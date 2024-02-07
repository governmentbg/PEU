using EAU.Users.Models;
using System;

namespace EAU.Web.Admin.App.Models
{
    /// <summary>
    /// Данни за потребителска сесия.
    /// </summary>
    public class UserLoginSessionVM
    {
        /// <summary>
        /// Брой неуспешни опити за вход.
        /// </summary>
        public Guid? LoginSessionID { get; set; }

        /// <summary>
        /// Уникален идентификатор на потребителска сесия.
        /// </summary>
        public Guid? UserSessionID { get; set; }

        /// <summary>
        /// Уникален идентификатор на потребителски профил.
        /// </summary>
        public int? UserID { get; set; }

        /// <summary>
        /// Дата, на която потребителят е влязъл в системата.
        /// </summary>
        public DateTime? LoginDateTime { get; set; }

        /// <summary>
        /// Дата, на която потребителят е излязъл от системата.
        /// </summary>
        public DateTime? LogoutDateTime { get; set; }

        /// <summary>
        /// IP адрес.
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// Начини на автентификация: 1 = потребителско име и парола; 2 = активна директория; 3 = електронен сертификат, 4 - НАП, 5 - Е-Авт.
        /// </summary>
        public AuthenticationTypes? AuthenticationType { get; set; }

        /// <summary>
        /// Уникален идентификатор на сертификат.
        /// </summary>
        public int? CertificateID { get; set; }

        /// <summary>
        /// Идентификатор на потребителя, когато е логнат с ПИК на НАП.
        /// </summary>
        public string UserIdentifier { get; set; }

        /// <summary>
        /// Потребител.
        /// </summary>
        public UserVM User { get; set; }

        /// <summary>
        /// Сертификат.
        /// </summary>
        public CertificateVM Certificate { get; set; }
    }
}
