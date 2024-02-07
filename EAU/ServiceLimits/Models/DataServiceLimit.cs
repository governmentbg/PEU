using EAU.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.ServiceLimits.Models
{
    /// <summary>
    /// Статуси на лимит: 0 = Неактивен; 1 = Активен;
    /// </summary>
    public enum DataServiceLimitStatus
    {
        /// <summary>
        /// Неактивен.
        /// </summary>
        Inactive = 0,

        /// <summary>
        /// Активен.
        /// </summary>
        Active = 1
    }

    /// <summary>
    /// Лимит
    /// </summary>
    public class DataServiceLimit
    {
        /// <summary>
        /// Идентификатор на лимит.
        /// </summary>
        [DapperColumn("service_limit_id")]
        public int? ServiceLimitID { get; set; }

        /// <summary>
        /// Код на услуга за предоставяне на данни.
        /// </summary>
        [DapperColumn("service_code")]
        public string ServiceCode { get; set; }

        /// <summary>
        /// Наименование на услуга за предоставяне на данни.
        /// </summary>
        [DapperColumn("service_name")]
        public string ServiceName { get; set; }

        /// <summary>
        /// Период от време като дата.
        /// </summary>
        [DapperColumn("requests_interval")]
        public DateTime RequestsIntervalFromStartDate { get; set; }

        /// <summary>
        /// Период от време.
        /// </summary>
        public TimeSpan RequestsInterval
        {
            get { return RequestsIntervalFromStartDate - new DateTime(1900, 01, 01, 00, 00, 00); }
            set { RequestsIntervalFromStartDate = new DateTime(1900, 01, 01, 00, 00, 00) + value; }
        }


        /// <summary>
        /// Максимален брой заявки за периода от време.
        /// </summary>
        [DapperColumn("requests_number")]
        public int RequestsNumber { get; set; }

        /// <summary>
        /// Статус на лимит.
        /// </summary>
        [DapperColumn("status")]
        public DataServiceLimitStatus Status { get; set; }
    }
}
