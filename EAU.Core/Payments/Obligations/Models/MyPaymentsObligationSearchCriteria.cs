using EAU.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Payments.Obligations.Models
{
    /// <summary>
    /// Критерии за търсене на задължения за моите плащания.
    /// </summary>
    public class MyPaymentsObligationSearchCriteria : BasePagedSearchCriteria
    {
        /// <summary>
        /// Идентификатор на инстанция на услуга.
        /// </summary>
        public long? ServiceInstanceID { get; set; }
    }
}