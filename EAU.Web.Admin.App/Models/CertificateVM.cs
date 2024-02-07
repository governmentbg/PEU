using System;

namespace EAU.Web.Admin.App.Models
{
    /// <summary>
    /// Сертификат.
    /// </summary>
    public class CertificateVM
    {
        /// <summary>
        /// Уникален идентификатор на сертификата.
        /// </summary>
        public int? CertificateID { get; set; }

        /// <summary>
        /// Сериен номер.
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// Издател.
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Субект.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Дата на валидност на сертификата - не преди.
        /// </summary>
        public DateTime? ValidNotBefore { get; set; }

        /// <summary>
        /// Дата на валидност на сертификата - не след.
        /// </summary>
        public DateTime? ValidNotAfter { get; set; }
    }
}
