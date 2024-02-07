using EAU.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Nomenclatures.Models
{
    /// <summary>
    /// Номенклатура видовете документи от които заявителя на услугата ще може да избира когато прилага документи
    /// </summary>
    public class ServiceDocumentType
    {
        /// <summary>
        /// Индектификатор на услуга
        /// </summary>
        [DapperColumn("service_id")]
        public int? ServiceID { get; set; }

        /// <summary>
        /// Идентификатор на вид документ
        /// </summary>
        [DapperColumn("doc_type_id")]
        public int? DocTypeID { get; set; }
    }
}
