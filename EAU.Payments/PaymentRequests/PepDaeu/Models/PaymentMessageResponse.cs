using System;

namespace EAU.Payments.PaymentRequests.PepDaeu.Models
{
    /// <summary>
    /// Заявка за отговор при плащане през ПЕП на ДАЕУ
    /// </summary>
    public class PaymentMessageResponse
    {
        /// <summary>
        /// Обект за приета заявкат.
        /// </summary>
        public AcceptedReceiptJson AcceptedReceiptJson { get; set; }

        /// <summary>
        /// Обект за неприета заявкат.
        /// </summary>
        public UnacceptedReceiptJson UnacceptedReceiptJson { get; set; }
    }

    /// <summary>
    /// Заявка за неприет отговор при плащане през ПЕП на ДАЕУ
    /// </summary>
    public class UnacceptedReceiptJson
    {
        /// <summary>
        /// Време на валидация.
        /// </summary>
        public DateTime? ValidationTime { get; set; }

        /// <summary>
        /// Грешки.
        /// </summary>
        public string[] Errors { get; set; }
    }

    /// <summary>
    /// Данни на заявка за приет отговор при плащане през ПЕП на ДАЕУ
    /// </summary>
    public class AcceptedReceiptJson
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Време на регистрация.
        /// </summary>
        public DateTime? RegistrationTime { get; set; }
    }
}
