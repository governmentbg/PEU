namespace EAU.Payments.PaymentRequests.PepDaeu.Models
{
    /// <summary>
    /// Обект за обратна връзка към ПЕП на ДАЕУ.
    /// </summary>
    public class NotificationMessageResponse
    {
        /// <summary>
        /// Флаг указващ дали сме успешно информирани.
        /// </summary>
        public bool? Success { get; set; }
    }
}
