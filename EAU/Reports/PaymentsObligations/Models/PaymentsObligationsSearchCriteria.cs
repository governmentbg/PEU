using EAU.Common.Models;
using EAU.Payments.Obligations.Models;
using System;

namespace EAU.Reports.PaymentsObligations.Models
{
    public class PaymentsObligationsSearchCriteria : BasePagedSearchCriteria
    {
        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public string DebtorIdentifier { get; set; }

        public ObligedPersonIdentTypes? DebtorIdentifierType { get; set; }
    }
}