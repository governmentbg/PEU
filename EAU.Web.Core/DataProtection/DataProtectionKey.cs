using EAU.Utilities;
using System;

namespace EAU.Web.Protection
{
    /// <summary>
    /// Модел на ключ за защита на данните.
    /// </summary>
    public class DataProtectionKey
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        [DapperColumn("id")]
        public string ID { get; set; }

        /// <summary>
        /// Xml на ключа.
        /// </summary>
        [DapperColumn("keyxml")]
        public string KeyXml { get; set; }

        /// <summary>
        /// Дата на създаване.
        /// </summary>
        [DapperColumn("creation_date")]
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Дата на активация.
        /// </summary>
        [DapperColumn("activation_date")]
        public DateTime ActivationDate { get; set; }

        /// <summary>
        /// Дата, до която е валиден.
        /// </summary>
        [DapperColumn("expiration_date")]
        public DateTime ExpirationDate { get; set; }
    }
}
