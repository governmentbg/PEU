namespace EAU.Payments.PaymentRequests.Epay.Models
{
    /// <summary>
    /// Заявка за обратна връзка от Epay
    /// </summary>
    public class PaymentsEpayCallbackRequest
    {
        /// <summary>
        /// Кодирано съдържание.
        /// </summary>
        public string Encoded { get; set; }

        /// <summary>
        /// Контролна сума.
        /// </summary>
        public string Checksum { get; set; }
    }
}
