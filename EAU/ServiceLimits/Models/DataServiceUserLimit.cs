using EAU.Users.Models;
using EAU.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.ServiceLimits.Models
{
    /// <summary>
    /// Лимит на потребител.
    /// </summary>
    public class DataServiceUserLimit
    {
        /// <summary>
        /// Идентификатор на лимит на потребител.
        /// </summary>
        [DapperColumn("user_limit_id")]
        public int? UserLimitID { get; set; }

        /// <summary>
        /// Идентификатор на лимит.
        /// </summary>
        [DapperColumn("service_limit_id")]
        public int? ServiceLimitID { get; set; }

        /// <summary>
        /// Лимит.
        /// </summary>
        public DataServiceLimit ServiceLimit { get; set; }

        /// <summary>
        /// Идентификатор на потребителски профил.
        /// </summary>
        [DapperColumn("user_id")]
        public int? UserID { get; set; }

        /// <summary>
        /// Потребител.
        /// </summary>
        public User User { get; set; }

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
