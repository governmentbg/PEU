using EAU.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Nomenclatures.Models
{
    /// <summary>
    /// Номенклатура на превод на група от чужд език.
    /// </summary>
    public class ServiceGroupTranslation
    {
        /// <summary>
        /// Идентификатор на запис за група.
        /// </summary>
        [DapperColumn("group_id")]
        public int GroupID { get; set; }

        /// <summary>
        /// Идентификатор на запис за език.
        /// </summary>
        [DapperColumn("language_id")]
        public int LanguageID { get; set; }

        /// <summary>
        /// Превод на наименование на група.
        /// </summary>
        [DapperColumn("name")]
        public string Name { get; set; }
    }
}
