using System;
using System.Collections.Generic;

namespace EAU.Payments.Obligations.Models
{
    /// <summary>
    /// Опции за зареждане на обекти.
    /// </summary>
    public class ObligationLoadOption
    {
        /// <summary>
        /// Флаг указващ дали да се заредят и заявките за плащане.
        /// </summary>
        public bool LoadPaymentRequests { get; set; }
    }

    /// <summary>
    /// Критерии за търсене по идентифициращи данни на задължение.
    /// </summary>
    public class ObligationIdentifiersSearchCriteria
    {
        /// <summary>
        /// Идентифкатор за задължение.
        /// </summary>
        public string ObligationIdentifier { get; set; }

        /// <summary>
        /// Дата на задължението.
        /// </summary>
        public DateTime? ObligationDate { get; set; }
    }

    /// <summary>
    /// Критерии за търсене на задължения.
    /// </summary>
    public class ObligationRepositorySearchCriteria
    {
        /// <summary>
        /// Критерии за търсене по идентифициращи данни на задължение.
        /// </summary>
        public List<ObligationIdentifiersSearchCriteria> ObligationIdentifiersSearchCriteria { get; set; }

        /// <summary>
        /// Видове задължения.
        /// </summary>
        public ObligationTypes? Type { get; set; }

        /// <summary>
        /// Флаг указващ дали задължението е активно (дата не е след ExpirationDate и статусът е InProcess)
        /// </summary>
        public bool? IsActive { get; set; }

        /// <summary>
        /// Идентифкатори за задължение.
        /// </summary>
        public List<long> ObligationIDs { get; set; }

        /// <summary>
        /// Статуси
        /// </summary>
        public List<ObligationStatuses> Statuses { get; set; }

        /// <summary>
        /// Идентификатор на заявител.
        /// </summary>
        public int? ApplicantID { get; set; }

        /// <summary>
        /// Флаг указващ дали да се търси задължения без заявител.
        /// </summary>
        public bool? IsApplicantAnonimous { get; set; }

        /// <summary>
        /// Идентификатор на инстанция на услуга.
        /// </summary>
        public long? ServiceInsanceID { get; set; }

        /// <summary>
        /// Флаг указващ дали да бъде направено заключване.
        /// </summary>
        public bool? WithLock { get; set; }

        /// <summary>
        /// Опции за зареждане.
        /// </summary>
        public ObligationLoadOption LoadOption { get; set; }
    }
}