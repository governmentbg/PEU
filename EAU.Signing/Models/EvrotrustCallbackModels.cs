using System.Text.Json.Serialization;

namespace EAU.Signing.Models
{
    /// <summary>
    /// Данни за готов документ.
    /// </summary>
    public partial class DataDocumentReady
    {
        /// <summary>
        /// Идентификатор на трансакция.
        /// </summary>
        [JsonPropertyName("transactionID")]
        public string TransactionID { get; set; }

        /// <summary>
        /// Статус.
        /// </summary>
        [JsonPropertyName("status")]
        public int? Status { get; set; }

        /// <summary>
        /// Причина за отхвърляне.
        /// </summary>
        [JsonPropertyName("rejectReason")]
        public string RejectReason { get; set; }
    }

    /// <summary>
    /// Данни за готова документна група.
    /// </summary>
    public partial class DataDocumentGroupReady
    {
        /// <summary>
        /// Идентификатор на трансакция.
        /// </summary>
        [JsonPropertyName("transactionID")]
        public string TransactionID { get; set; }

        /// <summary>
        /// Статус.
        /// </summary>
        [JsonPropertyName("status")]
        public int? Status { get; set; }

        /// <summary>
        /// Причина за отхвърляне.
        /// </summary>
        [JsonPropertyName("rejectReason")]
        public string RejectReason { get; set; }
    }
}
