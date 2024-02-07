using EAU.Common.Models;
using System;
using System.Collections.Generic;

namespace EAU.Services.ServiceInstances.Models
{
    /// <summary>
    /// Критерии за търсене за работа с инстанци на услуги.
    /// </summary>
    public class ServiceInstanceSearchCriteria : BasePagedSearchCriteria
    {
        /// <summary>
        /// Идентификатори на записи в инстанции на услуги.
        /// </summary>
        public List<long> ServiceInstanceIDs { get; set; }

        /// <summary>
        /// Статус.
        /// </summary>
        public ServiceInstanceStatuses? Status { get; set; }

        /// <summary>
        /// Идентификатор на заявител.
        /// </summary>
        public long? ApplicantID { get; set; }

        /// <summary>
        /// От дата на създаване на иснтанция на услугата.
        /// </summary>
        public DateTime? ServiceInstanceDateFrom { get; set; }

        /// <summary>
        /// До дата на създаване на иснтанция на услугата.
        /// </summary>
        public DateTime? ServiceInstanceDateTo { get; set; }

        /// <summary>
        /// Идентификатор на услуга.
        /// </summary>
        public int? ServiceID { get; set; }

        /// <summary>
        /// УРИ на преписка.
        /// </summary>
        public string CaseFileURI { get; set; }

        public ServiceInstanceLoadOption LoadOption { get; set; }
    }

    public class ServiceInstanceLoadOption
    {
        public bool? LoadWithLock { get; set; }
    }
}