using EAU.Utilities;
using System;
using System.Collections.Generic;
using System.Text;


namespace EAU.Nomenclatures.Models
{
    /// <summary>
    /// Номенклатура на държавите.
    /// </summary>
    public class Country
    {
        /// <summary>
        /// Идентификатор на запис на държава.
        /// </summary>
        [DapperColumn("country_id")]
        public int? CountryID { get; set; }

        /// <summary>
        /// Наименование на държава.
        /// </summary>
        [DapperColumn("name")]
        public string Name { get; set; }

        /// <summary>
        /// Код на държава на латиница.
        /// </summary>
        [DapperColumn("cod_l")]
        public string Code { get; set; }
    }
}
