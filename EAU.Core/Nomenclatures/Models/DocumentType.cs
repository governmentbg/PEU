using EAU.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Nomenclatures.Models
{
    /// <summary>
    /// Номенклатура на видовете документи, които поддържа портала.
    /// </summary>
    public class DocumentType
    {
        /// <summary>
        /// Идентификатор на вид документ.
        /// </summary>
        [DapperColumn("doc_type_id")]
        public int? DocumentTypeID { get; set; }

        /// <summary>
        /// Наименование на вид документ.
        /// </summary>
        [DapperColumn("name")]
        public string Name { get; set; }

        /// <summary>
        /// Универсален идентификатор на ресурс
        /// </summary>
        [DapperColumn("uri")]
        public string Uri { get; set; }

        /// <summary>
        /// Тип на документ
        /// </summary>
        [DapperColumn("type_id")]
        public DocumentTypes? Type { get; set; }
    }
}
