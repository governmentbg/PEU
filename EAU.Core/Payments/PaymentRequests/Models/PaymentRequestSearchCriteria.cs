using EAU.Common.Models;
using EAU.Payments.RegistrationsData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Payments.PaymentRequests.Models
{
    /// <summary>
    /// Критерии за търсене на заявки за плащания.
    /// </summary>
    public class PaymentRequestSearchCriteria : BasePagedSearchCriteria
    {
        /// <summary>
        /// Идентифкатори на заявки за плащания.
        /// </summary>
        public List<long> PaymentRequestIDs { get; set; }

        /// <summary>
        /// Идентифкатори на задължение.
        /// </summary>
        public List<long> ObligationIDs { get; set; }

        /// <summary>
        /// Канал за плащане.
        /// </summary>
        public RegistrationDataTypes? PaymentChannel { get; set; }

        /// <summary>
        /// Идентификатор на регистрационните данни.
        /// </summary>
        public int? RegistrationDataID { get; set; }

        /// <summary>
        /// Номер във външна система.
        /// </summary>
        public string ExternalPortalNumber { get; set; }

        /// <summary>
        /// Флак за заключване на записите.
        /// </summary>
        public bool? WithLock { get; set; }
    }
}
