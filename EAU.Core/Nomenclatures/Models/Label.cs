using EAU.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Nomenclatures.Models
{
    /// <summary>
    /// Номенклатура съхраняваща преводите за ресурсите на Български език
    /// </summary>
    public class Label
    {
        /// <summary>
        /// Идентификатор на етикет.
        /// </summary>
        [DapperColumn("label_id")]
        public int? LabelID { get; set; }

        /// <summary>
        /// Флаг указващ дали дадения запис е преведен за избрания език.
        /// </summary>
        [DapperColumn("is_translated")]
        public bool? IsTranslated { get; set; }

        /// <summary>
        /// Код на етикет.
        /// </summary>
        [DapperColumn("code")]
        public string Code { get; set; }

        /// <summary>
        /// Описание на етикет.
        /// </summary>
        [DapperColumn("description")]
        public string Description { get; set; }

        /// <summary>
        /// Стойност на етикет.
        /// </summary>
        [DapperColumn("value")]
        public string Value { get; set; }

        /// <summary>
        /// Код на език.
        /// </summary>
        [DapperColumn("language_code")]
        public string LanguageCode { get; set; }
    }
}
