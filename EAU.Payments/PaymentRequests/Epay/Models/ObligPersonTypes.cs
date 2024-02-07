using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Payments.PaymentRequests.Epay.Models
{
    /// <summary>
    /// Видове идентификатори на задължено лице: 0 = ЕГН; 1 = ЛНЧ; 2 = БУЛСТАТ.
    /// </summary>
    public enum ObligPersonTypes
    {
        /// <summary>
        /// ЕГН.
        /// </summary>
        EGN,

        /// <summary>
        /// ЛНЧ.
        /// </summary>
        LNC,

        /// <summary>
        /// БУЛСТАТ.
        /// </summary>
        BULSTAT
    }
}
