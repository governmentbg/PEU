using EAU.Utilities;
using System;

namespace EAU.Users.Models
{
    /// <summary>
    /// Сертификат.
    /// </summary>
    public class Certificate
    {
        /// <summary>
        /// Уникален идентификатор на сертификата.
        /// </summary>
        [DapperColumn("certificate_id")]
        public int? CertificateID { get; set; }

        /// <summary>
        /// Сериен номер.
        /// </summary>
        [DapperColumn("serial_number")]
        public string SerialNumber { get; set; }

        /// <summary>
        /// Издател.
        /// </summary>
        [DapperColumn("issuer")]
        public string Issuer { get; set; }

        /// <summary>
        /// Субект.
        /// </summary>
        [DapperColumn("subject")]
        public string Subject { get; set; }

        /// <summary>
        /// Дата на валидност на сертификата - не преди.
        /// </summary>
        [DapperColumn("not_before")]
        public DateTimeOffset? NotBefore { get; set; }

        /// <summary>
        /// Дата на валидност на сертификата - не след.
        /// </summary>
        [DapperColumn("not_after")]
        public DateTimeOffset? NotAfter { get; set; }

        /// <summary>
        /// Хеш на сертификата.
        /// </summary>
        [DapperColumn("thumbprint")]
        public string CertHash { get; set; }

        /// <summary>
        /// Съдържание.
        /// </summary>
        [DapperColumn("content")]
        public byte[] Content { get; set; }
    }
}
