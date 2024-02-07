using EAU.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Nomenclatures.Models
{
    /// <summary>
    /// Номенклатура на декларативни обстоятелства и политики
    /// </summary>
    public class Declaration
    {
        /// <summary>
        /// Идентификатор на декларативно обстоятелство/ политика
        /// </summary>
        [DapperColumn("delcaration_id")]
        public int? DeclarationID { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        [DapperColumn("description")]
        public string Description { get; set; }

        /// <summary>
        /// Съдържание
        /// </summary>
        [DapperColumn("content")]
        public string Content { get; set; }

        /// <summary>
        /// Задължително за маркиране в заявленията
        /// </summary>
        [DapperColumn("is_required")]
        public bool? IsRquired { get; set; }

        /// <summary>
        /// Код на декларативно обстоятелство/ политика
        /// </summary>
        [DapperColumn("code")]
        public string Code { get; set; }

        /// <summary>
        /// Изисква допълнително поле за въвеждане на описание
        /// </summary>
        [DapperColumn("is_additional_description_required")]
        public bool? IsAdditionalDescriptionRequired { get; set; }

        /// <summary>
        /// Дата на създаване/последна промяна.
        /// </summary>
        [DapperColumn("updated_on")]
        public DateTime? UpdatedOn { get; set; }
    }
}
