namespace EAU.Signing.Models
{
    /// <summary>
    /// Клас капсулиращ данни за подписващ участващ във процеса по подисване.
    /// </summary>
    public class SignerRequest
    {
        /// <summary>
        /// Име на подписващ.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Идентификатор на подписващ.
        /// </summary>
        public string Ident { get; set; }

        /// <summary>
        /// Ред на полагане на подписа.
        /// </summary>
        public short? Order { get; set; }
    }
}
