namespace EAU.Payments.PaymentRequests.PepDaeu.Models
{
    /// <summary>
    /// Заявка за обработване на плащане през PepDaeu
    /// </summary>
    public class VPOSResultMessageTransportRequest
    {
        /// <summary>
        /// Кодирано съдържание.
        /// </summary>
        public string ClientID { get; set; }

        /// <summary>
        /// Кодирано съдържание.
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// Контролна сума.
        /// </summary>
        public string Hmac { get; set; }
    }
}
