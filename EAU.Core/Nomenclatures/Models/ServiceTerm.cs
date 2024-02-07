using EAU.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Nomenclatures.Models
{
    /// <summary>
    /// Номенклатура начини на предаване на административните услуги в административните структури.
    /// </summary>
    public class ServiceTerm
    {
        /// <summary>
        /// Идентификатор на начини на предаване на административните услуги.
        /// </summary>
        [DapperColumn("service_term_id")]
        public int? ServiceTermID { get; set; }

        /// <summary>
        /// Идентификатор на административна услуга.
        /// </summary>
        [DapperColumn("service_id")]
        public int? ServiceID { get; set; }

        /// <summary>
        /// Тип на предаване
        /// </summary>
        [DapperColumn("service_term_type")]
        public AdmServiceTermType? ServiceTermType { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        [DapperColumn("price")]
        public decimal? Price { get; set; }

        /// <summary>
        /// Срок за изпълнение  в дни
        /// </summary>
        [DapperColumn("execution_period")]
        public int? ExecutionPeriod { get; set; }

        /// <summary>
        /// html съдържание с кратко описание на вида услуга не зависеща от срока
        /// </summary>
        [DapperColumn("description")]
        public string Description { get; set; }

        /// <summary>
        /// период на изпълнение
        /// </summary>
        [DapperColumn("period_type")]
        public ExecutionPeriodType? PeriodType { get; set; }

        /// <summary>
        /// Активен
        /// </summary>
        [DapperColumn("is_active")]
        public bool? IsActive { get; set; }
    }
}
