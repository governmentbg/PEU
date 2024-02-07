using EAU.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Nomenclatures.Models
{
    /// <summary>
    /// Номенклатура на начините на предаване.
    /// </summary>
    public class DeliveryChannel
    {
        /// <summary>
        /// Идентификатор на услуга
        /// </summary>
        [DapperColumn("delivery_channel_id")]
        public short? DeliveryChannelID { get; set; }

        /// <summary>
        /// Идентификатор на начин на предване
        /// </summary>
        [DapperColumn("name")]
        public string Name { get; set; }
    }
}
