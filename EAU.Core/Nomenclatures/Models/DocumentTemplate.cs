using EAU.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Nomenclatures.Models
{
    /// <summary>
    /// Номенклатура на шаблон за документ
    /// </summary>
    public class DocumentTemplate
    {
        /// <summary>
        /// Идентификатор на запис за шаблон на документ
        /// </summary>
        [DapperColumn("doc_template_id")]
        public int? DocTemplateID { get; set; }

        /// <summary>
        /// Идентификатор на вид документ
        /// </summary>
        [DapperColumn("document_type_id")]
        public int? DocumentTypeID { get; set; }

        /// <summary>
        /// Съдържание на шаблон
        /// </summary>
        [DapperColumn("content")]
        public string Content { get; set; }

        /// <summary>
        /// Подава се само по шаблон
        /// </summary>
        [DapperColumn("is_submitted_according_to_template")]
        public bool? IsSubmittedAccordingToTemplate { get; set; }


        /// <summary>
        /// Дата на последна редакция на шаблон за документ.
        /// </summary>
        [DapperColumn("updated_on")]
        public DateTime? UpdatedOn { get; set; }
    }
}
