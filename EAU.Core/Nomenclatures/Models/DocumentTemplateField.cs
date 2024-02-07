using EAU.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Nomenclatures.Models
{
    /// <summary>
    /// Номенклатура на полетата в шаблон за документ
    /// </summary>
    public class DocumentTemplateField
    {
        /// <summary>
        /// Уникален идентификатор на запис за поле във  шаблон за документ
        /// </summary>
        [DapperColumn("key")]
        public string Key { get; set; }
        /// <summary>
        /// Описание на поле
        /// </summary>
        [DapperColumn("description")]
        public string Description { get; set; }
    }
}
