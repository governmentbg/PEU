using EAU.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Nomenclatures.Models
{
    /// <summary>
    /// Номенклатура на групи услуги по направление на дейност в МВР
    /// </summary>
    public class ServiceGroup
    {
        /// <summary>
        /// Идентификатор на групата
        /// </summary>
        [DapperColumn("group_id")]
        public int? GroupID { get; set; }

        /// <summary>
        /// Флаг указващ дали дадения запис е преведен за избрания език.
        /// </summary>
        [DapperColumn("is_translated")]
        public bool? IsTranslated { get; set; }

        /// <summary>
        /// Наименование на групата
        /// </summary>
        [DapperColumn("name")]
        public string Name { get; set; }

        /// <summary>
        /// Номер на показване
        /// </summary>
        [DapperColumn("order_number")]
        public int? OrderNumber { get; set; }

        [DapperColumn("icon_name")]
        public string IconName { get; set; }

        /// <summary>
        /// Флаг указващ дали групата е активна
        /// </summary>
        [DapperColumn("is_active")]
        public bool? IsActive { get; set; }

        /// <summary>
        /// Код на език.
        /// </summary>
        [DapperColumn("language_code")]
        public string LanguageCode { get; set; }

        /// <summary>
        /// Дата на създаване/последна промяна.
        /// </summary>
        [DapperColumn("updated_on")]
        public DateTime? UpdatedOn { get; set; }
    }
}
