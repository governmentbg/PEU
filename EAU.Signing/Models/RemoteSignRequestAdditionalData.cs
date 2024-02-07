namespace EAU.Signing.Models
{
    /// <summary>
    /// Допълнителни данни за заявка за отдалечено подписване.
    /// </summary>
    public class RemoteSignRequestAdditionalData : SignerAdditionalData
    {
        /// <summary>
        /// Идентификатор на трансакция.
        /// </summary>
        public string TransactionID { get; set; }

        /// <summary>
        /// Идентификатор на документ.
        /// </summary>
        public string ThreadID { get; set; }
    }

    /// <summary>
    /// Допълнителни данни за подписващ.
    /// </summary>
    public class SignerAdditionalData
    {
        /// <summary>
        /// Сертификат на потребителя.
        /// </summary>
        public string UserCert { get; set; }

        /// <summary>
        /// Време на Digest.
        /// </summary>
        public long? DigestTime { get; set; }

        /// <summary>
        /// RelyingParty идентификатор за обратна връзка.
        /// </summary>
        public string RelyingPartyCallbackId { get; set; }

        /// <summary>
        /// Rsa ключ за декриптиране.
        /// </summary>
        public string RsaKeyForDecryption { get; set; }
    }
}
