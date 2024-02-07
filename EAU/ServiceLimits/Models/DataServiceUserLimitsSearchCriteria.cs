using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.ServiceLimits.Models
{
    /// <summary>
    /// Критерии за търсене на лимит на потребител.
    /// </summary>
    public class DataServiceUserLimitsSearchCriteria
    {
        /// <summary>
        /// Идентификатори на лимит на потребител.
        /// </summary>
        public int[] UserLimitIDs { get; set; }

        /// <summary>
        /// Идентификатор на лимит.
        /// </summary>
        public int? ServiceLimitID { get; set; }

        /// <summary>
        /// Идентификатор на потребителски профил
        /// </summary>
        public int? UserID { get; set; }

        /// <summary>
        /// Статус на лимит.
        /// </summary>
        public DataServiceLimitStatus? Status { get; set; }
    }
}
