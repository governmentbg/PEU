using EAU.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Nomenclatures.Models
{
    /// <summary>
    /// Номенклатура на превод на ресурс от чужд език.
    /// </summary>
    public class LabelTranslation
    {
        /// <summary>
        /// Идентификатор на запис за етикет.
        /// </summary>
        [DapperColumn("label_id")]
        public int LabelID { get; set; }

        /// <summary>
        /// Идентификатор на запис за език.
        /// </summary>
        [DapperColumn("language_id")]
        public int LanguageID { get; set; }

        /// <summary>
        /// Описание на етикет.
        /// </summary>
        [DapperColumn("description")]
        public string Description { get; set; }

        /// <summary>
        /// Текст на превод от етикет.
        /// </summary>
        [DapperColumn("value")]
        public string Value { get; set; }
    }
}
