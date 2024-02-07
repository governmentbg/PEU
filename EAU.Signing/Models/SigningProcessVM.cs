using System;
using System.Collections.Generic;

namespace EAU.Signing.Models
{
    /// <summary>
    /// Данни за процес по подписване, необходими за UI-a.
    /// </summary>
    public class SigningProcessVM
    {
        /// <summary>
        /// Идентификатор на процес за подписване.
        /// </summary>
        public Guid? ProcessID { get; set; }

        /// <summary>
        /// Статус на процес за подписване.
        /// </summary>
        public SigningRequestStatuses? Status { get; set; }

        /// <summary>
        /// Списък с подписващи към процеса.
        /// </summary>
        public List<SignerVM> Signers { get; set; }
    }

    /// <summary>
    /// Данни за подписващ, необходими за UI-a.
    /// </summary>
    public class SignerVM
    {
        /// <summary>
        /// Идентификатор на подписващ.
        /// </summary>
        public long? SignerID { get; set; }

        /// <summary>
        /// Име на подписващ.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ЕГН/ЛНЧ на подписващ.
        /// </summary>
        public string Ident { get; set; }

        /// <summary>
        /// Ред за полагане на подпис.
        /// </summary>
        public short? Order { get; set; }

        /// <summary>
        /// Статус на полагане на подпис.
        /// </summary>
        public SignerSigningStatuses? Status { get; set; }

        /// <summary>
        /// Канал за подписване.
        /// </summary>
        public SigningChannels? SigningChannel { get; set; }

        /// <summary>
        /// Причина за отказ.
        /// </summary>
        public string RejectReson { get; set; }
    }
}
