using EAU.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Payments.Obligations.Models
{
    /// <summary>
    /// Критерии за търсене на задължения за заявления.
    /// </summary>
    public class SIObligationSearchCriteria
    {
        /// <summary>
        /// Идентификатор на инстанция на услуга.
        /// </summary>
        public long? ServiceInstanceID { get; set; }

        /// <summary>
        /// УРИ на указания за плащане във wais.
        /// </summary>
        public string PaymentInstructionURI { get; set; }
    }
}