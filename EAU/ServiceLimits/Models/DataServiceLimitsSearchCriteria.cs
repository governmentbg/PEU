using EAU.Common.Models;

namespace EAU.ServiceLimits.Models
{
    /// <summary>
    /// Критерии за търсене на лимит.
    /// </summary>
    public class DataServiceLimitsSearchCriteria : BasePagedSearchCriteria
    {
        /// <summary>
        /// Идентификатори на лимит.
        /// </summary>
        public int[] ServiceLimitIDs { get; set; }

        /// <summary>
        /// Код на услуга за предоставяне на данни.
        /// </summary>
        public string ServiceCode { get; set; }

        /// <summary>
        /// Наименование на услуга за предоставяне на данни.
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// Статус на лимит.
        /// </summary>
        public DataServiceLimitStatus? Status { get; set; }
    }
}
