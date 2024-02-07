using System;

namespace EAU.Payments.PaymentRequests.PepDaeu.Models
{
    /// <summary>
    /// Обект за обратна връзка от ПЕП на ДАЕУ.
    /// </summary>
    public class NotificationMessageRequest
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Статус.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Време на промяна.
        /// </summary>
        public DateTime? ChangeTime { get; set; }
    }
}