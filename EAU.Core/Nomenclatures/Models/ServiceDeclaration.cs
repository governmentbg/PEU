using EAU.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Nomenclatures.Models
{
    /// <summary>
    /// Номенклатура на декларативно обстоятелство/ политика за услуга
    /// </summary>
    public class ServiceDeclaration
    {
        /// <summary>
        /// Индектификатор на услуга
        /// </summary>
        [DapperColumn("service_id")]
        public int? ServiceID { get; set; }

        /// <summary>
        /// Идентификатор на декларативно обстоятелство/ политика
        /// </summary>
        [DapperColumn("delcaration_id")]
        public int? DeclarationID { get; set; }
    }
}
