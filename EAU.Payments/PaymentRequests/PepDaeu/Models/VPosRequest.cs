namespace EAU.Payments.PaymentRequests.PepDaeu.Models
{
    /// <summary>
    /// Заявка за плащане с виртуален ПОС
    /// </summary>
    public class VPosRequest
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// okUrl.
        /// </summary>
        public string OkUrl { get; set; }

        /// <summary>
        /// cancelUrl.
        /// </summary>
        public string CancelUrl { get; set; }
    }
}