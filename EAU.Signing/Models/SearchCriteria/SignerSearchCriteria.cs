using System;
using System.Collections.Generic;

namespace EAU.Signing.Models.SearchCriteria
{
    /// <summary>
    /// Критерии за търсене на подписващ/и.
    /// </summary>
    public class SignerSearchCriteria
    {
        /// <summary>
        /// Уникален идентификатор на подписващия.
        /// </summary>
        public long? SignerID { get; set; }

        /// <summary>
        /// Уникални идентификатори на процеси.
        /// </summary>
        public List<Guid> ProcessIDs { get; set; }

        /// <summary>
        /// Канал за подписване.
        /// </summary>
        public SigningChannels? SigningChannel { get; set; }

        /// <summary>
        /// Идентификатор на трансакция.
        /// </summary>
        public string TransactionID { get; set; }
    }
}
