using System;

namespace EAU.Signing.Models.SearchCriteria
{
    /// <summary>
    /// Критерии за търсене на процес/и по подписване.
    /// </summary>
    public class SigningProcessesSearchCriteria
    {
        /// <summary>
        /// Уникален идентификатор на процес.
        /// </summary>
        public Guid? ProcessID { get; set; }

        /// <summary>
        /// Статус.
        /// </summary>
        public SigningRequestStatuses? Status { get; set; }

        /// <summary>
        /// Флаг, указващ дали да се зареди съдържанието.
        /// </summary>
        public bool LoadContent { get; set; }

        /// <summary>
        /// Флаг, указващ дали да се заредят данни за подписващите.
        /// </summary>
        public bool LoadSigners { get; set; }

        /// <summary>
        /// Флаг, указващ дали да прочете и заключи редовете на намерените процеси по подписване.
        /// </summary>
        public bool WithLock { get; set; }
    }
}
