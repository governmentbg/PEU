using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace EAU.Signing.Models
{
    /// <summary>
    /// Резултат от верификация на подпис.
    /// </summary>
    public class SignatureVerificationResult
    {
        /// <summary>
        /// Статус.
        /// </summary>
        public SignatureVerificationResultStatuses? Status { get; set; }

        /// <summary>
        /// Грешки.
        /// </summary>
        public List<SignatureVerificationError> Errors { get; set; }

        /// <summary>
        /// Сертификати на подписи.
        /// </summary>
        public List<X509Certificate> SignatursCerts { get; set; }
    }

    /// <summary>
    /// Резултат статус на верификация на подпис: 1 = Валиден.; 2 = Невалиден.;
    /// </summary>
    public enum SignatureVerificationResultStatuses
    {
        /// <summary>
        /// Валиден.
        /// </summary>
        Valid = 1,

        /// <summary>
        /// Невалиден.
        /// </summary>
        Invalid = 2
    }

    /// <summary>
    /// Грешка за верификация на подпис.
    /// </summary>
    public class SignatureVerificationError
    {
        /// <summary>
        /// Код.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Съобщение.
        /// </summary>
        public string Message { get; set; }
    }
}
