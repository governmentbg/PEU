using EAU.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Nomenclatures.Models
{
    /// <summary>
    /// Номенклатура на превод на услуга от чужд език.
    /// </summary>
    public class ServiceTranslation
    {
        /// <summary>
        /// Идентификатор на запис за услуга.
        /// </summary>
        [DapperColumn("service_id")]
        public int ServiceID { get; set; }

        /// <summary>
        /// Идентификатор на запис за език.
        /// </summary>
        [DapperColumn("language_id")]
        public int LanguageID { get; set; }

        /// <summary>
        /// Описание на услуга.
        /// </summary>
        [DapperColumn("description")]
        public string Description { get; set; }

        /// <summary>
        /// Превод на наименование на услуга.
        /// </summary>
        [DapperColumn("name")]
        public string Name { get; set; }

        /// <summary>
        /// Описание на прилаганите документи
        /// </summary>
        [DapperColumn("attached_documents_description")]
        public string AttachedDocumentsDescription { get; set; }
    }
}
