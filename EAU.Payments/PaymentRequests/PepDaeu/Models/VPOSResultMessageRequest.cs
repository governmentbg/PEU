using System;

namespace EAU.Payments.PaymentRequests.PepDaeu.Models
{
    /// <summary>
    /// Обект за обработване на плащане през PepDaeu
    /// </summary>
    public class VPOSResultMessageRequest
    {
        /// <summary>
        /// Идентификатор на заявката за плащане, за която е направен опит да бъде платена.
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// Системен за средата идентификатор на резултата.
        /// </summary>
        public string VposResultGid { get; set; }

        /// <summary>
        /// Статусите на резултата от плащането са: "SUCCESS" (Успешно плащане), "FAILURE" (Неуспешно плащане), "CANCELEDBYUSER" (Отказано от потребителя).
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Описание на възникналата грешка (има стойност само когато полето "status" има стойност "failure").
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Дата и час на получаване на резултата от виртуалния ПОС (ISO 8601 формат).
        /// </summary>
        public DateTime? ResultTime { get; set; }
    }
}