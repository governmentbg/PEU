using EAU.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Nomenclatures.Models
{
    /// <summary>
    /// Номенклатура начини на предаване на административните услуги в административните структури.
    /// </summary>
    public class ServiceDeliveryChannel
    {
        /// <summary>
        /// Индектификатор на услуга
        /// </summary>
        [DapperColumn("service_id")]
        public int? ServiceID { get; set; }

        /// <summary>
        /// Идентификатор на декларативно обстоятелство/ политика
        /// </summary>
        [DapperColumn("delivery_channel_id")]
        public short? DeliveryChannelID { get; set; }
    }
}
