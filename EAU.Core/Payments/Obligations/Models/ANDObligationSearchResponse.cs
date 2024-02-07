using System.Collections.Generic;

namespace EAU.Payments.Obligations.Models
{
    /// <summary>
    /// Резултат от търсене на задължения към АНД.
    /// </summary>
    public class ANDObligationSearchResponse
    {
        /// <summary>
        /// Задъжления
        /// </summary>
        public List<ObligationSearchResult> ObligationsData { get; set; }

        /// <summary>
        /// Флаг указващ дали има невръчени актове.
        /// </summary>
        public bool? HasNonHandedSlip { get; set; }

        /// <summary>
        /// Флаг, указващ дали има грешка при извикване на услугата за невръчени актове.
        /// </summary>
        public bool? ErrorOnHasNonHandedSlip { get; set; }        
    }
}
