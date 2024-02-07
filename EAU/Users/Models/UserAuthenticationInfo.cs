using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Users.Models
{
    /// <summary>
    /// Данни за регистриран сертификат към потребител
    /// </summary>
    public class UserAuthenticationInfo
    {
        /// <summary>
        /// Идентификатор на средство за автентификация
        /// </summary>
        public int UserAuthenticationId { get; set; }

        /// <summary>
        /// Издател
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Сериен номер
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// Титуляр
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Валиден до
        /// </summary>
        public DateTime ValidTo { get; set; }
    }
}
