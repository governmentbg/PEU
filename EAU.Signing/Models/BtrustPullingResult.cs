namespace EAU.Signing.Models
{
    /// <summary>
    /// Пулване на BTrust за да разберем дали документа е подписан или отказа от потребител през мобилното приложение на BTrust.
    /// </summary>
    public class BtrustPullingResult
    {
        /// <summary>
        /// Код.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Статус.
        /// </summary>
        public BtrustDocStatus? Status { get; set; }

        /// <summary>
        /// Причина за отхвърляне.
        /// </summary>
        public string RejectReson { get; set; }
    }

    /// <summary>
    /// Статус на документи за BTrust: 0 = Подписан; 1 = Отказан;
    /// </summary>
    public enum BtrustDocStatus
    {
        /// <summary>
        /// Подписан.
        /// </summary>
        SIGNED = 0,

        /// <summary>
        /// Отказан.
        /// </summary>
        REJECTED = 1,
    }
}
