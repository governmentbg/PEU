using System;

namespace EAU.Signing.Models
{
    /// <summary>
    /// Заявка за сглобяване на документа и подписа.
    /// </summary>
    public class AssembleSignatureWithDocumentRequest : CompleteBissSignRequest
    {
        /// <summary>
        /// Идентификатор на процеса за подписване
        /// </summary>
        public Guid? ProcessID { get; set; }

        /// <summary>
        /// Идентификатор на подписващия.
        /// </summary>
        public long SignerID { get; set; }
    }

    public class CompleteBissSignRequest 
    {
        /// <summary>
        /// Сертификат за подписване във формат Base64.
        /// </summary>
        public string Base64SigningCert { get; set; }

        /// <summary>
        /// Подпис на документа във формат Base64.
        /// </summary>
        public string Base64DocSignature { get; set; }

        /// <summary>
        /// Време на полагане на подписа.
        /// </summary>
        public long HashTime { get; set; }
    }
}
