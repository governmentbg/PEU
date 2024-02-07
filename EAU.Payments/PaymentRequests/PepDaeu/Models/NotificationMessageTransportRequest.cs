namespace EAU.Payments.PaymentRequests.PepDaeu.Models
{
    /// <summary>
    /// Заявка за обратна връзка от PepDaeu
    /// </summary>
    public class NotificationMessageTransportRequest
    {
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
