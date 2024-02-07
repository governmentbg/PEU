using System;

namespace EAU.Signing.Models
{
    /// <summary>
    /// Модел на заявка за създаване на хеш на документ към процес за подписване.
    /// </summary>
    public class GetDocumentHashRequest
    {
        /// <summary>
        /// Сертификат за подписване във формат Base64.
        /// </summary>
        public string Base64SigningCert { get; set; }

        /// <summary>
        /// Идентификатор на процеса по подписване.
        /// </summary>
        public Guid? SigningProcessID { get; set; }

        /// <summary>
        /// Идентификатор на подписващ.
        /// </summary>
        public SigningChannels? Channel { get; set; }
    }
}
